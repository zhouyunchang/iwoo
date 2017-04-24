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
    /// 工序 
    /// </summary>
    public class Process : Entity<int>
    {
        public const int MaxProductProcessNameLength = 20;


        /// <summary>
        /// 工序名称
        /// </summary>
        [Required]
        [StringLength(MaxProductProcessNameLength)]
        public virtual string Name { get; set; }

        ///// <summary>
        ///// 产品Id
        ///// </summary>
        //[Required]
        //public virtual int ProductId { get; set; }


        ///// <summary>
        ///// 产品
        ///// </summary>
        //public virtual Product Product { get; set; }


    }
}
