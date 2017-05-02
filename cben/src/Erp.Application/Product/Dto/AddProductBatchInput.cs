using Erp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application.Product.Dto
{

    /// <summary>
    /// 添加产品批次
    /// </summary>
    public class AddProductBatchInput
    {

        /// <summary>
        /// 批号
        /// </summary>
        [Required]
        [StringLength(ProductBatch.MaxProductBatchNoLength)]
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
        [StringLength(ProductBatch.MaxProductTechNoLength)]
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
