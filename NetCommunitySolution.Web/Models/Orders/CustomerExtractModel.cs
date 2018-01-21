using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Models.Orders
{
    /// <summary>
    /// 提现实体
    /// </summary>
    public class CustomerExtractModel : EntityDto
    {
        public CustomerExtractModel()
        {
            this.AvailableDepositBanks = new List<SelectListItem>();
        }

        /// <summary>
        /// 是否认证
        /// </summary>
        public bool IsAuth { get; set; }

        /// <summary>
        /// 最大金额（提现）
        /// </summary>
        public decimal MaxAccount { get; set; }


        [DisplayName("提现金额")]
        public decimal Account { get; set; }

        /// <summary>
        /// 银行卡
        /// </summary>
        [DisplayName("银行卡")]
        public int BackId { get; set; }
        [AllowHtml]
        [DisplayName("支付密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IList<SelectListItem> AvailableDepositBanks { get; set; }
    }
}