
using Erp.Application.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Erp.Api
{
    public class ProcessApi : ApiBase
    {

        public ApiResult AddProcess(AddProcessInput input)
        {
            var result = Request<ApiResult>("/api/Process", HttpMethod.POST, input);
            return result;
        }


        public ApiResult UpdateProcess(UpdateProcessInput input)
        {
            var result = Request<ApiResult>("/api/Process", HttpMethod.PUT, input);
            return result;
        }

        public ApiResult RemoveProcess(int id)
        {
            var result = Request<ApiResult>(
                string.Format("/api/Process/{0}", id), HttpMethod.DELETE);
            return result;
        }

        public IEnumerable<ProcessListDto> GetAllProcess()
        {
            var result = Request<ApiResult<ListResultDto<ProcessListDto>>>("/api/Process", HttpMethod.GET);
            return result.Result.Items;
        }

        public ProcessListDto GetProcess(int id)
        {
            var result = Request<ApiResult<ProcessListDto>>(
                string.Format("/api/Process/{0}", id), HttpMethod.GET);

            return result.Result;
        }

        public ApiResult AddCategory(AddProcessCategoryInput input)
        {
            var result = Request<ApiResult>("/api/Process/PostCategory", HttpMethod.POST, input);
            return result;
        }

        public ApiResult UpdateCategory(UpdateProcessCategoryInput input)
        {
            var result = Request<ApiResult>("/api/Process/PutCategory", HttpMethod.PUT, input);
            return result;
        }

        public ApiResult RemoveCategory(int id)
        {
            var result = Request<ApiResult>(
                string.Format("/api/Process/DeleteCategory?id={0}", id), HttpMethod.DELETE);
            return result;
        }

        public IEnumerable<ProcessCategoryListDto> GetAllCategory()
        {
            var result = Request<ApiResult<ListResultDto<ProcessCategoryListDto>>>(
                "/api/Process/GetCategory", HttpMethod.GET);

            return result.Result.Items;
        }

        public ProcessCategoryListDto GetCategory(int id)
        {
            var result = Request<ApiResult<ProcessCategoryListDto>>(
                string.Format("/api/Process/GetCategory?id={0}", id), HttpMethod.GET);

            return result.Result;
        }
    }
}
