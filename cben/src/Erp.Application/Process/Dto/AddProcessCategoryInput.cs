using Erp.Models;
using System.ComponentModel.DataAnnotations;

namespace Erp.Application.Process
{


    /// <summary>
    /// 添加工序分组
    /// </summary>
    public class AddProcessCategoryInput
    {


        /// <summary>
        /// 工序分组名称
        /// </summary>
        [Required]
        [StringLength(ProcessCategory.MaxProcessCategoryNameLength, ErrorMessage = "{0}长度不能大于{1}")]
        [Display(Name = "工序分组名称")]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }
    }
}