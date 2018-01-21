using System.ComponentModel;

namespace NetCommunitySolution.Domain.Configuration
{
    /// <summary>
    /// 资金配置
    /// </summary>
    public class AccountSetting:ISetting
    {
        /// <summary>
        /// 基准费率(平台的费率）
        /// </summary>
        [DisplayName("基础费率")]
        public decimal BaseRate { get; set; }

        /// <summary>
        /// 代理商费率
        /// </summary>
        [DisplayName("代理商费率")]
        public decimal VendorRate { get; set; }
        /// <summary>
        /// 普通费率
        /// </summary>
        [DisplayName("普通费率")]
        public decimal CommonRate { get; set; }
        /// <summary>
        /// 用户费率
        /// </summary>
        [DisplayName("用户费率")]
        public decimal MemberRate { get; set; }

        /// <summary>
        /// 一笔需要的钱
        /// </summary>
        [DisplayName("代付金额")]
        public decimal Payment { get; set; }


        [DisplayName("支付Url")]
        public string PaymentUrl { get; set; }
        [DisplayName("回调地址")]
        public string CallBackUrl { get; set; }

        /// <summary>
        /// 代理商ID
        /// </summary>
        [DisplayName("代理商ID")]
        public string AppId { get; set; }

        /// <summary>
        /// 秘钥
        /// </summary>
        [DisplayName("秘钥")]

        public string AppSecret { get; set; }

        
        [DisplayName("代理费")]
        public decimal AgencyFee{ get; set; }

        [DisplayName("单笔限制")]
        public bool TradeLimit { get; set; }

        [DisplayName("单笔最高")]
        public decimal MaxTrade { get; set; }
        [DisplayName("单笔最低")]
        public decimal MinTrade { get; set; }
    }
}
