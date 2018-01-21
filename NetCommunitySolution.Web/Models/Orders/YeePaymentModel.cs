using Abp.Application.Services.Dto;
using System.ComponentModel;

namespace NetCommunitySolution.Web.Models.Orders
{
    public class YeePaymentModel:EntityDto
    {
        /// <summary>
        /// 套现金额
        /// </summary>
        [DisplayName("套现金额")]
        public decimal Total { get; set; }

        public decimal Rate { get; set; }

        public decimal PaymentFee { get; set; }

        /// <summary>
        /// 是否易宝审核通过
        /// </summary>
        public bool IsAuth { get; set; }


        
    }
}