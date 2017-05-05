namespace Erp.Migrations
{
    using Cben.Core.Migrations.SeedData;
    using Cben.MultiTenancy;
    using Cben.Zero.EntityFramework;
    using Erp.Migrations.SeedData;
    using global::EntityFramework.DynamicFilters;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Erp.EntityFramework.ErpDbContext>,
        IMultiTenantSeed
    {
        public CbenTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Cben_Erp";
        }

        protected override void Seed(Erp.EntityFramework.ErpDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();

                new DefaultProcessCreator(context).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
