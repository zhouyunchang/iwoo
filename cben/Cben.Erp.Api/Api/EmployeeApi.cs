using Erp.Application.Employee;
using Erp.Application.Employee.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Erp.Api
{
    public class EmployeeApi : ApiBase
    {

        public ApiResult AddEmployee(AddEmployeeInput input)
        {
            var result = Request<ApiResult>("/api/Employee", HttpMethod.POST, input);
            return result;
        }


        public ApiResult UpdateEmployee(UpdateEmployeeInput input)
        {
            var result = Request<ApiResult>("/api/Employee", HttpMethod.PUT, input);
            return result;
        }

        public ApiResult RemoveEmployee(long id)
        {
            var result = Request<ApiResult>(
                string.Format("/api/Employee/{0}", id), HttpMethod.DELETE);
            return result;
        }

        public IEnumerable<EmployeeListDto> GetAllEmployee()
        {
            var result = Request<ApiResult<ListResultDto<EmployeeListDto>>>("/api/Employee", HttpMethod.GET);
            return result.Result.Items;
        }

        public EmployeeListDto GetEmployee(long id)
        {
            var result = Request<ApiResult<EmployeeListDto>>(
                string.Format("/api/Employee/{0}", id), HttpMethod.GET);

            return result.Result;
        }

    }
}
