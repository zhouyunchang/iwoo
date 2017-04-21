using Castle.MicroKernel.Registration;
using Cben.Dependency;
using Cben.WebApi.Controllers;
using System.Web.Http;

namespace Cben.WebApi
{
    public class ApiControllerConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .BasedOn<ApiController>()
                    .LifestyleTransient()
                );
        }
    }
}