namespace Cben.Configuration.Startup
{
    internal class ModuleConfigurations : IModuleConfigurations
    {
        public ICbenStartupConfiguration CbenConfiguration { get; private set; }

        public ModuleConfigurations(ICbenStartupConfiguration cbenConfiguration)
        {
            CbenConfiguration = cbenConfiguration;
        }
    }
}