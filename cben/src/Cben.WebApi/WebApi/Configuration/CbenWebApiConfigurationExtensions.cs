using Cben.Configuration.Startup;

namespace Cben.WebApi.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure Cben.Web.Api module.
    /// </summary>
    public static class CbenWebApiConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Cben.Web.Api module.
        /// </summary>
        public static ICbenWebApiConfiguration CbenMvc(this IModuleConfigurations configurations)
        {
            return configurations.CbenConfiguration.Get<ICbenWebApiConfiguration>();
        }
    }
}