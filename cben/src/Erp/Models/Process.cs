using Cben.Core.Users;
using Cben.Domain.Entities;
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
    /// 工序 
    /// </summary>
    public class Process : FullAuditedEntity<int, User>
    {
        public const int MaxProductProcessNameLength = 20;

        /// <summary>
        /// 工序名称
        /// </summary>
        [Required]
        [StringLength(MaxProductProcessNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 工序分类
        /// </summary>
        [Required]
        public virtual int CategoryId { get; set; }

        /// <summary>
        /// 工序分类
        /// </summary>
        public virtual ProcessCategory Category { get; set; }


    }
}
