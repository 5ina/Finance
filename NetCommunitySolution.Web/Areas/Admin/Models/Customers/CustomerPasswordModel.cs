using Abp.Application.Services.Dto;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Models.Customers
{
    public class CustomerPasswordModel:EntityDto
    {
        [AllowHtml]
        [DataType(DataType.Password)]
        [DisplayName("原密码")]
        public string OldPassword { get; set; }
        [DisplayName("新密码")]
        [DataType(DataType.Password)]
        [AllowHtml]
        public string NewPassword { get; set; }
        [AllowHtml]
        [DisplayName("确认密码")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}