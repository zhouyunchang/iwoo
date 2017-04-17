using System;
using System.Threading.Tasks;
using Cben.Application.Services;
using Cben.IdentityFramework;
using Cben.Runtime.Session;
using Cben.Core.MultiTenancy;
using Cben.Core.Users;
using Microsoft.AspNet.Identity;
using Cben;

namespace Cben.Application
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class CbenAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected CbenAppServiceBase()
        {
            LocalizationSourceName = CbenConsts.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(CbenSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(CbenSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}