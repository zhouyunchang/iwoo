using Cben.Erp.Api;
using Erp.Application.Process;
using Erp.Application.Product.Dto;
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
        private readonly ProductBatchApi _productBatchApi;
        private readonly ProductApi _productApi;
        private readonly EmployeeApi _employeeApi;

        public ProductController()
        {
            _processApi = new ProcessApi();
            _productBatchApi = new ProductBatchApi();
            _productApi = new ProductApi();
            _employeeApi = new EmployeeApi();
        }


        #region 工序管理

        /// <summary>
        /// 工序管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ProcessManage()
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

        #endregion


        #region 工序责任人录入

        /// <summary>
        /// 批次下产品管理
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public ActionResult ProductProcess(int batchId, string returnUrl)
        {
            var productBatch = _productBatchApi.GetProductBatch(batchId);
            ViewBag.ProductBatch = productBatch;

            var products = _productApi.GetProductByBatch(batchId);
            ViewBag.Products = products;

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

            ViewBag.Employee = _employeeApi.GetAllEmployee();

            ViewBag.ReturnUrl = returnUrl ?? Url.Action("Index", "ProductBatch");

            return View();
        }

        /// <summary>
        /// 获取工序责任人
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProductProcess(long id)
        {
            var product = _productApi.GetProduct(id);
            return Json(product);
        }


        /// <summary>
        /// 工序责任人录入 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddProduct()
        {
            AddProductInput input = new AddProductInput
            {
                ProductBatchId = int.Parse(Request["BatchId"]),
                ProcessRecords = new List<AddOrUpdateProcessRecordDto>()
            };

            var processes = _processApi.GetAllProcess();
            foreach (var item in processes)
            {
                string val = Request["p_" + item.Id];
                if (!string.IsNullOrEmpty(val))
                {
                    var ids = val.Split(',').Select(i => long.Parse(i)).ToList();

                    input.ProcessRecords.Add(new AddOrUpdateProcessRecordDto
                    {
                        ProcessId = item.Id,
                        PersonInCharge = ids
                    });
                }
            }

            var result = _productApi.AddProduct(input);
            return Json(new { success = result.Success, error = result.Error });
        }


        /// <summary>
        /// 工序责任人录入更新
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateProduct()
        {
            UpdateProductInput input = new UpdateProductInput
            {
                Id = long.Parse(Request["Id"]),
                ProductBatchId = int.Parse(Request["BatchId"]),
                ProcessRecords = new List<AddOrUpdateProcessRecordDto>()
            };

            var processes = _processApi.GetAllProcess();
            foreach (var item in processes)
            {
                string val = Request["p_" + item.Id];
                if (!string.IsNullOrEmpty(val))
                {
                    var ids = val.Split(',').Select(i => long.Parse(i)).ToList();

                    input.ProcessRecords.Add(new AddOrUpdateProcessRecordDto
                    {
                        ProcessId = item.Id,
                        PersonInCharge = ids
                    });
                }
            }

            var result = _productApi.UpdateProduct(input);
            return Json(new { success = result.Success, error = result.Error });
        }

        /// <summary>
        /// 删除工序责任人记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveProduct(long id)
        {
            var result = _productApi.RemoveProduct(id);
            return Json(new { success = result.Success, error = result.Error });
        }


        #endregion


    }
}
