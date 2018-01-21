using Abp.Domain.Entities;

namespace NetCommunitySolution.Domain.Directory
{
    public class Area : Entity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Province { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public string areaCode { get; set; }
    }
}
