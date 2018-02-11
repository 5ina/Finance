using Abp.Application.Services.Dto;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Models.Customers
{
    public class ChangePasswordModel:EntityDto
    {
        /// <summary>
        /// 
        /// </summary>
        [AllowHtml]
        [Required]
        [DisplayName("原密码")]
        public string OldPassword { get; set; }
        [AllowHtml]
        [Required]
        [DisplayName("新密码")]
        public string NewPassword { get; set; }

        [AllowHtml]
        [DataType(DataType.Password)]
        [DisplayName("确认密码")]
        public string ConfirmNewPassword { get; set; }
    }
}