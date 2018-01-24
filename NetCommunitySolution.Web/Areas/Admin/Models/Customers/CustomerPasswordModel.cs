using Abp.Application.Services.Dto;

namespace NetCommunitySolution.Web.Areas.Admin.Models.Customers
{
    public class CustomerPasswordModel:EntityDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}