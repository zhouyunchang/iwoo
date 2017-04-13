using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Cben.Collections.Extensions;
using Cben.Configuration.Startup;
using Cben.Dependency;
using Cben.PlugIns;
using Castle.Core.Logging;

namespace Cben.Modules
{
    /// <summary>
    /// This class is used to manage modules.
    /// </summary>
    public class CbenModuleManager : ICbenModuleManager
    {
        public CbenModuleInfo StartupModule { get; private set; }

        private Type _startupModuleType;

        public IReadOnlyList<CbenModuleInfo> Modules => _modules.ToImmutableList();

        public ILogger Logger { get; set; }

        private readonly IIocManager _iocManager;
        private readonly ICbenPlugInManager _CbenPlugInManager;
        private readonly CbenModuleCollection _modules;

        public CbenModuleManager(IIocManager iocManager, ICbenPlugInManager CbenPlugInManager)
        {
            _modules = new CbenModuleCollection();
            _iocManager = iocManager;
            _CbenPlugInManager = CbenPlugInManager;
            Logger = NullLogger.Instance;
        }

        public virtual void Initialize(Type startupModule)
        {
            _startupModuleType = startupModule;
            LoadAllModules();
        }

        public virtual void StartModules()
        {
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.ForEach(module => module.Instance.PreInitialize());
            sortedModules.ForEach(module => module.Instance.Initialize());
            sortedModules.ForEach(module => module.Instance.PostInitialize());
        }

        public virtual void ShutdownModules()
        {
            Logger.Debug("Shutting down has been started");

            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.Reverse();
            sortedModules.ForEach(sm => sm.Instance.Shutdown());

            Logger.Debug("Shutting down completed.");
        }

        private void LoadAllModules()
        {
            Logger.Debug("Loading Cben modules...");

            var moduleTypes = FindAllModules().Distinct().ToList();

            Logger.Debug("Found " + moduleTypes.Count + " Cben modules in total.");

            RegisterModules(moduleTypes);
            CreateModules(moduleTypes);

            CbenModuleCollection.EnsureKernelModuleToBeFirst(_modules);

            SetDependencies();

            Logger.DebugFormat("{0} modules loaded.", _modules.Count);
        }

        private List<Type> FindAllModules()
        {
            var modules = CbenModule.FindDependedModuleTypesRecursivelyIncludingGivenModule(_startupModuleType);

            _CbenPlugInManager
                .PlugInSources
                .GetAllModules()
                .ForEach(m => modules.AddIfNotContains(m));

            return modules;
        }

        private void CreateModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                var moduleObject = _iocManager.Resolve(moduleType) as CbenModule;
                if (moduleObject == null)
                {
                    throw new CbenInitializationException("This type is not an Cben module: " + moduleType.AssemblyQualifiedName);
                }

                moduleObject.IocManager = _iocManager;
                moduleObject.Configuration = _iocManager.Resolve<ICbenStartupConfiguration>();

                var moduleInfo = new CbenModuleInfo(moduleType, moduleObject);

                _modules.Add(moduleInfo);

                if (moduleType == _startupModuleType)
                {
                    StartupModule = moduleInfo;
                }

                Logger.DebugFormat("Loaded module: " + moduleType.AssemblyQualifiedName);
            }
        }

        private void RegisterModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                _iocManager.RegisterIfNot(moduleType);
            }
        }

        private void SetDependencies()
        {
            foreach (var moduleInfo in _modules)
            {
                moduleInfo.Dependencies.Clear();

                //Set dependencies for defined DependsOnAttribute attribute(s).
                foreach (var dependedModuleType in CbenModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                    {
                        throw new CbenInitializationException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                    }

                    if ((moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null))
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }
    }
}
