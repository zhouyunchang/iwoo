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

        public ProductBatchController() {
            _productBatchApi = new ProductBatchApi();
        }


        // GET: ProductBatch
        public ActionResult Index()
        {
            var productBatchList = _productBatchApi.GetAllProductBatch();

            return View(productBatchList);
        }


        [HttpPost]
        public ActionResult GetProductBatchInfo(long id)
        {
            return Json(new { });
        }


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
            model.Diameter =Convert.ToDouble(diameter);
            model.Pressure = Convert.ToDouble(pressure);
            var result = _productBatchApi.AddProductBatch(model);

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
        public ActionResult UpdateProductBatch()
        {
            return Json(new { });

        }



        [HttpPost]
        public ActionResult DelProductBatch(long id)
        {

            return Json(new { });
        }



    }
}