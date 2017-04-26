using Cben.Erp.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cben.Erp.Web.Controllers
{

    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            ValuesApi api = new ValuesApi();
            var lst = api.Get();

            return View();
        }

        public ActionResult Login()
        {
            Auth.ProcessUserAuthorization(Request);

            return RedirectToAction("Index");
        }

    }
}