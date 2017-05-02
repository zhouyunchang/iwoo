using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application.Product.Dto
{

    /// <summary>
    /// 更新产品批次
    /// </summary>
    public class UpdateProductBatchInput : AddProductBatchInput
    {

        /// <summary>
        /// 产品批次Id
        /// </summary>
        public int Id { get; set; }

    }
}
