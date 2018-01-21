using System.ComponentModel;

namespace NetCommunitySolution.Domain.Configuration
{
    /// <summary>
    /// 微信配置,用于服务层使用
    /// </summary>
    public class WechatSetting
    {

        /// <summary>
        /// 微信AppID
        /// </summary>
        [DisplayName("AppId")]
        public string AppId { get; set; }

        /// <summary>
        /// 微信AppSecret
        /// </summary>
        [DisplayName("Secret")]
        public string AppSecret { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        [DisplayName("Token")]
        public string Token { get; set; }
        /// <summary>
        /// 微信支付商户Id
        /// </summary>
        [DisplayName("商户ID")]
        public string MchId { get; set; }
        /// <summary>
        /// 微信支付秘钥Key
        /// </summary>
        [DisplayName("Key")]
        public string Key { get; set; }
        /// <summary>
        /// 回调Url
        /// </summary>
        [DisplayName("回调")]
        public string NotifyUrl { get; set; }

    }
}
