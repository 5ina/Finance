using NetCommunitySolution.Routes;
using NetCommunitySolution.Web.Framework.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace NetCommunitySolution.Web.App_Start
{
    /// <summary>
    /// 路由显示
    /// </summary>
    public class RouteProvider : NetCommunitySolutionAppServiceBase, IRouteProvider
    {

        public void RegisterRoutes(RouteCollection routes)
        {
            //home page
            routes.MapLocalizedRoute("HomePage",
                            "",
                            new { controller = "Home", action = "Index" },
                            new[] { "NetCommunitySolution.Web.Controllers" });

            //Customer Center
            routes.MapLocalizedRoute("Customer",
                            "customer",
                            new { controller = "Customer", action = "Index" },
                            new[] { "NetCommunitySolution.Web.Controllers" });

            //topics
            routes.MapLocalizedRoute("Restricted",
                            "noAccess",
                            new { controller = "Common", action = "Restricted" },
                            new[] { "NetCommunitySolution.Web.Controllers" });

            //topics
            routes.MapLocalizedRoute("TopicPopup",
                            "t-popup/{SystemName}",
                            new { controller = "Topic", action = "TopicDetailsPopup" },
                            new[] { "NetCommunitySolution.Web.Controllers" });


            //Account 佣金日志
            routes.MapLocalizedRoute("Account",
                            "account",
                            new { controller = "Account", action = "CustomerLog" },
                            new[] { "NetCommunitySolution.Web.Controllers" });


            //Payment
            routes.MapLocalizedRoute("Payment",
                            "payment",
                            new { controller = "Order", action = "YeePay" },
                            new[] { "NetCommunitySolution.Web.Controllers" });


            //Login
            routes.MapLocalizedRoute("Login",
                            "login",
                            new { controller = "Customer", action = "Login" },
                            new[] { "NetCommunitySolution.Web.Controllers" });
            //Logout
            routes.MapLocalizedRoute("Logout",
                            "logout",
                            new { controller = "Customer", action = "Logout" },
                            new[] { "NetCommunitySolution.Web.Controllers" });


            //Notify
            routes.MapLocalizedRoute("Notify",
                            "notify",
                            new { controller = "Notify", action = "Notify" },
                            new[] { "NetCommunitySolution.Web.Controllers" });

        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}