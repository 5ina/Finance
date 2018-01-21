using NetCommunitySolution.Routes;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace NetCommunitySolution.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var routePublisher = Abp.Dependency.IocManager.Instance.Resolve<IRoutePublisher>();
            routePublisher.RegisterRoutes(routes);

            //ASP.NET Web API Route Config
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "NetCommunitySolution.Web.Controllers" }
                );

            
        }
    }
}
