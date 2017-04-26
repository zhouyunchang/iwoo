using Cben.Core.EntityFramework;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.EntityFramework
{
    public class ErpDbContext : CbenCoreDbContext
    {

        public ErpDbContext()
            : base("Default")
        {

        }

        public ErpDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public ErpDbContext(DbConnection connection)
            : base(connection)
        {

        }

        /// <summary>
        /// 员工 
        /// </summary>
        public DbSet<Employee> Employee { get; set; }


    }
}
