using Cben.Core.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Cben.Core.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly CbenCoreDbContext _context;

        public InitialHostDbBuilder(CbenCoreDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
