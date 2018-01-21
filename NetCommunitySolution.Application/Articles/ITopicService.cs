using Abp.Application.Services;
using NetCommunitySolution.Domain.Articles;
using System.Collections.Generic;

namespace NetCommunitySolution.Articles
{
    /// <summary>
    /// 主题服务接口
    /// </summary>
    public interface ITopicService: IApplicationService
    {
        /// <summary>
        /// 删除主题
        /// </summary>
        /// <param name="topic">topicId</param>
        void DeleteTopic(int topicId);

        /// <summary>
        /// Gets a topic
        /// </summary>
        /// <param name="topicId">The topic identifier</param>
        /// <returns>Topic</returns>
        Topic GetTopicById(int topicId);

        /// <summary>
        /// 获取主题
        /// </summary>
        /// <param name="systemName">主题系统名称</param>        
        /// <returns>Topic</returns>
        Topic GetTopicBySystemName(string systemName);

        /// <summary>
        /// 获取所有主题
        /// </summary>
        /// <param name="showHidden">是否显示隐藏的主题</param>
        /// <returns>Topics</returns>
        IList<Topic> GetAllTopics(bool showHidden = false);

        /// <summary>
        /// 新增主题
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        int InsertTopic(Topic topic);

        /// <summary>
        /// 更新主题
        /// </summary>
        /// <param name="topic">Topic</param>
        void UpdateTopic(Topic topic);
    }
}
