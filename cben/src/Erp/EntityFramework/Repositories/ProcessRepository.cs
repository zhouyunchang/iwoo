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
    public class ProcessRepository : ErpRepositoryBase<Process, int>, IProcessRepository
    {
        public ProcessRepository(IDbContextProvider<ErpDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
