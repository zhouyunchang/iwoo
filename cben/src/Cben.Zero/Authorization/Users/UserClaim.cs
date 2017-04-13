using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using Cben.Domain.Entities;
using Cben.Domain.Entities.Auditing;

namespace Cben.Authorization.Users
{
    [Table("CbenUserClaims")]
    public class UserClaim : CreationAuditedEntity<long>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }

        public virtual long UserId { get; set; }

        public virtual string ClaimType { get; set; }

        public virtual string ClaimValue { get; set; }

        public UserClaim()
        {
            
        }

        public UserClaim(CbenUserBase user, Claim claim)
        {
            TenantId = user.TenantId;
            UserId = user.Id;
            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }
    }
}
