using Cben.Localization;
using Cben.Modules;
using System.Reflection;
using Cben.Configuration.Startup;
using Cben.Reflection;
using AutoMapper;
using Castle.MicroKernel.Registration;

namespace Cben.AutoMapper
{
    [DependsOn(typeof(CbenKernelModule))]
    public class CbenAutoMapperModule : CbenModule
    {
        private readonly ITypeFinder _typeFinder;

        private static bool _createdMappingsBefore;

        private static readonly object SyncObj = new object();

        public CbenAutoMapperModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public override void PreInitialize()
        {
            IocManager.Register<ICbenAutoMapperConfiguration, CbenAutoMapperConfiguration>();

            Configuration.ReplaceService<ObjectMapping.IObjectMapper, AutoMapperObjectMapper>();

            Configuration.Modules.CbenAutoMapper().Configurators.Add(CreateCoreMappings);
        }

        public override void PostInitialize()
        {
            CreateMappings();

            IocManager.IocContainer.Register(
                Component.For<IMapper>().Instance(Mapper.Instance).LifestyleSingleton()
            );
        }

        public void CreateMappings()
        {
            lock (SyncObj)
            {
                //We should prevent duplicate mapping in an application, since Mapper is static.
                if (_createdMappingsBefore)
                {
                    return;
                }

                Mapper.Initialize(configuration =>
                {
                    FindAndAutoMapTypes(configuration);
                    foreach (var configurator in Configuration.Modules.CbenAutoMapper().Configurators)
                    {
                        configurator(configuration);
                    }
                });

                _createdMappingsBefore = true;
            }
        }

        private void FindAndAutoMapTypes(IMapperConfigurationExpression configuration)
        {
            var types = _typeFinder.Find(type =>
                    type.IsDefined(typeof(AutoMapAttribute)) ||
                    type.IsDefined(typeof(AutoMapFromAttribute)) ||
                    type.IsDefined(typeof(AutoMapToAttribute))
            );

            Logger.DebugFormat("Found {0} classes define auto mapping attributes", types.Length);

            foreach (var type in types)
            {
                Logger.Debug(type.FullName);
                configuration.CreateAutoAttributeMaps(type);
            }
        }

        private void CreateCoreMappings(IMapperConfigurationExpression configuration)
        {
            var localizationContext = IocManager.Resolve<ILocalizationContext>();

            configuration.CreateMap<ILocalizableString, string>().ConvertUsing(ls => ls?.Localize(localizationContext));
            configuration.CreateMap<LocalizableString, string>().ConvertUsing(ls => ls == null ? null : localizationContext.LocalizationManager.GetString(ls));
        }
    }
}
