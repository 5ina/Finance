using NetCommunitySolution.Routes;
using NetCommunitySolution.Web.Framework.Localization;
using NetCommunitySolution.Web.Framework.Seo;
using System.Web.Routing;

namespace NetCommunitySolution.Web.App_Start
{
    public class GenericUrlRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //generic URLs
            routes.MapGenericPathRoute("GenericUrl",
                                       "{generic_se_name}",
                                       new { controller = "Common", action = "GenericUrl" },
                            new[] { "NetCommunitySolution.Web.Controllers" });



            routes.MapLocalizedRoute("Topic",
                            "{SeName}",
                            new { controller = "Topic", action = "TopicDetails" },
                            new[] { "NetCommunitySolution.Web.Controllers" });            

        }
        public int Priority
        {
            get
            {
                return -1000000;
            }
        }
    }
}