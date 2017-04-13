using System;
using System.ComponentModel.DataAnnotations.Schema;
using Cben.Domain.Entities.Auditing;
using Cben.MultiTenancy;

namespace Cben.Authorization.Users
{
    /// <summary>
    /// Represents a summary user
    /// </summary>
    [Table("CbenUserAccounts")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class UserAccount : FullAuditedEntity<long>
    {
        public virtual int? TenantId { get; set; }

        public virtual long UserId { get; set; }

        public virtual long? UserLinkId { get; set; }

        public virtual string UserName { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual DateTime? LastLoginTime { get; set; }
    }
}