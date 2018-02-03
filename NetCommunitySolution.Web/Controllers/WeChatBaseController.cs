using Abp.Web.Mvc.Controllers.Results;
using NetCommunitySolution.Web.Framework.Controllers;
using NetCommunitySolution.Web.Framework.WeChat;
using System.Text;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    [RestrictedAccess]
    [WeChatAntiForgery]
    public class WeChatBaseController : NetCommunitySolutionControllerBase
    {
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