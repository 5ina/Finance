using System.Collections.Generic;

namespace NetCommunitySolution.Domain.BankCards
{
    public class BankCode
    {
        /// <summary>
        /// 获取储蓄卡联行卡号
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetBankCode()
        {
            var bankCodes  = new Dictionary<string, string>();

            bankCodes.Add("102100099996", "工商银行");
            bankCodes.Add("103100000026", "农业银行");
            bankCodes.Add("104100000004", "中国银行");
            bankCodes.Add("105100000017", "建设银行");
            bankCodes.Add("301290000007", "交通银行");
            bankCodes.Add("302100011000", "中信银行");
            bankCodes.Add("303100000006", "光大银行");

            bankCodes.Add("305100000013", "民生银行");
            bankCodes.Add("306331003281", "广发银行");
            bankCodes.Add("307584007998", "平安银行");
            bankCodes.Add("308584000013", "招商银行");
            bankCodes.Add("309391000011", "兴业银行");
            bankCodes.Add("310290000013", "浦发银行");
            bankCodes.Add("313100000013", "北京银行");
            return bankCodes;
        }
        /// <summary>
        /// 获取信用卡联行卡号
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetPayBankCode()
        {
            var bankCodes = new Dictionary<string, string>();

            bankCodes.Add("102100099996", "工商银行");
            bankCodes.Add("103100000026", "农业银行");
            bankCodes.Add("104100000004", "中国银行");
            bankCodes.Add("105100000017", "建设银行");
            bankCodes.Add("302100011000", "中信银行");
            bankCodes.Add("303100000006", "光大银行");
            bankCodes.Add("304100040000", "华夏银行");
            
            bankCodes.Add("305100000013", "民生银行");
            bankCodes.Add("306331003281", "广发银行");
            bankCodes.Add("307584007998", "平安银行");
            bankCodes.Add("308584000013", "招商银行");
            bankCodes.Add("309391000011", "兴业银行");
            bankCodes.Add("310290000013", "浦发银行");
            bankCodes.Add("313100000013", "北京银行");
            bankCodes.Add("325290000012", "上海银行");
            bankCodes.Add("403100000004", "邮政银行");
            return bankCodes;
        }
    }
}
