using Cben.Application.Users.Dto;
using Erp.Application.Employee.Dto;
using Erp.Application.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application.Product.Dto
{
    public class EmployeeWorkStatDto
    {

        /// <summary>
        /// 人员
        /// </summary>
        public EmployeeListDto Employee { get; set; }


        /// <summary>
        /// 产品批次
        /// </summary>
        public ProductBatchListDto ProductBatch { get; set; }


        /// <summary>
        /// 工作量
        /// </summary>
        public List<ProcessCountDto> Process { get; set; }

    }

    public class ProcessCountDto
    {
        /// <summary>
        /// 工序
        /// </summary>
        public int ProcessId { get; set; }

        /// <summary>
        /// 次数
        /// </summary>
        public double Count { get; set; }
    }

}
