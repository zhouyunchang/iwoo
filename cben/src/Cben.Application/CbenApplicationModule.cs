using System.Reflection;
using Cben.AutoMapper;
using Cben.Modules;
using Cben.Core;

namespace Cben.Application
{
    [DependsOn(typeof(CbenCoreModule), typeof(CbenAutoMapperModule))]
    public class CbenApplicationModule : CbenModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.CbenAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<,>()
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
