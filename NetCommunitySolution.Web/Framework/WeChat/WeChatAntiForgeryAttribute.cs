using Abp.Runtime.Session;
using Castle.Core.Logging;
using NetCommunitySolution.CacheNames;
using NetCommunitySolution.Common;
using System;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Framework.WeChat
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class WeChatAntiForgeryAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var User_Agent = filterContext.RequestContext.HttpContext.Request.UserAgent;
            
            //判定是否微信打开
            if (!User_Agent.ToLower().Contains("micromessenger"))
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            
            var Logger = Abp.Dependency.IocManager.Instance.Resolve<ILogger>();
            var abpSession = Abp.Dependency.IocManager.Instance.Resolve<IAbpSession>();
                        
            //判定是否子方法
            if (filterContext.IsChildAction)
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            var customerId = Convert.ToInt32(abpSession.UserId);
            //判定当前访问用户是否在微信中打开
            if (customerId <= 0)
            {
                var _settingService = Abp.Dependency.IocManager.Instance.Resolve<ISettingService>();
                var appId = _settingService.GetSettingByKey<string>(WechatSettingNames.AppId);
                var resultPath = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect",
                    appId,
                    System.Web.HttpUtility.UrlEncode("http://" + filterContext.RequestContext.HttpContext.Request.Url.Host + "/WeChat/OAuth", System.Text.Encoding.UTF8),
                    filterContext.RequestContext.HttpContext.Request.Url);
                filterContext.Result = new RedirectResult(resultPath);
            }
        }
    }
}