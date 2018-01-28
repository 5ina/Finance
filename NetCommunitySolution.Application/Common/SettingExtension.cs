using NetCommunitySolution.CacheNames;
using NetCommunitySolution.Domain.Configuration;
using System;

namespace NetCommunitySolution.Common
{
    /// <summary>
    /// 配置的扩展方法
    /// </summary>
    public static class SettingExtension
    {
        /// <summary>
        /// 获取公共配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static CommonSetting GetCommonSettings(this ISettingService _settingService)
        {

            var config = new CommonSetting
            {
                Description = _settingService.GetSettingByKey<string>(CommonSettingNames.Description),
                Title = _settingService.GetSettingByKey<string>(CommonSettingNames.Title),
                Keywords = _settingService.GetSettingByKey<string>(CommonSettingNames.Keywords),
                IsClose = _settingService.GetSettingByKey<bool>(CommonSettingNames.IsClose),
                Name = _settingService.GetSettingByKey<string>(CommonSettingNames.Name),
                Subtitle = _settingService.GetSettingByKey<string>(CommonSettingNames.Subtitle),
            };
            return config;
        }

        /// <summary>
        /// 存储公共配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <param name="setting"></param>
        public static void SaveCommonSettings(this ISettingService _settingService, CommonSetting setting)
        {
            _settingService.SaveSetting(CommonSettingNames.Description, setting.Description);
            _settingService.SaveSetting(CommonSettingNames.Title, setting.Title);
            _settingService.SaveSetting(CommonSettingNames.Keywords, setting.Keywords);
            _settingService.SaveSetting(CommonSettingNames.IsClose, setting.IsClose);
            _settingService.SaveSetting(CommonSettingNames.Name, setting.Name);
            _settingService.SaveSetting(CommonSettingNames.Subtitle, setting.Subtitle);
        }

        /// <summary>
        /// 获取用户配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static CustomerSetting GetCustomerSettings(this ISettingService _settingService)
        {

            var config = new CustomerSetting
            {
                EnabledCaptcha = _settingService.GetSettingByKey<bool>(CustomerSettingNames.EnabledCaptcha),
                ModifyNickName = _settingService.GetSettingByKey<bool>(CustomerSettingNames.ModifyNickName),
                PasswordMaxLength = _settingService.GetSettingByKey<int>(CustomerSettingNames.PasswordMaxLength),
                PasswordMinLength = _settingService.GetSettingByKey<int>(CustomerSettingNames.PasswordMinLength),
                CaptchaLength = _settingService.GetSettingByKey<int>(CustomerSettingNames.CaptchaLength),
                CaptchaName = _settingService.GetSettingByKey<string>(CustomerSettingNames.CaptchaName),
            };
            return config;
        }


        /// <summary>
        /// 存储用户配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <param name="setting"></param>
        public static void SaveCustomerSettings(this ISettingService _settingService, CustomerSetting setting)
        {
            _settingService.SaveSetting(CustomerSettingNames.EnabledCaptcha, setting.EnabledCaptcha);
            _settingService.SaveSetting(CustomerSettingNames.ModifyNickName, setting.ModifyNickName);
            _settingService.SaveSetting(CustomerSettingNames.PasswordMaxLength, setting.PasswordMaxLength);
            _settingService.SaveSetting(CustomerSettingNames.PasswordMinLength, setting.PasswordMinLength);
            _settingService.SaveSetting(CustomerSettingNames.CaptchaName, setting.CaptchaName);
            _settingService.SaveSetting(CustomerSettingNames.CaptchaLength, setting.CaptchaLength);
        }


        /// <summary>
        /// 获取帖子配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static PostSetting GetPostSettings(this ISettingService _settingService)
        {

            var config = new PostSetting
            {
                PostPageSize = _settingService.GetSettingByKey<int>(PostSettingNames.PostPageSize),
                HotPostsCount = _settingService.GetSettingByKey<int>(PostSettingNames.HotPostsCount),
            };
            return config;
        }


