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
    /// 产品批次
    /// </summary>
    public class ProductBatch : FullAuditedEntity<int, User>
    {

        /// <summary>
        /// 批号最大长度
        /// </summary>
        public const int MaxProductBatchNoLength = 20;

        /// <summary>
        /// 工艺号最大长度
        /// </summary>
        public const int MaxProductTechNoLength = 20;

        /// <summary>
        /// 批号
        /// </summary>
        [Required]
        [StringLength(MaxProductBatchNoLength)]
        public string BatchNo { get; set; }

        /// <summary>
        /// 规格/容积
        /// </summary>
        [Required]
        public string Spec { get; set; }

        /// <summary>
        /// 工艺号
        /// </summary>
        [Required]
        [StringLength(MaxProductTechNoLength)]
        public string TechNo { get; set; }

        /// <summary>
        /// 直径
        /// </summary>
        [Required]
        public double Diameter { get; set; }

        /// <summary>
        /// 压力
        /// </summary>
        [Required]
        public double Pressure { get; set; }

    }
}
