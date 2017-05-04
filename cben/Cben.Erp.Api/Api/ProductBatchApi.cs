using Erp.Application.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Erp.Api
{
    public class ProductBatchApi :ApiBase
    {
        public ApiResult AddProductBatch(AddProductBatchInput input)
        {
            var result = Request<ApiResult>("/api/ProductBatch", HttpMethod.POST, input);
            return result;
        }

        public ApiResult UpdateProductBatch(UpdateProductBatchInput input)
        {
            var result = Request<ApiResult>("/api/ProductBatch", HttpMethod.PUT, input);
            return result;
        }


        public ApiResult RemoveProductBatch(int id) {
            var result = Request<ApiResult>(string.Format("/api/ProductBatch/{0}",id), HttpMethod.DELETE);
            return result;
        }

        public IEnumerable<ProductBatchListDto> GetAllProductBatch() {
            var result = Request<ApiResult<ListResultDto<ProductBatchListDto>>>("/api/ProductBatch", HttpMethod.GET);
            return result.Result.Items;
        }


        public ProductBatchListDto GetProductBatch(long id)
        {
            var result = Request<ApiResult<ProductBatchListDto>>(
                string.Format("/api/ProductBatch/{0}", id), HttpMethod.GET);

            return result.Result;
        }


    }
}
