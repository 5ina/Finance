using Abp.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        #region ctor && Fields
        private readonly ICacheManager _cacheManager;

        public HomeController(ICacheManager cacheManager)
        {
            this._cacheManager = cacheManager;
        }
        #endregion


        #region Method
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClearCache()
        {
            _cacheManager.ClearCache();
            return Index();
        }
        #endregion
    }
}