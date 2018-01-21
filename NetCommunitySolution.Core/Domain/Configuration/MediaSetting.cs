using System.ComponentModel;

namespace NetCommunitySolution.Domain.Configuration
{
    public class MediaSetting : ISetting
    {
        /// <summary>
        /// 启用头像
        /// </summary>
        [DisplayName("启用头像")]
        public bool EnabledAvatar { get; set; }


        [DisplayName("媒体模式")]
        public MediaMode MediaMode { get; set; }

        [DisplayName("头像最大限制")]
        public int MaxAvatarSize { get; set; }

        /// <summary>
        /// 文件类型例如 .png,.gif（用,隔开）
        /// </summary>
        [DisplayName("文件类型")]
        public string AvatarFile { get; set; }


        [DisplayName("AccessKey")]
        public string AccessKeyId { get; set; }

        [DisplayName("KeySecret")]
        public string AccessKeySecret { get; set; }
        [DisplayName("Endpoint")]
        public string Endpoint { get; set; }

        [DisplayName("Bucket")]
        public string Bucket { get; set; }
    }
}
