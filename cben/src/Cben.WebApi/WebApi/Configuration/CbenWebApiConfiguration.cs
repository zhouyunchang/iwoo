using Cben.Domain.Uow;
using Cben.Web.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Cben.WebApi.Configuration
{
    public class CbenWebApiConfiguration : ICbenWebApiConfiguration
    {
        public UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        public WrapResultAttribute DefaultWrapResultAttribute { get; }

        public List<string> ResultWrappingIgnoreUrls { get; }

        public string DomainFormat { get; set; }

        public string LocalizationCookieName { get; set; }

        public bool IsValidationEnabledForControllers { get; set; }

        public bool IsAutomaticAntiForgeryValidationEnabled { get; set; }

        public bool IsAuditingEnabled { get; set; }

        public bool IsAuditingEnabledForChildActions { get; set; }

        public bool SendAllExceptionsToClients { get; set; }

        public bool SetNoCacheForAllResponses { get; set; }

        public HttpConfiguration HttpConfiguration { get; set; }

        public CbenWebApiConfiguration()
        {
            DefaultUnitOfWorkAttribute = new UnitOfWorkAttribute();
            DefaultWrapResultAttribute = new WrapResultAttribute();
            IsValidationEnabledForControllers = true;
            IsAutomaticAntiForgeryValidationEnabled = true;
            IsAuditingEnabled = true;
            HttpConfiguration = GlobalConfiguration.Configuration;
            ResultWrappingIgnoreUrls = new List<string>();
            SetNoCacheForAllResponses = true;

            LocalizationCookieName = "Cben.Localization.CultureName";
        }
    }
}