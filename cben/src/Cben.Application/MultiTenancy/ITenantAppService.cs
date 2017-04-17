using System.Threading.Tasks;
using Cben.Application.Services;
using Cben.Application.Services.Dto;
using Cben.Application.MultiTenancy.Dto;

namespace Cben.Application.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultDto<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
