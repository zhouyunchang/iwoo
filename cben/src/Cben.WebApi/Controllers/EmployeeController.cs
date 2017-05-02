using Cben.Application.Services.Dto;
using Cben.Domain.Uow;
using Cben.WebApi.Authorization;
using Cben.WebApi.Models.Employee;
using Erp.Application.Employee;
using Erp.Application.Employee.Dto;
using Erp.Models;
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
    /// 员工接口
    /// </summary>
    [ApiAuthorize]
    public class EmployeeController : ApiControllerBase
    {

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IEmployeeAppService _employeeAppService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="employeeAppService"></param>
        public EmployeeController(
            IUnitOfWorkManager unitOfWorkManager,
            IEmployeeAppService employeeAppService)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _employeeAppService = employeeAppService;
        }

        /// <summary>
        /// 添加员工 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post([FromBody]AddEmployeeInput employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _employeeAppService.AddEmployee(employee);

            return Ok();
        }

        /// <summary>
        /// 更新员工
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put([FromBody]UpdateEmployeeInput employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _employeeAppService.UpdateEmployee(employee);

            return Ok();
        }

        /// <summary>
        /// 删除员工 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Delete(long id)
        {
            await _employeeAppService.RemoveEmployee(id);

            return Ok();
        }

        /// <summary>
        /// 获取所有员工
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<EmployeeListDto>> Get()
        {
            var lst = await _employeeAppService.GetAllEmployee();

            return lst;
        }

        /// <summary>
        /// 获取指定员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EmployeeListDto> Get(long id)
        {
            var employee = await _employeeAppService.GetById(id);
            return employee;
        }

    }
}
