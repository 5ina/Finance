using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace NetCommunitySolution.Domain.Customers
{
    /// <summary>
    /// 用户账户日志
    /// </summary>
    public class AccountLog:Entity, IHasCreationTime
    {
        /// <summary>
        /// 所属用户
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 账户余额变动
        /// </summary>
        public Int16 AccountModeId { get; set; }

        /// <summary>
        /// 变动的金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 变动后的余额
        /// </summary>
        public decimal Balance { get; set; }
        [Required, MaxLength(200)]
        public string Message { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
