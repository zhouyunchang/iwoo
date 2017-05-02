using System.ComponentModel.DataAnnotations;

namespace Erp.Application.Process
{

    /// <summary>
    /// 添加工序
    /// </summary>
    public class AddProcessInput
    {

        /// <summary>
        /// 工序名称
        /// </summary>
        [Required]
        [StringLength(Models.Process.MaxProductProcessNameLength, ErrorMessage = "{0}长度不能大于{1}")]
        [Display(Name = "工序名称")]
        public string Name { get; set; }


        /// <summary>
        /// 工序分组
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "工序分组名称")]
        public int CategoryId { get; set; }

    }
}