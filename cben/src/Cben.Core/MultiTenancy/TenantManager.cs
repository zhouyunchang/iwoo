using Cben.Application.Features;
using Cben.Domain.Repositories;
using Cben.MultiTenancy;
using Cben.Core.Editions;
using Cben.Core.Users;

namespace Cben.Core.MultiTenancy
{
    public class TenantManager : CbenTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            ICbenZeroFeatureValueStore featureValueStore
            ) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore
            )
        {
        }
    }
}