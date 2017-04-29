using Cben.WebApi.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cben.WebApi.Controllers
{

    /// <summary>
    /// 工序接口
    /// </summary>
    [ApiAuthorize]
    public class ProcessController : ApiControllerBase
    {
    }
}
