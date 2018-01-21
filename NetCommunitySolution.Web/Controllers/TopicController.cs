using Abp.Runtime.Caching;
using Abp.UI;
using NetCommunitySolution.Articles;
using NetCommunitySolution.Domain.Articles;
using NetCommunitySolution.Seo;
using NetCommunitySolution.Web.Models.Articles;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    public class TopicController : WeChatBaseController
    {

        #region Fields

        private readonly ITopicService _topicService;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Constructors

        public TopicController(ITopicService topicService,
            ICacheManager cacheManager)
        {
            this._topicService = topicService;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Utilities

        [NonAction]
        protected TopicModel PrepareTopicModel(Topic topic)
        {
            if (topic == null)
                throw new UserFriendlyException("brand");

            var model = new TopicModel
            {
                Id = topic.Id,
                SystemName = topic.SystemName,
                Title = topic.Title,
                Body = topic.Body,
                MetaKeywords = topic.MetaKeywords,
                MetaDescription = topic.MetaDescription,
                MetaTitle = topic.MetaTitle,
                SeName = topic.GetSeName(),
                TopicTemplateId = topic.TopicTemplateId
            };
            return model;
        }

        #endregion

        #region Methods
        
        public ActionResult TopicDetails(string systemName)
        {
            //load by store
            var topic = _topicService.GetTopicBySystemName(systemName);
            if (topic == null)
                return RedirectToAction("NoTopic");
            if (!topic.Published)
                return RedirectToAction("NoTopic");

            var model = PrepareTopicModel(topic);

            if (model == null)
                return RedirectToAction("NoTopic");

            return View("TopicDetail",model);
        }
        
        public ActionResult TopicBlock(int topicId)
        {
            var topic = _topicService.GetTopicById(topicId);
            if (topic == null)
                return RedirectToAction("NoTopic");
            if (!topic.Published)
                return RedirectToAction("NoTopic");
            var model = PrepareTopicModel(topic);
            if (model == null)
                return RedirectToAction("NoTopic");

            return View("TopicDetails", model);
        }

        /// <summary>
        /// 不存在的主题
        /// </summary>
        /// <returns></returns>
        public ActionResult NoTopic()
        {
            return View();
        }
        #endregion
    }
}