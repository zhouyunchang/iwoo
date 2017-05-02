using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cben.Erp.Web.Controllers
{
    public class StaffManagementController : Controller
    {
        // GET: StaffManagement
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUserInfo()
        {
           

            string UserCode = Request["UserCode"] ?? "";
            string UserName = Request["UserName"] ?? "";
            string RealName = Request["RealName"] ?? "";
            string PhoneNum = Request["PhoneNum"] ?? "";



            return Json(new { flag = true, msg = "成功" }, JsonRequestBehavior.AllowGet);
        }


    }
}
