using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cben.Application.Services.Dto;
using Cben.Authorization;
using Cben.AutoMapper;
using Cben.Extensions;
using Cben.MultiTenancy;
using Cben.Runtime.Security;
using Cben.Core.Authorization;
using Cben.Core.Authorization.Roles;
using Cben.Application.MultiTenancy.Dto;
using Cben.Core.MultiTenancy;
using Cben.Core.Editions;
using Cben.Core.Users;

namespace Cben.Application.MultiTenancy
{
    [CbenAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantAppService : CbenAppServiceBase, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly RoleManager _roleManager;
        private readonly EditionManager _editionManager;
        private readonly ICbenZeroDbMigrator _CbenZeroDbMigrator;

        public TenantAppService(
            TenantManager tenantManager, 
            RoleManager roleManager, 
            EditionManager editionManager, 
            ICbenZeroDbMigrator CbenZeroDbMigrator)
        {
            _tenantManager = tenantManager;
            _roleManager = roleManager;
            _editionManager = editionManager;
            _CbenZeroDbMigrator = CbenZeroDbMigrator;
        }

        public ListResultDto<TenantListDto> GetTenants()
        {
            return new ListResultDto<TenantListDto>(
                _tenantManager.Tenants
                    .OrderBy(t => t.TenancyName)
                    .ToList()
                    .MapTo<List<TenantListDto>>()
                );
        }

        public async Task CreateTenant(CreateTenantInput input)
        {
            //Create tenant
            var tenant = input.MapTo<Tenant>();
            tenant.ConnectionString = input.ConnectionString.IsNullOrEmpty()
                ? null
                : SimpleStringCipher.Instance.Encrypt(input.ConnectionString);

            var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
            if (defaultEdition != null)
            {
                tenant.EditionId = defaultEdition.Id;
            }

            CheckErrors(await TenantManager.CreateAsync(tenant));
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new tenant's id.

            //Create tenant database
            _CbenZeroDbMigrator.CreateOrMigrateForTenant(tenant);

            //We are working entities of new tenant, so changing tenant filter
            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                //Create static roles for new tenant
                CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                await CurrentUnitOfWork.SaveChangesAsync(); //To get static role ids

                //grant all permissions to admin role
                var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                await _roleManager.GrantAllPermissionsAsync(adminRole);

                //Create admin user for the tenant
                var adminUser = User.CreateTenantAdminUser(tenant.Id, input.AdminEmailAddress, User.DefaultPassword);
                CheckErrors(await UserManager.CreateAsync(adminUser));
                await CurrentUnitOfWork.SaveChangesAsync(); //To get admin user's id

                //Assign admin user to role!
                CheckErrors(await UserManager.AddToRoleAsync(adminUser.Id, adminRole.Name));
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }
    }
}