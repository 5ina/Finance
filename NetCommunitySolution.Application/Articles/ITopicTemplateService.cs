using Abp.Application.Services;
using NetCommunitySolution.Domain.Articles;
using System.Collections.Generic;

namespace NetCommunitySolution.Articles
{
    /// <summary>
    /// 主题模板服务接口
    /// </summary>
    public interface ITopicTemplateService: IApplicationService
    {
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="topicTemplateId">topicTemplateId</param>
        void DeleteTopicTemplate(int topicTemplateId);

        /// <summary>
        /// 获取所有模板
        /// </summary>
        /// <returns>Topic templates</returns>
        IList<TopicTemplate> GetAllTopicTemplates();

        /// <summary>
        /// 根据主键获取模板
        /// </summary>
        /// <param name="topicTemplateId">主键</param>
        /// <returns>Topic template</returns>
        TopicTemplate GetTopicTemplateById(int topicTemplateId);

        /// <summary>
        /// 新增模板
        /// </summary>
        /// <param name="topicTemplate"></param>
        /// <returns></returns>
        int InsertTopicTemplate(TopicTemplate topicTemplate);

        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="topicTemplate"></param>
        void UpdateTopicTemplate(TopicTemplate topicTemplate);
    }
}
