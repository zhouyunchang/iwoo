using System.Threading.Tasks;
using Cben.Auditing;
using Cben.Authorization;
using Cben.AutoMapper;
using Cben.Application.Sessions.Dto;

namespace Cben.Application.Sessions
{
    [CbenAuthorize]
    public class SessionAppService : CbenAppServiceBase, ISessionAppService
    {
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                User = (await GetCurrentUserAsync()).MapTo<UserLoginInfoDto>()
            };

            if (CbenSession.TenantId.HasValue)
            {
                output.Tenant = (await GetCurrentTenantAsync()).MapTo<TenantLoginInfoDto>();
            }

            return output;
        }
    }
}