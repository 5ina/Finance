using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NetCommunitySolution.Domain.Messages;

namespace NetCommunitySolution.Messages
{
    /// <summary>
    /// 私信服务接口
    /// </summary>
    public interface IPrivateMessageService : IApplicationService
    {
        /// <summary>
        /// 创建消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        int CreateMessage(Message message);

        /// <summary>
        /// 更新消息
        /// </summary>
        /// <param name="message"></param>
        void UpdateMessage(Message message);

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="messageId"></param>
        void DeleteMessage(int messageId);

        /// <summary>
        /// 清空消息
        /// </summary>
        /// <param name="customerId"></param>
        void ClearMyMessage(int customerId);

        /// <summary>
        /// 标记为已读
        /// </summary>
        /// <param name="customerId"></param>
        void ReadAllMyMessage(int customerId);

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Message GetMessageById(int messageId);

        /// <summary>
        /// 获取所有消息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="isRead"></param>
        /// <param name="keywords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedResult<Message> GetAllMessages(int customerId = 0, string keywords = "",
            bool? isRead = null,
            bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
