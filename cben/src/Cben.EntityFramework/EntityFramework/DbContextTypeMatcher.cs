using Cben.Domain.Uow;

namespace Cben.EntityFramework
{
    public class DbContextTypeMatcher : DbContextTypeMatcher<CbenDbContext>
    {
        public DbContextTypeMatcher(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) 
            : base(currentUnitOfWorkProvider)
        {
        }
    }
}