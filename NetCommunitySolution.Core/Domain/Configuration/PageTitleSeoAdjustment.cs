using System.ComponentModel;

namespace NetCommunitySolution.Domain.Configuration
{
    public enum PageTitleSeoAdjustment
    {
        /// <summary>
        /// 页面名称在后
        /// </summary>
        [Description("页面名称在后")]
        PagenameAfterStorename = 0,
        /// <summary>
        /// 页面名称在前
        /// </summary>
        [Description("页面名称在前")]
        StorenameAfterPagename = 10
    }
}
