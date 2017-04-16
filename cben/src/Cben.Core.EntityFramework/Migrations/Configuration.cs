namespace Cben.Core.Migrations
{
    using Cben.Core.Migrations.SeedData;
    using Cben.MultiTenancy;
    using Cben.Zero.EntityFramework;
    using global::EntityFramework.DynamicFilters;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration :
        DbMigrationsConfiguration<Cben.Core.EntityFramework.CbenCoreDbContext>,
        IMultiTenantSeed
    {

        public CbenTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Cben";
        }

        protected override void Seed(Cben.Core.EntityFramework.CbenCoreDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
