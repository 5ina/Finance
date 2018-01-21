using Castle.Core.Logging;
using System;
using System.Collections;
using System.Xml;

namespace NetCommunitySolution.Web.Framework.WeChat
{
    public class WechatRequest
    {
        private readonly string _xml;
        private XmlDocument xmlDoc;
        protected Hashtable Parameters;
        public WechatRequest(string xml)
        {
            this._xml = xml;
            this.xmlDoc = new XmlDocument();
            this.Parameters = new Hashtable();
        }

        /// <summary>
        /// 获取事件
        /// </summary>
        /// <returns></returns>
        public string LoadEvent(ILogger logger)
        {
            xmlDoc.LoadXml(this._xml);
            logger.Debug("xml:" + this._xml);
            var xmlJson = Newtonsoft.Json.JsonConvert.SerializeXmlNode(xmlDoc);

            var msgType = xmlDoc.SelectSingleNode("/xml/MsgType").InnerText;
            if (!String.IsNullOrWhiteSpace(msgType) && msgType == "event")
            {
                return xmlDoc.SelectSingleNode("/xml/Event").InnerText;
            }
            else if (!String.IsNullOrWhiteSpace(msgType) && msgType == "text")
            {
                return xmlDoc.SelectSingleNode("/xml/MsgType").InnerText;
            }

            return "";
        }

        public Hashtable LoadXml(bool defaultevent = true)
        {
            xmlDoc.LoadXml(this._xml);

            this.Parameters.Add("ToUserName", xmlDoc.SelectSingleNode("/xml/ToUserName").InnerText);
            this.Parameters.Add("FromUserName", xmlDoc.SelectSingleNode("/xml/FromUserName").InnerText);
            this.Parameters.Add("CreateTime", xmlDoc.SelectSingleNode("/xml/CreateTime").InnerText);
            this.Parameters.Add("MsgType", xmlDoc.SelectSingleNode("/xml/MsgType").InnerText);
            if (defaultevent)
            {
                this.Parameters.Add("Event", xmlDoc.SelectSingleNode("/xml/Event").InnerText);
                this.Parameters.Add("EventKey", xmlDoc.SelectSingleNode("/xml/EventKey").InnerText);
            }
            else
            {
                this.Parameters.Add("Content", xmlDoc.SelectSingleNode("/xml/Content").InnerText);
                this.Parameters.Add("MsgId", xmlDoc.SelectSingleNode("/xml/MsgId").InnerText);
            }
            return Parameters;
        }

    }
}