using Abp.Runtime.Caching;
using Castle.Core.Logging;
using NetCommunitySolution.CacheNames;
using NetCommunitySolution.Common;
using NetCommunitySolution.Web.Models.WeChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetCommunitySolution.Web.Framework.WeChat
{

    public class WeChatDefault
    {
        public string WeChatUrl = "https://api.weixin.qq.com/cgi-bin/";


        public AccessTokenModel GetAccessToken(string appId, string appSecret)
        {
            return WeChatHttpUtility.Get<AccessTokenModel>(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, appSecret));

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
        

        public bool SendMessage()
        {
            return true;
        }

    }
}