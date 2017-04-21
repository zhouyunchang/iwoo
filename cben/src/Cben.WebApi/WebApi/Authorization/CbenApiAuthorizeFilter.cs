using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Cben.Authorization;
using Cben.Dependency;
using Cben.Events.Bus;
using Cben.Events.Bus.Exceptions;
using Cben.Localization;
using Cben.Logging;
using Cben.Web;
using Cben.Web.Models;
using Cben.WebApi.Configuration;
using Cben.WebApi.Controllers;
using Cben.WebApi.Validation;
using Cben.WebApi.Models;

namespace Cben.WebApi.Authorization
{
    public class CbenApiAuthorizeFilter : IAuthorizationFilter, ITransientDependency
    {
        public bool AllowMultiple => false;

        private readonly IAuthorizationHelper _authorizationHelper;
        private readonly ICbenWebApiConfiguration _configuration;
        private readonly ILocalizationManager _localizationManager;
        private readonly IEventBus _eventBus;

        public CbenApiAuthorizeFilter(
            IAuthorizationHelper authorizationHelper,
            ICbenWebApiConfiguration configuration,
            ILocalizationManager localizationManager,
            IEventBus eventBus)
        {
            _authorizationHelper = authorizationHelper;
            _configuration = configuration;
            _localizationManager = localizationManager;
            _eventBus = eventBus;
        }

        public virtual async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return await continuation();
            }
            
            var methodInfo = actionContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return await continuation();
            }

            if (actionContext.ActionDescriptor.IsDynamicCbenAction())
            {
                return await continuation();
            }

            try
            {
                await _authorizationHelper.AuthorizeAsync(methodInfo);
                return await continuation();
            }
            catch (CbenAuthorizationException ex)
            {
                LogHelper.Logger.Warn(ex.ToString(), ex);
                _eventBus.Trigger(this, new CbenHandledExceptionData(ex));
                return CreateUnAuthorizedResponse(actionContext);
            }
        }

        protected virtual HttpResponseMessage CreateUnAuthorizedResponse(HttpActionContext actionContext)
        {
            var statusCode = GetUnAuthorizedStatusCode(actionContext);

            var wrapResultAttribute =
                HttpActionDescriptorHelper.GetWrapResultAttributeOrNull(actionContext.ActionDescriptor) ??
                _configuration.DefaultWrapResultAttribute;

            if (!wrapResultAttribute.WrapOnError)
            {
                return new HttpResponseMessage(statusCode);
            }

            return new HttpResponseMessage(statusCode)
            {
                Content = new ObjectContent<AjaxResponse>(
                    new AjaxResponse(
                        GetUnAuthorizedErrorMessage(statusCode),
                        true
                    ),
                    _configuration.HttpConfiguration.Formatters.JsonFormatter
                )
            };
        }

        private ErrorInfo GetUnAuthorizedErrorMessage(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Forbidden)
            {
                return new ErrorInfo(
                    _localizationManager.GetString(CbenWebConsts.LocalizationSourceName, "DefaultError403"),
                    _localizationManager.GetString(CbenWebConsts.LocalizationSourceName, "DefaultErrorDetail403")
                );
            }

            return new ErrorInfo(
                _localizationManager.GetString(CbenWebConsts.LocalizationSourceName, "DefaultError401"),
                _localizationManager.GetString(CbenWebConsts.LocalizationSourceName, "DefaultErrorDetail401")
            );
        }

        private static HttpStatusCode GetUnAuthorizedStatusCode(HttpActionContext actionContext)
        {
            return (actionContext.RequestContext.Principal?.Identity?.IsAuthenticated ?? false)
                ? HttpStatusCode.Forbidden
                : HttpStatusCode.Unauthorized;
        }
    }
}