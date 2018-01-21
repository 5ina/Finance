using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace NetCommunitySolution.Web.Framework.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class RestrictedAccessAttribute : ActionFilterAttribute
    {
                
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (filterContext.IsChildAction)
                return;


            string controllerName = filterContext.Controller.ToString();

            //判定是否公共控制器     
            var commonControllerName = string.Concat("NetCommunitySolution.Web.Controllers", ".", "CommonController");
            if (controllerName.Equals(commonControllerName, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            var actionName = string.Concat(filterContext.Controller.ToString(), ".", filterContext.ActionDescriptor.ActionName);
            //判定是否登录页面     
            var loginActionName = string.Concat("NetCommunitySolution.Web.Controllers", ".", "CustomerController.Login");
            if (actionName.Equals(loginActionName, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            var User_Agent = filterContext.RequestContext.HttpContext.Request.UserAgent;
            //判定是否微信打开
            if (!User_Agent.ToLower().Contains("micromessenger"))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Common",
                    action = "Restricted"
                }));
            }
            base.OnActionExecuting(filterContext);
        }
        
    }
}