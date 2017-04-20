using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Cben.Dependency;
using Cben.WebApi.Configuration;

namespace Cben.WebApi.Validation
{
    public class CbenApiValidationFilter : IActionFilter, ITransientDependency
    {
        public bool AllowMultiple => false;

        private readonly IIocResolver _iocResolver;
        private readonly ICbenWebApiConfiguration _configuration;

        public CbenApiValidationFilter(IIocResolver iocResolver, ICbenWebApiConfiguration configuration)
        {
            _iocResolver = iocResolver;
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (!_configuration.IsValidationEnabledForControllers)
            {
                return await continuation();
            }

            var methodInfo = actionContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return await continuation();
            }

            /* ModelState.IsValid is being checked to handle parameter binding errors (ex: send string for an int value).
             * These type of errors can not be catched from application layer. */

            if (actionContext.ModelState.IsValid
                && actionContext.ActionDescriptor.IsDynamicCbenAction())
            {
                return await continuation();
            }

            using (var validator = _iocResolver.ResolveAsDisposable<WebApiActionInvocationValidator>())
            {
                validator.Object.Initialize(actionContext, methodInfo);
                validator.Object.Validate();
            }

            return await continuation();
        }
    }
}