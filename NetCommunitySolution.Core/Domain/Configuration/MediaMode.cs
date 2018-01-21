using System.ComponentModel;

namespace NetCommunitySolution.Domain.Configuration
{
    /// <summary>
    /// 媒体模式
    /// </summary>
    public enum MediaMode : int
    {
        /// <summary>
        /// 本地模式
        /// </summary>
        [Description("本地模式")]
        Local = 1,
        /// <summary>
        /// 服务器模式
        /// </summary>
        [Description("服务器模式")]
        Alyun = 10
    }
}
