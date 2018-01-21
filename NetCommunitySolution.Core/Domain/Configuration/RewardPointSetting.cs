using System.ComponentModel;

namespace NetCommunitySolution.Domain.Configuration
{
    /// <summary>
    /// 积分配置
    /// </summary>
    public class RewardPointSetting:ISetting
    {
        /// <summary>
        /// 开启积分
        /// </summary>
        [DisplayName("开启积分")]
        public bool Enabled { get; set; }

        [DisplayName("发帖积分")]
        public int NewPost { get; set; }

        [DisplayName("回复积分")]
        public int Comment { get; set; }

        [DisplayName("结贴积分")]
        public int Solve { get; set; }

        [DisplayName("回帖选中")]
        public int Selected { get; set; }
        

        [DisplayName("每日最高")]
        public int DayMaxReward { get; set; }
    }
}
