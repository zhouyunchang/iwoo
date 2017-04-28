using Cben.Core.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cben.Domain.Entities.Auditing;

namespace Erp.Models
{
    /// <summary>
    /// 员工
    /// </summary>
    [Table("Erp_Employee")]
    public class Employee : FullAuditedEntity<long>, IFullAudited<Cben.Core.Users.User>
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
        /// 创建用户
        /// </summary>
        public User CreatorUser { get; set; }

        /// <summary>
        /// 最近修改用户
        /// </summary>
        public User LastModifierUser { get; set; }

        /// <summary>
        /// 删除用户
        /// </summary>
        public User DeleterUser { get; set; }

    }
}
