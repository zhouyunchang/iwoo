using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cben.Modules;
using Cben.Threading;
using Cben.Core.Authorization;
using Cben.Dependency;
using Cben.Runtime.Session;
using System.Threading;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Cben.Application.Tests
{
    [TestClass]
    public class TestBase<TStartupModule> where TStartupModule : CbenModule
    {

        public TestApplication<TStartupModule> Application;

        protected LogInManager LogInManager => IocManager.Instance.Resolve<LogInManager>();

        public TestBase()
        {
            Application = new TestApplication<TStartupModule>();
            Application.Application_Start();
        }

        [TestInitialize]
        public void Initialize()
        {
            AsyncHelper.RunSync(() => LogInManager.LoginAsync("admin", "123456"));
        }


        [TestCleanup]
        public void Cleanup()
        {
            Application.Application_End();
        }
    }
}
