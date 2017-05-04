using Cben.Erp.Api;
using Erp.Application.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cben.Erp.Web.Controllers
{
    public class ProductBatchController : BaseController
    {
        private readonly ProductBatchApi _productBatchApi;

        public ProductBatchController()
        {
            _productBatchApi = new ProductBatchApi();
        }


        /// <summary>
        /// 台账管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var productBatchList = _productBatchApi.GetAllProductBatch();

            return View(productBatchList);
        }


        /// <summary>
        /// 获取批次信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProductBatchInfo(int id)
        {
            var entity = _productBatchApi.GetProductBatch(id);
            return Json(entity);
        }


        /// <summary>
        /// 添加批次
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddProductBatchInfo()
        {
            string batchNo = Request["BatchNo"] ?? "";
            string spec = Request["Spec"] ?? "";
            string techNo = Request["TechNo"] ?? "";
            string diameter = Request["Diameter"] ?? "";
            string pressure = Request["Pressure"] ?? "";
            AddProductBatchInput model = new AddProductBatchInput();
            model.BatchNo = batchNo;
            model.Spec = spec;
            model.TechNo = techNo;
            model.Diameter = Convert.ToDouble(diameter);
            model.Pressure = Convert.ToDouble(pressure);
            var result = _productBatchApi.AddProductBatch(model);

            if (result.Success)
            {
                return Json(new { flag = true, msg = "成功" });
            }
            else
            {
                return Json(new { flag = false, msg = "添加失败" });
            }
        }


        /// <summary>
        /// 更新批次台账
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateProductBatch()
        {
            UpdateProductBatchInput input = new UpdateProductBatchInput
            {
                Id = int.Parse(Request["BatchId"]),
                BatchNo = Request["BatchNo"],
                TechNo = Request["TechNo"],
                Spec = Request["Spec"],
                Diameter = double.Parse(Request["Diameter"]),
                Pressure = double.Parse(Request["Pressure"])
            };
            var result = _productBatchApi.UpdateProductBatch(input);

            return Json(new { flag = result.Success, msg = result.Error });
        }


        /// <summary>
        /// 删除批次
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelProductBatch(int id)
        {
            var result = _productBatchApi.RemoveProductBatch(id);
            return Json(new { flag = result.Success, msg = result.Error });
        }



    }
}