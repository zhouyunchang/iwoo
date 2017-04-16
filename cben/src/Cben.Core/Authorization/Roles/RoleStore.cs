using Cben.Authorization.Roles;
using Cben.Authorization.Users;
using Cben.Domain.Repositories;
using Cben.Core.Users;

namespace Cben.Core.Authorization.Roles
{
    public class RoleStore : CbenRoleStore<Role, User>
    {
        public RoleStore(
            IRepository<Role> roleRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
            : base(
                roleRepository,
                userRoleRepository,
                rolePermissionSettingRepository)
        {
        }
    }
}