using NetCommunitySolution.Web.Framework.WeChat.Dto;
using System;

namespace NetCommunitySolution.Web.Models.Customers
{
    /// <summary>
    /// 我的二维码
    /// </summary>
    public class CustomerQRModel
    {
        public int CustomerID { get; set; }

        public DateTime CreateTime { get; set; }

        public string QR_Url { get; set; }
        /// <summary>
        /// 有效天数
        /// </summary>
        public int Expire { get; set; }

        public WxConfigModel Config { get; set; }
    }
}