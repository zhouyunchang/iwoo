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
    /// 产品批次
    /// </summary>
    [ApiAuthorize]
    public class ProductBatchController : ApiControllerBase
    {
        private readonly IProductBatchAppService _prodBatchAppService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="prodBatchAppService"></param>
        public ProductBatchController(
            IProductBatchAppService prodBatchAppService)
        {
            _prodBatchAppService = prodBatchAppService;
        }


        /// <summary>
        /// 添加产品批次
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(AddProductBatchInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _prodBatchAppService.AddProductBatch(input);
            return Ok();
        }


        /// <summary>
        /// 更新产品批次
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put(UpdateProductBatchInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _prodBatchAppService.UpdateProductBatch(input);
            return Ok();
        }

        /// <summary>
        /// 删除产品批次
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _prodBatchAppService.RemoveProductBatch(id);
            return Ok();
        }

        /// <summary>
        /// 获取所有产品批次
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<ProductBatchListDto>> Get()
        {
            var result = await _prodBatchAppService.GetAllProductBatch();
            return result;
        }

        /// <summary>
        /// 获取指定产品批准
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductBatchListDto> Get(int id)
        {
            var result = await _prodBatchAppService.GetProductBatchById(id);
            return result;
        }

    }
}
