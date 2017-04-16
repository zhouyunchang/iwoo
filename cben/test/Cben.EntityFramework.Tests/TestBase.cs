using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cben.Modules;

namespace Cben.EntityFramework.Tests
{
    [TestClass]
    public class TestBase<TStartupModule> where TStartupModule : CbenModule
    {

        public TestApplication<TStartupModule> Application;


        [TestInitialize]
        public void Initialize()
        {
            Application = new TestApplication<TStartupModule>();
            Application.Application_Start();
        }


        [TestCleanup]
        public void Cleanup()
        {
            Application.Application_End();
        }
    }
}
