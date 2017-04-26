using Cben.Domain.Entities;
using Cben.EntityFramework;
using Cben.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.EntityFramework.Repositories
{
    public abstract class ErpRepositoryBase<TEntity, TPrimaryKey>
        : EfRepositoryBase<ErpDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {

        protected ErpRepositoryBase(IDbContextProvider<ErpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

    }


    public abstract class ErpRepositoryBase<TEntity> : ErpRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected ErpRepositoryBase(IDbContextProvider<ErpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }

}
