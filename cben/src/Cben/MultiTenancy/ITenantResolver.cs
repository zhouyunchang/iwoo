namespace Cben.MultiTenancy
{
    public interface ITenantResolver
    {
        int? ResolveTenantId();
    }
}