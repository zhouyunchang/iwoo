using System;
using System.Linq;
using System.Web;
using Cben.Dependency;
using Cben.Extensions;
using Cben.MultiTenancy;
using Cben.Text;
using Cben.Web.Configuration;

namespace Cben.Web.MultiTenancy
{
    public class DomainTenantResolveContributor : ITenantResolveContributor, ITransientDependency
    {
        private readonly ICbenMvcConfiguration _multiTenancyConfiguration;
        private readonly ITenantStore _tenantStore;

        public DomainTenantResolveContributor(
            ICbenMvcConfiguration multiTenancyConfiguration,
            ITenantStore tenantStore)
        {
            _multiTenancyConfiguration = multiTenancyConfiguration;
            _tenantStore = tenantStore;
        }

        public int? ResolveTenantId()
        {
            if (_multiTenancyConfiguration.DomainFormat.IsNullOrEmpty())
            {
                return null;
            }

            var httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return null;
            }

            var hostName = httpContext.Request.Url.Host.RemovePreFix("http://", "https://");
            var result = new FormattedStringValueExtracter().Extract(hostName, _multiTenancyConfiguration.DomainFormat, true);
            if (!result.IsMatch || !result.Matches.Any())
            {
                return null;
            }

            var tenancyName = result.Matches[0].Value;
            if (tenancyName.IsNullOrEmpty())
            {
                return null;
            }

            if (string.Equals(tenancyName, "www", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var tenantInfo = _tenantStore.Find(tenancyName);
            if (tenantInfo == null)
            {
                return null;
            }

            return tenantInfo.Id;
        }
    }
}