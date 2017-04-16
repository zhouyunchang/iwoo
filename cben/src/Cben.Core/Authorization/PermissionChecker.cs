using Cben.Authorization;
using Cben.Core.Authorization.Roles;
using Cben.Core.MultiTenancy;
using Cben.Core.Users;

namespace Cben.Core.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
