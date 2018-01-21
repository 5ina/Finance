using System.Reflection;
using Abp.Modules;

namespace NetCommunitySolution
{
    public class NetCommunitySolutionCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
