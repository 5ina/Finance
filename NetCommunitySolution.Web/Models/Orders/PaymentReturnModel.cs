namespace NetCommunitySolution.Web.Models.Orders
{
    /// <summary>
    /// 支付返回信息实体
    /// </summary>
    public class PaymentReturnModel
    {

        /// <summary>
        /// 
        /// </summary>
        public int respCode { get; set; }
        /// <summary>
        /// 验签失败！
        /// </summary>
        public string respMsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string version { get; set; }
    }

}