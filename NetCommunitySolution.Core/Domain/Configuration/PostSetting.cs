using System.ComponentModel;

namespace NetCommunitySolution.Domain.Configuration
{
    public class PostSetting : ISetting
    {

        /// <summary>
        /// 热门帖子个数
        /// </summary>
        [DisplayName("热门帖子个数")]
        public int HotPostsCount { get; set; }

        /// <summary>
        /// 分页个数
        /// </summary>
        [DisplayName("分页个数")]
        public int PostPageSize { get; set; }

    }
}
