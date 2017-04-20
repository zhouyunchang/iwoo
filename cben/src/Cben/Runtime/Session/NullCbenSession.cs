using System;
using Cben.Configuration.Startup;
using Cben.MultiTenancy;
using Cben.Runtime.Remoting;

namespace Cben.Runtime.Session
{
    /// <summary>
    /// Implements null object pattern for <see cref="ICbenSession"/>.
    /// </summary>
    public class NullCbenSession : CbenSessionBase
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullCbenSession Instance { get; } = new NullCbenSession();

        /// <inheritdoc/>
        public override long? UserId => null;

        /// <inheritdoc/>
        public override int? TenantId => null;

        public override MultiTenancySides MultiTenancySide => MultiTenancySides.Tenant;

        public override long? ImpersonatorUserId => null;

        public override int? ImpersonatorTenantId => null;

        private NullCbenSession() 
            : base(new MultiTenancyConfig(), new DataContextAmbientScopeProvider<SessionOverride>(new CallContextAmbientDataContext()))
        {

        }
    }
}