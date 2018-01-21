using System.ComponentModel;

namespace NetCommunitySolution.Domain.Configuration
{
    /// <summary>
    /// Seo配置
    /// </summary>
    public class SeoSetting : ISetting
    {
        /// <summary>
        /// 标题分隔符
        /// </summary>
        [DisplayName("标题分隔符")]
        public string PageTitleSeparator { get; set; }
        /// <summary>
        /// 页面名称
        /// </summary>
        [DisplayName("页面名称")]
        public PageTitleSeoAdjustment PageTitleSeoAdjustment { get; set; }
        /// <summary>
        /// 默认标题
        /// </summary>
        [DisplayName("默认标题")]
        public string DefaultTitle { get; set; }
        /// <summary>
        /// 默认关键字
        /// </summary>
        [DisplayName("默认关键字")]
        public string DefaultMetaKeywords { get; set; }
        /// <summary>
        /// 默认说明
        /// </summary>
        [DisplayName("默认说明")]
        public string DefaultMetaDescription { get; set; }
        /// <summary>
        /// 西方字符
        /// </summary>
        [DisplayName("西方字符")]
        public bool ConvertNonWesternChars { get; set; }
        /// <summary>
        /// 允许unicode
        /// </summary>
        [DisplayName("允许unicode")]
        public bool AllowUnicodeCharsInUrls { get; set; }
        /// <summary>
        /// 规范URL标记
        /// </summary>
        [DisplayName("规范URL标记")]
        public bool CanonicalUrlsEnabled { get; set; }
        /// <summary>
        /// Slugs 限制 ,隔开
        /// </summary>
        [DisplayName("Slugs限制")]
        public string ReservedUrlRecordSlugs { get; set; }
        /// <summary>
        /// 自定义标签
        /// </summary>
        [DisplayName("自定义标签")]
        public string CustomHeadTags { get; set; }


        [DisplayName("压缩js")]
        public bool EnableJsBundling { get; set; }
        [DisplayName("压缩css")]
        public bool EnableCssBundling { get; set; }


    }
}
