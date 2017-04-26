using Cben.Zero.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cben.EntityFramework;

namespace Cben.Core.EntityFramework.Repositories
{
    public class ClientRepository : CbenCoreRepositoryBase<Client, int>, IClientRepository
    {
        public ClientRepository(IDbContextProvider<CbenCoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }


    }
}
