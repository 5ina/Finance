using Abp.Domain.Entities;

namespace NetCommunitySolution.Domain.Articles
{
    public class TopicTemplate :Entity
    {

        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 视图路径
        /// </summary>
        public string ViewPath { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
