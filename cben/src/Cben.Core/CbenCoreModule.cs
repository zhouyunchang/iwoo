
using System.Reflection;

using Cben.Core.Authorization.Roles;
using Cben.Core.MultiTenancy;
using Cben.Core.Users;
using Cben.Localization.Dictionaries;
using Cben.Localization.Dictionaries.Xml;
using Cben.Modules;
using Cben.Zero;
using Cben.Zero.Configuration;
using Cben.Core.Authorization;

namespace Cben.Core
{
    [DependsOn(typeof(CbenZeroCoreModule))]
    public class CbenCoreModule : CbenModule
    {

        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            Configuration.MultiTenancy.IsEnabled = true;

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    CbenCoreConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Cben.Localization.Source"
                        )
                    )
                );

            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Authorization.Providers.Add<CbenAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

    }
}
