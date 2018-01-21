using Abp.Runtime.Session;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Framework.Security
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AdminAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }

        private IEnumerable<AdminAuthorizeAttribute> GetAdminAuthorizeAttributes(ActionDescriptor descriptor)
        {
            return descriptor.GetCustomAttributes(typeof(AdminAuthorizeAttribute), true)
                .Concat(descriptor.ControllerDescriptor.GetCustomAttributes(typeof(AdminAuthorizeAttribute), true))
                .OfType<AdminAuthorizeAttribute>();
        }

        private bool IsAdminPageRequested(AuthorizationContext filterContext)
        {
            var adminAttributes = GetAdminAuthorizeAttributes(filterContext.ActionDescriptor);
            if (adminAttributes != null && adminAttributes.Any())
                return true;
            return false;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");
            
            if (IsAdminPageRequested(filterContext))
            {
                if (!this.HasAdminAccess(filterContext))
                    this.HandleUnauthorizedRequest(filterContext);
            }
        }

        public virtual bool HasAdminAccess(AuthorizationContext filterContext)
        {
            var abpSession= Abp.Dependency.IocManager.Instance.Resolve<IAbpSession>();
            var customerService = Abp.Dependency.IocManager.Instance.Resolve<ICustomerService>();
            if (abpSession.UserId.HasValue)
            {
                if (abpSession.UserId > 0)
                {
                    var customer = customerService.GetCustomerId(Convert.ToInt32(abpSession.UserId));
                    if (customer.CustomerRoleId == (int)CustomerRole.System)
                        return true;
                }
            }
            return false;
        }
    }
}