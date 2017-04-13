namespace Cben.MultiTenancy
{
    public interface ICbenZeroDbMigrator
    {
        void CreateOrMigrateForHost();

        void CreateOrMigrateForTenant(CbenTenantBase tenant);
    }
}