        /// <summary>
        /// 存储贴子配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <param name="setting"></param>
        public static void SavePostSettings(this ISettingService _settingService, PostSetting setting)
        {
            _settingService.SaveSetting(PostSettingNames.HotPostsCount, setting.HotPostsCount);
            _settingService.SaveSetting(PostSettingNames.PostPageSize, setting.PostPageSize);
        }

        
        /// <summary>
        /// 获取媒体配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static MediaSetting GetMediaSettings(this ISettingService _settingService)
        {

            var config = new MediaSetting
            {
                AvatarFile = _settingService.GetSettingByKey<string>(MediaSettingNames.AvatarFile),
                EnabledAvatar = _settingService.GetSettingByKey<bool>(MediaSettingNames.EnabledAvatar),
                MaxAvatarSize = _settingService.GetSettingByKey<int>(MediaSettingNames.MaxAvatarSize),
                MediaMode = _settingService.GetSettingByKey<MediaMode>(MediaSettingNames.MediaMode),
                AccessKeyId = _settingService.GetSettingByKey<string>(MediaSettingNames.AccessKeyId),
                AccessKeySecret = _settingService.GetSettingByKey<string>(MediaSettingNames.AccessKeySecret),
                Bucket = _settingService.GetSettingByKey<string>(MediaSettingNames.Bucket),
                Endpoint = _settingService.GetSettingByKey<string>(MediaSettingNames.Endpoint),
            };
            return config;
        }


        /// <summary>
        /// 存储媒体配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <param name="setting"></param>
        public static void SaveMediaSettings(this ISettingService _settingService, MediaSetting setting)
        {
            _settingService.SaveSetting(MediaSettingNames.AvatarFile, setting.AvatarFile);
            _settingService.SaveSetting(MediaSettingNames.EnabledAvatar, setting.EnabledAvatar);
            _settingService.SaveSetting(MediaSettingNames.MaxAvatarSize, setting.MaxAvatarSize);
            _settingService.SaveSetting(MediaSettingNames.MediaMode, setting.MediaMode);
            _settingService.SaveSetting(MediaSettingNames.AccessKeySecret, setting.AccessKeySecret);
            _settingService.SaveSetting(MediaSettingNames.AccessKeyId, setting.AccessKeyId);
            _settingService.SaveSetting(MediaSettingNames.Bucket, setting.Bucket);
            _settingService.SaveSetting(MediaSettingNames.Endpoint, setting.Endpoint);
        }



        /// <summary>
        /// 获取媒体配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static RewardPointSetting GetRewardSettings(this ISettingService _settingService)
        {

            var config = new RewardPointSetting
            {
                Enabled = _settingService.GetSettingByKey<bool>(RewardPointSettingNames.Enabled),
                Comment = _settingService.GetSettingByKey<int>(RewardPointSettingNames.Comment),
                NewPost = _settingService.GetSettingByKey<int>(RewardPointSettingNames.NewPost),
                Selected = _settingService.GetSettingByKey<int>(RewardPointSettingNames.Selected),
                Solve = _settingService.GetSettingByKey<int>(RewardPointSettingNames.Solve),
                DayMaxReward = _settingService.GetSettingByKey<int>(RewardPointSettingNames.DayMaxReward),                
            };
            return config;
        }


        /// <summary>
        /// 存储媒体配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <param name="setting"></param>
        public static void SaveRewardSettings(this ISettingService _settingService, RewardPointSetting setting)
        {
            _settingService.SaveSetting(RewardPointSettingNames.Enabled, setting.Enabled);
            _settingService.SaveSetting(RewardPointSettingNames.Comment, setting.Comment);
            _settingService.SaveSetting(RewardPointSettingNames.NewPost, setting.NewPost);
            _settingService.SaveSetting(RewardPointSettingNames.Selected, setting.Selected);
            _settingService.SaveSetting(RewardPointSettingNames.Solve, setting.Solve);
            _settingService.SaveSetting(RewardPointSettingNames.DayMaxReward, setting.DayMaxReward);
        }

        
        /// <summary>
        /// 微信配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static WechatSetting GetWeChatSettings(this ISettingService _settingService)
        {

            var config = new WechatSetting
            {
                AppId = _settingService.GetSettingByKey<string>(WechatSettingNames.AppId),
                AppSecret = _settingService.GetSettingByKey<string>(WechatSettingNames.AppSecret),
                Key = _settingService.GetSettingByKey<string>(WechatSettingNames.Key),
                MchId = _settingService.GetSettingByKey<string>(WechatSettingNames.MchId),
                NotifyUrl = _settingService.GetSettingByKey<string>(WechatSettingNames.NotifyUrl),
                Token = _settingService.GetSettingByKey<string>(WechatSettingNames.Token),
                Expire = _settingService.GetSettingByKey<int>(WechatSettingNames.Expire),
            };
            return config;
        }


