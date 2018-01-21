using Abp.Application.Services;

namespace NetCommunitySolution
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class NetCommunitySolutionAppServiceBase : ApplicationService
    {
        protected NetCommunitySolutionAppServiceBase()
        {
            LocalizationSourceName = NetCommunitySolutionConsts.LocalizationSourceName;
        }
    }
}