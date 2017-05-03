using Cben.Erp.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cben.Erp.Web.Controllers
{

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            UserApi api = new UserApi();
            var userInfo = api.GetUserInfo();

            CurrentUser = userInfo;

            return View();
        }


        public ActionResult Callback()
        {
            Auth.ProcessUserAuthorization(Request);

            return RedirectToAction("Index");
        }

    }
}