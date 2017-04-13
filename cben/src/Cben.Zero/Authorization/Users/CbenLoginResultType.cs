namespace Cben.Authorization.Users
{
    public enum CbenLoginResultType : byte
    {
        Success = 1,

        InvalidUserNameOrEmailAddress,
        
        InvalidPassword,
        
        UserIsNotActive,

        InvalidTenancyName,
        
        TenantIsNotActive,

        UserEmailIsNotConfirmed,

        UnknownExternalLogin,

        LockedOut
    }
}