using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cben.Modules;

namespace Cben.Reflection
{
    public class CbenAssemblyFinder : IAssemblyFinder
    {
        private readonly ICbenModuleManager _moduleManager;

        public CbenAssemblyFinder(ICbenModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public List<Assembly> GetAllAssemblies()
        {
            var assemblies = new List<Assembly>();

            foreach (var module in _moduleManager.Modules)
            {
                assemblies.Add(module.Assembly);
                assemblies.AddRange(module.Instance.GetAdditionalAssemblies());
            }

            return assemblies.Distinct().ToList();
        }
    }
}