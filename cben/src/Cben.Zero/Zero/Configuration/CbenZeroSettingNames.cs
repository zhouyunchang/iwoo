namespace Cben.Zero.Configuration
{
    public static class CbenZeroSettingNames
    {
        public static class UserManagement
        {
            /// <summary>
            /// "Cben.Zero.UserManagement.IsEmailConfirmationRequiredForLogin".
            /// </summary>
            public const string IsEmailConfirmationRequiredForLogin = "Cben.Zero.UserManagement.IsEmailConfirmationRequiredForLogin";

            public static class UserLockOut
            {
                /// <summary>
                /// "Cben.Zero.UserManagement.UserLockOut.IsEnabled".
                /// </summary>
                public const string IsEnabled = "Cben.Zero.UserManagement.UserLockOut.IsEnabled";

                /// <summary>
                /// "Cben.Zero.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout".
                /// </summary>
                public const string MaxFailedAccessAttemptsBeforeLockout = "Cben.Zero.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout";

                /// <summary>
                /// "Cben.Zero.UserManagement.UserLockOut.DefaultAccountLockoutSeconds".
                /// </summary>
                public const string DefaultAccountLockoutSeconds = "Cben.Zero.UserManagement.UserLockOut.DefaultAccountLockoutSeconds";
            }

            public static class TwoFactorLogin
            {
                /// <summary>
                /// "Cben.Zero.UserManagement.TwoFactorLogin.IsEnabled".
                /// </summary>
                public const string IsEnabled = "Cben.Zero.UserManagement.TwoFactorLogin.IsEnabled";

                /// <summary>
                /// "Cben.Zero.UserManagement.TwoFactorLogin.IsEmailProviderEnabled".
                /// </summary>
                public const string IsEmailProviderEnabled = "Cben.Zero.UserManagement.TwoFactorLogin.IsEmailProviderEnabled";

                /// <summary>
                /// "Cben.Zero.UserManagement.TwoFactorLogin.IsSmsProviderEnabled".
                /// </summary>
                public const string IsSmsProviderEnabled = "Cben.Zero.UserManagement.TwoFactorLogin.IsSmsProviderEnabled";

                /// <summary>
                /// "Cben.Zero.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled".
                /// </summary>
                public const string IsRememberBrowserEnabled = "Cben.Zero.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled";
            }
        }

        public static class OrganizationUnits
        {
            /// <summary>
            /// "Cben.Zero.OrganizationUnits.MaxUserMembershipCount".
            /// </summary>
            public const string MaxUserMembershipCount = "Cben.Zero.OrganizationUnits.MaxUserMembershipCount";
        }
    }
}