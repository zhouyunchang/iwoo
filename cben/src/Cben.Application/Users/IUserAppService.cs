using System.Threading.Tasks;
using Cben.Application.Services;
using Cben.Application.Services.Dto;
using Cben.Application.Users.Dto;

namespace Cben.Application.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);

        Task<ListResultDto<UserListDto>> GetUsers();

        Task CreateUser(CreateUserInput input);
    }
}