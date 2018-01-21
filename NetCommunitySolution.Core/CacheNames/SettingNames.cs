namespace NetCommunitySolution.CacheNames
{
    /// <summary>
    /// 公共配置
    /// </summary>
    public class CommonSettingNames
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        public static string Name { get { return "net.setting.common.name"; } }
        /// <summary>
        /// 副标题
        /// </summary>
        public static string Subtitle { get { return "net.setting.common.subtitle"; } }
        /// <summary>
        /// 项目Title
        /// </summary>
        public static string Title { get { return "net.setting.common.title"; } }

        /// <summary>
        /// 项目Keywords
        /// </summary>
        public static string Keywords { get { return "net.setting.common.keywords"; } }

        /// <summary>
        /// 项目Description
        /// </summary>
        public static string Description { get { return "net.setting.common.description"; } }
        /// <summary>
        /// 是否关闭
        /// </summary>
        public static string IsClose { get { return "net.setting.common.isclose"; } }
        /// <summary>
        /// 是否关闭
        /// </summary>
        public static string IsOpenRegistration { get { return "net.setting.common.isopenregistation"; } }
        /// <summary>
        /// 注册模式
        /// </summary>
        public static string RegistrationId { get { return "net.setting.common.registrationid"; } }
        

    }

    /// <summary>
    /// 用户配置
    /// </summary>
    public class CustomerSettingNames
    {
        /// <summary>
        /// 是否允许修改昵称
        /// </summary>
        public static string ModifyNickName { get { return "net.setting.customer.modify.nickname"; } }
        
        /// <summary>
        /// 密码最大长度
        /// </summary>
        public static string PasswordMaxLength { get { return "net.setting.customer.passwordmaxlength"; } }

        /// <summary>
        /// 密码最小长度
        /// </summary>
        public static string PasswordMinLength { get { return "net.setting.customer.passwordminlength"; } }

        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public static string EnabledCaptcha { get { return "net.setting.customer.enabledcaptcha"; } }
        /// <summary>
        /// 验证码长度
        /// </summary>
        public static string CaptchaLength { get { return "net.setting.customer.captchalength"; } }

        /// <summary>
        /// 验证码名称
        /// </summary>
        public static string CaptchaName { get { return "net.setting.customer.captchaname"; } }


    }

    /// <summary>
    /// 帖子配置
    /// </summary>
    public class PostSettingNames
    {
        /// <summary>
        /// 热门帖子个数
        /// </summary>
        public static string HotPostsCount { get { return "net.setting.post.hotpostscount"; } }

        /// <summary>
        /// 帖子分页个数
        /// </summary>
        public static string PostPageSize { get { return "net.setting.post.pagesize"; } }
    }


    /// <summary>
    /// 帖子配置
    /// </summary>
    public class MediaSettingNames
    {
        /// <summary>
        /// 是否启用头像
        /// </summary>
        public static string EnabledAvatar { get { return "net.setting.media.enabledavatar"; } }

        /// <summary>
        /// 媒体存储模式
        /// </summary>
        public static string MediaMode { get { return "net.setting.media.mediamode"; } }


        /// <summary>
        /// 头像最大限制
        /// </summary>
        public static string MaxAvatarSize { get { return "net.setting.media.maxavatarsize"; } }

        /// <summary>
        /// 头像图像文件类型
        /// </summary>
        public static string AvatarFile { get { return "net.setting.media.avatarfile"; } }


        /// <summary>
        /// 头像图像文件类型
        /// </summary>
        public static string AccessKeyId { get { return "net.setting.media.accesskeyid"; } }
        /// <summary>
        /// 头像图像文件类型
        /// </summary>
        public static string AccessKeySecret { get { return "net.setting.media.accesskeysecret"; } }
        /// <summary>
        /// 头像图像文件类型
        /// </summary>
        public static string Endpoint { get { return "net.setting.media.endpoint"; } }
        /// <summary>
        /// bucket
        /// </summary>
        public static string Bucket { get { return "net.setting.media.bucket"; } }
    }

    /// <summary>
    /// 积分配置
    /// </summary>
    public class RewardPointSettingNames
    {
        public static string Enabled { get { return "net.setting.reward.enable"; } }
        
        public static string NewPost { get { return "net.setting.reward.newpost"; } }
        
        public static string Comment { get { return "net.setting.reward.comment"; } }
        
        public static string Solve { get { return "net.setting.reward.solve"; } }
        
        public static string Selected { get { return "net.setting.reward.selected"; } }
        
        public static string DayMaxReward { get { return "net.setting.reward.daymaxreward"; } }
    }


    /// <summary>
    /// 微信配置,用于服务层使用
    /// </summary>
    public class WechatSettingNames
    {

        /// <summary>
        /// 微信AppID
        /// </summary>
        public static string AppId { get { return "net.setting.wechat.appid"; } }

        /// <summary>
        /// 微信AppSecret
        /// </summary>
        public static string AppSecret { get { return "net.setting.wechat.appsecret"; } }
        /// <summary>
        /// Token
        /// </summary>
        public static string Token { get { return "net.setting.wechat.token"; } }
        /// <summary>
        /// 微信支付商户Id
        /// </summary>
        public static string MchId { get { return "net.setting.wechat.mchid"; } }
        /// <summary>
        /// 微信支付秘钥Key
        /// </summary>
        public static string Key { get { return "net.setting.wechat.key"; } }
        /// <summary>
        /// 回调Url
        /// </summary>
        public static string NotifyUrl { get { return "net.setting.wechat.notifyurl"; } }

    }

    /// <summary>
    /// seo配置
    /// </summary>
    public class SeoSettingNames
    {
        /// <summary>
        /// 标题分隔符
        /// </summary>
        public static string PageTitleSeparator { get { return "net.setting.seo.pagetitleseparator"; } }
        /// <summary>
        /// 页面名称
        /// </summary>
        public static string PageTitleSeoAdjustment { get { return "net.setting.seo.pagetitleseoadjustment"; } }
        /// <summary>
        /// 默认标题
        /// </summary>
        public static string DefaultTitle { get { return "net.setting.seo.defaulttitle"; } }
        /// <summary>
        /// 默认关键字
        /// </summary>
        public static string DefaultMetaKeywords { get { return "net.setting.seo.defaultmetakeywords"; } }
        /// <summary>
        /// 默认说明
        /// </summary>
        public static string DefaultMetaDescription { get { return "net.setting.seo.defaultmetadescription"; } }        
        /// <summary>
        /// 西方字符
        /// </summary>
        public static string ConvertNonWesternChars { get { return "net.setting.seo.convertnonwesternchars"; } }
        /// <summary>
        /// 允许unicode
        /// </summary>
        public static string AllowUnicodeCharsInUrls { get { return "net.setting.seo.allowunicode"; } }
        /// <summary>
        /// 规范URL标记
        /// </summary>
        public static string CanonicalUrlsEnabled { get { return "net.setting.seo.canonicalurl"; } }
        /// <summary>
        /// Slugs 限制
        /// </summary>
        public static string ReservedUrlRecordSlugs { get { return "net.setting.seo.reservedurlrecordslugs"; } }
        /// <summary>
        /// 自定义标签
        /// </summary>
        public static string CustomHeadTags { get { return "net.setting.seo.customheadtags"; } }
        public static string EnableJsBundling { get { return "net.setting.seo.enablejsbundling"; } }
        public static string EnableCssBundling { get { return "net.setting.seo.enablecssbundling"; } }


    }


    /// <summary>
    /// 费率配置
    /// </summary>
    public class AccountSettingNames 
    {
        /// <summary>
        /// 基准费率
        /// </summary>
        public static string BaseRate { get { return "net.setting.rate.baserate"; } }

        /// <summary>
        /// 代理商费率
        /// </summary>
        public static string VendorRate { get { return "net.setting.rate.vendorrate"; } }

        /// <summary>
        /// 用户费率
        /// </summary>
        public static string CommonRate { get { return "net.setting.rate.commonrate"; } }


        /// <summary>
        /// 用户费率
        /// </summary>
        public static string MemberRate { get { return "net.setting.rate.memberrate"; } }

        /// <summary>
        /// 代付费用
        /// </summary>
        public static string Payment { get { return "net.setting.rate.payment"; } }


        /// <summary>
        /// 支付接口URL
        /// </summary>
        public static string PaymentUrl { get { return "net.setting.rate.paymenturl"; } }
        /// <summary>
        /// 回调地址
        /// </summary>
        public static string CallBackUrl { get { return "net.setting.rate.callbackurl"; } }
        /// <summary>
        /// 商户ID
        /// </summary>
        public static string AppId { get { return "net.setting.rate.appid"; } }
        /// <summary>
        /// 商户ID
        /// </summary>
        public static string AppSecret { get { return "net.setting.rate.secret"; } }
        
        /// <summary>
        /// 代理费
        /// </summary>
        public static string AgencyFee { get { return "net.setting.rate.agencyfee"; } }

        /// <summary>
        /// 单笔限制
        /// </summary>
        public static string TradeLimit { get { return "net.setting.rate.tradelimit"; } }

        /// <summary>
        /// 单笔最高
        /// </summary>

        public static string MaxTrade { get { return "net.setting.rate.maxtrade"; } }
        /// <summary>
        /// 单笔最低
        /// </summary>

        public static string MinTrade { get { return "net.setting.rate.mintrade"; } }
    }
}
