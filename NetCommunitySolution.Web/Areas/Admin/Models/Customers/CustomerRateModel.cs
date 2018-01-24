using Abp.Application.Services.Dto;
using System.ComponentModel;
using System.Web.Mvc;

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
        [AllowHtml]
        [DisplayName("交易费率")]
        public decimal Rate { get; set; }

        /// <summary>
        /// 单笔费用
        /// </summary>
        [AllowHtml]
        [DisplayName("单笔费用")]
        public decimal Payment { get; set; }

        /// <summary>
        /// 最低费用
        /// </summary>
        public decimal MinPayment { get; set; }
    }
}