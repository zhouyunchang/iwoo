using Cben.MultiTenancy;

namespace Cben.Zero.EntityFramework
{
    public interface IMultiTenantSeed
    {
        CbenTenantBase Tenant { get; set; }
    }
}