using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace NetCommunitySolution.Domain.Messages
{
    /// <summary>
    /// 消息模块
    /// </summary>
    public class Message : Entity, ISoftDelete, IHasCreationTime
    {
        /// <summary>
        /// 发送人
        /// </summary>
        public int FromCustomerId { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public int ToCustomerId { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        [Required, MaxLength(50)]
        public string Subject { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        [Required, MaxLength(500)]
        public string Text { get; set; }
        

        public bool IsRead { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
