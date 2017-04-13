using System.Linq;
using System.Reflection;
using Cben.Application.Features;
using Cben.Authorization.Roles;
using Cben.Authorization.Users;
using Cben.Dependency;
using Cben.Localization;
using Cben.Localization.Dictionaries;
using Cben.Localization.Dictionaries.Xml;
using Cben.Modules;
using Cben.MultiTenancy;
using Cben.Reflection;
using Cben.Zero.Configuration;
using Cben.Configuration.Startup;
using Castle.MicroKernel.Registration;

namespace Cben.Zero
{
    /// <summary>
    /// Cben zero core module.
    /// </summary>
    [DependsOn(typeof(CbenKernelModule))]
    public class CbenZeroCoreModule : CbenModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IRoleManagementConfig, RoleManagementConfig>();
            IocManager.Register<IUserManagementConfig, UserManagementConfig>();
            IocManager.Register<ILanguageManagementConfig, LanguageManagementConfig>();
            IocManager.Register<ICbenZeroEntityTypes, CbenZeroEntityTypes>();
            IocManager.Register<ICbenZeroConfig, CbenZeroConfig>();

            Configuration.ReplaceService<ITenantStore, TenantStore>(DependencyLifeStyle.Transient);

            Configuration.Settings.Providers.Add<CbenZeroSettingProvider>();

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    CbenZeroConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(), "Cben.Zero.Localization.Source"
                        )));

            IocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        public override void Initialize()
        {
            FillMissingEntityTypes();

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.Register<IMultiTenantLocalizationDictionary, MultiTenantLocalizationDictionary>(DependencyLifeStyle.Transient); //could not register conventionally

            RegisterTenantCache();
        }

        private void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {
            if (typeof(ICbenZeroFeatureValueStore).IsAssignableFrom(handler.ComponentModel.Implementation) && !IocManager.IsRegistered<ICbenZeroFeatureValueStore>())
            {
                IocManager.IocContainer.Register(
                    Component.For<ICbenZeroFeatureValueStore>().ImplementedBy(handler.ComponentModel.Implementation).Named("CbenZeroFeatureValueStore").LifestyleTransient()
                    );
            }
        }

        private void FillMissingEntityTypes()
        {
            using (var entityTypes = IocManager.ResolveAsDisposable<ICbenZeroEntityTypes>())
            {
                if (entityTypes.Object.User != null &&
                    entityTypes.Object.Role != null &&
                    entityTypes.Object.Tenant != null)
                {
                    return;
                }

                using (var typeFinder = IocManager.ResolveAsDisposable<ITypeFinder>())
                {
                    var types = typeFinder.Object.FindAll();
                    entityTypes.Object.Tenant = types.FirstOrDefault(t => typeof(CbenTenantBase).IsAssignableFrom(t) && !t.IsAbstract);
                    entityTypes.Object.Role = types.FirstOrDefault(t => typeof(CbenRoleBase).IsAssignableFrom(t) && !t.IsAbstract);
                    entityTypes.Object.User = types.FirstOrDefault(t => typeof(CbenUserBase).IsAssignableFrom(t) && !t.IsAbstract);
                }
            }
        }

        private void RegisterTenantCache()
        {
            if (IocManager.IsRegistered<ITenantCache>())
            {
                return;
            }

            using (var entityTypes = IocManager.ResolveAsDisposable<ICbenZeroEntityTypes>())
            {
                var implType = typeof (TenantCache<,>)
                    .MakeGenericType(entityTypes.Object.Tenant, entityTypes.Object.User);

                IocManager.Register(typeof (ITenantCache), implType, DependencyLifeStyle.Transient);
            }
        }
    }
}
