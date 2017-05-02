using Cben;
using Cben.Dependency;
using Cben.Modules;
using Erp.EntityFramework.Repositories;
using Erp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Erp
{

    public class ErpModule : CbenModule
    {

        public override void PreInitialize()
        {
            IocManager.Register<IEmployeeRepository, EmployeeRepository>(DependencyLifeStyle.Transient);
            IocManager.Register<IProcessRepository, ProcessRepository>(DependencyLifeStyle.Transient);
            IocManager.Register<IProcessCategoryRepository, ProcessCategoryRepository>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

    }
}
