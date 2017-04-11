using System;
using Cben.Configuration.Startup;
using Cben.Dependency;
using Cben.Dependency.Installers;
using Cben.Modules;
using Cben.PlugIns;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using JetBrains.Annotations;

namespace Cben
{
    /// <summary>
    /// This is the main class that is responsible to start entire Cben system.
    /// Prepares dependency injection and registers core components needed for startup.
    /// It must be instantiated and initialized (see <see cref="Initialize"/>) first in an application.
    /// </summary>
    public class CbenBootstrapper : IDisposable
    {
        /// <summary>
        /// Get the startup module of the application which depends on other used modules.
        /// </summary>
        public Type StartupModule { get; }

        /// <summary>
        /// A list of plug in folders.
        /// </summary>
        public PlugInSourceList PlugInSources { get; }

        /// <summary>
        /// Gets IIocManager object used by this class.
        /// </summary>
        public IIocManager IocManager { get; }

        /// <summary>
        /// Is this object disposed before?
        /// </summary>
        protected bool IsDisposed;

        private CbenModuleManager _moduleManager;
        private ILogger _logger;

        /// <summary>
        /// Creates a new <see cref="CbenBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="CbenModule"/>.</param>
        private CbenBootstrapper([NotNull] Type startupModule)
            : this(startupModule, Dependency.IocManager.Instance)
        {

        }

        /// <summary>
        /// Creates a new <see cref="CbenBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="CbenModule"/>.</param>
        /// <param name="iocManager">IIocManager that is used to bootstrap the Cben system</param>
        private CbenBootstrapper([NotNull] Type startupModule, [NotNull] IIocManager iocManager)
        {
            Check.NotNull(startupModule, nameof(startupModule));
            Check.NotNull(iocManager, nameof(iocManager));

            if (!typeof(CbenModule).IsAssignableFrom(startupModule))
            {
                throw new ArgumentException($"{nameof(startupModule)} should be derived from {nameof(CbenModule)}.");
            }

            StartupModule = startupModule;
            IocManager = iocManager;

            PlugInSources = new PlugInSourceList();
            _logger = NullLogger.Instance;
        }

        /// <summary>
        /// Creates a new <see cref="CbenBootstrapper"/> instance.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="CbenModule"/>.</typeparam>
        public static CbenBootstrapper Create<TStartupModule>()
            where TStartupModule : CbenModule
        {
            return new CbenBootstrapper(typeof(TStartupModule));
        }

        /// <summary>
        /// Creates a new <see cref="CbenBootstrapper"/> instance.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="CbenModule"/>.</typeparam>
        /// <param name="iocManager">IIocManager that is used to bootstrap the Cben system</param>
        public static CbenBootstrapper Create<TStartupModule>([NotNull] IIocManager iocManager)
            where TStartupModule : CbenModule
        {
            return new CbenBootstrapper(typeof(TStartupModule), iocManager);
        }

        /// <summary>
        /// Creates a new <see cref="CbenBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="CbenModule"/>.</param>
        public static CbenBootstrapper Create([NotNull] Type startupModule)
        {
            return new CbenBootstrapper(startupModule);
        }

        /// <summary>
        /// Creates a new <see cref="CbenBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="CbenModule"/>.</param>
        /// <param name="iocManager">IIocManager that is used to bootstrap the Cben system</param>
        public static CbenBootstrapper Create([NotNull] Type startupModule, [NotNull] IIocManager iocManager)
        {
            return new CbenBootstrapper(startupModule, iocManager);
        }

        /// <summary>
        /// Initializes the Cben system.
        /// </summary>
        public virtual void Initialize()
        {
            ResolveLogger();

            try
            {
                RegisterBootstrapper();
                IocManager.IocContainer.Install(new CbenCoreInstaller());

                IocManager.Resolve<CbenPlugInManager>().PlugInSources.AddRange(PlugInSources);
                IocManager.Resolve<CbenStartupConfiguration>().Initialize();

                _moduleManager = IocManager.Resolve<CbenModuleManager>();
                _moduleManager.Initialize(StartupModule);
                _moduleManager.StartModules();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.ToString(), ex);
                throw;
            }
        }

        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
            {
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(CbenBootstrapper));
            }
        }

        private void RegisterBootstrapper()
        {
            if (!IocManager.IsRegistered<CbenBootstrapper>())
            {
                IocManager.IocContainer.Register(
                    Component.For<CbenBootstrapper>().Instance(this)
                    );
            }
        }

        /// <summary>
        /// Disposes the Cben system.
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            _moduleManager?.ShutdownModules();
        }
    }
}
