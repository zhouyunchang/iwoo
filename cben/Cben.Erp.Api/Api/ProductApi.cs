using Erp.Application.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Erp.Api
{

    /// <summary>
    /// 产品Api
    /// </summary>
    public class ProductApi : ApiBase
    {

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ApiResult AddProduct(AddProductInput input)
        {
            var result = Request<ApiResult>("/api/Product", HttpMethod.POST, input);
            return result;
        }

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ApiResult UpdateProduct(UpdateProductInput input)
        {
            var result = Request<ApiResult>("/api/Product", HttpMethod.PUT, input);
            return result;
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiResult RemoveProduct(long id)
        {
            var result = Request<ApiResult>(
                string.Format("/api/Product/{0}", id), HttpMethod.DELETE);
            return result;
        }

        /// <summary>
        /// 获取批次下所有产品
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public IEnumerable<ProductListDto> GetProductByBatch(int batchId)
        {
            var result = Request<ApiResult<ListResultDto<ProductListDto>>>(
                string.Format("/api/ProductBatch/{0}/Product", batchId), HttpMethod.GET);

            return result.Result.Items;
        }


        /// <summary>
        /// 获取指定产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductListDto GetProduct(long id)
        {
            var result = Request<ApiResult<ProductListDto>>(
                string.Format("/api/Product/{0}", id), HttpMethod.GET);
            return result.Result;
        }

    }
}
