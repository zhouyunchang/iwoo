using Cben.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Core.Migrations.SeedData
{
    public class DefaultClientCreator
    {
        private readonly CbenCoreDbContext _context;

        public const string DefaultClientIdentifier = "CbenWeb";

        public const string DefaultClientSecret = "CbenWeb";

        public DefaultClientCreator(CbenCoreDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateClient();
        }

        private void CreateClient()
        {
            var defaultClient = _context.Clients.FirstOrDefault(e => e.ClientIdentifier == DefaultClientIdentifier);
            if (defaultClient == null)
            {
                defaultClient = new Zero.OAuth2.Client
                {
                    ClientIdentifier = DefaultClientIdentifier,
                    Name = DefaultClientIdentifier,
                    ClientSecret = DefaultClientSecret,
                    ClientType = 1,
                    Callback = ""
                };

                _context.Clients.Add(defaultClient);
                _context.SaveChanges();
            }
        }

    }
}
