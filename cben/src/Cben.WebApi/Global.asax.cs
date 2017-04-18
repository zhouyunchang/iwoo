using Cben.Threading;
using Cben.WebApi.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Cben.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        public static CbenBootstrapper CbenBootstrapper => CbenBootstrapper.Create<WebApiModule>();

        protected virtual void Application_Start()
        {
            ThreadCultureSanitizer.Sanitize();
            CbenBootstrapper.Initialize();
        }

        protected virtual void Application_End()
        {
            CbenBootstrapper.Dispose();
        }
    }
}
