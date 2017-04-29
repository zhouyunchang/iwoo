using Erp.Models;
using Erp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cben.EntityFramework;
using System.Data.Entity;

namespace Erp.EntityFramework.Repositories
{
    public class ProcessCategoryRepository : ErpRepositoryBase<ProcessCategory, int>, IProcessCategoryRepository
    {
        public ProcessCategoryRepository(IDbContextProvider<ErpDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<ProcessCategory>> MatchByName(string name)
        {
            return await GetAllListAsync(e => e.Name.Contains(name));
        }
    }
}
