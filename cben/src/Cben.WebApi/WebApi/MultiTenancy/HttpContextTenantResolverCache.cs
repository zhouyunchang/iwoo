using System.Web;
using Cben.Dependency;
using Cben.MultiTenancy;

namespace Cben.WebApi.MultiTenancy
{
    public class HttpContextTenantResolverCache : ITenantResolverCache, ITransientDependency
    {
        private const string CacheItemKey = "Cben.MultiTenancy.TenantResolverCacheItem";

        public TenantResolverCacheItem Value
        {
            get
            {
                return HttpContext.Current?.Items[CacheItemKey] as TenantResolverCacheItem;
            }

            set
            {
                var httpContext = HttpContext.Current;
                if (httpContext == null)
                {
                    return;
                }

                httpContext.Items[CacheItemKey] = value;
            }
        }
    }
}
