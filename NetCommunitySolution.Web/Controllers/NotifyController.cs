using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Web.Security.AntiForgery;
using NetCommunitySolution.Common;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Configuration;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Domain.Orders;
using NetCommunitySolution.Messages;
using NetCommunitySolution.Orders;
using NetCommunitySolution.Security;
using NetCommunitySolution.Web.Framework.Mvc;
using NetCommunitySolution.Web.Framework.WeChat;
using NetCommunitySolution.Web.Framework.WeChat.Dto;
using NetCommunitySolution.Web.Models.Messages;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Xml.Linq;

namespace NetCommunitySolution.Web.Controllers
{
    public class NotifyController : NetCommunitySolutionControllerBase
    {
        #region ctor && Fields

        private readonly ICacheManager _cacheManager;
        private readonly AccountSetting accountSetting;
        private readonly WechatSetting wechatSetting;
        private readonly ICustomerService _customerService;
        private readonly ICustomerAttributeService _attributeService;
        private readonly IAccountLogService _logService;
        private readonly IOrderService _orderService;
        private readonly IEncryptionService _encryptionService;
        private readonly IPrivateMessageService _messageService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IYeeSevice _yeeService;

        public NotifyController(ISettingService _settingService,
            ICustomerAttributeService attributeService,
            ICustomerService customerService,
            IOrderService orderService,
            IAccountLogService logService,
            ICacheManager cacheManager,
            IEncryptionService encryptionService,
            IPrivateMessageService messageService, 
            IUnitOfWorkManager unitOfWorkManager,
            IYeeSevice yeeService)
        {
            this._cacheManager = cacheManager;
            this._customerService = customerService;
            this._orderService = orderService;
            this._attributeService = attributeService;
            this._logService = logService;
            this._encryptionService = encryptionService;
            this._messageService = messageService;
            this._unitOfWorkManager = unitOfWorkManager;
            this._yeeService = yeeService;
            accountSetting = _settingService.GetAccountSettings();
            wechatSetting = _settingService.GetWeChatSettings();
        }
        #endregion

        #region Utilities

        [NonAction]
        private void AuditCustomer(int customerId)
        {

        }

        [NonAction]
        private void OrderComplete()
        {
            
        }
        /// <summary>
        /// 参数转换为哈希表
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [NonAction]
        private Hashtable ParseUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;
            var hash = new Hashtable();

            try
            {

                // 开始分析参数对   
                Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", System.Text.RegularExpressions.RegexOptions.Compiled);
                MatchCollection mc = re.Matches(url);

                foreach (Match m in mc)
                {
                    hash.Add(m.Result("$2").ToLower(), m.Result("$3"));
                }

            }
            catch { }
            return hash;
        }
        
        #endregion

