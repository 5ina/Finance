using Abp.Runtime.Caching;
using NetCommunitySolution.Common;
using NetCommunitySolution.Security;
using NetCommunitySolution.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Controllers
{
    public class SettingController : AdminBaseController
    {
        #region Fields && Ctor

        private readonly ISettingService _settingService;
        private readonly ICacheManager _cacheManager;
        private readonly IPaymentService _paymentService;

        public SettingController(ISettingService settingService, 
            ICacheManager cacheManager,
            IPaymentService paymentService)
        {
            this._settingService = settingService;
            this._cacheManager = cacheManager;
            this._paymentService = paymentService;
        }

        #endregion

        #region Utilities

        #endregion

        #region Method
        public ActionResult Common()
        {
            var model = _settingService.GetCommonSettings();
            return View(model);
        }

        [HttpPost]
        public ActionResult Common(Domain.Configuration.CommonSetting model)
        {
            _settingService.SaveCommonSettings(model);
            ViewBag.Result = "true";
            return View();
        }


        public ActionResult CustomerSetting()
        {
            var model = _settingService.GetCustomerSettings();
            return View(model);
        }

        [HttpPost]
        public ActionResult CustomerSetting(Domain.Configuration.CustomerSetting model)
        {
            _settingService.SaveCustomerSettings(model);
            ViewBag.Result = "true";
            return View();
        }


        public ActionResult PostSetting()
        {
            var model = _settingService.GetPostSettings();
            return View(model);
        }

        [HttpPost]
        public ActionResult PostSetting(Domain.Configuration.PostSetting model)
        {
            _settingService.SavePostSettings(model);
            ViewBag.Result = "true";
            return View();
        }


        public ActionResult MediaSetting()
        {
            var model = _settingService.GetMediaSettings();
            return View(model);
        }

        [HttpPost]
        public ActionResult MediaSetting(Domain.Configuration.MediaSetting model)
        {
            _settingService.SaveMediaSettings(model);
            ViewBag.Result = "true";
            return View();
        }


        public ActionResult RewardSetting()
        {
            var model = _settingService.GetRewardSettings();
            return View(model);
        }

        [HttpPost]
        public ActionResult RewardSetting(Domain.Configuration.RewardPointSetting model)
        {
            _settingService.SaveRewardSettings(model);
            ViewBag.Result = "true";
            return View();
        }



        public ActionResult WeChatSetting()
        {
            var model = _settingService.GetWeChatSettings();
            return View(model);
        }

        [HttpPost]
        public ActionResult WeChatSetting(Domain.Configuration.WechatSetting model)
        {
            _settingService.SaveWechatSettings(model);
            ViewBag.Result = "true";
            return View(model);
        }



        public ActionResult SeoSetting()
        {
            var model = _settingService.GetSeoSetting();
            return View(model);
        }

        [HttpPost]
        public ActionResult SeoSetting(Domain.Configuration.SeoSetting model)
        {
            _settingService.SaveSeoSetting(model);
            ViewBag.Result = "true";
            return View(model);
        }



        public ActionResult RateSetting()
        {
            var model = _settingService.GetAccountSettings();
            return View(model);
        }
        

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult RateSetting(Domain.Configuration.AccountSetting model, bool continueEditing)
        {
            _settingService.SaveAccountSettings(model);
            ViewBag.Result = "true";

            if (continueEditing)
            {
                var addUrl = "http://service.budianpay.com:8888/v1/merch/add";
                var result = _paymentService.PutOn(addUrl);
                
            }

            return View(model);
        }

        #endregion

    }
}