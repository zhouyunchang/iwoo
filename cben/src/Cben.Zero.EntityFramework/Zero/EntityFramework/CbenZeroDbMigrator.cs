using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Transactions;
using Cben.Data;
using Cben.Dependency;
using Cben.Domain.Uow;
using Cben.Extensions;
using Cben.MultiTenancy;

namespace Cben.Zero.EntityFramework
{
    public abstract class CbenZeroDbMigrator<TDbContext, TConfiguration> : ICbenZeroDbMigrator, ITransientDependency
        where TDbContext : DbContext
        where TConfiguration : DbMigrationsConfiguration<TDbContext>, IMultiTenantSeed, new()
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDbPerTenantConnectionStringResolver _connectionStringResolver;
        private readonly IIocResolver _iocResolver;

        protected CbenZeroDbMigrator(
            IUnitOfWorkManager unitOfWorkManager, 
            IDbPerTenantConnectionStringResolver connectionStringResolver,
            IIocResolver iocResolver)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _connectionStringResolver = connectionStringResolver;
            _iocResolver = iocResolver;
        }

        public virtual void CreateOrMigrateForHost()
        {
            CreateOrMigrate(null);
        }

        public virtual void CreateOrMigrateForTenant(CbenTenantBase tenant)
        {
            if (tenant.ConnectionString.IsNullOrEmpty())
            {
                return;
            }

            CreateOrMigrate(tenant);
        }

        protected virtual void CreateOrMigrate(CbenTenantBase tenant)
        {
            var args = new DbPerTenantConnectionStringResolveArgs(
                tenant == null ? (int?) null : (int?) tenant.Id,
                tenant == null ? MultiTenancySides.Host : MultiTenancySides.Tenant
                );

            args["DbContextType"] = typeof (TDbContext);
            args["DbContextConcreteType"] = typeof(TDbContext);

            var nameOrConnectionString = ConnectionStringHelper.GetConnectionString(
                _connectionStringResolver.GetNameOrConnectionString(args)
            );

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                using (var dbContext = _iocResolver.ResolveAsDisposable<TDbContext>(new {nameOrConnectionString = nameOrConnectionString}))
                {
                    var dbInitializer = new MigrateDatabaseToLatestVersion<TDbContext, TConfiguration>(
                        true,
                        new TConfiguration
                        {
                            Tenant = tenant
                        });

                    dbInitializer.InitializeDatabase(dbContext.Object);

                    _unitOfWorkManager.Current.SaveChanges();
                    uow.Complete();
                }
            }
        }
    }
}
