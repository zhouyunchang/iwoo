using Cben.Core.Authorization.Roles;
using Cben.Core.MultiTenancy;
using Cben.Core.Users;
using Cben.Zero.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Core.EntityFramework
{
    public class CbenCoreDbContext : CbenZeroDbContext<Tenant, Role, User>
    {
        public CbenCoreDbContext()
            : base("SQLExpress")
        {

        }

        public CbenCoreDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public CbenCoreDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
