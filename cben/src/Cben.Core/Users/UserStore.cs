using Cben.Authorization.Users;
using Cben.Core.Authorization.Roles;
using Cben.Domain.Repositories;
using Cben.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Core.Users
{
    public class UserStore : CbenUserStore<Role, User>
    {

        public UserStore(
            IRepository<User, long> userRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role> roleRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository, 
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<UserClaim, long> userClaimStore)
            : base(
                userRepository,
                userLoginRepository,
                userRoleRepository,
                roleRepository,
                userPermissionSettingRepository,
                unitOfWorkManager,
                userClaimStore)
        {
        }

    }
}
