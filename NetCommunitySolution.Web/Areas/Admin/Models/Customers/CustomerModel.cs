using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Customers;

namespace NetCommunitySolution.Web.Areas.Admin.Models.Customers
{
    [AutoMap(typeof(Customer))]
    public class CustomerModel : EntityDto
    {
        
        public string Mobile { get; set; }
        
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

        /// <summary>
        /// 我的佣金
        /// </summary>
        public decimal Account { get; set; }

        /// <summary>
        /// 可以录入代理
        /// </summary>
        public bool Promoter { get; set; }
    }
}