using Abp.Domain.Repositories;
using NetCommunitySolution.Domain.Articles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCommunitySolution.Articles
{
    /// <summary>
    /// 主题模板服务接口实现类
    /// </summary>
    public class TopicTemplateService : NetCommunitySolutionAppServiceBase, ITopicTemplateService
    {
        #region Fields

        private readonly IRepository<TopicTemplate> _topicTemplateRepository;

        #endregion

        #region Ctor
        
        public TopicTemplateService(IRepository<TopicTemplate> topicTemplateRepository)
        {
            this._topicTemplateRepository = topicTemplateRepository;
        }

        #endregion

        #region Methods

        public void DeleteTopicTemplate(int topicTemplateId)
        {
            if (topicTemplateId <= 0)
                throw new ArgumentNullException("topicTemplate");

            _topicTemplateRepository.Delete(topicTemplateId);
        }
        
        public IList<TopicTemplate> GetAllTopicTemplates()
        {
            var query = from pt in _topicTemplateRepository.GetAll()
                        orderby pt.DisplayOrder
                        select pt;

            var templates = query.ToList();
            return templates;
        }
        
        public TopicTemplate GetTopicTemplateById(int topicTemplateId)
        {
            if (topicTemplateId == 0)
                return null;

            return _topicTemplateRepository.Get(topicTemplateId);
        }
        
        public int InsertTopicTemplate(TopicTemplate topicTemplate)
        {
            if (topicTemplate == null)
                throw new ArgumentNullException("topicTemplate");

            var templateId = _topicTemplateRepository.InsertAndGetId(topicTemplate);

            return templateId;
        }
        
        public virtual void UpdateTopicTemplate(TopicTemplate topicTemplate)
        {
            if (topicTemplate == null)
                throw new ArgumentNullException("topicTemplate");

            _topicTemplateRepository.Update(topicTemplate);
        }

        #endregion
    }
}
