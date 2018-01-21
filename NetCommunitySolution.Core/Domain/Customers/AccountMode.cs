using System.ComponentModel;

namespace NetCommunitySolution.Domain.Customers
{
    /// <summary>
    /// 账户变动模式
    /// </summary>
    public enum AccountMode:int
    {
        /// <summary>
        /// 套现
        /// </summary>
        [Description("套现")]
        Cash = 1,

        /// <summary>
        /// 代理
        /// </summary>
        [Description("代理")]
        Agent = 10,

        /// <summary>
        /// 佣金(不用于订单）
        /// </summary>
        [Description("佣金")]
        Income = 15,

        /// <summary>
        /// 结算
        /// </summary>
        [Description("结算")]
        Extract = 20
    }
}
