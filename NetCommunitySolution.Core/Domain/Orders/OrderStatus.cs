using System.ComponentModel;

namespace NetCommunitySolution.Domain.Orders
{
    public enum OrderStatus : int
    {
        /// <summary>
        /// 未支付
        /// </summary>
        [Description("未支付")]
        Pending = 10,
        /// <summary>
        /// 支付
        /// </summary>
        [Description("已支付")]
        Paid = 20,

        /// <summary>
        /// 套现到账
        /// </summary>
        [Description("已到账")]
        Arrival = 30,

        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Cancel = -10,

    }
}
