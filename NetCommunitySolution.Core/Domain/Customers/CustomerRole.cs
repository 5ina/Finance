using System.ComponentModel;

namespace NetCommunitySolution.Domain.Customers
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public enum CustomerRole : int
    {
        /// <summary>
        /// 系统管理员
        /// </summary>
        [Description("管理员")]
        System = 1,
        /// <summary>
        /// 代理商
        /// </summary>
        [Description("代理商")]
        Vendor = 2,
        /// <summary>
        /// 购买者
        /// </summary>
        [Description("注册用户")]
        Buyer = 3,
    }
}