using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Erp.Api
{
    public class ApiResult
    {
        public string TargetUrl { get; set; }

        public bool Success { get; set; }

        public string Error { get; set; }

        public bool UnAuthorizedRequest { get; set; }
    }

    public class ApiResult<T> : ApiResult
    {
        public T Result { get; set; }

    }

    public class ListResultDto<T>
    {
        public List<T> Items { get; set; }
    }

}
