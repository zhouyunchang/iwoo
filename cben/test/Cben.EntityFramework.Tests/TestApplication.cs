using Cben;
using Cben.Modules;
using Cben.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.EntityFramework.Tests
{
    public class TestApplication<TStartupModule> where TStartupModule : CbenModule
    {

        public static CbenBootstrapper CbenBootstrapper => CbenBootstrapper.Create<TStartupModule>();

        public virtual void Application_Start()
        {
            ThreadCultureSanitizer.Sanitize();
            CbenBootstrapper.Initialize();
        }

        public virtual void Application_End()
        {
            CbenBootstrapper.Dispose();
        }
        

    }

}
