using Cben.Erp.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angle.Controllers
{
    public class WidgetsController : BaseController
    {
        // GET: Widgets
        public ActionResult Index()
        {
            return View();
        }
    }
}