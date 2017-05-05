using Cben.Application.Services.Dto;
using Cben.WebApi.Authorization;
using Erp.Application.Product;
using Erp.Application.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Cben.WebApi.Controllers
{
    [ApiAuthorize]
    public class WorkStatController : ApiControllerBase
    {
        private readonly IWorkStatAppService _workStatService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="workStatService"></param>
        public WorkStatController(IWorkStatAppService workStatService)
        {
            _workStatService = workStatService;
        }

        /// <summary>
        /// 月工作量数据
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        [Route("api/WorkStat/Month")]
        [HttpGet]
        public async Task<ListResultDto<EmployeeWorkStatDto>> MonthStat(DateTime month)
        {
            var result = await _workStatService.GetMonthStat(month);
            return result;
        }


    }
}
