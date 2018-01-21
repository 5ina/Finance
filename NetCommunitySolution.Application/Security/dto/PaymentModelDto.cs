namespace NetCommunitySolution.Security.dto
{
    public class PaymentModelDto: CommonParamaterModelDto,IParamater
    {
        /// <summary>
        /// 接口类型
        /// </summary>
        public string tranType { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public string orderAmount { get; set; }

        /// <summary>
        /// 订单标题
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// 订单描述
        /// </summary>
        public string desc { get; set; }

        /// <summary>
        /// 订单编码
        /// </summary>
        public string merOrderNo { get; set; }

        /// <summary>
        /// 前台跳转地址
        /// </summary>
        public string frontUrl { get; set; }

        /// <summary>
        /// 后台通知地址
        /// </summary>
        public string backUrl { get; set; }

        /// <summary>
        /// 交易手续费
        /// </summary>
        public decimal tradeRate { get; set; }

        /// <summary>
        /// 支付手续费
        /// </summary>
        public decimal drawFee { get; set; }

        /// <summary>
        /// 支付卡卡号
        /// </summary>
        public string payBankCard { get; set; }

        /// <summary>
        /// 支付卡联行号
        /// </summary>
        public string payBankCode { get; set; }

        /// <summary>
        /// 支付卡预留手机号
        /// </summary>
        public string payPreMobile { get; set; }

        /// <summary>
        /// 支付卡人名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 支付卡人身份证号
        /// </summary>
        public string idcard { get; set; }

        /// <summary>
        /// 储蓄卡账号
        /// </summary>
        public string bankCard { get; set; }

        /// <summary>
        /// 储蓄卡联行号
        /// </summary>
        public string bankCode { get; set; }

        /// <summary>
        /// 储蓄卡手机号
        /// </summary>
        public string preMobile { get; set; }

        /// <summary>
        /// 储蓄卡开户省
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 储蓄卡开户城市
        /// </summary>
        public string city { get; set; }
    }
}
