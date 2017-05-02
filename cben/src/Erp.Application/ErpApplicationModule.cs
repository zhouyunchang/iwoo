using Cben.Dependency;
using Cben.Modules;
using Erp.Application.Employee;
using Erp.Application.Process;
using Erp.Application.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application
{
    [DependsOn(typeof(ErpModule))]
    public class ErpApplicationModule : CbenModule
    {

        public override void PreInitialize()
        {
            IocManager.Register<IEmployeeAppService, EmployeeAppService>(DependencyLifeStyle.Transient);
            IocManager.Register<IProcessAppService, ProcessAppService>(DependencyLifeStyle.Transient);
            IocManager.Register<IProductBatchAppService, ProductBatchAppService>(DependencyLifeStyle.Transient);
            IocManager.Register<IProductAppService, ProductAppService>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
