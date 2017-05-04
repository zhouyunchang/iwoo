using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application.Product.Dto
{

    /// <summary>
    /// 产品工序负责人录入 
    /// </summary>
    public class AddProductInput
    {
        
        /// <summary>
        /// 产品批次Id
        /// </summary>
        [Required]
        public int ProductBatchId { get; set; }


        /// <summary>
        /// 产品工序负责人
        /// </summary>
        public List<AddOrUpdateProcessRecordDto> ProcessRecords { get; set; } = new List<AddOrUpdateProcessRecordDto>();

    }


    /// <summary>
    /// 工序负责人Dto
    /// </summary>
    public class AddOrUpdateProcessRecordDto
    {
        /// <summary>
        /// 工序Id
        /// </summary>
        [Required]
        public int ProcessId { get; set; }

        /// <summary>
        /// 工序负责人
        /// </summary>
        public List<long> PersonInCharge { get; set; } = new List<long>();
    }

}
