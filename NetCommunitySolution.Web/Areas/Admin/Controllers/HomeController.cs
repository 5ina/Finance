using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}