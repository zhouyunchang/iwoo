using Cben;
using Cben.Configuration.Startup;
using Cben.Application;
using Cben.Core;
using Cben.Dependency;
using Cben.Modules;
using Cben.Runtime.Session;
using Cben.Zero.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Cben.Collections.Extensions;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json.Serialization;
using Cben.Json;
using Cben.WebApi.Configuration;
using System.Web.Mvc;
using System.Web.Http.Dispatcher;
using Cben.WebApi.Session;
using Cben.WebApi.MultiTenancy;
using Microsoft.AspNet.Identity;
using Cben.WebApi.Authorization;
using Cben.WebApi.Auditing;
using Cben.WebApi.Validation;
using Cben.WebApi.Uow;
using Cben.WebApi.ExceptionHandling;
using Cben.WebApi.Controllers;
using Microsoft.Owin.Security.OAuth;
using Castle.MicroKernel.Registration;
using Cben.Localization.Dictionaries;
using Cben.Localization.Dictionaries.Xml;
using Erp.Application;
using Erp;

namespace Cben.WebApi
{

    [DependsOn(
        typeof(CbenCoreDbModule),
        typeof(CbenApplicationModule),
        typeof(ErpModule),
        typeof(ErpApplicationModule))]
    public class WebApiModule : CbenModule
    {

        public override void PreInitialize()
        {
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

            IocManager.AddConventionalRegistrar(new ApiControllerConventionalRegistrar());

            IocManager.Register<ICbenWebApiConfiguration, CbenWebApiConfiguration>(DependencyLifeStyle.Transient);

            // database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.ReplaceService<IPrincipalAccessor, HttpContextPrincipalAccessor>(DependencyLifeStyle.Transient);

            Configuration.MultiTenancy.Resolvers.Add<DomainTenantResolveContributor>();
            Configuration.MultiTenancy.Resolvers.Add<HttpHeaderTenantResolveContributor>();
            Configuration.MultiTenancy.Resolvers.Add<HttpCookieTenantResolveContributor>();

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    CbenWebConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Cben.WebApi.Localization.Source"
                        )
                    )
                );

            AddIgnoredTypes();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            var httpConfiguration = Configuration.Modules.WebApi().HttpConfiguration;
            httpConfiguration.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
        }

        public override void PostInitialize()
        {
            HttpConfiguration httpConfiguration = IocManager.Resolve<ICbenWebApiConfiguration>().HttpConfiguration;

            InitializeAspNetServices(httpConfiguration);
            InitializeFormatters(httpConfiguration);
            InitializeModelBinders(httpConfiguration);
            InitializeFilters(httpConfiguration);

            //Configuration.Modules.WebApi().HttpConfiguration.EnsureInitialized();
        }

        private void AddIgnoredTypes()
        {
            var ignoredTypes = new[]
            {
                typeof(HttpPostedFileBase),
                typeof(IEnumerable<HttpPostedFileBase>),
                typeof(HttpPostedFileWrapper),
                typeof(IEnumerable<HttpPostedFileWrapper>)
            };

            foreach (var ignoredType in ignoredTypes)
            {
                Configuration.Auditing.IgnoredTypes.AddIfNotContains(ignoredType);
                Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
            }
        }

        private void InitializeAspNetServices(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Services.Replace(typeof(IHttpControllerActivator),
                new CbenApiControllerActivator(IocManager));
        }

        private void InitializeFormatters(HttpConfiguration httpConfiguration)
        {
            //Remove formatters except JsonFormatter.
            //foreach (var currentFormatter in httpConfiguration.Formatters.ToList())
            //{
            //    if (!(currentFormatter is JsonMediaTypeFormatter ||
            //        currentFormatter is JQueryMvcFormUrlEncodedFormatter))
            //    {
            //        httpConfiguration.Formatters.Remove(currentFormatter);
            //    }
            //}

            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.Converters.Insert(0, new CbenDateTimeConverter());
        }

        private void InitializeModelBinders(HttpConfiguration httpConfiguration)
        {
            var cbenApiDateTimeBinder = new CbenApiDateTimeBinder();
            httpConfiguration.BindParameter(typeof(DateTime), cbenApiDateTimeBinder);
            httpConfiguration.BindParameter(typeof(DateTime?), cbenApiDateTimeBinder);
        }

        private void InitializeFilters(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Filters.Add(IocManager.Resolve<CbenApiAuthorizeFilter>());
            httpConfiguration.Filters.Add(IocManager.Resolve<CbenApiAuditFilter>());
            httpConfiguration.Filters.Add(IocManager.Resolve<CbenApiValidationFilter>());
            httpConfiguration.Filters.Add(IocManager.Resolve<CbenApiUowFilter>());
            httpConfiguration.Filters.Add(IocManager.Resolve<CbenApiExceptionFilterAttribute>());

            httpConfiguration.MessageHandlers.Add(IocManager.Resolve<ResultWrapperHandler>());
        }
    }
}