using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.BankCards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Models.BankCards
{
    /// <summary>
    /// 银行卡
    /// </summary>
    [AutoMap(typeof(BankCard))]
    public class BankCardModel :EntityDto
    {
        public BankCardModel()
        {
            this.AvailableBankCodes = new List<SelectListItem>();
        }

        /// <summary>
        /// 信用卡Or储蓄卡
        /// </summary>
        public BankCardMode BankCardMode { get { return (BankCardMode)this.BankCardModeId; } }


        /// <summary>
        /// 所属用户
        /// </summary>
        public int CustomerId { get; set; }

        [DisplayName("银行")]
        public string BankCode { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        [DisplayName("卡号")]
        public string No { get; set; }
        /// <summary>
        /// 持卡人姓名
        /// </summary>
        [DisplayName("持卡人")]
        public string Name { get; set; }

        [DisplayName("开户省份")]
        public string Province { get; set; }
        [DisplayName("开户城市")]
        public string City { get; set; }

        /// <summary>
        /// 所属银行
        /// </summary>
        public string Bank { get; set; }

        [DisplayName("手机号")]
        public string Mobile { get; set; }

        public int BankCardModeId { get; set; }

        public bool IsDeleted { get; set; }

        public IList<SelectListItem> AvailableBankCodes { get; set; }

    }
}