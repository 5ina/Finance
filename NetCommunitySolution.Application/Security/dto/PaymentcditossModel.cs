namespace NetCommunitySolution.Security.dto
{
    /// <summary>
    /// 青联支付实体
    /// </summary>
    public class PaymentcditossModel: IParamater
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 商户编号
        /// </summary>
        public int customerid { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string sdorderno { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public double total_fee { get; set; }

        /// <summary>
        /// 支付编号 （默认bank)
        /// </summary>
        public string paytype { get; set; }

        /// <summary>
        /// 银行编号
        /// </summary>
        public string bankcode { get; set; }

        /// <summary>
        /// 异步通知地址
        /// </summary>
        public string notifyurl { get; set; }

        /// <summary>
        /// 同步跳转
        /// </summary>
        public string returnurl { get; set; }

        /// <summary>
        /// 订单备注说明
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 微信二维码
        /// </summary>
        public string get_code { get; set; }
    }
}
