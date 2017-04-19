using Castle.MicroKernel.Registration;
using Cben.Dependency;
using System.Web.Mvc;

namespace Cben.WebApi.Module
{
    public class MvcControllerConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .BasedOn<Controller>()
                    .LifestyleTransient()
                );
        }
    }
}