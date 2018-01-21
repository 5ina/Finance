using Abp.Web.Mvc.Views;

namespace NetCommunitySolution.Web.Views
{
    public abstract class NetCommunitySolutionWebViewPageBase : NetCommunitySolutionWebViewPageBase<dynamic>
    {

    }

    public abstract class NetCommunitySolutionWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected NetCommunitySolutionWebViewPageBase()
        {
            LocalizationSourceName = NetCommunitySolutionConsts.LocalizationSourceName;
        }
    }
}