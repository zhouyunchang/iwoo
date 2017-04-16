using Cben.Core.EntityFramework;
using Cben.Modules;
using Cben.Zero.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Core
{
    [DependsOn(
        typeof(CbenZeroEntityFrameworkModule),
        typeof(CbenCoreModule))]
    public class CbenCoreDbModule : CbenModule
    {

        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CbenCoreDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

    }
}
