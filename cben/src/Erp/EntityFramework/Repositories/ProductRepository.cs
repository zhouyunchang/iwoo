using Erp.Models;
using Erp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cben.EntityFramework;

namespace Erp.EntityFramework.Repositories
{
    public class ProductRepository : ErpRepositoryBase<Product, long>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<ErpDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


        /// <summary>
        /// 删除工序责任人
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public async Task DeleteProcessRecordAsync(ProcessRecord record)
        {
            Context.Entry(record).State = System.Data.Entity.EntityState.Deleted;
            await Task.FromResult<object>(null);
        }
    }
}
