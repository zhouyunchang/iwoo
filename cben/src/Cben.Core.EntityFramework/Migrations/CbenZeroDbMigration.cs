using Cben.Core.EntityFramework;
using Cben.Dependency;
using Cben.Domain.Uow;
using Cben.MultiTenancy;
using Cben.Zero.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Core.Migrations
{
    public class CbenZeroDbMigrator : CbenZeroDbMigrator<CbenCoreDbContext, Configuration>
    {
        public CbenZeroDbMigrator(
            IUnitOfWorkManager unitOfWorkManager,
            IDbPerTenantConnectionStringResolver connectionStringResolver,
            IIocResolver iocResolver)
            : base(
                unitOfWorkManager,
                connectionStringResolver,
                iocResolver)
        {
        }
    }
}
