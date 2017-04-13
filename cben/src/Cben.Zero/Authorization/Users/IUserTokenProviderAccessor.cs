using Microsoft.AspNet.Identity;

namespace Cben.Authorization.Users
{
    public interface IUserTokenProviderAccessor
    {
        IUserTokenProvider<TUser, long> GetUserTokenProviderOrNull<TUser>() 
            where TUser : CbenUser<TUser>;
    }
}