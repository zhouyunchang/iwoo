using Cben.Erp.Api;
using Erp.Application.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cben.Erp.Web.Controllers
{
    public class ProductController : BaseController
    {

        private readonly ProcessApi _processApi;

        public ProductController()
        {
            _processApi = new ProcessApi();

        }

        /// <summary>
        /// 工序管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ProcessManager()
        {
            var category = _processApi.GetAllCategory();
            var processes = _processApi.GetAllProcess();
            var dict = new Dictionary<ProcessCategoryListDto, IList<ProcessListDto>>();
            foreach (var item in category.OrderBy(i => i.OrderNum))
            {
                dict.Add(item,
                    processes.Where(i => i.Category.Id == item.Id).OrderBy(i => i.OrderNum).ToList()
                    );
            }

            ViewBag.Processes = dict;

            return View();
        }

        /// <summary>
        /// 获取工序
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProcess(int id)
        {
            var result = _processApi.GetProcess(id);
            return Json(result);
        }

        /// <summary>
        /// 添加工序
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddProcess()
        {
            AddProcessInput input = new AddProcessInput
            {
                CategoryId = int.Parse(Request["Category"]),
                GuidePrice = decimal.Parse(Request["Price"]),
                Name = Request["Name"],
                OrderNum = 0
            };

            var result = _processApi.AddProcess(input);
            return Json(new { success = result.Success, error = result.Error });
        }

        /// <summary>
        /// 更新工序
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateProcess()
        {
            UpdateProcessInput input = new UpdateProcessInput
            {
                Id = int.Parse(Request["Id"]),
                CategoryId = int.Parse(Request["Category"]),
                GuidePrice = decimal.Parse(Request["Price"]),
                Name = Request["Name"],
                OrderNum = 0
            };

            var result = _processApi.UpdateProcess(input);
            return Json(new { success = result.Success, error = result.Error });
        }

        /// <summary>
        /// 删除工序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveProcess(int id)
        {
            var result = _processApi.RemoveProcess(id);
            return Json(new { success = result.Success, error = result.Error });
        }


        /// <summary>
        /// 添加工序分班
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddProcessCategory()
        {
            AddProcessCategoryInput input = new AddProcessCategoryInput
            {
                Name = Request["Name"],
                OrderNum = 0
            };

            var result = _processApi.AddCategory(input);
            return Json(new { success = result.Success, error = result.Error });
        }


        /// <summary>
        /// 更新工序分班
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateProcessCategory()
        {
            UpdateProcessCategoryInput input = new UpdateProcessCategoryInput
            {
                Id = int.Parse(Request["Id"]),
                Name = Request["Name"],
                OrderNum = 0
            };

            var result = _processApi.UpdateCategory(input);
            return Json(new { success = result.Success, error = result.Error });
        }

        /// <summary>
        /// 删除工序分班
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveProcessCategory(int id)
        {
            var result = _processApi.RemoveCategory(id);
            return Json(new { success = result.Success, error = result.Error });
        }

    }
}
