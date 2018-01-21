using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Models.Orders
{
    [AutoMap(typeof(Order))]
    public class OrderModel : EntityDto
    {
        public OrderModel()
        {
            this.AvailableCreditBanks = new List<SelectListItem>();
            this.AvailableDepositBanks = new List<SelectListItem>();
        }

        
        public string OrderSn { get; set; }

        public int CustomerId { get; set; }

        public int OrderStatusId { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        [DisplayName("借款金额")]
        public decimal OrderTotal { get; set; }
        /// <summary>
        /// 银行卡
        /// </summary>
        [DisplayName("借款账户")]

        public int PaymentBankId { get; set; }

        /// <summary>
        /// 收款账户
        /// </summary>
        [DisplayName("结算账户")]
        public int ReceivableId { get; set; }


        public bool IsDeleted { get; set; }


        public bool IsAuth { get; set; }

        [AllowHtml]
        [DisplayName("支付密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool HasPassword { get; set; }


        public IList<SelectListItem> AvailableCreditBanks { get; set; }
        public IList<SelectListItem> AvailableDepositBanks { get; set; }
    }
}