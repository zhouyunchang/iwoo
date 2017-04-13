using Cben.Configuration.Startup;

namespace Cben.BackgroundJobs
{
    internal class BackgroundJobConfiguration : IBackgroundJobConfiguration
    {
        public bool IsJobExecutionEnabled { get; set; }
        
        public ICbenStartupConfiguration CbenConfiguration { get; private set; }

        public BackgroundJobConfiguration(ICbenStartupConfiguration CbenConfiguration)
        {
            CbenConfiguration = CbenConfiguration;

            IsJobExecutionEnabled = true;
        }
    }
}