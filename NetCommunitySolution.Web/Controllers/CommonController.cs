using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    public class CommonController : WeChatBaseController
    {
        /// <summary>
        /// 限制访问
        /// </summary>
        /// <returns></returns>
        public ActionResult Restricted()
        {
            return View();
        }
    }
}