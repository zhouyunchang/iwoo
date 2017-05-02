using Cben.Core.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cben.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Erp.Models
{
    /// <summary>
    /// 员工
    /// </summary>
    [Table("Erp_Employee")]
    public class Employee : FullAuditedEntity<long, User>
    {

        /// <summary>
        /// 员工序号最大长度
        /// </summary>
        public const int MaxSerialNumberLength = 20;

        /// <summary>
        /// 员工ID
        /// </summary>
        [Key]
        public override long Id { get; set; }

        /// <summary>
        /// 员工序号
        /// </summary>
        [Required]
        [StringLength(MaxSerialNumberLength)]
        public string SerialNumber { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 产品批准工序负责人
        /// </summary>
        public virtual List<ProcessRecord> ProcessRecords { get; set; }

    }
}
