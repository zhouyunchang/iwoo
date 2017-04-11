namespace Cben.Runtime.Security
{
    /// <summary>
    /// Used to get Cben-specific claim type names.
    /// </summary>
    public static class CbenClaimTypes
    {
        /// <summary>
        /// TenantId.
        /// </summary>
        public const string TenantId = "http://www.cocoben.com/identity/claims/tenantId";

        /// <summary>
        /// ImpersonatorUserId.
        /// </summary>
        public const string ImpersonatorUserId = "http://www.cocoben.com/identity/claims/impersonatorUserId";
        
        /// <summary>
        /// ImpersonatorTenantId
        /// </summary>
        public const string ImpersonatorTenantId = "http://www.cocoben.com/identity/claims/impersonatorTenantId";
    }
}
