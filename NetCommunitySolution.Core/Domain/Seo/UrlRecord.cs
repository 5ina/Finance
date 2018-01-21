using Abp.Domain.Entities;

namespace NetCommunitySolution.Domain.Seo
{
    /// <summary>
    /// Url Slug
    /// </summary>
    public partial class UrlRecord : Entity
    {
        /// <summary>
        /// 实体Id
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Slug
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        
    }
}
