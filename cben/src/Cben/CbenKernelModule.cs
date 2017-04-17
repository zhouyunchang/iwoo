using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using Cben.Application.Features;
using Cben.Application.Navigation;
using Cben.Application.Services;
using Cben.Auditing;
using Cben.Authorization;
using Cben.BackgroundJobs;
using Cben.Collections.Extensions;
using Cben.Configuration;
using Cben.Configuration.Startup;
using Cben.Dependency;
using Cben.Domain.Uow;
using Cben.Events.Bus;
using Cben.Localization;
using Cben.Localization.Dictionaries;
using Cben.Localization.Dictionaries.Xml;
using Cben.Modules;
using Cben.Net.Mail;
using Cben.Notifications;
using Cben.Runtime.Caching;
using Cben.Runtime.Validation.Interception;
using Cben.Threading;
using Cben.Threading.BackgroundWorkers;
using Cben.Timing;
using Castle.MicroKernel.Registration;
using Cben.Runtime;
using Cben.Runtime.Remoting;
using Cben.MultiTenancy;

namespace Cben
{
    /// <summary>
    /// Kernel (core) module of the Cben system.
    /// No need to depend on this, it's automatically the first module always.
    /// </summary>
    public sealed class CbenKernelModule : CbenModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());

            IocManager.Register<IScopedIocResolver, ScopedIocResolver>(DependencyLifeStyle.Transient);
            IocManager.Register(typeof(IAmbientScopeProvider<>), typeof(DataContextAmbientScopeProvider<>), DependencyLifeStyle.Transient);

            InitializeInterceptors();

            AddAuditingSelectors();
            AddLocalizationSources();
            AddSettingProviders();
            AddUnitOfWorkFilters();

            ConfigureCaches();
            AddIgnoredTypes();
        }

        public override void Initialize()
        {
            foreach (var replaceAction in ((CbenStartupConfiguration)Configuration).ServiceReplaceActions.Values)
            {
                replaceAction();
            }

            IocManager.IocContainer.Install(new EventBusInstaller(IocManager));

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly(),
                new ConventionalRegistrationConfig
                {
                    InstallInstallers = false
                });
        }

        public override void PostInitialize()
        {
            RegisterMissingComponents();

            IocManager.Resolve<SettingDefinitionManager>().Initialize();
            IocManager.Resolve<FeatureManager>().Initialize();
            IocManager.Resolve<PermissionManager>().Initialize();
            IocManager.Resolve<LocalizationManager>().Initialize();
            IocManager.Resolve<NotificationDefinitionManager>().Initialize();
            IocManager.Resolve<NavigationManager>().Initialize();

            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                var workerManager = IocManager.Resolve<IBackgroundWorkerManager>();
                workerManager.Start();
                workerManager.Add(IocManager.Resolve<IBackgroundJobManager>());
            }
        }

        public override void Shutdown()
        {
            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                IocManager.Resolve<IBackgroundWorkerManager>().StopAndWaitToStop();
            }
        }

        private void InitializeInterceptors()
        {
            ValidationInterceptorRegistrar.Initialize(IocManager);
            AuditingInterceptorRegistrar.Initialize(IocManager);
            UnitOfWorkRegistrar.Initialize(IocManager);
            AuthorizationInterceptorRegistrar.Initialize(IocManager);
        }

        private void AddUnitOfWorkFilters()
        {
            Configuration.UnitOfWork.RegisterFilter(CbenDataFilters.SoftDelete, true);
            Configuration.UnitOfWork.RegisterFilter(CbenDataFilters.MustHaveTenant, true);
            Configuration.UnitOfWork.RegisterFilter(CbenDataFilters.MayHaveTenant, true);
        }

        private void AddSettingProviders()
        {
            Configuration.Settings.Providers.Add<LocalizationSettingProvider>();
            Configuration.Settings.Providers.Add<EmailSettingProvider>();
            Configuration.Settings.Providers.Add<NotificationSettingProvider>();
            Configuration.Settings.Providers.Add<TimingSettingProvider>();
        }

        private void AddAuditingSelectors()
        {
            Configuration.Auditing.Selectors.Add(
                new NamedTypeSelector(
                    "Cben.ApplicationServices",
                    type => typeof(IApplicationService).IsAssignableFrom(type)
                )
            );
        }

        private void AddLocalizationSources()
        {
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    CbenConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(), "Cben.Localization.Sources.CbenXmlSource"
                    )));
        }

        private void ConfigureCaches()
        {
            Configuration.Caching.Configure(CbenCacheNames.ApplicationSettings, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(8);
            });

            Configuration.Caching.Configure(CbenCacheNames.TenantSettings, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(60);
            });

            Configuration.Caching.Configure(CbenCacheNames.UserSettings, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(20);
            });
        }

        private void AddIgnoredTypes()
        {
            var commonIgnoredTypes = new[] { typeof(Stream), typeof(Expression) };

            foreach (var ignoredType in commonIgnoredTypes)
            {
                Configuration.Auditing.IgnoredTypes.AddIfNotContains(ignoredType);
                Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
            }

            var validationIgnoredTypes = new[] { typeof(Type) };
            foreach (var ignoredType in validationIgnoredTypes)
            {
                Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
            }
        }

        private void RegisterMissingComponents()
        {
            if (!IocManager.IsRegistered<IGuidGenerator>())
            {
                IocManager.IocContainer.Register(
                    Component
                        .For<IGuidGenerator, SequentialGuidGenerator>()
                        .Instance(SequentialGuidGenerator.Instance)
                );
            }

            IocManager.RegisterIfNot<IUnitOfWork, NullUnitOfWork>(DependencyLifeStyle.Transient);
            IocManager.RegisterIfNot<IAuditingStore, SimpleLogAuditingStore>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IPermissionChecker, NullPermissionChecker>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IRealTimeNotifier, NullRealTimeNotifier>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<INotificationStore, NullNotificationStore>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IUnitOfWorkFilterExecuter, NullUnitOfWorkFilterExecuter>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IClientInfoProvider, NullClientInfoProvider>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<ITenantStore, NullTenantStore>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<ITenantResolverCache, NullTenantResolverCache>(DependencyLifeStyle.Singleton);

            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                IocManager.RegisterIfNot<IBackgroundJobStore, InMemoryBackgroundJobStore>(DependencyLifeStyle.Singleton);
            }
            else
            {
                IocManager.RegisterIfNot<IBackgroundJobStore, NullBackgroundJobStore>(DependencyLifeStyle.Singleton);
            }
        }
    }
}