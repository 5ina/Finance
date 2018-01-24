using Abp.Application.Services.Dto;

namespace NetCommunitySolution.Web.Areas.Admin.Models.Customers
{
    public class CustomerRateModel:EntityDto
    {
        /// <summary>
        /// 最低费率
        /// </summary>
        public decimal MinRate { get; set; }
        /// <summary>
        /// 费率
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// 单笔费用
        /// </summary>
        public decimal Payment { get; set; }

        /// <summary>
        /// 最低费用
        /// </summary>
        public decimal MinPayment { get; set; }
    }
}