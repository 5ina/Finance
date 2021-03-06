﻿using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Abp.Web.Security.AntiForgery;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NetCommunitySolution.Authentication;
using NetCommunitySolution.Authentication.Dto;
using NetCommunitySolution.Common;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Directory;
using NetCommunitySolution.Domain.Configuration;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Media;
using NetCommunitySolution.Messages;
using NetCommunitySolution.Orders;
using NetCommunitySolution.Security;
using NetCommunitySolution.Security.YeeDto;
using NetCommunitySolution.Web.Framework.Controllers;
using NetCommunitySolution.Web.Framework.WeChat;
using NetCommunitySolution.Web.Models.Customers;
using NetCommunitySolution.Web.Models.Messages;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    [Mobile]
    public class CustomerController : WeChatBaseController
    {

        #region ctor && Fields

        private const string PRODUCTREVIEW_BY_CUSTOMERID = "strore.product.review.by.customerid-{0}";
        private const string FAVORITE_BY_CUSTOMERID = "strore.favorite.by.customerid-{0}";

        private const string CUSTOMER_AGENT_CENTER = "store.customer.agent";
        /// <summary>
        /// 二维码请求URL
        /// </summary>
        private const string QRCode_Url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}";
        
        private readonly ICacheManager _cacheManager;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly ICustomerService _customerService;
        private readonly IEncryptionService _encryptionService;
        private readonly ISMSMessageService _messageService;
        private readonly IOrderService _orderService;
        private readonly LoginManager _loginManager;
        private readonly CustomerManager _customerManager;
        private readonly IAreaService _areaService;
        private readonly IOssService _ossService;
        private readonly IYeeSevice _yeeService;
        private readonly WechatSetting wechatSetting;
        private readonly IPrivateMessageService _privateMessageService;

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public CustomerController(
                                ICacheManager cacheManager,
                                ICustomerAttributeService customerAttributeService,
                                ICustomerService customerService,
                                IEncryptionService encryptionService,
                                ISMSMessageService messageService,
                                IUnitOfWorkManager unitOfWorkManager,
                                IAreaService areaService,
                                IOssService ossService,
                                IYeeSevice yeeService,
                                ISettingService settingService,
                                IOrderService orderService,
                                IPrivateMessageService privateMessageService,
                                LoginManager loginManager,
                                CustomerManager customerManager)
        {
            this._cacheManager = cacheManager;
            this._customerAttributeService = customerAttributeService;
            this._customerService = customerService;
            this._messageService = messageService;
            this._encryptionService = encryptionService;
            this._unitOfWorkManager = unitOfWorkManager;
            this._areaService = areaService;
            this._orderService = orderService;
            this._ossService = ossService;
            this._yeeService = yeeService;
            this._privateMessageService = privateMessageService;
            this.wechatSetting = settingService.GetWeChatSettings();

            this._loginManager = loginManager;
            this._customerManager = customerManager;
        }
        #endregion

        #region Utilities
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion

        #region Method

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Index()
        {
            var customer = _customerService.GetCustomerId(this.CustomerId);
            var model = customer.MapTo<CustomerInfoModel>();
            model.CustomerAvatar = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.Avatar);
            var audit = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.YeeAudit);
            var auth = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.YeeAuth);
            model.Authentication = audit && auth;
            model.Agent = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.Agent);
            model.Account = customer.GetCustomerAttributeValue<decimal>(CustomerAttributeNames.Account);
            model.AgentID = customer.Agent;
            return View(model);
        }
        
        #region Account

        public ActionResult Account()
        {
            return View();
        }

        public ActionResult MyPromoter()
        {
            var model = new MyPromoterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult MyPromoter(MyPromoterModel model)
        {
            if (String.IsNullOrWhiteSpace(model.ExtensionCode))
            {
                ModelState.AddModelError("", "请输入推广码");
            }
            var promoterId = _customerAttributeService.GetCustomerIdByAttributeValue<string>(model.ExtensionCode);

            if (promoterId == 0)
            {
                ModelState.AddModelError("", "推广码不正确");
            }
            if (ModelState.IsValid)
            {
                var customer = _customerService.GetCustomerId(this.CustomerId);
                customer.Agent = promoterId;
                _customerService.UpdateCustomer(customer);
                return RedirectToAction("Success");
            }
            return View(model);
        }
        
        #endregion

        #region Security
        public ActionResult Security()
        {
            var customer = _customerService.GetCustomerId(this.CustomerId);
            var model = new SecurityModel();
            var audit  = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.YeeAudit);
            var auth = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.YeeAuth);
            model.IsAuth = audit && auth;
            model.IsBindMobile = !String.IsNullOrWhiteSpace(customer.Mobile);
            model.IsAgent = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.Agent);
            return View(model);
        }

        public ActionResult ExtensionCode()
        {
            var customer = _customerService.GetCustomerId(this.CustomerId);
            var agent = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.Agent);
            if (!agent)
                return RedirectToAction("Index");
            var model = new ExtensionCodeModel();
            model.Id = customer.Id;
            model.ExtensionCode = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.ExtensionCode);
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            var model = new ChangePasswordModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (!model.NewPassword.Equals(model.ConfirmNewPassword))
                ModelState.AddModelError("", "新密码输入不一致");

            var customer = _customerService.GetCustomerId(this.CustomerId);
            var oldPassword = _encryptionService.CreatePasswordHash(model.OldPassword, customer.PasswordSalt);
            if(!oldPassword.Equals(customer.Password))
                ModelState.AddModelError("", "密码错误，请从新输入");

            if (ModelState.IsValid)
            {
                var newPassword = _encryptionService.CreatePasswordHash(model.NewPassword, customer.PasswordSalt);
                customer.Password = newPassword;
                _customerService.UpdateCustomer(customer);
                return RedirectToAction("Security");
            }
            return View(model);
        }

        #endregion
        #endregion

        #region Picture
        [HttpPost]
        [DisableAbpAntiForgeryTokenValidation]
        public ActionResult AsyncUploadImage()
        {
            //Stream stream = null;
            var fileName = "";
            var contentType = "";
            //if (String.IsNullOrEmpty(Request["image"]))
            //{
                // IE
                HttpPostedFileBase httpPostedFile = Request.Files[0];
                if (httpPostedFile == null)
                    throw new ArgumentException("文件不存在");
                //stream = httpPostedFile.InputStream;
                fileName = Path.GetFileName(httpPostedFile.FileName);
                contentType = httpPostedFile.ContentType;
            //}
            //else
            //{
            //    stream = Request.InputStream;
            //    fileName = Request["image"];
            //}

            //var fileBinary = new byte[stream.Length];
            //stream.Read(fileBinary, 0, fileBinary.Length);
            //stream.Close();

            var fileExtension = Path.GetExtension(fileName);
            var fileBinary = CompressionImage(httpPostedFile.InputStream, 30);
            var url = _yeeService.Uploadimage(fileBinary, contentType, fileExtension);

            return AbpJson(new
            {
                success = true,
                Url =  url,
            });
        }
        private byte[] CompressionImage(Stream fileStream, long quality)
        {
            
            using (System.Drawing.Image img = System.Drawing.Image.FromStream(fileStream))
            {
                using (Bitmap bitmap = new Bitmap(img))
                {
                    ImageCodecInfo CodecInfo = GetEncoder(img.RawFormat);
                    System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitmap.Save(ms, CodecInfo, myEncoderParameters);
                        myEncoderParameters.Dispose();
                        myEncoderParameter.Dispose();
                        return ms.ToArray();
                    }
                }
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                { return codec; }
            }
            return null;
        }
        #endregion

        #region 区县
        public ActionResult Areas()
        {
            return _cacheManager.GetCache("net.controller.area.all").Get("net.controller.area.all", () =>
            {
                var provinces = _areaService.GetProvinces();
                var areas = provinces.Select(p => new
                {
                    label = p,
                    value = "",
                    children = GetCities(p)
                }).ToList();
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(areas);
                return Content(json);
            });
        }

        private dynamic GetCities(string province)
        {
            var cities = _areaService.GetCities(province);
            return cities.Select(c => new
            {
                label = c,
                value = "",
                children = GetCounties(c)
            }).ToList();
        }

        private dynamic GetCounties(string city)
        {
            var counties = _areaService.GetCounty(city);
            return counties.Select(c => new
            {
                label = c.County,
                value = c.areaCode,
            }).ToList();
        }
        #endregion

        #region  易宝 认证
        //认证
        public ActionResult CustomerAuth()
        {
            var model = new CustomerAuthModel();
            var customer = _customerService.GetCustomerId(this.CustomerId);

            #region get Attribute
            model.sysmch_id = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.SysMchId);

            model.bind_mobile = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.bind_mobile);
            model.id_card = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.id_card);
            model.id_card_back_photo = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.id_card_back_photo);
            model.id_card_photo = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.id_card_photo);
            model.legal_person = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.legal_person);
            model.person_photo = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.person_photo);
            model.region_text = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.region_text);
            model.area_code = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.area_code);
            model.bank_account_number = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.bank_account_number);
            model.bank_card_photo = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.bank_card_photo);
            model.bank_name = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.bank_name);
            model.Audit = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.YeeAudit);
            model.AuditReason = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.YeeAuditReason);
            #endregion

            return View(model);
        }

        [UnitOfWork]
        [HttpPost]
        public ActionResult CustomerAuth(CustomerAuthModel model)
        {
            #region validate
            if (model.area_code == "")
                ModelState.AddModelError("", "请选择银行卡开户地区");
            if (model.bank_account_number == "")
                ModelState.AddModelError("", "请输入结算卡卡号");
            if (model.bank_card_photo == "")
                ModelState.AddModelError("", "请提交结算卡照片");
            if (model.bank_name == "")
                ModelState.AddModelError("", "请输入银行名称");
            if (model.bind_mobile == "")
                ModelState.AddModelError("", "请输入手机号");
            if (model.id_card == "")
                ModelState.AddModelError("", "请输入身份证");
            if (model.id_card_back_photo == "")
                ModelState.AddModelError("", "请提交身份证反面照片");
            if (model.id_card_photo == "")
                ModelState.AddModelError("", "请提交身份证正面照片");
            if (model.legal_person == "")
                ModelState.AddModelError("", "请输入真实姓名");
            if (model.person_photo == "")
                ModelState.AddModelError("", "请提交手持身份证照片");
            #endregion


            if (ModelState.IsValid)
            {
                #region Save Attribute
                var customer = _customerService.GetCustomerId(this.CustomerId);
                var area = _areaService.GetAreaByCode(model.area_code);
                model.region_text = string.Format("{0} {1} {2}", area.Province, area.City, area.County);

                var paymerch = model.MapTo<PaymerchantregModel>();
                #endregion

                #region 创建易宝商户
                var mchId = _yeeService.MchCreate(paymerch.bind_mobile, this.CustomerId);
                customer.SaveCustomerAttribute<int>(CustomerAttributeNames.SysMchId, mchId);
                customer.SaveYeeInfomation(_customerAttributeService, paymerch);
                paymerch.sysmch_id = mchId.ToString();
                #endregion

                var result = _yeeService.Paymerchantreg(paymerch);
                customer.SaveCustomerAttribute<bool>(CustomerAttributeNames.YeeAuth, result);
                customer.Mobile = model.bind_mobile;
                //发送通知
                SendMessageToUser(customer);
                _customerService.UpdateCustomer(customer);
                return RedirectToAction("Success");
            }
            return View(model);
        }
        #endregion

        #region 结算卡
        public ActionResult Settlement()
        {
            var model = new SettlementModel();
            var customer = _customerService.GetCustomerId(this.CustomerId);
            model.IsAuth = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.YeeAuth);


            #region get Attribute
            model.sysmch_id = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.SysMchId);
            
            model.id_card_back_photo = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.id_card_back_photo);
            model.id_card_photo = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.id_card_photo);
            model.person_photo = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.person_photo);
            model.region_text = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.region_text);
            model.area_code = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.area_code);
            model.bank_account_number = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.bank_account_number);
            model.bank_card_photo = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.bank_card_photo);
            model.bank_name = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.bank_name);
            model.Audit = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.YeeAudit);
            model.AuditReason = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.YeeAuditReason);
            #endregion

            return View(model);
        }


        [HttpPost]
        public ActionResult Settlement(SettlementModel model)
        {

            #region validate
            if (model.area_code == "")
                ModelState.AddModelError("", "请选择银行卡开户地区");
            if (model.bank_account_number == "")
                ModelState.AddModelError("", "请输入结算卡卡号");
            if (model.bank_card_photo == "")
                ModelState.AddModelError("", "请提交结算卡照片");
            if (model.bank_name == "")
                ModelState.AddModelError("", "请输入银行名称");
            if (model.id_card_back_photo == "")
                ModelState.AddModelError("", "请提交身份证反面照片");
            if (model.id_card_photo == "")
                ModelState.AddModelError("", "请提交身份证正面照片");
            if (model.person_photo == "")
                ModelState.AddModelError("", "请提交手持身份证照片");
            #endregion

            if (ModelState.IsValid)
            {

                #region Save Attribute
                var customer = _customerService.GetCustomerId(this.CustomerId);
                var area = _areaService.GetAreaByCode(model.area_code);
                model.region_text = string.Format("{0} {1} {2}", area.Province, area.City, area.County);

                var paymerch = model.MapTo<PaymerchantregModel>();
                #endregion

                #region 创建易宝商户
                var mchId = _yeeService.MchCreate(paymerch.bind_mobile, this.CustomerId);
                customer.SaveCustomerAttribute<int>(CustomerAttributeNames.SysMchId, mchId);
                customer.SaveYeeInfomation(_customerAttributeService, paymerch);
                paymerch.sysmch_id = mchId.ToString();
                paymerch.legal_person = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.legal_person);
                paymerch.id_card = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.id_card);
                paymerch.bind_mobile = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.bind_mobile);
                #endregion

                var result = _yeeService.Paymerchantreg(paymerch);
                customer.SaveCustomerAttribute<bool>(CustomerAttributeNames.YeeAuth, result);
                return RedirectToAction("Success");
            }
            return View(model);

        }
        #endregion

        #region 我的推广码
        public ActionResult MyQRCode()
        {
            var customer = _customerService.GetCustomerId(this.CustomerId);
            var expireTime = customer.GetCustomerAttributeValue<DateTime>(CustomerAttributeNames.QRCodeExpireTime);

            var expire = wechatSetting.Expire;
            var model = new CustomerQRModel();
            model.CustomerID = this.CustomerId;
            WeChatDefault wx = new WeChatDefault();
            if (expireTime > DateTime.Now)
            {
                model.QR_Url = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.MyQR_Code);
            }
            else
            {
                var access_token = wx.GetAccessToken(wechatSetting.AppId, wechatSetting.AppSecret, _cacheManager);
                string url = string.Format(QRCode_Url, access_token.access_token);
                expireTime = DateTime.Now.AddSeconds(expire * 24 * 60 * 60);

                var result = wx.QRCode(_cacheManager, wechatSetting.AppId, wechatSetting.AppSecret, expire * 24 * 60 * 60, false, this.CustomerId);
                customer.SaveCustomerAttribute<DateTime>(CustomerAttributeNames.QRCodeExpireTime, expireTime);
                customer.SaveCustomerAttribute<string>(CustomerAttributeNames.MyQR_Code, result);

                model.QR_Url = result;
            }
            model.CreateTime = expireTime;
            model.Expire = expire;

            var config = wx.WxConfig(_cacheManager, wechatSetting.AppId, wechatSetting.AppSecret, Request.Url.Host + this.Request.Url.PathAndQuery);
            model.Config = config;
            return View(model);
        }
        #endregion

        #region Login && Logout

        [HttpPost]
        public ActionResult Login(CustomerLoginModel model)
        {

            var loginResult = _customerService.ValidateCustomer(model.LoginName, model.Password);
            switch (loginResult.Result)
            {
                case LoginResults.Successful:
                    {
                        var customerDto = loginResult.Customer.MapTo<CustomerDto>();
                        //生成ClaimsIdentity
                        var identity = _loginManager.CreateUserIdentity(customerDto);
                        //用户登录
                        AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
                        if (customerDto.CustomerRole == CustomerRole.System)
                        {
                            return RedirectToAction("Index", "Home", new { Area = "Admin" });
                        }
                        else
                            return RedirectToAction("Index", "Home");

                    }

                case LoginResults.Deleted:
                    ModelState.AddModelError("", "该用户已经被冻结");
                    break;
                case LoginResults.NotRegistered:
                    ModelState.AddModelError("", "用户不存在");
                    break;
                case LoginResults.Unauthorized:
                    ModelState.AddModelError("", "该用户未授权登录管理平台");
                    break;
                case LoginResults.WrongPassword:
                default:
                    ModelState.AddModelError("", "密码错误");
                    break;
            }
            return View(model);
        }

        public ActionResult Login()
        {
            if (AbpSession.UserId.HasValue)
                return RedirectToAction("Index", "Home", new { Area = "Admin" });
            var model = new CustomerLoginModel();
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            AbpSession = NullAbpSession.Instance;
            return Redirect("/login");
        }

        #endregion

        #region Rate
        public ActionResult Rate()
        {
            var customer = _customerService.GetCustomerId(this.CustomerId);            
            var model = new CustomerRateModel();
            model.mch_Id = customer.GetCustomerAttributeValue<int>(CustomerAttributeNames.SysMchId);
            var resultRate = _yeeService.QueryRate(model.mch_Id, 1);
            model.Rate = resultRate.rate;
            var resultPayment = _yeeService.QueryRate(model.mch_Id, 3);
            model.Payment = Convert.ToInt32(resultPayment.rate);
            return View(model);
        }
        #endregion

        #region report
        [ChildActionOnly]
        public ActionResult MyCustomers()
        {
            var customers = _customerService.GetCustomerByAgentId(this.CustomerId);
            var count = customers.Count();
            return Content(count.ToString());
        }

        [ChildActionOnly]
        public ActionResult Turnover()
        {
            DateTime now = DateTime.Now;
            var startTime = new DateTime(now.Year, now.Month, 1);
            var orders = _orderService.GetAllOrders(createdFrom: startTime, agentId: this.CustomerId);
            var total = orders.Items.Sum(o => o.OrderTotal);
            return Content(total.ToString());
        }

        public ActionResult MyCustomerList()
        {
            var model = new CustomerAgentListModel();

            DateTime now = DateTime.Now;
            var startTime = new DateTime(now.Year, now.Month, 1);
            var customers = _customerService.GetCustomerByAgentId(this.CustomerId);
            model.Total = customers.Count();
            var monthCustomers = customers.Where(e => e.CreationTime > startTime).ToList();
            model.MonthTotal = monthCustomers.Count();

            model.Customers = monthCustomers.Select(e => new CustomerAgentListModel.MyCustomer
            {
                CreateTime = e.CreationTime.ToString("yyyy/MM/dd"),
                CustomerId = e.Id,
                Mobile = e.Mobile,
                NickName = e.NickName,
            }).ToList();
            return View(model);
        }

        /// <summary>
        /// 推广中心
        /// </summary>
        /// <returns></returns>
        public ActionResult AgentCenter()
        {
            var model = _cacheManager.GetCache(CUSTOMER_AGENT_CENTER).Get(CUSTOMER_AGENT_CENTER, ()=>
            {
                var customers = _customerService.GetCustomerByAgentId(this.CustomerId);
                var centerModel = new CustomerAgentCenterModel();
                centerModel.CustomerCount = customers.Count();
                DateTime now = DateTime.Now;
                var startTime = new DateTime(now.Year, now.Month, 1);
                var orders = _orderService.GetAllOrders(createdFrom: startTime, agentId: this.CustomerId);
                centerModel.TotalAmount = orders.Items.Sum(o => o.OrderTotal);
                return centerModel;
            });
            return View(model);
        }

        #endregion

        #region wechat message
        
        [NonAction]
        private void SendMessageToUser(Customer customer)
        {
            var model = new WeChatMessageModel();
            model.touser = customer.OpenId;
            model.msgtype = "news";

            StringBuilder desc = new StringBuilder();
            desc.Append("账户认证");
            desc.Append("\n");

            desc.Append("您的信息以及提交审核，请等待管理员审核后再进行操作");

            SendMessage(Newtonsoft.Json.JsonConvert.SerializeObject(model));
        }
        

        [NonAction]
        private void SendMessage(string msg)
        {
            string msgUrl = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";
            WeChatDefault wxDefault = new WeChatDefault();
            var token = wxDefault.GetAccessToken(wechatSetting.AppId, wechatSetting.AppSecret);
            string url = string.Format(msgUrl, token.access_token);
            HttpWebResponseUtility client = new HttpWebResponseUtility();
            client.CreatePostHttpResponse(url: url, data: msg);
        }
        #endregion
    }
}
