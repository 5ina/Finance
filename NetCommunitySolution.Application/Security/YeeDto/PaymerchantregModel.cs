namespace NetCommunitySolution.Security.YeeDto
{
    public class PaymerchantregModel
    {
        /// <summary>
        /// 快捷系统商户ID
        /// </summary>
        public string sysmch_id { get; set; }


        /// <summary>
        /// 签约手机号
        /// </summary>
        public string bind_mobile { get; set; }

        /// <summary>
        /// 签约姓名
        /// </summary>
        public string legal_person { get; set; }
        /// <summary>
        /// 签约身份证
        /// </summary>
        public string id_card { get; set; }
        /// <summary>
        /// 签约银行卡号
        /// </summary>
        public string bank_account_number { get; set; }

        /// <summary>
        /// 签约银行名称
        /// </summary>
        public string bank_name { get; set; }

        /// <summary>
        /// 地区编码
        /// </summary>
        public string area_code { get; set; }
        /// <summary>
        /// 省市区
        /// </summary>
        public string region_text { get; set; }
        /// <summary>
        /// 银行卡证明照图片地址
        /// </summary>
        public string bank_card_photo { get; set; }
        /// <summary>
        /// 身份证正面照图片地址
        /// </summary>
        public string id_card_photo { get; set; }
        /// <summary>
        /// 身份证反面照图片地址
        /// </summary>
        public string id_card_back_photo { get; set; }
        /// <summary>
        /// 手持身份证与银行卡合影图片地址
        /// </summary>
        public string person_photo { get; set; }


    }
}
