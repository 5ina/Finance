using Abp.Application.Services;
using System.Web.Routing;

namespace NetCommunitySolution.Routes
{
    public interface IRouteProvider: IApplicationService
    {
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routes"></param>
        void RegisterRoutes(RouteCollection routes);

        int Priority { get; }
    }
}
