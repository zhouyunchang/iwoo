using System;
using System.Collections.Generic;
using System.Linq;
using Cben.Modules;

namespace Cben.PlugIns
{
    public interface IPlugInSource
    {
        List<Type> GetModules();
    }

    public static class PlugInSourceExtensions
    {
        public static List<Type> GetModulesWithAllDependencies(this IPlugInSource plugInSource)
        {
            return plugInSource
                .GetModules()
                .SelectMany(CbenModule.FindDependedModuleTypesRecursivelyIncludingGivenModule)
                .Distinct()
                .ToList();
        }
    }
}