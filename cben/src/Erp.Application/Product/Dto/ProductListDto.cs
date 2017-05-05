using Cben.Application.Services.Dto;
using Cben.AutoMapper;
using Erp.Application.Employee.Dto;
using Erp.Application.Process;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application.Product.Dto
{
    /// <summary>
    /// 产品列表Dto
    /// </summary>
    [AutoMapFrom(typeof(Models.Product))]
    public class ProductListDto : EntityDto<long>
    {

        /// <summary>
        /// 产品批次
        /// </summary>
        public ProductBatchListDto ProductBatch { get; set; }

        /// <summary>
        /// 工序负责人
        /// </summary>
        public IList<ProcessRecordListDto> ProcessRecords { get; set; }

    }


    /// <summary>
    /// 工序负责人Dto
    /// </summary>
    [AutoMapFrom(typeof(ProcessRecord))]
    public class ProcessRecordListDto : EntityDto<Guid>
    {

        /// <summary>
        /// 工序
        /// </summary>
        public ProcessListDto Process { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public List<ProcessRecordEmployeeDto> PersonInCharge { get; set; }

        public string PersonInChargeString
        {
            get
            {
                if (PersonInCharge == null || PersonInCharge.Count == 0) return string.Empty;

                return string.Join(", ",
                    PersonInCharge.Select(i => string.Format("{0}({1})", i.Employee.SerialNumber, i.Employee.User.Name))
                );
            }
        }
    }

    [AutoMapFrom(typeof(ProcessRecordEmployee))]
    public class ProcessRecordEmployeeDto
    {

        /// <summary>
        /// 负责人
        /// </summary>
        public EmployeeListDto Employee { get; set; }

        /// <summary>
        /// 次数
        /// </summary>
        public double Times { get; set; }
    }

}
