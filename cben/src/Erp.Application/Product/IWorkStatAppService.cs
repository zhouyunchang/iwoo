using Cben.Application.Services;
using Cben.Application.Services.Dto;
using Erp.Application.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application.Product
{

    /// <summary>
    /// 工作统计服务
    /// </summary>
    public interface IWorkStatAppService : IApplicationService
    {

        /// <summary>
        /// 月工作量数据
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        Task<ListResultDto<EmployeeWorkStatDto>> GetMonthStat(DateTime month);
    }
}
