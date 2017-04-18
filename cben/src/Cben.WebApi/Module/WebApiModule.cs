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

namespace Cben.WebApi.Module
{

    [DependsOn(
        typeof(CbenCoreDbModule),
        typeof(CbenApplicationModule))]
    public class WebApiModule : CbenModule
    {

        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new ApiControllerConventionalRegistrar());

            // database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.ReplaceService<IPrincipalAccessor, HttpContextPrincipalAccessor>(DependencyLifeStyle.Singleton);

            AddIgnoredTypes();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            InitializeFormatters(GlobalConfiguration.Configuration);
            InitializeModelBinders(GlobalConfiguration.Configuration);
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

        private static void InitializeFormatters(HttpConfiguration httpConfiguration)
        {
            //Remove formatters except JsonFormatter.
            foreach (var currentFormatter in httpConfiguration.Formatters.ToList())
            {
                if (!(currentFormatter is JsonMediaTypeFormatter ||
                    currentFormatter is JQueryMvcFormUrlEncodedFormatter))
                {
                    httpConfiguration.Formatters.Remove(currentFormatter);
                }
            }

            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.Converters.Insert(0, new CbenDateTimeConverter());
        }

        private static void InitializeModelBinders(HttpConfiguration httpConfiguration)
        {
            var cbenApiDateTimeBinder = new CbenApiDateTimeBinder();
            httpConfiguration.BindParameter(typeof(DateTime), cbenApiDateTimeBinder);
            httpConfiguration.BindParameter(typeof(DateTime?), cbenApiDateTimeBinder);
        }
    }
}