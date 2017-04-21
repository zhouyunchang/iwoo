using Castle.Core.Logging;
using Cben.Application.Features;
using Cben.Authorization;
using Cben.Configuration;
using Cben.Domain.Uow;
using Cben.Events.Bus;
using Cben.Localization;
using Cben.Localization.Sources;
using Cben.Reflection;
using Cben.Runtime.Session;
using Cben.Web.Models;
using Cben.WebApi.Configuration;
using Cben.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

namespace Cben.WebApi.Controllers
{
    public class ApiControllerBase : ApiController
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

        public ICbenWebApiConfiguration CbenWebApiConfiguration { get; set; }

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
        protected ApiControllerBase()
        {
            CbenSession = NullCbenSession.Instance;
            Logger = NullLogger.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
            PermissionChecker = NullPermissionChecker.Instance;
            EventBus = NullEventBus.Instance;

            LocalizationSourceName = CbenWebConsts.LocalizationSourceName;
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



    }
}
