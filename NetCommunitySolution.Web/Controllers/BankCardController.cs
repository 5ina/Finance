using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using NetCommunitySolution.BankCards;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.BankCards;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Messages;
using NetCommunitySolution.Web.Framework.Htmls;
using NetCommunitySolution.Web.Models.BankCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    public class BankCardController : WeChatBaseController
    {
        #region ctor && Fields

        private const string PRODUCTREVIEW_BY_CUSTOMERID = "strore.product.review.by.customerid-{0}";
        private const string FAVORITE_BY_CUSTOMERID = "strore.favorite.by.customerid-{0}";
        

        private readonly ICacheManager _cacheManager;
        private readonly ICustomerService _customerService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ISMSMessageService _messageService;
        public BankCardController(
                                ICacheManager cacheManager,
                                ICustomerService customerService,
                                ISMSMessageService messageService,
                                IUnitOfWorkManager unitOfWorkManager)
        {
            this._cacheManager = cacheManager;
            this._customerService = customerService;
            this._messageService = messageService;
            this._unitOfWorkManager = unitOfWorkManager;
        }
        #endregion


        #region Utilities
        [NonAction]
        protected void PrepareBankCardModel(BankCardModel model)
        {
            if (model == null)
                throw new Exception("model is null");
            var customer = _customerService.GetCustomerId(this.CustomerId);
            model.Name = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.RealName);
            model.Mobile = customer.Mobile;

            if (model.BankCardMode == BankCardMode.Credit)
            {
                var code = BankCode.GetPayBankCode();
                model.AvailableBankCodes = code.ToSelectListItem(true,model.BankCode);
            }
            else if (model.BankCardMode == BankCardMode.Deposit)
            {
                var code = BankCode.GetBankCode();
                model.AvailableBankCodes = code.ToSelectListItem(true, model.BankCode);
            }
        }
        #endregion
        #region
        public ActionResult MyCard()
        {
            var model = new CustomerCardsModel();
            var cards = _bankService.GetMyCards(this.CustomerId);
            model.CardItems = cards.MapTo<IList<BankCardModel>>();
            return View(model);
        }

        public ActionResult NewCard(int mode)
        {
            var model = new BankCardModel();
            model.BankCardModeId = mode;
            PrepareBankCardModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult NewCard(BankCardModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.MapTo<BankCard>();
                entity.CustomerId = this.CustomerId;
                if (model.BankCardMode == BankCardMode.Credit)
                {
                    var banksCode = BankCode.GetPayBankCode();
                    var code = banksCode.FirstOrDefault(e => e.Key.Contains(model.BankCode));
                    entity.Bank = code.Value;
                }
                else if (model.BankCardMode == BankCardMode.Deposit)
                {
                    var banksCode = BankCode.GetBankCode();
                    var code = banksCode.FirstOrDefault(e => e.Key.Contains(model.BankCode));
                    entity.Bank = code.Value;
                }
                _bankService.CreateCard(entity);
                return RedirectToAction("MyCard");
            }
            PrepareBankCardModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult GetCityByProvince(string Province)
        {
            var address = AccountOpenCode.GetAddressCode();
            var cities = address.FirstOrDefault(e => e.Name.Contains(Province)).Cities;

            cities.Select(c => new
            {
                Text = c.Name,
                Value = c.Name
            });
            return Json(cities);
        }

        /// <summary>
        /// 信用卡还款
        /// </summary>
        /// <returns></returns>
        public ActionResult Payment()
        {
            
            return View();
        }

        #endregion
    }
}