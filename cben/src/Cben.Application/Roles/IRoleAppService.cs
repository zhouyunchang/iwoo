using System.Threading.Tasks;
using Cben.Application.Services;
using Cben.Application.Roles.Dto;

namespace Cben.Application.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
