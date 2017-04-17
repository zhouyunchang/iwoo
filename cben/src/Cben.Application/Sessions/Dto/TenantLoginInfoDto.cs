using Cben.Application.Services.Dto;
using Cben.AutoMapper;
using Cben.Core.MultiTenancy;

namespace Cben.Application.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}