using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Erp.Api
{
    public class ValuesApi : ApiBase
    {

        public IEnumerable<string> Get()
        {
            var result = Request<ApiResult<List<string>>>("/api/values", HttpMethod.GET);
            return result.Result;
        }

    }
}
