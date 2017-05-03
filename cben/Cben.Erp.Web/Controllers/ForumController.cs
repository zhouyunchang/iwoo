using Cben.Erp.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angle.Controllers
{
    public class ForumController : BaseController
    {
        public ActionResult ForumCategories()
        {
            return View();
        }
        public ActionResult ForumTopics()
        {
            return View();
        }
        public ActionResult ForumDiscussion()
        {
            return View();
        }
    }
}