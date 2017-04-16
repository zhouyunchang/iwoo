using Cben.MultiTenancy;
using Cben.Core.Users;

namespace Cben.Core.MultiTenancy
{
    public class Tenant : CbenTenant<User>
    {
        public Tenant()
        {
            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}