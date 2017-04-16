using Cben.Authorization;
using Cben.Authorization.Users;
using Cben.Configuration;
using Cben.Core.Authorization.Roles;
using Cben.Domain.Repositories;
using Cben.Domain.Uow;
using Cben.IdentityFramework;
using Cben.Localization;
using Cben.Organizations;
using Cben.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Core.Users
{
    public class UserManager : CbenUserManager<Role, User>
    {

        public UserManager(
            UserStore userStore, 
            RoleManager roleManager,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            ILocalizationManager localizationManager,
            ISettingManager settingManager,
            IdentityEmailMessageService emailService,
            IUserTokenProviderAccessor userTokenProviderAccessor)
            : base(
                  userStore, 
                  roleManager, 
                  permissionManager, 
                  unitOfWorkManager,
                  cacheManager,
                  organizationUnitRepository,
                  userOrganizationUnitRepository,
                  organizationUnitSettings,
                  localizationManager,
                  emailService,
                  settingManager,
                  userTokenProviderAccessor
                  )
        {

        }


    }
}
