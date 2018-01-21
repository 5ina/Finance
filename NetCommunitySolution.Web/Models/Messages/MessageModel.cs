using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Messages;
using System;

namespace NetCommunitySolution.Web.Models.Messages
{
    [AutoMap(typeof(Message))]
    public class MessageModel:EntityDto
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
        public string Subject { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Text { get; set; }


        public bool IsRead { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
    }
}