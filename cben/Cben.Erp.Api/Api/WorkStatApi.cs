using Erp.Application.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Erp.Api
{
    public class WorkStatApi : ApiBase
    {

        public IEnumerable<EmployeeWorkStatDto> GetMonthStat(DateTime month)
        {
            var result = Request<ApiResult<ListResultDto<EmployeeWorkStatDto>>>(
                string.Format("/api/WorkStat/Month?month={0}", month.ToString("yyyy-MM")), HttpMethod.GET);
            return result.Result.Items;
        }

    }
}
