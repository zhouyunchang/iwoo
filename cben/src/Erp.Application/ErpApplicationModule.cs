using Cben.Dependency;
using Cben.Modules;
using Erp.Application.Employee;
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
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
