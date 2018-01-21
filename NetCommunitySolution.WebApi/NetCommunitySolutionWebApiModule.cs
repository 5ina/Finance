using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;

namespace NetCommunitySolution
{
    [DependsOn(typeof(AbpWebApiModule), typeof(NetCommunitySolutionApplicationModule))]
    public class NetCommunitySolutionWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(NetCommunitySolutionApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
