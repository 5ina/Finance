namespace NetCommunitySolution.Security.YeeDto
{
    public class ProfitTransferResultModel
    {
        /// <summary>
        /// 通信状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }

        public int sysmch_id { get; set; }

        public int type { get; set; }

        public string out_trade_no { get; set; }
        
        public int day { get; set; }

        public int trade_status { get; set; }
    }
}
