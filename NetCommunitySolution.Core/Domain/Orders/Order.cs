using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace NetCommunitySolution.Domain.Orders
{
    public class Order : Entity, ISoftDelete, IHasCreationTime, IModificationAudited
    {

        [Required, MaxLength(50)]
        public string OrderSn { get; set; }

        public int CustomerId { get; set; }

        public int OrderStatusId { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderTotal { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        [MaxLength(50)]
        public string Serial {get;set;}

        /// <summary>
        /// 订单模式
        /// </summary>
        public int OrderModeId { get; set; }
        
        /// <summary>
        /// 提成人
        /// </summary>
        public int AgentId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationTime { get; set; }
                

        public long? LastModifierUserId { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}
