namespace Erp.Application.Process
{

    /// <summary>
    /// 更新分组名称
    /// </summary>
    public class UpdateProcessCategoryInput : AddProcessCategoryInput
    {

        /// <summary>
        /// 工序分组Id
        /// </summary>
        public int Id { get; set; }

    }
}