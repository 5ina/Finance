using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace NetCommunitySolution.Routes
{
    /// <summary>
    /// 路由发布接口
    /// </summary>
    public interface IRoutePublisher: IApplicationService
    {
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routes">Routes</param>
        void RegisterRoutes(RouteCollection routes);
    }
}
