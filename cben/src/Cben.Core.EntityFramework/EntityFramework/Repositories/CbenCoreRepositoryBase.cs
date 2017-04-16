using Cben.Domain.Entities;
using Cben.EntityFramework;
using Cben.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Core.EntityFramework.Repositories
{
    public abstract class CbenCoreRepositoryBase<TEntity, TPrimaryKey>
        : EfRepositoryBase<CbenCoreDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected CbenCoreRepositoryBase(IDbContextProvider<CbenCoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

    public abstract class CbenCoreRepositoryBase<TEntity> : CbenCoreRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected CbenCoreRepositoryBase(IDbContextProvider<CbenCoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }

}
