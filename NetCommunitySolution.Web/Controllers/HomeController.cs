using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    public class HomeController : WeChatBaseController
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}