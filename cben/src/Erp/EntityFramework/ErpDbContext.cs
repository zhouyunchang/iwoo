using Cben.Core.EntityFramework;
using Cben.EntityFramework;
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

        /// <summary>
        /// 员工 
        /// </summary>
        public DbSet<Employee> Employee { get; set; }

        /// <summary>
        /// 工序
        /// </summary>
        public DbSet<Process> Processes { get; set; }

        /// <summary>
        /// 工序分类
        /// </summary>
        public DbSet<ProcessCategory> ProcessCategory { get; set; }


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


    }
}
