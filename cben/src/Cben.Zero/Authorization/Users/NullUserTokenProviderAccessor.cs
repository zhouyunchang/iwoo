using Cben.Dependency;
using Microsoft.AspNet.Identity;

namespace Cben.Authorization.Users
{
    public class NullUserTokenProviderAccessor : IUserTokenProviderAccessor, ISingletonDependency
    {
        public IUserTokenProvider<TUser, long> GetUserTokenProviderOrNull<TUser>() where TUser : CbenUser<TUser>
        {
            return null;
        }
    }
}