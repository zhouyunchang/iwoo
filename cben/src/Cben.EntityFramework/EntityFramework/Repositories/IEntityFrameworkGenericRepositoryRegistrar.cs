using System;
using Cben.Dependency;

namespace Cben.EntityFramework.Repositories
{
    public interface IEntityFrameworkGenericRepositoryRegistrar
    {
        void RegisterForDbContext(Type dbContextType, IIocManager iocManager);
    }
}