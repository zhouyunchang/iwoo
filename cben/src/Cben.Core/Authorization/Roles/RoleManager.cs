using Cben.Authorization;
using Cben.Authorization.Roles;
using Cben.Domain.Uow;
using Cben.Runtime.Caching;
using Cben.Zero.Configuration;
using Cben.Core.Users;

namespace Cben.Core.Authorization.Roles
{
    public class RoleManager : CbenRoleManager<Role, User>
    {
        public RoleManager(
            RoleStore store,
            IPermissionManager permissionManager,
            IRoleManagementConfig roleManagementConfig,
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                store,
                permissionManager,
                roleManagementConfig,
                cacheManager,
                unitOfWorkManager)
        {
        }
    }
}