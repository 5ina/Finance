using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Customers;
using System;

namespace NetCommunitySolution.Web.Models.Customers
{
    [AutoMap(typeof(AccountLog))]
    public class AccountLogModel : EntityDto
    {
        /// <summary>
        /// 所属用户
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 变动模式
        /// </summary>
        public Int16 AccountModeId { get; set; }

        /// <summary>
        /// 变动模式
        /// </summary>
        /// 
        public AccountMode AccountMode { get { return (AccountMode)this.AccountModeId; } }

        /// <summary>
        /// 变动的金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 变动后的余额
        /// </summary>
        public decimal Balance { get; set; }
        public string Message { get; set; }
        public DateTime CreationTime { get; set; }
    }
}