using Cben.Authorization;
using Cben.Authorization.Users;
using Cben.Configuration;
using Cben.Configuration.Startup;
using Cben.Dependency;
using Cben.Domain.Repositories;
using Cben.Domain.Uow;
using Cben.Zero.Configuration;
using Cben.Core.Authorization.Roles;
using Cben.Core.MultiTenancy;
using Cben.Core.Users;

namespace Cben.Core.Authorization
{
    public class LogInManager : CbenLogInManager<Tenant, Role, User>
    {
        public LogInManager(
            UserManager userManager,
            IMultiTenancyConfig multiTenancyConfig,
            IRepository<Tenant> tenantRepository,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository,
            IUserManagementConfig userManagementConfig, IIocResolver iocResolver,
            RoleManager roleManager)
            : base(
                  userManager,
                  multiTenancyConfig,
                  tenantRepository,
                  unitOfWorkManager,
                  settingManager,
                  userLoginAttemptRepository,
                  userManagementConfig,
                  iocResolver,
                  roleManager)
        {
        }
    }
}
