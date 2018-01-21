using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Models.Orders
{
    public class UpgradeModel:EntityDto
    {
        public UpgradeModel()
        {
            this.AvailableBanks = new List<SelectListItem>();
        }

        /// <summary>
        /// 金额
        /// </summary>
        [DisplayName("代理费")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 银行卡
        /// </summary>
        [DisplayName("付款账户")]

        public int PaymentBankId { get; set; }

        public bool IsAuth { get; set; }

        [AllowHtml]
        [DisplayName("支付密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public IList<SelectListItem> AvailableBanks { get; set; }
    }
}