        #region 分润
        /// <summary>
        /// 分润计算
        /// </summary>
        /// <param name="customer">套现用户</param>
        /// <param name="agentCustomer">推广用户</param>
        /// <param name="actual_amount">实际到账金额</param>
        /// <param name="total">用户支付金额</param>
        /// <param name="amount">金额</param>
        private void Profit(Customer customer, Customer agentCustomer, decimal actual_amount, decimal total,decimal amount)
        {
            var customerIsAgent = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.Agent);
            if (!customerIsAgent)
            {
                var profitRate = accountSetting.MemberRate - accountSetting.VendorRate;
                var profitPayment = accountSetting.Payment - 1;
                var profit = total * (profitRate / 100);
                var balance = agentCustomer.GetCustomerAttributeValue<decimal>(CustomerAttributeNames.Account);
                agentCustomer.SaveCustomerAttribute<decimal>(CustomerAttributeNames.Account, balance + profit);
                _logService.InsertLog(new AccountLog
                {
                    AccountModeId = (int)AccountMode.Income,
                    Balance = balance + profit,
                    CustomerId = agentCustomer.Id,
                    Message = string.Format("推广用户下单分润,{0}下单金额为{1}元，分润金额为{2}", customer.NickName, total, profit),
                    Money = profit,
                    CreationTime = DateTime.Now
                });
            }
        }
        #endregion

        #region 易宝回调

        /// <summary>
        /// 订单支付确认
        /// </summary>
        /// <param name="hash"></param>
        [NonAction]
        private void Order_Notify(Hashtable hash)
        {
            var orderSn = hash["out_trade_no"].ToString();
            var order = _orderService.GetOrderBySn(orderSn);
            order.OrderStatusId = (int)OrderStatus.Paid;
            order.Serial = hash["request_id"].ToString();
            _orderService.UpdateOrder(order);
        }

        /// <summary>
        /// 审核通知
        /// </summary>
        /// <param name="hash"></param>
        [NonAction]
        private void Paymerchantreg_Notify(Hashtable hash)
        {
            var mchId = Convert.ToInt32(hash["sysmch_id"]);
            var customerId = _attributeService.GetCustomerId<int>(CustomerAttributeNames.SysMchId, mchId);
            var customer = _customerService.GetCustomerId(customerId);
            switch (hash["merchant_status"])
            {
                case "3":
                    if (customerId > 0)
                    {
                        customer.SaveCustomerAttribute<bool>(CustomerAttributeNames.YeeAudit, true);
                        customer.SaveCustomerAttribute<bool>(CustomerAttributeNames.YeeAuth, true);
                        var rate = accountSetting.CommonRate * 10;
                        _yeeService.SetRate(mchId, 1, rate);
                        _yeeService.SetRate(mchId, 3, accountSetting.Payment);
                    }
                    break;
                case "4":
                    customer.SaveCustomerAttribute<bool>(CustomerAttributeNames.YeeAudit, false);
                    customer.SaveCustomerAttribute<bool>(CustomerAttributeNames.YeeAuth, false);
                    break;
            }


        }

        /// <summary>
        /// 提现通知
        /// </summary>
        /// <param name="hash"></param>
        [NonAction]
        private void Withdraw_Notify(Hashtable hash)
        {
            var external = hash["external_no"].ToString();
            var order = _orderService.GetOrderByExternal(external);
            if (order != null)
            {
                order.OrderStatusId = (int)OrderStatus.Arrival;
                _orderService.UpdateOrder(order);

                //分润
                var agent = order.AgentId;
                if (agent > 0 && order.CustomerId>0)
                {
                    var customer = _customerService.GetCustomerId(order.CustomerId);
                    var agentCustomer = _customerService.GetCustomerId(order.AgentId);
                    var actual_amount = Convert.ToDecimal(hash["actual_amount"]);
                    var amount = Convert.ToDecimal(hash["amount"]);
                    Profit(customer, agentCustomer, actual_amount, order.OrderTotal, amount);

                }

            }
        }

        #endregion

        #region 回调

        [DisableAbpAntiForgeryTokenValidation]
        public ActionResult Notify()
        {

            //接收从微信后台POST过来的数据
            int requestLength = Convert.ToInt32(Request.InputStream.Length);

            byte[] buffer = new byte[requestLength];
            Request.InputStream.Read(buffer, 0, requestLength);
            var requestString = System.Text.Encoding.UTF8.GetString(buffer);
            //requestString = "status=0&msg=%E9%A9%B3%E5%9B%9E%E9%80%9A%E7%9F%A5&op=paymerchantreg_notify&sysmch_id=5959&merchant_status=4&reason=%E7%AE%A1%E7%90%86%E5%91%98%E9%A9%B3%E5%9B%9E&sign=D986248F0D1FF273D3C3922EDE869170";
            Logger.Debug("异步通知：" + requestString);

            var hash = ParseUrl(requestString);
            switch (hash["op"])
            {
                //审核通知
                case "paymerchantreg_notify":
                    Paymerchantreg_Notify(hash);
                    break;
                    //订单通知
                case "order_notify":
                    Order_Notify(hash);
                    break;

                    //提现通知
                case "withdraw_notify":
                    Withdraw_Notify(hash);
                    break;
            }            

            return View();
        }


        [NonAction]
        public string GetNotifyData()
        {
            //接收从微信后台POST过来的数据
            int requestLength = Convert.ToInt32(Request.InputStream.Length);

            byte[] buffer = new byte[requestLength];
            Request.InputStream.Read(buffer, 0, requestLength);
            var requestString = Encoding.UTF8.GetString(buffer);
            return requestString;
        }

        //微信回调
        public ActionResult WeChatNotify()
        {
            Logger.Debug("微信回调接口");
            var requestString = GetNotifyData();

            var res = XDocument.Parse(requestString);
            Logger.Debug("回调的数据" + requestString);

            //通信成功
            if (res.Element("xml").Element("return_code").Value == "SUCCESS")
            {
                if (res.Element("xml").Element("result_code").Value == "SUCCESS")
                {
                    //交易成功
                    Logger.Debug("return_Code:SUCCESS");
                    string orderSn = res.Element("xml").Element("out_trade_no").Value;

                    using (var unitOfWork = _unitOfWorkManager.Begin())
                    {
                        var order = _orderService.GetOrderBySn(orderSn);
                        if (order.OrderStatusId > (int)OrderStatus.Paid)
                            return new NullJsonResult();
                        order.OrderStatusId = (int)OrderStatus.Paid;
                        _orderService.UpdateOrder(order);                                               

                        #region 支付列表
                        var customer = _customerService.GetCustomerId(order.CustomerId);
                        _messageService.CreateMessage(new Domain.Messages.Message
                        {
                            CreationTime = DateTime.Now,
                            FromCustomerId = 0,
                            IsDeleted = false,
                            IsRead = false,
                            Subject = "升级区代",
                            Text = "恭喜您升级为区域代理",
                            ToCustomerId = order.CustomerId
                        });

                        #endregion

                        //发送消息
                        SendMessageToUser(order, order.OrderTotal, customer);
                        customer.SaveCustomerAttribute<bool>(CustomerAttributeNames.Agent, true);

                        var mchId = customer.GetCustomerAttributeValue<int>(CustomerAttributeNames.SysMchId);
                        //调整费率
                        _yeeService.SetRate(mchId, 1, accountSetting.VendorRate * 10);
                        _yeeService.SetRate(mchId, 3, 1M);
                        //推广码
                        customer.SaveCustomerAttribute<string>(CustomerAttributeNames.ExtensionCode, CommonHelper.GetTimeStamp());
                        //用户升级代理后给代理商提成
                        AgentCommission(customer.NickName, customer.Agent, order.OrderTotal);
                        unitOfWork.Complete();
                    }
                }
                else
                {
                    Logger.Debug("return_Code:FAIL");
                }
            }
            else
            {
                Logger.Debug("return_Code:FAIL,签名失败");
            }
            return new NullJsonResult();
        }
        #endregion

        #region Message
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="amount"></param>
        /// <param name="customer"></param>
        private void SendMessageToUser(WeChatMessageModel model)
        {
            SendOrderMessage(Newtonsoft.Json.JsonConvert.SerializeObject(model));
        }

        private void SendMessageToUser(Order order, decimal amount, Customer customer)
        {
            var model = new WeChatMessageModel();
            model.touser = customer.OpenId;
            model.msgtype = "news";

            StringBuilder desc = new StringBuilder();
            desc.Append("恭喜您升级为区域代理");
            desc.Append("\n");
            desc.Append("您缴纳了区域代理费用：" + order.OrderTotal + "元");
            desc.Append("\n");
            desc.Append("您的佣金：" + amount);

            model.news.articles.Add(new WeChatMessageModel.ArticlesItem
            {
                url = string.Format("{0}/{1}", HttpContext.Request.Url.Host, "/Customer"),
                title = "恭喜您,用户升级",
                picurl = "",
                description = desc.ToString(),
            });
            SendMessageToUser(model);
        }

        private void SendMessageToUser(Customer customer,bool audit,string content)
        {
            var model = new WeChatMessageModel();
            model.touser = customer.OpenId;
            model.msgtype = "news";

            StringBuilder desc = new StringBuilder();
            desc.Append("审核通知");
            desc.Append("\n");
            if (audit)
                desc.Append("恭喜您：用户审核失败");
            else
                desc.Append("您的状态审核失败：" + content);
            
            SendMessageToUser(model);
        }


        //发送通知短信（管理员）
        [NonAction]
        private void SendOrderMessage(string msg)
        {
            string msgUrl = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";
            WeChatDefault wxDefault = new WeChatDefault();
            var token = wxDefault.GetAccessToken(wechatSetting.AppId, wechatSetting.AppSecret);
            string url = string.Format(msgUrl, token.access_token);
            HttpWebResponseUtility client = new HttpWebResponseUtility();
            client.CreatePostHttpResponse(url: url, data: msg);
        }

        /// <summary>
        /// 普通用户升级区域代理后的提成
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="agent"></param>
        /// <param name="total">order</param>
        private void AgentCommission(string customerName,int agent,decimal total)
        {
            var agentCustomer = _customerService.GetCustomerId(agent);
            if (agentCustomer != null && agent > 0)
            {
                var balance = agentCustomer.GetCustomerAttributeValue<decimal>(CustomerAttributeNames.Account);
                var money = total * 0.2m;

                _logService.InsertLog(new AccountLog
                {
                    AccountModeId = (int)AccountMode.Income,
                    CreationTime = DateTime.Now,
                    CustomerId = agent,
                    Message = string.Format("您推广的用户[{0}]升级为区域代理商，返佣{1}元", customerName, money.ToString("#0.00")),
                    Money = money,
                    Balance = balance + money
                });
                agentCustomer.SaveCustomerAttribute<decimal>(CustomerAttributeNames.Account, balance + money);
            }
        }
        #endregion
    }
};