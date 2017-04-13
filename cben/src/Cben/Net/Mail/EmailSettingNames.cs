namespace Cben.Net.Mail
{
    /// <summary>
    /// Declares names of the settings defined by <see cref="EmailSettingProvider"/>.
    /// </summary>
    public static class EmailSettingNames
    {
        /// <summary>
        /// Cben.Net.Mail.DefaultFromAddress
        /// </summary>
        public const string DefaultFromAddress = "Cben.Net.Mail.DefaultFromAddress";

        /// <summary>
        /// Cben.Net.Mail.DefaultFromDisplayName
        /// </summary>
        public const string DefaultFromDisplayName = "Cben.Net.Mail.DefaultFromDisplayName";

        /// <summary>
        /// SMTP related email settings.
        /// </summary>
        public static class Smtp
        {
            /// <summary>
            /// Cben.Net.Mail.Smtp.Host
            /// </summary>
            public const string Host = "Cben.Net.Mail.Smtp.Host";

            /// <summary>
            /// Cben.Net.Mail.Smtp.Port
            /// </summary>
            public const string Port = "Cben.Net.Mail.Smtp.Port";

            /// <summary>
            /// Cben.Net.Mail.Smtp.UserName
            /// </summary>
            public const string UserName = "Cben.Net.Mail.Smtp.UserName";

            /// <summary>
            /// Cben.Net.Mail.Smtp.Password
            /// </summary>
            public const string Password = "Cben.Net.Mail.Smtp.Password";

            /// <summary>
            /// Cben.Net.Mail.Smtp.Domain
            /// </summary>
            public const string Domain = "Cben.Net.Mail.Smtp.Domain";

            /// <summary>
            /// Cben.Net.Mail.Smtp.EnableSsl
            /// </summary>
            public const string EnableSsl = "Cben.Net.Mail.Smtp.EnableSsl";

            /// <summary>
            /// Cben.Net.Mail.Smtp.UseDefaultCredentials
            /// </summary>
            public const string UseDefaultCredentials = "Cben.Net.Mail.Smtp.UseDefaultCredentials";
        }
    }
}