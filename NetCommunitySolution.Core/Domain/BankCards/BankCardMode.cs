using System;
using System.ComponentModel;

namespace NetCommunitySolution.Domain.BankCards
{
    /// <summary>
    /// 银行卡模式
    /// </summary>
    public enum BankCardMode : Int16
    {
        /// <summary>
        /// 信用卡
        /// </summary>
        [Description("信用卡")]
        Credit = 1,
        /// <summary>
        /// 储蓄卡
        /// </summary>
        [Description("储蓄卡")]
        Deposit = 2
    } 
}
