namespace NetCommunitySolution.Security.YeeDto
{
    public class PaymentResultModel
    {

        public int status { get; set; }
        public string msg { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        public string request_id { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 跳转Url
        /// </summary>
        public string url { get; set; }
    }
}
