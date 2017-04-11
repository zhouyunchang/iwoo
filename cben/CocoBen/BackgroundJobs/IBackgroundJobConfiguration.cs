using Cben.Configuration.Startup;

namespace Cben.BackgroundJobs
{
    /// <summary>
    /// Used to configure background job system.
    /// </summary>
    public interface IBackgroundJobConfiguration
    {
        /// <summary>
        /// Used to enable/disable background job execution.
        /// </summary>
        bool IsJobExecutionEnabled { get; set; }

        /// <summary>
        /// Gets the Cben configuration object.
        /// </summary>
        ICbenStartupConfiguration CbenConfiguration { get; }
    }
}