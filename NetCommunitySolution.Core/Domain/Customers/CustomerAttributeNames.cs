namespace NetCommunitySolution.Domain.Customers
{
    /// <summary>
    /// 用户属性名称
    /// </summary>
    public static class CustomerAttributeNames
    {

        #region 收货地址
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public static string UserName { get { return "userName"; } }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public static string TelNumber { get { return "telNumber"; } }

        /// <summary>
        /// 收货省份
        /// </summary>
        public static string ProvinceName { get { return "provinceName"; } }
        /// <summary>
        /// 收货城市
        /// </summary>
        public static string CityName { get { return "cityName"; } }

        /// <summary>
        /// 收货区县
        /// </summary>
        public static string CountryName { get { return "countryName"; } }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public static string DetailInfo { get { return "detailInfo"; } }
        #endregion

        #region 基本信息

        /// <summary>
        /// 身份证号
        /// </summary>
        public static string CardNo { get { return "CardNo"; } }
        public static string RealName { get { return "RealName"; } }

        public static string Sex { get { return "sex"; } }
        public static string Avatar { get { return "avatar"; } }
        
        /// <summary>
        /// 我的推广码
        /// </summary>
        public static string ExtensionCode { get { return "ExtensionCode"; } }

        /// <summary>
        /// 用户账户余额
        /// </summary>
        public static string Account { get { return "Account"; } }
        /// <summary>
        /// 积分
        /// </summary>
        public static string Reward { get{ return "reward"; } }

        /// <summary>
        /// 二维码过期时间
        /// </summary>
        public static string QRCodeExpireTime { get { return "QRCode_Time"; } }
        /// <summary>
        /// 我的推广码
        /// </summary>
        public static string MyQR_Code { get { return "QRCode_URL"; } }

        /// <summary>
        /// 生日
        /// </summary>
        public static string Birthday { get { return "Birthday"; } }

        #endregion

        #region 安全
        /// <summary>
        /// 支付密码
        /// </summary>
        public static string TransactionPassword { get{ return "TransactionPassword"; } }

        /// <summary>
        /// 是否认证
        /// </summary>
        public static string Authentication { get { return "Authentication"; } }

        /// <summary>
        /// 是否代理商
        /// </summary>
        public static string Agent { get { return "Agent"; } }
        #endregion

        #region 易宝信息

        /// <summary>
        /// 易宝认证
        /// </summary>
        public static string YeeAuth { get { return "YeeAuth"; } }

        /// <summary>
        /// 易宝审核
        /// </summary>
        public static string YeeAudit { get { return "YeeAudit"; } }

        /// <summary>
        /// 审核未通过原因
        /// </summary>
        public static string YeeAuditReason { get { return "YeeAuditReason"; } }
        /// <summary>
        /// 用户的商户id
        /// </summary>
        public static string SysMchId { get { return "sysmch_id"; } }
        #endregion

        #region Yee Mch


        /// <summary>
        /// 签约手机号
        /// </summary>
        public static string bind_mobile { get { return "bind_mobile"; } }

        /// <summary>
        /// 签约姓名
        /// </summary>
        public static string legal_person { get { return "legal_person"; } }
        /// <summary>
        /// 签约身份证
        /// </summary>
        public static string id_card { get { return "id_card"; } }
        /// <summary>
        /// 签约银行卡号
        /// </summary>
        public static string bank_account_number { get { return "bank_account_number"; } }

        /// <summary>
        /// 签约银行名称
        /// </summary>
        public static string bank_name { get { return "bank_name"; } }

        /// <summary>
        /// 地区编码
        /// </summary>
        public static string area_code { get { return "area_code"; } }
        /// <summary>
        /// 省市区
        /// </summary>
        public static string region_text { get { return "region_text"; } }
        /// <summary>
        /// 银行卡证明照图片地址
        /// </summary>
        public static string bank_card_photo { get { return "bank_card_photo"; } }
        /// <summary>
        /// 身份证正面照图片地址
        /// </summary>
        public static string id_card_photo { get { return "id_card_photo"; } }
        /// <summary>
        /// 身份证反面照图片地址
        /// </summary>
        public static string id_card_back_photo { get { return "id_card_back_photo"; } }
        /// <summary>
        /// 手持身份证与银行卡合影图片地址
        /// </summary>
        public static string person_photo { get { return "person_photo"; } }
        #endregion
    }
}
