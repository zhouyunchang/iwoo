using System.Security.Claims;
using Cben.MultiTenancy;

namespace Cben.Authorization.Users
{
    public class CbenLoginResult<TTenant, TUser>
        where TTenant : CbenTenant<TUser>
        where TUser : CbenUser<TUser>
    {
        public CbenLoginResultType Result { get; private set; }

        public TTenant Tenant { get; private set; }

        public TUser User { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public CbenLoginResult(CbenLoginResultType result, TTenant tenant = null, TUser user = null)
        {
            Result = result;
            Tenant = tenant;
            User = user;
        }

        public CbenLoginResult(TTenant tenant, TUser user, ClaimsIdentity identity)
            : this(CbenLoginResultType.Success, tenant)
        {
            User = user;
            Identity = identity;
        }
    }
}