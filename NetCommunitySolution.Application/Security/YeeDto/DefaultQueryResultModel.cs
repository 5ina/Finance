namespace NetCommunitySolution.Security.YeeDto
{
    public class DefaultQueryResultModel
    {
        public string status { get; set; }

        /// <summary>
        /// 通道一的默认交易费率
        /// </summary>
        public decimal def_rate { get; set; }

        /// <summary>
        /// 通道一默认提现费率
        /// </summary>
        public int def_transferrate { get; set; }
        /// <summary>
        /// 通道一的默认交易费率
        /// </summary>
        public decimal def_rate7 { get; set; }

        /// <summary>
        /// 通道一默认提现费率
        /// </summary>
        public int def_transferrate7 { get; set; }

        /// <summary>
        /// 通道二默认交易费率
        /// </summary>
        public decimal tf_def_rate { get; set; }
        /// <summary>
        /// 通道二默认提现费率
        /// </summary>
        public int tf_def_transferrate { get; set; }
        public string msg { get; set; }

        public string sgin { get; set; }
    }
}
