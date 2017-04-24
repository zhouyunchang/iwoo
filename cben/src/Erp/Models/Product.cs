using Cben.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cben.Events.Bus;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Erp.Models
{

    /// <summary>
    /// 产品
    /// </summary>
    public class Product : AggregateRoot<int>
    {

        public const int MaxProductNameLength = 50;

        public const int MaxBatchNumberLength = 20;

        /// <summary>
        /// 产品名称
        /// </summary>
        [Required]
        [StringLength(MaxProductNameLength)]
        public virtual string Name { get; set; }


        /// <summary>
        /// 钢瓶批号
        /// </summary>
        [Required]
        [StringLength(MaxBatchNumberLength)]
        public virtual string BatchNumber { get; set; }


        /// <summary>
        /// 产品规格
        /// </summary>
        [ForeignKey("ProductId")]
        public virtual ICollection<ProductSpecification> ProductSpecifications { get; set; }


        /// <summary>
        /// 产品工序
        /// </summary>
        [ForeignKey("ProductId")]
        public virtual ICollection<Process> ProductProcesses { get; set; }


    }
}
