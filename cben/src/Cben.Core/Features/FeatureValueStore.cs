using Cben.Application.Features;
using Cben.Domain.Repositories;
using Cben.Domain.Uow;
using Cben.MultiTenancy;
using Cben.Runtime.Caching;
using Cben.Core.MultiTenancy;
using Cben.Core.Users;

namespace Cben.Core.Features
{
    public class FeatureValueStore : CbenFeatureValueStore<Tenant, User>
    {
        public FeatureValueStore(
            ICacheManager cacheManager, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            IRepository<Tenant> tenantRepository, 
            IRepository<EditionFeatureSetting, long> editionFeatureRepository, 
            IFeatureManager featureManager, 
            IUnitOfWorkManager unitOfWorkManager) 
            : base(cacheManager, 
                  tenantFeatureRepository, 
                  tenantRepository, 
                  editionFeatureRepository, 
                  featureManager, 
                  unitOfWorkManager)
        {
        }
    }
}