using Cben.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Models
{

    /// <summary>
    /// 产品规格
    /// </summary>
    public class ProductSpecification : Entity<int>
    {

        public const int MaxProductSpecifictionNameLength = 50;

        /// <summary>
        /// 产品规格名称
        /// </summary>
        [Required]
        [StringLength(MaxProductSpecifictionNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        [Required]
        public virtual int ProductId { get; set; }


        /// <summary>
        /// 产品
        /// </summary>
        public virtual Product Product { get; set; }

    }
}
