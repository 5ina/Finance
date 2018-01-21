using System;
using System.ComponentModel;

namespace NetCommunitySolution.Domain.Orders
{
    public enum TransferStatus : Int16
    {
        /// <summary>
        /// 转账未成功
        /// </summary>
        [Description("转账未成功")]
        Fail = 0,
        /// <summary>
        /// 划账到系统商户余额成功
        /// </summary>
        [Description("划账成功")]
        Debit = 1,
        /// <summary>
        /// 商户通道一余额已到帐
        /// </summary>
        [Description("余额到账")]
        Arrival = 2,

        /// <summary>
        /// 商户通道一余额已提现(可能未到帐)
        /// </summary>
        [Description("余额已提现")]
        Withdrawals = 3,
        /// <summary>
        /// 商户通道一提现已到帐
        /// </summary>
        [Description("结束")]
        Complete = 4
    }
}
