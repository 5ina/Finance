using Abp.Runtime.Caching;
using Castle.Core.Logging;
using NetCommunitySolution.CacheNames;
using NetCommunitySolution.Common;
using NetCommunitySolution.Web.Framework.WeChat.Dto;
using NetCommunitySolution.Web.Models.WeChat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetCommunitySolution.Web.Framework.WeChat
{

    public class WeChatDefault
    {
        public string WeChatUrl = "https://api.weixin.qq.com/cgi-bin/";


        public AccessTokenModel GetAccessToken(string appId, string appSecret, ICacheManager _cacheManager)
        {
            return WeChatHttpUtility.Get<AccessTokenModel>(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, appSecret));
        }
        public AccessTokenModel GetAccessToken(string appId, string appSecret)
        {
            var cacheManager = Abp.Dependency.IocManager.Instance.Resolve<ICacheManager>();
            return GetAccessToken(appId, appSecret, cacheManager);
        }

        public OAuthUserModel GetWeChatUserInfo(ISettingService _settingService, string code)
        {
            var appId = _settingService.GetSettingByKey<string>(WechatSettingNames.AppId);
            var appSecret = _settingService.GetSettingByKey<string>(WechatSettingNames.AppSecret);

            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    var oauthToken = WeChatHttpUtility.Get<OAuthTokenModel>(string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appId, appSecret, code));
                    if (!String.IsNullOrWhiteSpace(oauthToken.access_token))
                    {
                        var accessToken = GetAccessToken(appId, appSecret);
                        var userInfoUrl = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN",
                            oauthToken.access_token, oauthToken.openid);
                        var userInfo = WeChatHttpUtility.Get<OAuthUserModel>(userInfoUrl);

                        return userInfo;
                    }
                }
            }
            catch
            {
            }

            return null;
        }

        public OAuthUserModel GetWeChatUserInfo(ISettingService _settingService, ICacheManager cacheManager, string openId)
        {
            var appId = _settingService.GetSettingByKey<string>(WechatSettingNames.AppId);
            var appSecret = _settingService.GetSettingByKey<string>(WechatSettingNames.AppSecret);

            try
            {
                var Logger = Abp.Dependency.IocManager.Instance.Resolve<ILogger>();
                Logger.Debug("获取用户信息");

                var token = GetAccessToken(appId, appSecret);
                var userInfo = WeChatHttpUtility.Get<OAuthUserModel>(string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", token.access_token, openId), Logger);

                Logger.Debug(token.access_token + "|||" + userInfo.nickname + "|||" + userInfo.openid);
                return userInfo;
            }
            catch
            {
            }

            return null;
        }


        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="expire">有效时间</param>
        /// <param name="permanent">是否永久性二维码</param>
        /// <returns></returns>
        public string QRCode(ICacheManager cacheManager, string appId, string appSecret, int expire, bool permanent, int scene)
        {
            var access_token = GetAccessToken(appId, appSecret, cacheManager);
            var QRCode_Url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}";
            var url = string.Format(QRCode_Url, access_token.access_token);
            string data = string.Empty;
            if (permanent)
            {
                data = "{ \"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": { \"scene\": { \"scene_str\": \"" + scene + "\"} } }";
            }
            else
            {
                data = "{ \"expire_seconds\": " + expire + ", \"action_name\": \"QR_SCENE\", \"action_info\": { \"scene\": { \"scene_id\": " + scene + "} } }";
            }
            var result = WeChatHttpUtility.Post(url, data);
            var Logger = Abp.Dependency.IocManager.Instance.Resolve<ILogger>();
            Logger.Debug("返回的数据:" + result);
            var model = JsonConvert.DeserializeObject<QcScreenModel>(result);
            var ticket_url = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}";
            return string.Format(ticket_url, model.ticket);
        }


        public WxConfigModel WxConfig(ICacheManager _cacheManager,string appId,string appSecret, string url)
        {
            var token = GetAccessToken(appId, appSecret,_cacheManager);
            var tickent = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", token.access_token);

            JSApiTicketModel apiTicker = WeChatHttpUtility.Get<JSApiTicketModel>(tickent);

            var nonceStr = CommonHelper.GenerateNonceStr();
            var timestamp = CommonHelper.GetTimeStamp();

            var urlPath = "jsapi_ticket=" + apiTicker.ticket + "&noncestr=" + nonceStr + "&timestamp=" + timestamp + "&url=http://" + url;
            var signature = CommonHelper.EncryptToSHA1(urlPath);

            var jsonData = new WxConfigModel
            {
                appId = appId,
                timestamp = timestamp,
                noncestr = nonceStr,
                signature = signature,
                ticket = apiTicker.ticket,
                value = signature,
                urlPath = urlPath
            };
            return jsonData;

        }

        public bool SendMessage()
        {
            return true;
        }

        /// <summary>
        /// 二维码实体
        /// </summary>
        public class QcScreenModel
        {
            /// <summary>
            /// 获取的二维码ticket，凭借此ticket可以在有效时间内换取二维码。
            /// </summary>
            public string ticket { get; set; }
            /// <summary>
            /// 该二维码有效时间，以秒为单位。 最大不超过2592000（即30天）。
            /// </summary>
            public int expire_seconds { get; set; }
            /// <summary>
            /// 二维码图片解析后的地址，开发者可根据该地址自行生成需要的二维码图片
            /// </summary>
            public string url { get; set; }
        }

        /// <summary>
        /// ticket实体
        /// </summary>
        public class JSApiTicketModel
        {
            public string errcode { get; set; }
            public string errmsg { get; set; }
            public string ticket { get; set; }
            public int expires_in { get; set; }

            public override string ToString()
            {
                return this.ticket;
            }


        }

    }
}