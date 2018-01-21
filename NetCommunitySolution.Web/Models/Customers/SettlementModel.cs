using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Security.YeeDto;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCommunitySolution.Web.Models.Customers
{
    [AutoMap(typeof(PaymerchantregModel))]
    public class SettlementModel : EntityDto
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public string sysmch_id { get; set; }

        [DisplayName("结算银行卡")]
        public string bank_account_number { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        [DisplayName("银行名称")]
        public string bank_name { get; set; }

        /// <summary>
        /// 结算卡照片地址
        /// </summary>
        [DisplayName("银行卡照片")]
        [UIHint("Picture")]
        public string bank_card_photo { get; set; }

        /// <summary>
        /// 身份证正面照图片地址
        /// </summary>
        [DisplayName("身份证正面")]
        [UIHint("Picture")]
        public string id_card_photo { get; set; }

        /// <summary>
        /// 身份证反面照图片地址
        /// </summary>
        [DisplayName("身份证反面")]
        [UIHint("Picture")]
        public string id_card_back_photo { get; set; }

        /// <summary>
        /// 手持身份证
        /// </summary>
        [DisplayName("手持身份证")]
        [UIHint("Picture")]
        public string person_photo { get; set; }

        /// <summary>
        /// 省市区
        /// </summary>
        [DisplayName("开户地区")]
        public string region_text { get; set; }

        /// <summary>
        /// 地区编码
        /// </summary>
        [DisplayName("区域编码")]
        public string area_code { get; set; }


        /// <summary>
        /// 是否认证通过
        /// </summary>
        public bool IsAuth { get; set; }
        /// <summary>
        /// 是否认证通过
        /// </summary>
        public bool Audit { get; set; }

        /// <summary>
        /// 未审核原因
        /// </summary>
        public string AuditReason { get; set; }
    }
}