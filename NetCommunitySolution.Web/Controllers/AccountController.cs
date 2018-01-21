using Abp.AutoMapper;
using Abp.Runtime.Caching;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Web.Models.Customers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    public class AccountController : WeChatBaseController
    {

        #region ctor && Fields

        private const string PRODUCTREVIEW_BY_CUSTOMERID = "strore.product.review.by.customerid-{0}";
        private const string FAVORITE_BY_CUSTOMERID = "strore.favorite.by.customerid-{0}";


        private readonly ICacheManager _cacheManager;
        private readonly ICustomerService _customerService;
        private readonly IAccountLogService _logService;
        public AccountController(ICacheManager cacheManager,
                                ICustomerService customerService,
                                IAccountLogService logService)
        {
            this._cacheManager = cacheManager;
            this._customerService = customerService;
            this._logService = logService;
        }
        #endregion


        #region Method

        /// <summary>
        /// 我的佣金列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerLog()
        {
            var accountLogs = _logService.GetAccountByCustomerId(this.CustomerId, (Int16)AccountMode.Income);
            var model = accountLogs.MapTo<List<AccountLogModel>>();
            return View(model);
        }

        public ActionResult AllLogs()
        {
            var accountLogs = _logService.GetAccountByCustomerId(this.CustomerId);
            var model = accountLogs.MapTo<List<AccountLogModel>>();
            return View(model);
        }
        #endregion

    }
}