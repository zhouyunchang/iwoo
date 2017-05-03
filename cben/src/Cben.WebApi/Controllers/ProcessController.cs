using Cben.Application.Services.Dto;
using Cben.WebApi.Authorization;
using Erp.Application.Process;
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
    /// 工序接口
    /// </summary>
    [ApiAuthorize]
    public class ProcessController : ApiControllerBase
    {

        private readonly IProcessAppService _processAppService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="processAppService"></param>
        public ProcessController(
            IProcessAppService processAppService)
        {
            _processAppService = processAppService;
        }

        #region 工序

        /// <summary>
        /// 获取所有工序
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<ProcessListDto>> Get()
        {
            var lst = await _processAppService.GetAllProcess();

            return lst;
        }

        /// <summary>
        /// 根据id获取指定的工序
        /// </summary>
        public async Task<ProcessListDto> Get(int id)
        {
            var process = await _processAppService.GetProcessById(id);
            return process;
        }

        /// <summary>
        /// 添加工序
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(AddProcessInput process)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _processAppService.AddProcess(process);
            return Ok();
        }

        /// <summary>
        /// 更新工序
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put(UpdateProcessInput process)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _processAppService.UpdateProcess(process);
            return Ok();
        }

        /// <summary>
        /// 删除工序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _processAppService.RemoveProcess(id);
            return Ok();
        }

        #endregion


        #region 工序分组

        /// <summary>
        /// 获取所有工序分组
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/process/category")]
        public async Task<ListResultDto<ProcessCategoryListDto>> AllCategory()
        {
            var category = await _processAppService.GetAllProcessCategory();
            return category;
        }


        /// <summary>
        /// 获取指定工序分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/process/category/{id}")]
        public async Task<ProcessCategoryListDto> Category(int id)
        {
            var category = await _processAppService.GetProcessCategoryById(id);
            return category;
        }


        /// <summary>
        /// 添加工序分组
        /// </summary>
        /// <param name="processCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/process/category")]
        public async Task<IHttpActionResult> PostCategory(AddProcessCategoryInput processCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _processAppService.AddProcessCategory(processCategory);
            return Ok();
        }


        /// <summary>
        /// 更新工序分组
        /// </summary>
        /// <param name="processCategory"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/process/category")]
        public async Task<IHttpActionResult> PutCategory(UpdateProcessCategoryInput processCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _processAppService.UpdateProcessCategory(processCategory);
            return Ok();
        }

        /// <summary>
        /// 删除工序分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/process/category/{id}")]
        public async Task<IHttpActionResult> DeleteCategory(int id)
        {
            await _processAppService.RemoveProcessCategory(id);
            return Ok();
        }

        #endregion


    }
}
