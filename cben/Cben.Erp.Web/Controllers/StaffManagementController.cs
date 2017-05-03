using Cben.Erp.Api;
using Erp.Application.Employee;
using Erp.Application.Employee.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cben.Erp.Web.Controllers
{
    public class StaffManagementController : BaseController
    {
        EmployeeApi eApi = new EmployeeApi();

        // GET: StaffManagement
        public ActionResult Index()
        {
            var userInfoList = eApi.GetAllEmployee();

            return View(userInfoList);
        }

        [HttpPost]
        public ActionResult GetUserInfo(long id)
        {
            var userInfo = eApi.GetEmployee(id);
            return Json(userInfo);
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
            long UserId = long.Parse(Request["UserId"]);

            UpdateEmployeeInput input = new UpdateEmployeeInput();
            input.Id = UserId;
            input.UserName = UserName;
            input.SerialNumber = UserCode;
            input.Name = RealName;
            input.Phone = PhoneNum;
            var result = eApi.UpdateEmployee(input);
            if (result.Success)
                return Json(new { flag = true, msg = "成功" });
            else
                return Json(new { flag = false, msg = "失败" });

        }


        [HttpPost]
        public ActionResult DelUserInfo(long id)
        {
            var result = eApi.RemoveEmployee(id);
            return Json(new
            {
                flag = result.Success,
                msg = result.Success ? "成功" : "失败"
            });

        }

    }
}
