using Cben.Erp.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angle.Controllers
{
    public class TablesController : BaseController
    {
        public ActionResult TableJQGrid()
        {
            return View();
        }
        public ActionResult TableDatatable()
        {
            return View();
        }
        public ActionResult TableExtended()
        {
            return View();
        }
        public ActionResult TableStandard()
        {
            return View();
        }

    }
}