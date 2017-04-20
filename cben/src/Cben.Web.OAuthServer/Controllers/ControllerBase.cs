using Castle.Core.Logging;
using Cben.Application.Features;
using Cben.Authorization;
using Cben.Configuration;
using Cben.Domain.Entities;
using Cben.Domain.Uow;
using Cben.Events.Bus;
using Cben.Events.Bus.Exceptions;
using Cben.Localization;
using Cben.Localization.Sources;
using Cben.Logging;
using Cben.Reflection;
using Cben.Runtime.Session;
using Cben.Runtime.Validation;
using Cben.Web.Models;
using Cben.Web.Configuration;
using Cben.Web.Controllers.Results;
using Cben.Web.Extensions;
using Cben.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Cben.Web.Controllers
{
    /// <summary>
    /// Base class for all MVC Controllers in Cben system.
    /// </summary>
    public abstract class ControllerBase : Controller
    {
        /// <summary>
        /// Gets current session information.
        /// </summary>
        public ICbenSession CbenSession { get; set; }

        /// <summary>
        /// Gets the event bus.
        /// </summary>
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// Reference to the permission manager.
        /// </summary>
        public IPermissionManager PermissionManager { get; set; }

        /// <summary>
        /// Reference to the setting manager.
        /// </summary>
        public ISettingManager SettingManager { get; set; }

        /// <summary>
        /// Reference to the permission checker.
        /// </summary>
        public IPermissionChecker PermissionChecker { protected get; set; }

        /// <summary>
        /// Reference to the feature manager.
        /// </summary>
        public IFeatureManager FeatureManager { protected get; set; }

        /// <summary>
        /// Reference to the permission checker.
        /// </summary>
        public IFeatureChecker FeatureChecker { protected get; set; }

        /// <summary>
        /// Reference to the localization manager.
        /// </summary>
        public ILocalizationManager LocalizationManager { protected get; set; }

        /// <summary>
        /// Reference to the error info builder.
        /// </summary>
        public IErrorInfoBuilder ErrorInfoBuilder { protected get; set; }

        /// <summary>
        /// Gets/sets name of the localization source that is used in this application service.
        /// It must be set in order to use <see cref="L(string)"/> and <see cref="L(string,CultureInfo)"/> methods.
        /// </summary>
        protected string LocalizationSourceName { get; set; }

        /// <summary>
        /// Gets localization source.
        /// It's valid if <see cref="LocalizationSourceName"/> is set.
        /// </summary>
        protected ILocalizationSource LocalizationSource
        {
            get
            {
                if (LocalizationSourceName == null)
                {
                    throw new CbenException("Must set LocalizationSourceName before, in order to get LocalizationSource");
                }

                if (_localizationSource == null || _localizationSource.Name != LocalizationSourceName)
                {
                    _localizationSource = LocalizationManager.GetSource(LocalizationSourceName);
                }

                return _localizationSource;
            }
        }
        private ILocalizationSource _localizationSource;

        /// <summary>
        /// Reference to the logger to write logs.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Reference to <see cref="IUnitOfWorkManager"/>.
        /// </summary>
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new CbenException("Must set UnitOfWorkManager before use it.");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        private IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Gets current unit of work.
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }

        public ICbenMvcConfiguration CbenMvcConfiguration { get; set; }

        /// <summary>
        /// MethodInfo for currently executing action.
        /// </summary>
        private MethodInfo _currentMethodInfo;

        /// <summary>
        /// WrapResultAttribute for currently executing action.
        /// </summary>
        private WrapResultAttribute _wrapResultAttribute;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected ControllerBase()
        {
            CbenSession = NullCbenSession.Instance;
            Logger = NullLogger.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
            PermissionChecker = NullPermissionChecker.Instance;
            EventBus = NullEventBus.Instance;
        }

        /// <summary>
        /// Gets localized string for given key name and current language.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        /// <summary>
        /// Gets localized string for given key name and specified culture information.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }

        /// <summary>
        /// Checks if current user is granted for a permission.
        /// </summary>
        /// <param name="permissionName">Name of the permission</param>
        protected Task<bool> IsGrantedAsync(string permissionName)
        {
            return PermissionChecker.IsGrantedAsync(permissionName);
        }

        /// <summary>
        /// Checks if current user is granted for a permission.
        /// </summary>
        /// <param name="permissionName">Name of the permission</param>
        protected bool IsGranted(string permissionName)
        {
            return PermissionChecker.IsGranted(permissionName);
        }


        /// <summary>
        /// Checks if given feature is enabled for current tenant.
        /// </summary>
        /// <param name="featureName">Name of the feature</param>
        /// <returns></returns>
        protected virtual Task<bool> IsEnabledAsync(string featureName)
        {
            return FeatureChecker.IsEnabledAsync(featureName);
        }

        /// <summary>
        /// Checks if given feature is enabled for current tenant.
        /// </summary>
        /// <param name="featureName">Name of the feature</param>
        /// <returns></returns>
        protected virtual bool IsEnabled(string featureName)
        {
            return FeatureChecker.IsEnabled(featureName);
        }


        #region OnActionExecuting / OnActionExecuted

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SetCurrentMethodInfoAndWrapResultAttribute(filterContext);
            base.OnActionExecuting(filterContext);
        }

        private void SetCurrentMethodInfoAndWrapResultAttribute(ActionExecutingContext filterContext)
        {
            //Prevent overriding for child actions
            if (_currentMethodInfo != null)
            {
                return;
            }

            _currentMethodInfo = filterContext.ActionDescriptor.GetMethodInfoOrNull();
            _wrapResultAttribute =
                ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
                    _currentMethodInfo,
                    CbenMvcConfiguration.DefaultWrapResultAttribute
                );
        }

        #endregion

        #region Exception handling

        protected override void OnException(ExceptionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            //If exception handled before, do nothing.
            //If this is child action, exception should be handled by main action.
            if (context.ExceptionHandled || context.IsChildAction)
            {
                base.OnException(context);
                return;
            }

            //Log exception
            if (_wrapResultAttribute == null || _wrapResultAttribute.LogError)
            {
                LogHelper.LogException(Logger, context.Exception);
            }

            // If custom errors are disabled, we need to let the normal ASP.NET exception handler
            // execute so that the user can see useful debugging information.
            if (!context.HttpContext.IsCustomErrorEnabled)
            {
                base.OnException(context);
                return;
            }

            // If this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method),
            // ignore it.
            if (new HttpException(null, context.Exception).GetHttpCode() != 500)
            {
                base.OnException(context);
                return;
            }

            //Check WrapResultAttribute
            if (_wrapResultAttribute == null || !_wrapResultAttribute.WrapOnError)
            {
                base.OnException(context);
                return;
            }

            //We handled the exception!
            context.ExceptionHandled = true;

            //Return an error response to the client.
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode = GetStatusCodeForException(context);

            context.Result = IsJsonResult(_currentMethodInfo)
                ? GenerateJsonExceptionResult(context)
                : GenerateNonJsonExceptionResult(context);

            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            context.HttpContext.Response.TrySkipIisCustomErrors = true;

            //Trigger an event, so we can register it.
            EventBus.Trigger(this, new CbenHandledExceptionData(context.Exception));
        }

        private bool IsJsonResult(MethodInfo method)
        {
            return typeof(JsonResult).IsAssignableFrom(method.ReturnType) ||
                   typeof(Task<JsonResult>).IsAssignableFrom(method.ReturnType);
        }

        protected virtual int GetStatusCodeForException(ExceptionContext context)
        {
            if (context.Exception is CbenAuthorizationException)
            {
                return context.HttpContext.User.Identity.IsAuthenticated
                    ? (int)HttpStatusCode.Forbidden
                    : (int)HttpStatusCode.Unauthorized;
            }

            if (context.Exception is CbenValidationException)
            {
                return (int)HttpStatusCode.BadRequest;
            }

            if (context.Exception is EntityNotFoundException)
            {
                return (int)HttpStatusCode.NotFound;
            }

            return (int)HttpStatusCode.InternalServerError;
        }

        protected virtual ActionResult GenerateJsonExceptionResult(ExceptionContext context)
        {
            context.HttpContext.Items.Add("IgnoreJsonRequestBehaviorDenyGet", "true");
            return new CbenJsonResult(
                new AjaxResponse(
                    ErrorInfoBuilder.BuildForException(context.Exception),
                    context.Exception is CbenAuthorizationException
                    )
                );
        }

        protected virtual ActionResult GenerateNonJsonExceptionResult(ExceptionContext context)
        {
            return new ViewResult
            {
                ViewName = "Error",
                MasterName = string.Empty,
                ViewData = new ViewDataDictionary<ErrorViewModel>(new ErrorViewModel(ErrorInfoBuilder.BuildForException(context.Exception), context.Exception)),
                TempData = context.Controller.TempData
            };
        }

        #endregion
    }
}