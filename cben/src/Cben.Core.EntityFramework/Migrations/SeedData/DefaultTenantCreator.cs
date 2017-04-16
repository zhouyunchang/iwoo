using System.Linq;
using Cben.Core.EntityFramework;
using Cben.Core.MultiTenancy;

namespace Cben.Core.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly CbenCoreDbContext _context;

        public DefaultTenantCreator(CbenCoreDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
