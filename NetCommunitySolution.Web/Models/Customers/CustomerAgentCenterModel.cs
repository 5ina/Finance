using Abp.Application.Services.Dto;

namespace NetCommunitySolution.Web.Models.Customers
{
    public class CustomerAgentCenterModel : EntityDto
    {
        /// <summary>
        /// 推广数量
        /// </summary>
        public int CustomerCount { get; set; }
        
        /// <summary>
        /// 交易总额度(本月)
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}