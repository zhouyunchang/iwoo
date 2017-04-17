using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cben.Application.Users;
using Cben.Dependency;
using Cben.Threading;
using Cben.Core.Authorization;

namespace Cben.Application.Tests
{
    /// <summary>
    /// Summary description for UserTests
    /// </summary>
    [TestClass]
    public class UserTests : TestBase<TestModule>
    {
        protected IUserAppService UserService => IocManager.Instance.Resolve<IUserAppService>();
        

        /// <summary>
        /// UserId
        /// </summary>
        public const int UserId = 1;


        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetUserTest()
        {

            var t = UserService.GetUsers();
            t.Wait();
            var users = t.Result;

            Assert.AreNotSame(users.Items.Count, 0);
        }
    }
}
