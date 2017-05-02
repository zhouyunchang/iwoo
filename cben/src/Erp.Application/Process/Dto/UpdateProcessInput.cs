using System.ComponentModel.DataAnnotations;

namespace Erp.Application.Process
{


    /// <summary>
    /// 更新工序
    /// </summary>
    public class UpdateProcessInput : AddProcessInput
    {

        /// <summary>
        /// 工序
        /// </summary>
        [Required]
        public int Id { get; set; }

    }
}