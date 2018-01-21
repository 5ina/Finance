using System.ComponentModel;

namespace NetCommunitySolution.Domain.Configuration
{
    /// <summary>
    /// 公共配置
    /// </summary>
    public class CommonSetting : ISetting
    {

        [DisplayName("网站名称")]
        public string Name { get; set; }
        [DisplayName("副标题")]
        public string Subtitle { get; set; }
        /// <summary>
        ///标题
        /// </summary>
        [DisplayName("标题")]
        public string Title { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        [DisplayName("关键字")]
        public string Keywords { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [DisplayName("说明")]
        public string Description { get; set; }

        /// <summary>
        /// 是否关闭
        /// </summary>
        [DisplayName("关闭网站")]
        public bool IsClose { get; set; }
        
    }
}
