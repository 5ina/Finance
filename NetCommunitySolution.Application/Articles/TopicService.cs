using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Articles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCommunitySolution.Articles
{

    public class TopicService : NetCommunitySolutionAppServiceBase,ITopicService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {2} : show hidden?
        /// </remarks>
        private const string TOPICS_ALL_KEY = "net.topics.all-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : topic ID
        /// </remarks>
        private const string TOPICS_BY_ID_KEY = "net.topics.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string TOPICS_PATTERN_KEY = "net.topics.";

        #endregion

        #region Fields

        private readonly IRepository<Topic> _topicRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public TopicService(IRepository<Topic> topicRepository,
            ICacheManager cacheManager)
        {
            this._topicRepository = topicRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods
        
        public void DeleteTopic(int topicId)
        {
            if (topicId <= 0)
                throw new ArgumentNullException("topic");

            _topicRepository.Delete(topicId);

            //cache
            _cacheManager.RemoveByPattern(TOPICS_PATTERN_KEY);
            
        }
        
        public Topic GetTopicById(int topicId)
        {
            if (topicId == 0)
                return null;

            string key = string.Format(TOPICS_BY_ID_KEY, topicId);
            return _cacheManager.GetCache(key).Get(key, () => _topicRepository.Get(topicId));
        }
        
        public Topic GetTopicBySystemName(string systemName)
        {
            if (String.IsNullOrEmpty(systemName))
                return null;

            var query = _topicRepository.GetAll();
            query = query.Where(t => t.SystemName == systemName);
            query = query.OrderBy(t => t.Id);
            var topics = query.ToList();
            return topics.FirstOrDefault();
        }
        
        public IList<Topic> GetAllTopics( bool showHidden = false)
        {
            string key = string.Format(TOPICS_ALL_KEY, showHidden);
            return _cacheManager.GetCache(key).Get(key, () =>
            {
                var query = _topicRepository.GetAll();
                query = query.OrderBy(t => t.DisplayOrder).ThenBy(t => t.SystemName);

                if (!showHidden)
                    query = query.Where(t => t.Published);

                return query.ToList();
            });
        }
        
        public int InsertTopic(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException("topic");

            var topicId = _topicRepository.InsertAndGetId(topic);

            //cache
            _cacheManager.RemoveByPattern(TOPICS_PATTERN_KEY);

            return topicId;            
        }
        
        public void UpdateTopic(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException("topic");

            _topicRepository.Update(topic);

            //cache
            _cacheManager.RemoveByPattern(TOPICS_PATTERN_KEY);
            
        }

        #endregion
    }
}
