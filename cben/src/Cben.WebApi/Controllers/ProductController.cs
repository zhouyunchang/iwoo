using Cben.Application.Services.Dto;
using Cben.WebApi.Authorization;
using Erp.Application.Product;
using Erp.Application.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Cben.WebApi.Controllers
{

    /// <summary>
    /// 产品接口
    /// </summary>
    [ApiAuthorize]
    public class ProductController : ApiControllerBase
    {

        private readonly IProductAppService _productAppService;


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="productAppService"></param>
        public ProductController(
            IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }


        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(AddProductInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productAppService.AddProduct(input);
            return Ok();
        }


        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put(UpdateProductInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productAppService.UpdateProduct(input);
            return Ok();
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Delete(long id)
        {
            await _productAppService.RemoveProduct(id);
            return Ok();
        }


        /// <summary>
        /// 获取指定产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductListDto> Get(long id)
        {
            var dto = await _productAppService.GetProduct(id);
            return dto;
        }

        /// <summary>
        /// 获取批次下所有产品
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        [Route("api/ProductBatch/{batchId}/Product")]
        [HttpGet]
        public async Task<ListResultDto<ProductListDto>> GetByBatch(int batchId)
        {
            var lst = await _productAppService.GetAllProduct(batchId);
            return lst;
        }


    }
}
