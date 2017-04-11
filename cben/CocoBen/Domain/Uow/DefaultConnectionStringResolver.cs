using System;
using System.Configuration;
using Cben.Configuration.Startup;
using Cben.Dependency;

namespace Cben.Domain.Uow
{
    /// <summary>
    /// Default implementation of <see cref="IConnectionStringResolver"/>.
    /// Get connection string from <see cref="ICbenStartupConfiguration"/>,
    /// or "Default" connection string in config file,
    /// or single connection string in config file.
    /// </summary>
    public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency
    {
        private readonly ICbenStartupConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConnectionStringResolver"/> class.
        /// </summary>
        public DefaultConnectionStringResolver(ICbenStartupConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }

            var defaultConnectionString = _configuration.DefaultNameOrConnectionString;
            if (!string.IsNullOrWhiteSpace(defaultConnectionString))
            {
                return defaultConnectionString;
            }

            if (ConfigurationManager.ConnectionStrings["Default"] != null)
            {
                return "Default";
            }

            if (ConfigurationManager.ConnectionStrings.Count == 1)
            {
                return ConfigurationManager.ConnectionStrings[0].ConnectionString;
            }

            throw new CbenException("Could not find a connection string definition for the application. Set ICbenStartupConfiguration.DefaultNameOrConnectionString or add a 'Default' connection string to application .config file.");
        }
    }
}