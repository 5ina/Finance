using Abp.Application.Services.Dto;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Models.Customers
{
    public class CustomerLoginModel:EntityDto
    {

        [AllowHtml]
        [DisplayName("登录名称")]
        public string LoginName { get; set; }
        [AllowHtml]
        [DisplayName("登录密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}