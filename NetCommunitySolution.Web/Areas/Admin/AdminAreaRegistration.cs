using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }      

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Admin_default",
                url: "Admin/{controller}/{action}/{id}",
                defaults: new { action = "Index", Controller = "Home", id = UrlParameter.Optional },
                namespaces: new string[] { "NetCommunitySolution.Web.Areas.Admin.Controllers" });
        }
    }
}