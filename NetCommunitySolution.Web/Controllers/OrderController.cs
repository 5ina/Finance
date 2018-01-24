using Abp.BackgroundJobs;
using Abp.Runtime.Caching;
using NetCommunitySolution.Common;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Configuration;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Domain.Orders;
using NetCommunitySolution.Orders;
using NetCommunitySolution.Security;
using NetCommunitySolution.Web.Framework.WeChat;
using NetCommunitySolution.Web.Models.Orders;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    /// <summary>
    /// 订单控制器
    /// </summary>
    public class OrderController : WeChatBaseController
    {

        #region ctor && Fields

        private readonly ICacheManager _cacheManager;
        private readonly AccountSetting accountSetting;
        private readonly WechatSetting wechatSetting;
        private readonly IPaymentService _paymentService;
        private readonly IYeeSevice _yeeService;
        private readonly ICustomerService _customerService;
        private readonly ICustomerAttributeService _attributeService;
        private readonly IAccountLogService _logService;
        private readonly IOrderService _orderService;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IEncryptionService _encryptionService;

        public OrderController(ISettingService _settingService,
            IPaymentService paymentService,
            IYeeSevice yeeService,
            ICustomerAttributeService attributeService,
            ICustomerService customerService,
            IOrderService orderService,
            IAccountLogService logService,
            ICacheManager cacheManager,
            IEncryptionService encryptionService,
            IBackgroundJobManager backgroundJobManager)
        {
            this._cacheManager = cacheManager;
            this._paymentService = paymentService;
            this._customerService = customerService;
            this._orderService = orderService;
            this._attributeService = attributeService;
            this._logService = logService;
            this._encryptionService = encryptionService;
            this._yeeService = yeeService;
            accountSetting = _settingService.GetAccountSettings();
            wechatSetting = _settingService.GetWeChatSettings();

            this._backgroundJobManager = backgroundJobManager;
        }
        #endregion

        #region Utilities   
        [NonAction]
        public bool PaymentValidators(string password)
        {
            var customer = _customerService.GetCustomerId(this.CustomerId);
            var inputPassword = _encryptionService.CreatePasswordHash(password, customer.PasswordSalt);
            var paymentPassword = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.TransactionPassword);

            return paymentPassword.Equals(inputPassword);
        }

        /// <summary>
        /// 单笔交易额限制
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        [NonAction]
        public bool TradeLimit(decimal amount)
        {
            var limit = true;

            if (amount <= 0)
                limit = false;

            if (accountSetting.TradeLimit)
            {
                if (amount > accountSetting.MaxTrade)
                    return false;
                if (amount < accountSetting.MinTrade)
                    return false;
            }
            return limit;
        }


        private void PrepareWithdrawalsModel(WithdrawalModel model)
        {
            var customer = _customerService.GetCustomerId(this.CustomerId);
            var agent = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.Agent);
            model.IsAgent = agent;
            model.MaxTotal = customer.GetCustomerAttributeValue<decimal>(CustomerAttributeNames.Account);
            model.MinTotal = 0m;
        }

        private void PrepareYeePaymentModel(YeePaymentModel model)
        {

            var customer = _customerService.GetCustomerId(this.CustomerId);
            var yeeAuth = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.YeeAuth);
            var yeeAudit = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.YeeAudit);
            var agent = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.Agent);
            if (agent)
                model.Rate = accountSetting.VendorRate;
            else if (customer.Agent == 0 && !agent)
                model.Rate = accountSetting.CommonRate;
            else if (customer.Agent != 0 && !agent)
                model.Rate = accountSetting.MemberRate;

            if (customer.CustomerRoleId == (int)CustomerRole.System)
                model.Rate = accountSetting.BaseRate;

            model.PaymentFee = accountSetting.Payment;
            model.IsAuth = yeeAudit && yeeAuth;
        }
        private void PrepareAgentModel(WeChatPaymentModel model)
        {
            model.AppId = wechatSetting.AppId;
            model.Noncestr = CommonHelper.GenerateNonceStr();
            model.TimeStamp = CommonHelper.GetTimeStamp();
            model.CommonRate = accountSetting.MemberRate;
            model.OrderSn = CommonHelper.GenerateOrderSN();
            model.Total = accountSetting.AgencyFee;
            model.Rate = accountSetting.VendorRate;

            var total = FormatTotal(accountSetting.AgencyFee);
            var result = UnifiedOrder(model.Noncestr, "升级为代理商", model.OrderSn, total, model.TimeStamp);
            //var jsApiString = GetJsApiParameters(result, wechatSetting.Key);
            var handler = result.GetJsApiParametersRequest(wechatSetting.Key);
            model.Signature = result["sign"].ToString();
            var jsonData = new
            {
                appId = handler["appId"],
                timeStamp = handler["timeStamp"],
                nonceStr = handler["nonceStr"],
                package = handler["package"],
                signType = handler["signType"],
                paySign = handler["paySign"],
            };
            model.wxJsApiParam = JsonConvert.SerializeObject(jsonData);
        }
        #endregion


        #region Method Yee Pay

        public ActionResult YeePay()
        {
            var model = new YeePaymentModel();
            PrepareYeePaymentModel(model);            
            return View(model);
        }

        [HttpPost]
        public ActionResult YeePay(YeePaymentModel model)
        {
            if (!TradeLimit(model.Total))
                ModelState.AddModelError("", "单笔交易额超出界限");

            if (ModelState.IsValid)
            {
                var customer = _customerService.GetCustomerId(this.CustomerId);
                var order = new Order
                {
                    AgentId = customer.Agent,
                    CustomerId = customer.Id,
                    IsDeleted = false,
                    OrderModeId = (int)AccountMode.Cash,
                    OrderSn = CommonHelper.GenerateOrderSN(),
                    OrderStatusId = (int)OrderStatus.Pending,
                    OrderTotal = model.Total,
                    CreationTime = DateTime.Now,
                };                
                order.Id = _orderService.CreateOrder(order);
                var mchId = customer.GetCustomerAttributeValue<int>(CustomerAttributeNames.SysMchId);
                var result = _yeeService.Payment(mchId, model.Total, order.OrderSn);
                if (result.status == 0)
                {
                    return Redirect(result.url);
                }
                ModelState.AddModelError("", result.msg);

            }
            PrepareYeePaymentModel(model);
            return View(model);

        }
        #endregion
        #region 提现
        public ActionResult Withdrawals()
        {
            var model = new WithdrawalModel();
            PrepareWithdrawalsModel(model);
            return View(model);
        }
        public ActionResult Withdrawals(WithdrawalModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    AgentId = 0,
                    CreationTime = DateTime.Now,
                    CustomerId = this.CustomerId,
                    IsDeleted = false,
                    OrderModeId = (int)AccountMode.Extract,
                    OrderSn = CommonHelper.GenerateOrderSN(),
                    OrderTotal = model.Total,
                    OrderStatusId = (int)OrderStatus.Pending,

                };
                _orderService.CreateOrder(order);

                return RedirectToAction("Success", new { msg = "您的提现已经申请，等额审核后自动拨款" });
            }
            PrepareWithdrawalsModel(model);
            return View(model);

        }



        #endregion

        #region success

        public ActionResult Success(string msg)
        {
            return View();
        }
        #endregion

        #region WeChatPay

        public ActionResult Agent()
        {
            var model = new WeChatPaymentModel();
            PrepareAgentModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Agent(WeChatPaymentModel model)
        {
            var customer = _customerService.GetCustomerId(this.CustomerId);

            Order order = new Order
            {
                AgentId = customer.Agent,
                CustomerId = customer.Id,
                CreationTime = DateTime.Now,
                IsDeleted = false,
                OrderModeId = (int)AccountMode.Agent,
                OrderSn = model.OrderSn,
                OrderStatusId = (int)OrderStatus.Pending,
                OrderTotal = accountSetting.AgencyFee,
                Serial = "",
            };
            order.Id = _orderService.CreateOrder(order);

            var jsonData = new {
                OrderId = order.Id
            };
            return AbpJson(jsonData);
        }


        /// <summary>
        /// 统一下单接口
        /// </summary>
        /// <param name="nonce">随即字符串</param>
        /// <param name="sgin">签名</param>
        /// <param name="body">说明</param>
        /// <param name="out_trade_no">订单号</param>
        /// <param name="total_fee">总金额</param>
        [NonAction]
        private Hashtable UnifiedOrder(string nonce, string body, string out_trade_no, int total_fee,string timeSpan)
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

            Hashtable request = new Hashtable();
            var customer = _customerService.GetCustomerId(this.CustomerId);

            request.Add("appid", wechatSetting.AppId);
            request.Add("mch_id", wechatSetting.MchId);
            request.Add("nonce_str", nonce);
            request.Add("out_trade_no", out_trade_no);
            request.Add("total_fee", total_fee.ToString());
            //request.SetParameter("total_fee", "1");
            request.Add("body", body);
            request.Add("notify_url", wechatSetting.NotifyUrl);
            request.Add("spbill_create_ip", Request.UserHostAddress);            
            request.Add("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            request.Add("trade_type", "JSAPI");


            request.Add("openid", customer.OpenId);
            //设置签名
            var sign = request.CreateMd5Sign(wechatSetting.Key);
            Logger.Debug("json:" + request.FromJson());
            string response = HttpService.Post(request.ParseXML(), url, false, 6);
            var result = response.FormatSorted( sign);
            Logger.Debug("result:" + response);
            if (result["return_code"].ToString().ToUpper() == "SUCCESS")
            {
                request.Add("prepay_id", result["prepay_id"].ToString());
            }
            else
            {
                request.Add("prepay_id", result["return_code"].ToString());
            }
            request.Add("timeStamp", timeSpan);
            return request;
        }

        [NonAction]

        private string GetJsApiParameters(Hashtable result, string key)
        {
            var hash = new Hashtable();
            hash.Add("appId", wechatSetting.AppId);
            hash.Add("timeStamp", CommonHelper.GetTimeStamp());
            hash.Add("nonceStr", CommonHelper.GenerateNonceStr());
            hash.Add("package", "prepay_id=" + result["prepay_id"]);
            hash.Add("signType", "MD5");
            hash.Add("paySign", hash.CreateMd5Sign(key, false)); //result.GetParameter("sign"));

            string parameters = hash.FromJson();
            return parameters;
        }


        [NonAction]
        private int FormatTotal(decimal orderTotal)
        {
            if (orderTotal <= 0)
                return 100;

            var returnValue = orderTotal * 100;
            return Convert.ToInt32(returnValue);
        }


        
        #endregion

    }
}