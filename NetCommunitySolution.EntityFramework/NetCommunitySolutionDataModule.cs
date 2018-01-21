using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using NetCommunitySolution.EntityFramework;

namespace NetCommunitySolution
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(NetCommunitySolutionCoreModule))]
    public class NetCommunitySolutionDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<NetCommunitySolutionDbContext>(null);
        }
    }
}
