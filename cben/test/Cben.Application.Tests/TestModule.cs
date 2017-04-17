using Cben.Application;
using Cben.Core;
using Cben.Modules;
using Cben.Runtime.Session;
using Cben.Zero.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cben.Configuration.Startup;
using Cben.Dependency;

namespace Cben.Application.Tests
{

    [DependsOn(
        typeof(CbenCoreDbModule),
        typeof(CbenApplicationModule))]
    public class TestModule : CbenModule
    {

        public override void PreInitialize()
        {
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.ReplaceService<IPrincipalAccessor, TestPrincipalAccessor>(DependencyLifeStyle.Transient);

            //Configure navigation/menu
            //Configuration.Navigation.Providers.Add<CbenNavigationProvider>();

            //Configure Hangfire - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
            //Configuration.BackgroundJobs.UseHangfire(configuration =>
            //{
            //    configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            //});
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

        }


    }
}
