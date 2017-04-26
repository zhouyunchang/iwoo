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
    public class EmployeeRepository : ErpRepositoryBase<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(IDbContextProvider<ErpDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


    }
}
