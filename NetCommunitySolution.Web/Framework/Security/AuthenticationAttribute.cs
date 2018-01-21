using Abp.Runtime.Session;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Customers;
using System;
using System.Web.Mvc;


namespace NetCommunitySolution.Web.Framework.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (filterContext.IsChildAction)
                return;

            var abpSession = Abp.Dependency.IocManager.Instance.Resolve<IAbpSession>();
            var customerService = Abp.Dependency.IocManager.Instance.Resolve<ICustomerService>();
            var customer = customerService.GetCustomerId(Convert.ToInt32(abpSession.UserId));
            var auth = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.Authentication);
                        

            base.OnActionExecuting(filterContext);
        }
    }
}