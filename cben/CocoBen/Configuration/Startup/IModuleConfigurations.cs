namespace Cben.Configuration.Startup
{
    /// <summary>
    /// Used to provide a way to configure modules.
    /// Create entension methods to this class to be used over <see cref="ICbenStartupConfiguration.Modules"/> object.
    /// </summary>
    public interface IModuleConfigurations
    {
        /// <summary>
        /// Gets the Cben configuration object.
        /// </summary>
        ICbenStartupConfiguration CbenConfiguration { get; }
    }
}