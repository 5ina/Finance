using Abp.Application.Services.Dto;

namespace NetCommunitySolution.Web.Models.Customers
{
    public class SecurityModel : EntityDto
    {
        /// <summary>
        /// 是否认证
        /// </summary>
        public bool IsAuth { get; set; }

        /// <summary>
        /// 是否绑定手机
        /// </summary>
        public bool IsBindMobile { get; set; }

        /// <summary>
        /// 是否代理商
        /// </summary>
        public bool IsAgent { get; set; }
    }
}