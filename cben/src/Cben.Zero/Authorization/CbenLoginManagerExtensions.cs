using Cben.Authorization.Roles;
using Cben.Authorization.Users;
using Cben.MultiTenancy;
using Cben.Threading;

namespace Cben.Authorization
{
    public static class CbenLogInManagerExtensions
    {
        public static CbenLoginResult<TTenant, TUser> Login<TTenant, TRole, TUser>(
            this CbenLogInManager<TTenant, TRole, TUser> logInManager, 
            string userNameOrEmailAddress, 
            string plainPassword, 
            string tenancyName = null, 
            bool shouldLockout = true)
                where TTenant : CbenTenant<TUser>
                where TRole : CbenRole<TUser>, new()
                where TUser : CbenUser<TUser>
        {
            return AsyncHelper.RunSync(
                () => logInManager.LoginAsync(
                    userNameOrEmailAddress,
                    plainPassword,
                    tenancyName,
                    shouldLockout
                )
            );
        }
    }
}