        /// <summary>
        /// 存储微信配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <param name="setting"></param>
        public static void SaveWechatSettings(this ISettingService _settingService, WechatSetting setting)
        {            
            _settingService.SaveSetting(WechatSettingNames.AppId, setting.AppId);
            _settingService.SaveSetting(WechatSettingNames.AppSecret, setting.AppSecret);
            _settingService.SaveSetting(WechatSettingNames.Key, setting.Key);
            _settingService.SaveSetting(WechatSettingNames.MchId, setting.MchId);
            _settingService.SaveSetting(WechatSettingNames.NotifyUrl, setting.NotifyUrl);
            _settingService.SaveSetting(WechatSettingNames.Token, setting.Token);
            _settingService.SaveSetting(WechatSettingNames.Expire, setting.Expire);
        }



        /// <summary>
        /// 微信配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static SeoSetting GetSeoSetting(this ISettingService _settingService)
        {

            var config = new SeoSetting
            {
                AllowUnicodeCharsInUrls = _settingService.GetSettingByKey<bool>(SeoSettingNames.AllowUnicodeCharsInUrls),
                CanonicalUrlsEnabled = _settingService.GetSettingByKey<bool>(SeoSettingNames.CanonicalUrlsEnabled),
                ConvertNonWesternChars = _settingService.GetSettingByKey<bool>(SeoSettingNames.ConvertNonWesternChars),
                ReservedUrlRecordSlugs = _settingService.GetSettingByKey<string>(SeoSettingNames.ReservedUrlRecordSlugs),
                CustomHeadTags = _settingService.GetSettingByKey<string>(SeoSettingNames.CustomHeadTags),
                PageTitleSeparator = _settingService.GetSettingByKey<string>(SeoSettingNames.PageTitleSeparator),
                DefaultMetaDescription = _settingService.GetSettingByKey<string>(SeoSettingNames.DefaultMetaDescription),
                DefaultMetaKeywords = _settingService.GetSettingByKey<string>(SeoSettingNames.DefaultMetaKeywords),
                DefaultTitle = _settingService.GetSettingByKey<string>(SeoSettingNames.DefaultTitle),
                PageTitleSeoAdjustment = _settingService.GetSettingByKey<PageTitleSeoAdjustment>(SeoSettingNames.PageTitleSeoAdjustment),
                EnableCssBundling = _settingService.GetSettingByKey<bool>(SeoSettingNames.EnableCssBundling),
                EnableJsBundling = _settingService.GetSettingByKey<bool>(SeoSettingNames.EnableJsBundling),
            };
            return config;
        }


        /// <summary>
        /// 存储微信配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <param name="setting"></param>
        public static void SaveSeoSetting(this ISettingService _settingService, SeoSetting setting)
        {
            _settingService.SaveSetting(SeoSettingNames.AllowUnicodeCharsInUrls, setting.AllowUnicodeCharsInUrls);
            _settingService.SaveSetting(SeoSettingNames.CanonicalUrlsEnabled, setting.CanonicalUrlsEnabled);
            _settingService.SaveSetting(SeoSettingNames.ConvertNonWesternChars, setting.ConvertNonWesternChars);

            var customHeadTags = setting.CustomHeadTags;
            if (!String.IsNullOrWhiteSpace(customHeadTags))
                _settingService.SaveSetting(SeoSettingNames.CustomHeadTags, customHeadTags);


            var defaultMetaDescription = setting.DefaultMetaDescription;
            if (!String.IsNullOrWhiteSpace(defaultMetaDescription))
                _settingService.SaveSetting(SeoSettingNames.DefaultMetaDescription, defaultMetaDescription);
            

            var defaultMetaKeywords = setting.DefaultMetaKeywords;
            if (!String.IsNullOrWhiteSpace(defaultMetaKeywords))
                _settingService.SaveSetting(SeoSettingNames.DefaultMetaKeywords, defaultMetaKeywords);

            var defaultTitle = setting.DefaultTitle;
            if (!String.IsNullOrWhiteSpace(defaultTitle))
                _settingService.SaveSetting(SeoSettingNames.DefaultTitle, defaultTitle);
         
            _settingService.SaveSetting(SeoSettingNames.PageTitleSeoAdjustment, setting.PageTitleSeoAdjustment);

            var pageTitleSeparator = setting.PageTitleSeparator;
            if (!String.IsNullOrWhiteSpace(pageTitleSeparator))
                _settingService.SaveSetting(SeoSettingNames.PageTitleSeparator, pageTitleSeparator);
            
            var reservedUrlRecordSlugs = setting.ReservedUrlRecordSlugs;
            if (!String.IsNullOrWhiteSpace(reservedUrlRecordSlugs))
                _settingService.SaveSetting(SeoSettingNames.ReservedUrlRecordSlugs, reservedUrlRecordSlugs);
            
        }



