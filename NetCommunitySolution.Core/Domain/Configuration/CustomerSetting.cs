using System.ComponentModel;

namespace NetCommunitySolution.Domain.Configuration
{
    public class CustomerSetting :ISetting
    {
        /// <summary>
        /// 是否允许修改昵称
        /// </summary>
        [DisplayName("允许修改昵称")]
        public bool ModifyNickName { get; set; }
        
        /// <summary>
        /// 密码最大长度
        /// </summary>
        [DisplayName("密码最长")]
        public int PasswordMaxLength { get; set; }

        /// <summary>
        /// 密码最小长度
        /// </summary>
        [DisplayName("密码最小")]
        public int PasswordMinLength { get; set; }

        /// <summary>
        /// 是否启用验证码
        /// </summary>
        [DisplayName("是否启用验证码")]
        public bool EnabledCaptcha { get; set; }

        [DisplayName("验证码长度")]
        public int CaptchaLength { get; set; }

        [DisplayName("验证码名称")]
        public string CaptchaName { get; set; }
    }
}
