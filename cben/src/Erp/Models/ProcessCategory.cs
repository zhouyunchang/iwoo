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
    /// 工序分类, 焊接班, 试水班等
    /// </summary>
    public class ProcessCategory : FullAuditedEntity<int, User>
    {
        public const int MaxProcessCategoryNameLength = 20;

        /// <summary>
        /// 工序分类名称
        /// </summary>
        [StringLength(MaxProcessCategoryNameLength)]
        public virtual string Name { get; set; }
        

    }
}
