using Abp.Domain.Entities;
using NetCommunitySolution.Domain.Seo;

namespace NetCommunitySolution.Domain.Articles
{
    public class Topic : Entity, ISlugSupported
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether this topic should be included in top menu
        /// </summary>
        public bool IncludeInTopMenu { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 网站关闭时是否可以访问
        /// </summary>
        public bool AccessibleWhenStoreClosed { get; set; }


        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value of used topic template identifier
        /// </summary>
        public int TopicTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }

    }
}