using Cben.Dependency;
using Cben.Threading;
using Cben.Web.Localization;
using System;

namespace Cben.Web
{
    public class OAuthServerApplication : System.Web.HttpApplication
    {

        public static CbenBootstrapper CbenBootstrapper => CbenBootstrapper.Create<OAuthServerModule>();

        protected virtual void Application_Start()
        {
            ThreadCultureSanitizer.Sanitize();
            CbenBootstrapper.Initialize();
        }

        protected virtual void Application_End()
        {
            CbenBootstrapper.Dispose();
        }

        /// <summary>
        /// This method is called by ASP.NET system when a request starts.
        /// </summary>
        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            SetCurrentCulture();
        }

        protected virtual void SetCurrentCulture()
        {
            CbenBootstrapper.IocManager.Using<ICurrentCultureSetter>(cultureSetter => cultureSetter.SetCurrentCulture(Context));
        }
    }
}
