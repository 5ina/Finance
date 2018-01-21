using Abp.Application.Services.Dto;

namespace NetCommunitySolution.Customers
{
    public class CustomerAttributeDto:EntityDto
    {

        public string Sex { get; set; }
        public string Avatar { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public string Reward { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public string QQ { get; set; }

        public string Introduce { get; set; }
    }
}
