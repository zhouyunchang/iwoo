using System.Security.Claims;
using System.Web;
using Cben.Runtime.Session;

namespace Cben.WebApi.Session
{
    public class HttpContextPrincipalAccessor : DefaultPrincipalAccessor
    {
        public override ClaimsPrincipal Principal => HttpContext.Current?.User as ClaimsPrincipal ?? base.Principal;
    }
}
