using System;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Framework.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class MemberLoginAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool _ignore;
        public MemberLoginAttribute(bool ignore = false)
        {
            this._ignore = ignore;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {

            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (_ignore)
                return;

            //don't apply filter to child methods
            if (filterContext.IsChildAction)
                return;
            //only POST requests
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
                return;

            var abpSession = Abp.Dependency.IocManager.Instance.Resolve<Abp.Runtime.Session.IAbpSession>();

            if (!abpSession.UserId.HasValue)
            {
                filterContext.Result = new RedirectResult("/Customer/NotLogged");
            }
        }
    }
}