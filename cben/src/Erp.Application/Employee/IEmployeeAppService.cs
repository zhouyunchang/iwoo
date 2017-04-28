using Cben.Application.Services;
using Cben.Application.Services.Dto;
using Erp.Application.Employee.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application.Employee
{
    public interface IEmployeeAppService : IApplicationService
    {

        /// <summary>
        /// 添加员工 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddEmployee(AddEmployeeInput input);

        /// <summary>
        /// 更新员工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateEmployee(UpdateEmployeeInput input);

        /// <summary>
        /// 删除员工 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task RemoveEmployee(long employeeId);

        /// <summary>
        /// 获取所有员工
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<EmployeeListDto>> GetAllEmployee();

        /// <summary>
        /// 获取员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EmployeeListDto> GetById(long id);
    }
}
