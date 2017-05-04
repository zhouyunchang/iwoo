using Cben.Core.Users;
using Cben.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Models
{
    /// <summary>
    /// 产品
    /// </summary>
    public class Product : FullAuditedEntity<long, User>
    {

        public Product()
        {
            ProcessRecords = new List<ProcessRecord>();
        }

        /// <summary>
        /// 产品批次Id
        /// </summary>
        [Required]
        public virtual int ProductBatchId { get; set; }

        /// <summary>
        /// 产品批准
        /// </summary>
        public virtual ProductBatch ProductBatch { get; set; }


        /// <summary>
        /// 产品工序负责人
        /// </summary>
        public virtual List<ProcessRecord> ProcessRecords { get; set; }

    }
}
