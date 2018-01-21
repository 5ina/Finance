using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Controllers.Results;
using NetCommunitySolution.Web.Framework.Security;
using System.Text;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Controllers
{

    [RouteArea("Admin")]
    [AdminAuthorize]
    public abstract class AdminBaseController : AbpController
    {
        public int CustomerId
        {
            get
            {
                return System.Convert.ToInt32(AbpSession.UserId);
            }
        }

        protected override AbpJsonResult AbpJson(object data, string contentType = null,
            Encoding contentEncoding = null, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet,
            bool wrapResult = true, bool camelCase = false, bool indented = false)
        {
            return new AbpJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = int.MaxValue,
                CamelCase = camelCase,
                Indented = indented,
            };
        }
    }
}