using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace NetCommunitySolution.Domain.BankCards
{
    public class BankCard : Entity, IHasCreationTime, ISoftDelete
    {
        /// <summary>
        /// 所属用户
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 联行卡号
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// 开户省份(储蓄卡）
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 开户城市(储蓄卡）
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        [Required ,MaxLength(30)]
        public string No { get; set; }
        /// <summary>
        /// 持卡人姓名
        /// </summary>
        [Required, MaxLength(10)]
        public string Name { get; set; }
        
        [Required, MaxLength(15)]
        public string Mobile { get; set; }

        [Required,MaxLength(30)]
        public string Bank { get; set; }

        /// <summary>
        /// 卡片类型
        /// </summary>
        public int BankCardModeId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
