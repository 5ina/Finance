using Abp.Application.Services.Dto;

namespace NetCommunitySolution.Web.Models.Customers
{
    /// <summary>
    /// 我的推广码
    /// </summary>
    public class ExtensionCodeModel : EntityDto
    {
        public string ExtensionCode { get; set; }
    }
}