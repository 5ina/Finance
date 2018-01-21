using Abp.Application.Services.Dto;
using System.ComponentModel;

namespace NetCommunitySolution.Web.Models.Customers
{
    public class MyPromoterModel : EntityDto
    {
        /// <summary>
        /// 推广代码
        /// </summary>
        [DisplayName("推广代码")]
        public string ExtensionCode { get; set; }
    }
}