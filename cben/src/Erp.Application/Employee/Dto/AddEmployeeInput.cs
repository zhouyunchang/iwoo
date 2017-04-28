
using Cben.Authorization.Users;
using Cben.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Erp.Application.Employee
{

    /// <summary>
    /// 员工
    /// </summary>
    public class AddEmployeeInput
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        [Required]
        [StringLength(CbenUserBase.MaxUserNameLength, ErrorMessage = "{0}长度不能大于{1}")]
        [Display(Name = "用户名称")]
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [StringLength(User.MaxNameLength, ErrorMessage = "{0}长度不能大于{1}")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 员工序号
        /// </summary>
        [Required]
        [StringLength(Erp.Models.Employee.MaxSerialNumberLength, ErrorMessage = "{0}长度不能大于{1}")]
        [Display(Name = "员工序号")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }


    }
}