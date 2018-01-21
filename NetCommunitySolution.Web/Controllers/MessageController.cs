using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Messages;
using NetCommunitySolution.Web.Models.Messages;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    public class MessageController : WeChatBaseController
    {

        #region ctor && Fields

        private const string PRODUCTREVIEW_BY_CUSTOMERID = "strore.product.review.by.customerid-{0}";
        private const string FAVORITE_BY_CUSTOMERID = "strore.favorite.by.customerid-{0}";

        /// <summary>
        /// 二维码请求URL
        /// </summary>
        private const string QRCode_Url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}";

        private readonly ICacheManager _cacheManager;
        private readonly IPrivateMessageService _messageService;
        private readonly ICustomerService _customerService;

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public MessageController(
                                ICacheManager cacheManager,
                                IPrivateMessageService messageService,
                                ICustomerService customerService,
                                IUnitOfWorkManager unitOfWorkManager)
        {
            this._cacheManager = cacheManager;
            this._messageService = messageService;
            this._customerService = customerService;
            this._unitOfWorkManager = unitOfWorkManager;
        }
        #endregion

        #region Utilities

        #endregion

        #region Method
        public ActionResult List()
        {
            var messages = _messageService.GetAllMessages(customerId: this.CustomerId);
            var model = messages.Items.MapTo<List<MessageModel>>();
            return View(model);
        }

        public ActionResult Message(int messageId)
        {
            var message = _messageService.GetMessageById(messageId);
            var model = message.MapTo<MessageModel>();
            if (!message.IsRead)
            {
                message.IsRead = true;
                _messageService.UpdateMessage(message);
            }
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult MessageCount()
        {
            var messages = _messageService.GetAllMessages(this.CustomerId, isRead: false);
            if (messages.TotalCount <= 0)
            {
                return Content("");
            }
            else
            {
                return Content("<span class=\"weui-badge\" style=\"position: absolute; top: -2px; right: -13px;\">" + messages.TotalCount + "</span> ");
            }
        }
        #endregion
    }
}