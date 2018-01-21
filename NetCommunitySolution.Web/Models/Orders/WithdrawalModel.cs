using System.ComponentModel;

namespace NetCommunitySolution.Web.Models.Orders
{
    public class WithdrawalModel
    {
        public decimal MaxTotal { get; set; }        

        public decimal MinTotal { get; set; }

        [DisplayName("提现金额")]
        public decimal Total { get; set; }

        public bool IsAgent { get; set; }
    }
}