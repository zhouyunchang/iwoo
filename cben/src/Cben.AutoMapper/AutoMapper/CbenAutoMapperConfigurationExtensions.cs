using Cben.Configuration.Startup;

namespace Cben.AutoMapper
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure Cben.AutoMapper module.
    /// </summary>
    public static class CbenAutoMapperConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Cben.AutoMapper module.
        /// </summary>
        public static ICbenAutoMapperConfiguration CbenAutoMapper(this IModuleConfigurations configurations)
        {
            return configurations.CbenConfiguration.Get<ICbenAutoMapperConfiguration>();
        }
    }
}