        /// <summary>
        /// 获取费率配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static AccountSetting GetAccountSettings(this ISettingService _settingService)
        {

            var config = new AccountSetting
            {
                BaseRate = _settingService.GetSettingByKey<decimal>(AccountSettingNames.BaseRate),
                MemberRate = _settingService.GetSettingByKey<decimal>(AccountSettingNames.MemberRate),
                CommonRate = _settingService.GetSettingByKey<decimal>(AccountSettingNames.CommonRate),
                VendorRate = _settingService.GetSettingByKey<decimal>(AccountSettingNames.VendorRate),
                Payment = _settingService.GetSettingByKey<decimal>(AccountSettingNames.Payment),
                AgencyFee = _settingService.GetSettingByKey<decimal>(AccountSettingNames.AgencyFee),
                TradeLimit = _settingService.GetSettingByKey<bool>(AccountSettingNames.TradeLimit),
                MaxTrade = _settingService.GetSettingByKey<decimal>(AccountSettingNames.MaxTrade),
                MinTrade = _settingService.GetSettingByKey<decimal>(AccountSettingNames.MinTrade),
                AppId = _settingService.GetSettingByKey<string>(AccountSettingNames.AppId),
                AppSecret = _settingService.GetSettingByKey<string>(AccountSettingNames.AppSecret),
                PaymentUrl = _settingService.GetSettingByKey<string>(AccountSettingNames.PaymentUrl),
                CallBackUrl = _settingService.GetSettingByKey<string>(AccountSettingNames.CallBackUrl),
            };
            return config;
        }


        /// <summary>
        /// 存储媒体配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <param name="setting"></param>
        public static void SaveAccountSettings(this ISettingService _settingService, AccountSetting setting)
        {
            _settingService.SaveSetting(AccountSettingNames.BaseRate, setting.BaseRate);
            _settingService.SaveSetting(AccountSettingNames.MemberRate, setting.MemberRate);
            _settingService.SaveSetting(AccountSettingNames.VendorRate, setting.VendorRate);
            _settingService.SaveSetting(AccountSettingNames.CommonRate, setting.CommonRate);
            _settingService.SaveSetting(AccountSettingNames.Payment, setting.Payment);
            _settingService.SaveSetting(AccountSettingNames.PaymentUrl, setting.PaymentUrl);
            _settingService.SaveSetting(AccountSettingNames.CallBackUrl, setting.CallBackUrl);
            _settingService.SaveSetting(AccountSettingNames.AgencyFee, setting.AgencyFee);
            _settingService.SaveSetting(AccountSettingNames.TradeLimit, setting.TradeLimit);
            _settingService.SaveSetting(AccountSettingNames.MaxTrade, setting.MaxTrade);
            _settingService.SaveSetting(AccountSettingNames.MinTrade, setting.MinTrade);
            if (!String.IsNullOrWhiteSpace(setting.AppId))
                _settingService.SaveSetting(AccountSettingNames.AppId, setting.AppId);
            if (!String.IsNullOrWhiteSpace(setting.AppSecret))
                _settingService.SaveSetting(AccountSettingNames.AppSecret, setting.AppSecret);
        }
    }
}

