using Cben.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;

namespace Cben.WebApi.Module
{
    public class HttpContextPrincipalAccessor : DefaultPrincipalAccessor
    {

        public override ClaimsPrincipal Principal => HttpContext.Current?.User as ClaimsPrincipal ?? base.Principal;

    }
}