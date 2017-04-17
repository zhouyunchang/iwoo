using Cben.Dependency;
using Cben.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Cben.Runtime.Security;

namespace Cben.Application.Tests
{
    public class TestPrincipalAccessor : IPrincipalAccessor, ISingletonDependency
    {
        public virtual ClaimsPrincipal Principal
        {
            get
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, "admin"));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, "2"));
                claims.Add(new Claim(CbenClaimTypes.TenantId, "1"));
                claims.Add(new Claim(ClaimTypes.Email, "admin@cocoben.com"));
                var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                return new ClaimsPrincipal(id);
            }
        }
    }
}
