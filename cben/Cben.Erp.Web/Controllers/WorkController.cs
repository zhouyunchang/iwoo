using Cben.Erp.Api;
using Erp.Application.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cben.Erp.Web.Controllers
{

    public class WorkController : BaseController
    {

        private readonly WorkStatApi _workStatApi;
        private readonly ProcessApi _processApi;

        public WorkController()
        {
            _workStatApi = new WorkStatApi();
            _processApi = new ProcessApi();
        }


        public ActionResult MonthStat(DateTime? month)
        {
            month = month ?? DateTime.Now;

            var result = _workStatApi.GetMonthStat(month.Value);
            ViewBag.WorkStat = result;

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

            ViewBag.Month = month.Value.ToString("yyyy-MM");

            return View();
        }
    }
}