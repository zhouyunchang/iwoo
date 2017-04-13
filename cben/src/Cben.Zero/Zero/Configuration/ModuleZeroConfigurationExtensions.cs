﻿using Cben.Configuration.Startup;

namespace Cben.Zero.Configuration
{
    /// <summary>
    /// Extension methods for module zero configurations.
    /// </summary>
    public static class ModuleZeroConfigurationExtensions
    {
        /// <summary>
        /// Used to configure module zero.
        /// </summary>
        /// <param name="moduleConfigurations"></param>
        /// <returns></returns>
        public static ICbenZeroConfig Zero(this IModuleConfigurations moduleConfigurations)
        {
            return moduleConfigurations.CbenConfiguration.Get<ICbenZeroConfig>();
        }
    }
}