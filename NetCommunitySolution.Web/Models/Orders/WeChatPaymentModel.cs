namespace NetCommunitySolution.Web.Models.Orders
{
    public class WeChatPaymentModel
    {
        public string AppId { get; set; }
        public string TimeStamp { get; set; }

        public string Noncestr { get; set; }

        public string Signature { get; set; }

        public string OrderSn { get; set; }

        public string wxJsApiParam { get; set; }
    }
}