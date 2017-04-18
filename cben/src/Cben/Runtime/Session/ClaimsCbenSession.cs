using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Cben.Configuration.Startup;
using Cben.Dependency;
using Cben.MultiTenancy;
using Cben.Runtime.Security;

namespace Cben.Runtime.Session
{
    /// <summary>
    /// Implements <see cref="ICbenSession"/> to get session properties from claims of <see cref="Thread.CurrentPrincipal"/>.
    /// </summary>
    public class ClaimsCbenSession : ICbenSession, ISingletonDependency
    {
        public virtual long? UserId
        {
            get
            {
                var userIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }

                long userId;
                if (!long.TryParse(userIdClaim.Value, out userId))
                {
                    return null;
                }

                return userId;
            }
        }

        public virtual int? TenantId
        {
            get
            {
                if (!MultiTenancy.IsEnabled)
                {
                    return MultiTenancyConsts.DefaultTenantId;
                }

                var tenantIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == CbenClaimTypes.TenantId);
                if (string.IsNullOrEmpty(tenantIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt32(tenantIdClaim.Value);
            }
        }

        public virtual long? ImpersonatorUserId
        {
            get
            {
                var impersonatorUserIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == CbenClaimTypes.ImpersonatorUserId);
                if (string.IsNullOrEmpty(impersonatorUserIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt64(impersonatorUserIdClaim.Value);
            }
        }

        public virtual int? ImpersonatorTenantId
        {
            get
            {
                if (!MultiTenancy.IsEnabled)
                {
                    return MultiTenancyConsts.DefaultTenantId;
                }

                var impersonatorTenantIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == CbenClaimTypes.ImpersonatorTenantId);
                if (string.IsNullOrEmpty(impersonatorTenantIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt32(impersonatorTenantIdClaim.Value);
            }
        }

        public virtual MultiTenancySides MultiTenancySide
        {
            get
            {
                return MultiTenancy.IsEnabled && !TenantId.HasValue
                    ? MultiTenancySides.Host
                    : MultiTenancySides.Tenant;
            }
        }

        protected readonly IPrincipalAccessor PrincipalAccessor;

        protected readonly IMultiTenancyConfig MultiTenancy;

        public ClaimsCbenSession(IMultiTenancyConfig multiTenancy, IPrincipalAccessor principalAccessor)
        {
            MultiTenancy = multiTenancy;
            PrincipalAccessor = principalAccessor;
            //PrincipalAccessor = DefaultPrincipalAccessor.Instance;
        }
    }
}