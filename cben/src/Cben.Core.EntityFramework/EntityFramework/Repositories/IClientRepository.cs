using Cben.Domain.Repositories;
using Cben.Zero.OAuth2;

namespace Cben.Core.EntityFramework.Repositories
{
    internal interface IClientRepository : IRepository<Client, int>
    {
    }
}