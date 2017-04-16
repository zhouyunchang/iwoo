using System.Reflection;
using Cben.Domain.Uow;
using Cben.EntityFramework;
using Cben.Modules;
using Cben.MultiTenancy;
using Castle.MicroKernel.Registration;

namespace Cben.Zero.EntityFramework
{
    /// <summary>
    /// Entity framework integration module for Cben Zero.
    /// </summary>
    [DependsOn(typeof(CbenZeroCoreModule), typeof(CbenEntityFrameworkModule))]
    public class CbenZeroEntityFrameworkModule : CbenModule
    {
        public override void PreInitialize()
        {
            Configuration.ReplaceService(typeof(IConnectionStringResolver), () =>
            {
                IocManager.IocContainer.Register(
                    Component.For<IConnectionStringResolver, IDbPerTenantConnectionStringResolver>()
                        .ImplementedBy<DbPerTenantConnectionStringResolver>()
                        .LifestyleTransient()
                    );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
