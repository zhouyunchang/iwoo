using Cben.Domain.Repositories;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee, long>
    {
    }
}
