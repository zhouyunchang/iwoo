using Cben.Core.Users;
using Cben.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Models
{

    /// <summary>
    /// 产品工序负责人
    /// </summary>
    public class ProcessRecord : FullAuditedEntity<Guid, User>
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        [Required]
        public virtual long ProductId { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// 工序
        /// </summary>
        [Required]
        public virtual int ProcessId { get; set; }

        public virtual Process Process { get; set; }

        /// <summary>
        /// 工序负责人
        /// </summary>
        public virtual List<Employee> PersonInCharge { get; set; }

    }
}
