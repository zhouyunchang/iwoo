using Cben.Application.Services;
using Cben.Application.Services.Dto;
using Erp.Application.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application.Product
{
    public interface IProductAppService : IApplicationService
    {

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddProduct(AddProductInput input);

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateProduct(UpdateProductInput input);

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveProduct(long id);

        /// <summary>
        /// 获取指定的产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductListDto> GetProduct(long id);

        /// <summary>
        /// 获取批次下所有产品
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<ProductListDto>> GetAllProduct(int batchId);


    }
}
