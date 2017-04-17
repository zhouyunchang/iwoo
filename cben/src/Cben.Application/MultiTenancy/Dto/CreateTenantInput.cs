using System.ComponentModel.DataAnnotations;
using Cben.Authorization.Users;
using Cben.AutoMapper;
using Cben.MultiTenancy;
using Cben.Core.MultiTenancy;

namespace Cben.Application.MultiTenancy.Dto
{
    [AutoMapTo(typeof(Tenant))]
    public class CreateTenantInput
    {
        [Required]
        [StringLength(CbenTenantBase.MaxTenancyNameLength)]
        [RegularExpression(Tenant.TenancyNameRegex)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(CbenUserBase.MaxEmailAddressLength)]
        public string AdminEmailAddress { get; set; }

        [MaxLength(CbenTenantBase.MaxConnectionStringLength)]
        public string ConnectionString { get; set; }
    }
}