using Cben.Erp.Api;
using Erp.Application.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cben.Erp.Web.Controllers
{
    public class StaffManagementController : ControllerBase
    {
        EmployeeApi eApi = new EmployeeApi();

        // GET: StaffManagement
        public ActionResult Index()
        {
            var userInfoList = eApi.GetAllEmployee();

            return View(userInfoList);
        }


        [HttpPost]
        public ActionResult AddUserInfo()
        {
            string UserCode = Request["UserCode"] ?? "";
            string UserName = Request["UserName"] ?? "";
            string RealName = Request["RealName"] ?? "";
            string PhoneNum = Request["PhoneNum"] ?? "";
            AddEmployeeInput model = new AddEmployeeInput();
            model.UserName = UserName;
            model.SerialNumber = UserCode;
            model.Name = RealName;
            model.Phone = PhoneNum;
            var result = eApi.AddEmployee(model);

            if (result.Success)
            {
                return Json(new { flag = true, msg = "成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { flag = false, msg = "添加失败" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateUserInfo()
        {

            string UserCode = Request["UserCode"] ?? "";
            string UserName = Request["UserName"] ?? "";
            string RealName = Request["RealName"] ?? "";
            string PhoneNum = Request["PhoneNum"] ?? "";


            return Json(new { flag = true, msg = "成功" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DelUserInfo()
        {
            string id = Request["id"] ?? "0";
            if (id != "0")
            {
                int intid = Convert.ToInt32(id);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
