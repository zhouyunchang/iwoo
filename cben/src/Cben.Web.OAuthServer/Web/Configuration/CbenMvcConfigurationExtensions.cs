using Cben.Configuration.Startup;

namespace Cben.Web.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure Cben.Web.Api module.
    /// </summary>
    public static class CbenMvcConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Cben.Web.Api module.
        /// </summary>
        public static ICbenMvcConfiguration CbenMvc(this IModuleConfigurations configurations)
        {
            return configurations.CbenConfiguration.Get<ICbenMvcConfiguration>();
        }
    }
}