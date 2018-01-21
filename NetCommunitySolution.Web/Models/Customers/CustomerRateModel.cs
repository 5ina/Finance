using Abp.Application.Services.Dto;

namespace NetCommunitySolution.Web.Models.Customers
{
    public class CustomerRateModel : EntityDto
    {
        public int mch_Id { get; set; }

        /// <summary>
        /// 提现费
        /// </summary>
        public int Payment { get; set; }

        public decimal Rate { get; set; }        
    }
}