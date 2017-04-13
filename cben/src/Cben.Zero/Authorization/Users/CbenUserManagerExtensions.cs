using System;
using Cben.Authorization.Roles;
using Cben.MultiTenancy;
using Cben.Threading;

namespace Cben.Authorization.Users
{
    /// <summary>
    /// Extension methods for <see cref="CbenUserManager{TRole,TUser}"/>.
    /// </summary>
    public static class CbenUserManagerExtensions
    {
        /// <summary>
        /// Check whether a user is granted for a permission.
        /// </summary>
        /// <param name="manager">User manager</param>
        /// <param name="userId">User id</param>
        /// <param name="permissionName">Permission name</param>
        public static bool IsGranted<TRole, TUser>(CbenUserManager<TRole, TUser> manager, long userId, string permissionName)
            where TRole : CbenRole<TUser>, new()
            where TUser : CbenUser<TUser>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            return AsyncHelper.RunSync(() => manager.IsGrantedAsync(userId, permissionName));
        }

        //public static CbenUserManager<TRole, TUser> Login<TRole, TUser>(CbenUserManager<TRole, TUser> manager, string userNameOrEmailAddress, string plainPassword, string tenancyName = null)
        //    where TRole : CbenRole<TUser>, new()
        //    where TUser : CbenUser<TUser>
        //{
        //    if (manager == null)
        //    {
        //        throw new ArgumentNullException(nameof(manager));
        //    }

        //    return AsyncHelper.RunSync(() => manager.LoginAsync(userNameOrEmailAddress, plainPassword, tenancyName));
        //}
    }
}