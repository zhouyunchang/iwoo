using System.Security.Claims;
using System.Web;
using Cben.Runtime.Session;

namespace Cben.Web.Session
{
    public class HttpContextPrincipalAccessor : DefaultPrincipalAccessor
    {
        public override ClaimsPrincipal Principal => HttpContext.Current?.User as ClaimsPrincipal ?? base.Principal;
    }
}
