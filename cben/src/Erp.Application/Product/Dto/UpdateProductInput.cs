using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application.Product.Dto
{
    public class UpdateProductInput : AddProductInput
    {

        /// <summary>
        /// 产品Id
        /// </summary>
        public long Id { get; set; }

    }
}
