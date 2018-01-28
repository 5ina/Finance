using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Customers;
using System.ComponentModel;

namespace NetCommunitySolution.Web.Models.Customers
{
    [AutoMap(typeof(Customer))]
    public class CustomerInfoModel : EntityDto
    {

        [DisplayName("手机号")]
        public string Mobile { get; set; }

        [DisplayName("昵称")]
        public string NickName { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public bool Authentication { get; set; }

        public string CustomerAvatar { get; set; }

        /// <summary>
        /// 是否代理商
        /// </summary>
        public bool Agent { get; set; }

        public int AgentID { get; set; }
        /// <summary>
        /// 我的佣金
        /// </summary>
        public decimal Account { get; set; }
        
    }
}