using Abp.Runtime.Session;
using Castle.Core.Logging;
using System;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Framework.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class MobileAttribute : FilterAttribute, System.Web.Mvc.IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var User_Agent = filterContext.RequestContext.HttpContext.Request.UserAgent;

            var Logger = Abp.Dependency.IocManager.Instance.Resolve<ILogger>();
            Logger.Debug("Mobile" + User_Agent);
            //判定是否微信打开
            if (User_Agent.ToLower().Contains("micromessenger"))
            {
                return;
            }

            if (filterContext.IsChildAction)
                return;
            string controllerName = filterContext.Controller.ToString();


            var actionName = string.Concat(filterContext.Controller.ToString(), ".", filterContext.ActionDescriptor.ActionName);
            //判定是否登录页面     
            var loginActionName = string.Concat("NetCommunitySolution.Web.Controllers", ".", "CustomerController.Login");
            if (actionName.Equals(loginActionName, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            var abpSession = Abp.Dependency.IocManager.Instance.Resolve<IAbpSession>();
            if (abpSession.UserId.HasValue)
            {
                return;
            }

            filterContext.Result = new RedirectResult("http://www.bb-girl.cn/customer/login");
        }

        
    }
}