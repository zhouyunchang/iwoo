using Cben.Domain.Uow;
using Cben.Web.Models;

namespace Cben.Web.Configuration
{
    public class CbenMvcConfiguration : ICbenMvcConfiguration
    {
        public UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        public WrapResultAttribute DefaultWrapResultAttribute { get; }

        public string DomainFormat { get; set; }

        public string LocalizationCookieName { get; set; }

        public bool IsValidationEnabledForControllers { get; set; }

        public bool IsAutomaticAntiForgeryValidationEnabled { get; set; }

        public bool IsAuditingEnabled { get; set; }

        public bool IsAuditingEnabledForChildActions { get; set; }

        public bool SendAllExceptionsToClients { get; set; }

        public CbenMvcConfiguration()
        {
            DefaultUnitOfWorkAttribute = new UnitOfWorkAttribute();
            DefaultWrapResultAttribute = new WrapResultAttribute();
            IsValidationEnabledForControllers = true;
            IsAutomaticAntiForgeryValidationEnabled = true;
            IsAuditingEnabled = true;

            LocalizationCookieName = "Cben.Localization.CultureName";
        }
    }
}