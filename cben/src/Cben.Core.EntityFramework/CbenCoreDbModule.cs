using Cben.Core.EntityFramework;
using Cben.Core.EntityFramework.Repositories;
using Cben.Modules;
using Cben.Zero.EntityFramework;
using Cben.Dependency;
using System.Data.Entity;
using System.Reflection;

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

#if !SQLExpress
            Configuration.DefaultNameOrConnectionString = "Default";
#else
            Configuration.DefaultNameOrConnectionString = "SQLExpress";
#endif

            IocManager.Register<IClientRepository, ClientRepository>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

    }
}
