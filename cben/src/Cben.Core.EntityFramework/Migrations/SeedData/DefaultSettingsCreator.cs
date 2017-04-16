using System.Linq;
using Cben.Configuration;
using Cben.Localization;
using Cben.Net.Mail;
using Cben.Core.EntityFramework;

namespace Cben.Core.Migrations.SeedData
{
    public class DefaultSettingsCreator
    {
        private readonly CbenCoreDbContext _context;

        public DefaultSettingsCreator(CbenCoreDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //Emailing
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "admin@cocoben.com");
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "cocoben.com mailer");

            //Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "en");
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}