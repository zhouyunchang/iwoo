using Cben.Domain.Repositories;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Repositories
{
    public interface IProductRepository : IRepository<Product, long>
    {

        /// <summary>
        /// 删除工序责任人
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task DeleteProcessRecordAsync(ProcessRecord record);

    }
}
