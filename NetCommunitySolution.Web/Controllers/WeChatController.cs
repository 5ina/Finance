using Abp.AutoMapper;
using Abp.Runtime.Caching;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NetCommunitySolution.Authentication;
using NetCommunitySolution.Authentication.Dto;
using NetCommunitySolution.CacheNames;
using NetCommunitySolution.Common;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Security;
using NetCommunitySolution.Web.Framework.WeChat;
using NetCommunitySolution.Web.Models.WeChat;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NetCommunitySolution.Web.Controllers
{
    public class WeChatController : NetCommunitySolutionControllerBase
    {
        #region ctor && Fields
        private readonly ISettingService _settingService;
        private readonly ICustomerService _customerService;
        private readonly LoginManager _loginManager;
        private readonly ICacheManager _cacheManager;

        public WeChatController(ISettingService settingService,
            ICustomerService customerService,
            LoginManager loginManager,
            ICacheManager cacheManager)
        {
            this._settingService = settingService;
            this._customerService = customerService;
            this._loginManager = loginManager;
            this._cacheManager = cacheManager;
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

        #region unit

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        [NonAction]
        private bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ActionResult WxAuth(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return Content("");
            }
            string echoString = HttpContext.Request.QueryString["echostr"];
            string signature = HttpContext.Request.QueryString["signature"];
            string timestamp = HttpContext.Request.QueryString["timestamp"];
            string nonce = HttpContext.Request.QueryString["nonce"];
            if (CheckSignature(token, signature, timestamp, nonce))
            {
                if (!string.IsNullOrEmpty(echoString))
                {
                    return Content(echoString);
                }
            }
            return Content("");
        }


        [NonAction]
        private AccessTokenModel GetAccessToken(string appId, string appSecret)
        {
            AccessTokenModel token = WeChatHttpUtility.Get<AccessTokenModel>(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, appSecret));
            return token;
        }
        #endregion

        #region open api
        /// <summary>
        /// 对微信统一接口
        /// </summary>
        /// <param name="echoStr"></param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public ActionResult OpenApi(string echoStr, string signature, string timestamp, string nonce)
        {
            var token = _settingService.GetSettingByKey<string>(WechatSettingNames.Token);
            if (Request.HttpMethod.ToUpper() == "POST")
            {
                //微信服务器对接口消息
                using (Stream stream = HttpContext.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    var postString = Encoding.UTF8.GetString(postBytes);
                    Handle(postString); //执行手机微信操作命令
                }
            }
            return Content("");

        }


        /**
         * snsapi_base
         * **/
        public ActionResult OAuth()
        {
            var appId = _settingService.GetSettingByKey<string>(WechatSettingNames.AppId);
            var appSecret = _settingService.GetSettingByKey<string>(WechatSettingNames.AppSecret);

            string code = Request.QueryString["code"];
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    var oauthToken = WeChatHttpUtility.Get<OAuthTokenModel>(string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appId, appSecret, code));
                    if (!String.IsNullOrWhiteSpace(oauthToken.access_token))
                    {
                        //var accessToken = _cacheManager.GetCache(NetCommunitySolutionConsts.CACHE_ACCESS_TOKEN)
                        //    .Get(NetCommunitySolutionConsts.CACHE_ACCESS_TOKEN, () => GetAccessToken(appId, appSecret));
                        var accessToken = GetAccessToken(appId, appSecret);
                        var userInfoUrl = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN",
                            oauthToken.access_token, oauthToken.openid);
                        var userInfo = WeChatHttpUtility.Get<OAuthUserModel>(userInfoUrl);

                        if (userInfo != null||  !String.IsNullOrWhiteSpace(userInfo.openid))
                        {
                            var jsonData = JsonConvert.SerializeObject(userInfo);
                            try
                            {
                                var customer = _customerService.GetCustomerByOpenId(userInfo.openid);

                                #region 假设用户不存在
                                if (customer == null || customer.Id == 0) // 判定用户是否存在
                                {
                                    var salt = CommonHelper.GenerateCode(6);
                                    var _encryptionService = Abp.Dependency.IocManager.Instance.Resolve<IEncryptionService>();
                                    var password = _encryptionService.CreatePasswordHash("123456", salt);
                                    customer = new Customer
                                    {
                                        Mobile = "",
                                        Password = password,
                                        OpenId = userInfo.openid,
                                        CustomerRoleId = (int)CustomerRole.Buyer,
                                        NickName = userInfo.nickname,
                                        PasswordSalt = salt,
                                        IsSubscribe = true,
                                        CreationTime = DateTime.Now,
                                        LastModificationTime = DateTime.Now,
                                        VerificationCode = "",
                                        Agent = 0,
                                    };
                                    if (!String.IsNullOrWhiteSpace(customer.OpenId))
                                    {
                                        customer.Id = _customerService.CreateCustomer(customer);
                                    }
                                }
                                #endregion
                                else
                                {
                                    customer.NickName = userInfo.nickname;
                                    customer.SaveCustomerAttribute<string>(CustomerAttributeNames.Avatar, userInfo.headimgurl);
                                    customer.SaveCustomerAttribute<int>(CustomerAttributeNames.Sex, userInfo.sex);
                                    customer.NickName = userInfo.nickname;
                                    _customerService.UpdateCustomer(customer);
                                }
                                var dto = customer.MapTo<CustomerDto>();
                                var identity = _loginManager.CreateUserIdentity(dto);

                                //用户登录
                                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
                            }
                            catch (Exception e)
                            {
                                Logger.Debug("错误信息:" + e.Message);
                                Logger.Debug("错误内容：" + userInfo.openid + userInfo.nickname);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message);
            }
            return Redirect(Request.QueryString["state"]);
        }


        #endregion

        #region 
        /// <summary>
        /// 处理信息并应答
        /// </summary>
        [NonAction]
        private void Handle(string postStr)
        {
            WechatRequest wr = new WechatRequest(postStr);
            var eventStr = wr.LoadEvent(Logger);
            Hashtable parameters = new Hashtable();
            Customer customer = new Customer();
            Logger.Debug("eventStr" + eventStr);
            switch (eventStr.ToLower())
            {
                //case "scan"://关注
                case "subscribe":
                    parameters = wr.LoadXml();
                    //新用户
                    customer = NewCustomer(parameters);                    
                    break;
                case "unsubscribe":
                    parameters = wr.LoadXml();
                    //UnSubscribe(parameters);
                    //退订关注
                    break;
                case "text": //关键字
                    parameters = wr.LoadXml(false);
                    //KeywordReply(parameters);
                    break;
                default:
                    break;
            }            
        }
        #endregion

        #region Customer
        private Customer NewCustomer(Hashtable parameters)
        {
            var openId = parameters["FromUserName"].ToString();
            var customer = _customerService.GetCustomerByOpenId(openId);
            
            var code = CommonHelper.GenerateCode(6);

            if (customer == null || customer.Id == 0) // 判定用户是否存在
            {
                var _encryptionService = Abp.Dependency.IocManager.Instance.Resolve<IEncryptionService>();
                var password = _encryptionService.CreatePasswordHash("123456", code);
                customer = new Customer
                {
                    Mobile = "",
                    Password = password,
                    OpenId = openId,
                    CustomerRoleId = (int)CustomerRole.Buyer,
                    NickName = "",
                    PasswordSalt = code,
                    IsSubscribe = true,
                    CreationTime = DateTime.Now,
                    Agent = 0,
                };
                customer.Id = _customerService.CreateCustomer(customer);   
            }

            return customer;

        }
        #endregion
    }
}