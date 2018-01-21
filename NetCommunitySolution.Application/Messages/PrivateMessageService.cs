using System;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Messages;

namespace NetCommunitySolution.Messages
{
    public class PrivateMessageService : NetCommunitySolutionAppServiceBase, IPrivateMessageService
    {

        #region Ctor && Field

        private const string MESSAGE_BY_ID = "net.message.by-id.{0}";
        private const string MESSAGE_BY_CUSTOMERID = "net.message.by-openid.{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string MESSAGE_PATTERN_KEY = "net.message.";


        private readonly IUnitOfWorkManager _unitOfWorkManage;
        private readonly IRepository<Message> _messageRepository;
        private readonly ICacheManager _cacheManager;

        public PrivateMessageService(IRepository<Message> messageRepository,
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManage)
        {
            this._messageRepository = messageRepository;
            this._cacheManager = cacheManager;
            this._unitOfWorkManage = unitOfWorkManage;
        }

        #endregion
        #region Method
        public void ClearMyMessage(int customerId)
        {
            var messages = _messageRepository.GetAllList(m => m.ToCustomerId == customerId);

            messages.ForEach(m =>
            {
                DeleteMessage(m.Id);
            });
            _cacheManager.RemoveByPattern(MESSAGE_PATTERN_KEY);
        }

        public int CreateMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException("privateMessage");

            var messageId = _messageRepository.InsertAndGetId(message);

            _cacheManager.RemoveByPattern(MESSAGE_PATTERN_KEY);
            return messageId;

        }

        public void DeleteMessage(int messageId)
        {
            if (messageId > 0)
                _messageRepository.Delete(messageId);
        }

        public IPagedResult<Message> GetAllMessages(int customerId = 0, string keywords = "",
            bool? isRead = null,
            bool showHidden = false,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {

            using (_unitOfWorkManage.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var query = _messageRepository.GetAll();
                if (!String.IsNullOrWhiteSpace(keywords))
                    query = query.Where(m => m.Subject.Contains(keywords) || m.Text.Contains(keywords));

                if (customerId > 0)
                    query = query.Where(m => m.ToCustomerId == customerId);

                if (!showHidden)
                    query = query.Where(m => !m.IsDeleted);

                if (isRead.HasValue)
                    query = query.Where(m => isRead.Value == m.IsRead);

                query = query.OrderByDescending(m => m.CreationTime);

                return new PagedResult<Message>(query, pageIndex, pageSize);
            }
        }

        public Message GetMessageById(int messageId)
        {
            if (messageId <= 0)
                return null;

            return _cacheManager.GetCache(MESSAGE_BY_ID).Get(MESSAGE_BY_ID, () =>
            {
                return _messageRepository.Get(messageId);
            });
        }

        public void ReadAllMyMessage(int customerId)
        {
            var messages = _messageRepository.GetAllList(m => m.ToCustomerId == customerId);

            messages.ForEach(m =>
            {
                m.IsRead = true;
                UpdateMessage(m);
            });
            _cacheManager.RemoveByPattern(MESSAGE_PATTERN_KEY);
        }

        public void UpdateMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException("privateMessage");

            _messageRepository.Update(message);

            _cacheManager.RemoveByPattern(MESSAGE_PATTERN_KEY);
        }
        #endregion
    }
}
