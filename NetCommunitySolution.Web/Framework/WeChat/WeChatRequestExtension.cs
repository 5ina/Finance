using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace NetCommunitySolution.Web.Framework.WeChat
{
    public static class WeChatRequestExtension
    {
        /// <summary>
        /// 将哈希表输出为xml
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static string ParseXML(this Hashtable hash)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");

            ArrayList list = new ArrayList(hash.Keys);
            list.Sort();

            foreach (string k in list)
            {
                string v = Convert.ToString(hash[k]);
                //if (Regex.IsMatch(v, @"^[0-9.]$"))
                //{

                //    sb.Append("<" + k + ">" + v + "</" + k + ">");
                //}
                //else
                //{
                //    sb.Append("<" + k + "><![CDATA[" + v + "]]></" + k + ">");
                //}
                sb.Append("<" + k + ">" + v + "</" + k + ">");

            }
            sb.Append("</xml>");
            return sb.ToString();
        }

        /// <summary>
        /// 将哈希表输出为json
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static string FromJson(this Hashtable hash)
        {
            return JsonConvert.SerializeObject(hash);
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="key"></param>
        /// <param name="addSign"></param>
        /// <returns></returns>
        public static string CreateMd5Sign(this Hashtable hash, string key,  bool addSign = true)
        {
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(hash.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = Convert.ToString(hash[k]);
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "".CompareTo(v) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            sb.Append("key=" + key);
            string sign = CommonHelper.GetMD5(sb.ToString()).ToUpper();
            if (addSign)
                hash.Add("sign", sign);
            return sign;
        }


        /**
        * @将xml转为WxPayData对象并返回对象内部的数据
        * @param string 待转换的xml串
        * @return 经转换得到的Dictionary
        * @throws WxPayException
        */
        public static SortedDictionary<string, object> FormatSorted(this string xml, string sign)
        {

            SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();

            if (string.IsNullOrEmpty(xml))
            {
                m_values["return_code"] = "ERROR";
                return m_values;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;

            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }

            return m_values;

        }

        /// <summary>
        /// 返回微信所需的主要值
        /// </summary>
        /// <param name="result"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Hashtable GetJsApiParametersRequest(this Hashtable result, string key)
        {
            var data = new Hashtable();
            data.Add("appId", result["appid"]);
            data.Add("timeStamp", result["timeStamp"]);
            data.Add("nonceStr", result["nonce_str"]);
            data.Add("package", "prepay_id=" + result["prepay_id"]);

            data.Add("signType", "MD5");
            data.Add("paySign", data.CreateMd5Sign(key, false)); //result.GetParameter("sign"));

            return data;
        }
    }
}