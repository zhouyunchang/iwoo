using Cben.Erp.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angle.Controllers
{
    public class BlogController : BaseController
    {
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult BlogArticles()
        {
            return View();
        }
        public ActionResult BlogArticleView()
        {
            return View();
        }
        public ActionResult BlogPost()
        {
            return View();
        }
    }
}