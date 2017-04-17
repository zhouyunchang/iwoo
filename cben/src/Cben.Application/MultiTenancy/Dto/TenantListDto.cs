using Cben.Application.Services.Dto;
using Cben.AutoMapper;
using Cben.Core.MultiTenancy;

namespace Cben.Application.MultiTenancy.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantListDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}