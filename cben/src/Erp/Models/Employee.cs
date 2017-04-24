using Cben.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cben;
using Cben.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp.Models
{
    /// <summary>
    /// 员工
    /// </summary>
    public class Employee : Entity<int>
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        [Column("UserId")]
        public override int Id { get; set; }


        /// <summary>
        /// 员工序号
        /// </summary>
        [Required]
        public string SerialNumber { get; set; }


        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }

    }
}
