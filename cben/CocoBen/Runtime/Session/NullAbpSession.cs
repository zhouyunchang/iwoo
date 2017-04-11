using Cben.MultiTenancy;

namespace Cben.Runtime.Session
{
    /// <summary>
    /// Implements null object pattern for <see cref="ICbenSession"/>.
    /// </summary>
    public class NullCbenSession : ICbenSession
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullCbenSession Instance { get { return SingletonInstance; } }
        private static readonly NullCbenSession SingletonInstance = new NullCbenSession();

        /// <inheritdoc/>
        public long? UserId { get { return null; } }

        /// <inheritdoc/>
        public int? TenantId { get { return null; } }

        public MultiTenancySides MultiTenancySide { get { return MultiTenancySides.Tenant; } }
        
        public long? ImpersonatorUserId { get { return null; } }
        
        public int? ImpersonatorTenantId { get { return null; } }

        private NullCbenSession()
        {

        }
    }
}