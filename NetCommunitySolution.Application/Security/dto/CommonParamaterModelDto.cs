namespace NetCommunitySolution.Security.dto
{
    public class CommonParamaterModelDto
    {
        public CommonParamaterModelDto()
        {
            this.version = "1.0.0";
            this.platformNo = "P20180115033127893550";
            this.channelNo = "C20171019977520082719";
            this.merNo = "M20180115034515603760";
        }

        /// <summary>
        /// sdk版本
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 机构平台编码
        /// </summary>
        public string platformNo { get; set; }
        /// <summary>
        /// 支付渠道编码
        /// </summary>
        public string channelNo { get; set; }

        /// <summary>
        /// 商户编码
        /// </summary>
        public string merNo { get; set; }
    }
}
