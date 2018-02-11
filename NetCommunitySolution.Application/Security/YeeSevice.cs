using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using NetCommunitySolution.Common;
using NetCommunitySolution.Domain.Orders;
using NetCommunitySolution.Security.YeeDto;
using Newtonsoft.Json;

namespace NetCommunitySolution.Security
{
    public class YeeSevice : NetCommunitySolutionAppServiceBase, IYeeSevice
    {
        #region Fields

        private readonly string AppSecret;
        private readonly string AppId;
        private readonly string url;       

        public YeeSevice(ISettingService settingService)
        {
            var account = settingService.GetAccountSettings();
            this.AppSecret = account.AppSecret;
            this.AppId = account.AppId;
            this.url = account.PaymentUrl;
        }
        #endregion


        #region Utilities
        private void AddCommonParams(Hashtable hash)
        {
            hash.Add("appid", AppId);
            hash.Add("nonce_str", CommonHelper.GenerateCode(8));
            hash.Add("timestamp", CommonHelper.GetTimeStamp());
            hash.Add("version", "2");
        }
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        private string Sign(Hashtable hash)
        {
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(hash.Keys);
            akeys.Sort();
            foreach (string k in akeys)
            {
                string v = Convert.ToString(hash[k]);
                if (null != v && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + AppSecret);
            var sign =CommonHelper.GetMD5(sb.ToString());
            return sign.ToUpper();
        }

        private string Post(string url,Hashtable param)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpResponse = null;
            httpRequest.Method = "POST";

            string bodys = ToParams(param);
            //根据API的要求，定义相对应的Content-Type
            httpRequest.ContentType = "application/x-www-form-urlencoded";

            if (0 < bodys.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try

            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }

            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            return reader.ReadToEnd();
        }

        private string PostFile(string url, Hashtable param, byte[] file, string mimeType,string suffix)
        {
            string boundary = "------------------" + DateTime.Now.Ticks.ToString("x");
            string bodys = ToParams(param);
            url = url + "?" + bodys;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpResponse = null;
            httpRequest.Method = "POST";
            httpRequest.KeepAlive = true;
            httpRequest.Expect = "";
            //根据API的要求，定义相对应的Content-Type
            httpRequest.ContentType = "multipart/form-data; boundary=" + boundary;

            
            byte[] line = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
            byte[] enterER = Encoding.ASCII.GetBytes("\r\n");
            string fformat = "Content-Disposition:form-data; name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(line, 0, line.Length);        //项目分隔符
                string s = string.Format(fformat, "file", CommonHelper.GetTimeStamp() + suffix, mimeType);
                byte[] data = Encoding.UTF8.GetBytes(s);
                stream.Write(data, 0, data.Length);
                stream.Write(file, 0, file.Length);
                stream.Write(enterER, 0, enterER.Length);  //添加\r\n


                string format = "--" + boundary + "\r\nContent-Disposition:form-data;name=\"{0}\"\r\n\r\n{1}\r\n";    //自带项目分隔符
                foreach (string key in param.Keys)
                {
                    string p = string.Format(format, key, param[key]);
                    byte[] pBytes = Encoding.UTF8.GetBytes(s);
                    stream.Write(pBytes, 0, pBytes.Length);
                }


                byte[] foot_data = Encoding.UTF8.GetBytes("--" + boundary + "--\r\n");      //项目最后的分隔符字符串需要带上--  
                stream.Write(foot_data, 0, foot_data.Length);
                

                httpRequest.ContentLength = stream.Length;
                Stream requestStream = httpRequest.GetRequestStream(); //写入请求数据
                stream.Position = 0L;
                stream.CopyTo(requestStream); //
                stream.Close();
                requestStream.Close();
            }
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }

            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            return reader.ReadToEnd();
        }

        private string ToParams(Hashtable hash)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var k in hash.Keys)
            {
                string v = Convert.ToString(hash[k]);
                if (null != v && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将实体转换为哈希表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        private Hashtable ToHashtable<T>(T model)
        {
            var hash = new Hashtable();

            string tStr = string.Empty;
            var properties = model.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
                return hash;
            
            foreach (var item in properties)
            {
                var prop = model.GetType().GetProperty(item.Name);
                hash.Add(item.Name, prop.GetValue(model, null));
            }
            return hash;
        }

        #endregion

        #region Method
        public int MchCreate(string mobile, int customerId)
        {
            var mchurl = string.Format("{0}{1}", url, "/openapi/mch/mchcreate");
            Hashtable hash = new Hashtable();
            hash.Add("username", mobile);
            hash.Add("mobile", mobile);
            hash.Add("out_mch_id", customerId);
            AddCommonParams(hash);//公共参数
            hash.Add("sign", Sign(hash));//排序+签名

            var result = Post(mchurl, hash);

            var model = JsonConvert.DeserializeObject<MchCreateResultModel>(result);
            return model.sysmch_id;

        }

        public bool Paymerchantreg(PaymerchantregModel model)
        {
            var paymerchantregurl = string.Format("{0}{1}", url, "/openapi/mch/paymerchantreg");
            var hash = ToHashtable<PaymerchantregModel>(model);
            AddCommonParams(hash);
            hash.Add("sign", Sign(hash));
            var result = Post(paymerchantregurl, hash);
            var resultModel = JsonConvert.DeserializeObject<PaymerchantregResultModel>(result);
            if (resultModel.status == 0)
                return true;
            else
                return false;
        }

        public TransferStatus Transfer(int sysmch_id, int to_sysmch_id, int type, decimal transfer_amount, string out_trade_no, string remark, int pay_type)
        {
            var transferUrl = string.Format("{0}{1}", url, "/openapi/pay/profit_transfer");
            var hash = new Hashtable();
            hash.Add("sysmch_id", sysmch_id);
            hash.Add("to_sysmch_id", to_sysmch_id);
            hash.Add("type", type);
            hash.Add("transfer_amount", transfer_amount);
            hash.Add("out_trade_no", out_trade_no);
            hash.Add("remark", remark);
            hash.Add("pay_type", pay_type);
            AddCommonParams(hash);
            hash.Add("sign", Sign(hash));
            var result = Post(transferUrl, hash);
            var resultModel = JsonConvert.DeserializeObject<TransferResultModel>(result);
            return (TransferStatus)resultModel.trade_status;
        }

        public TransferStatus ProfitTransfer(int sysmch_id, int type, decimal transfer_amount, string out_trade_no, int pay_type)
        {
            var transferUrl = string.Format("{0}{1}", url, "/openapi/pay/profit_transfer");
            var hash = new Hashtable();
            hash.Add("sysmch_id", sysmch_id);
            hash.Add("type", type);
            hash.Add("transfer_amount", transfer_amount);
            hash.Add("out_trade_no", out_trade_no);
            hash.Add("pay_type", pay_type);
            AddCommonParams(hash);
            hash.Add("sign", Sign(hash));
            var result = Post(transferUrl, hash);
            var resultModel = JsonConvert.DeserializeObject<TransferResultModel>(result);
            switch (resultModel.trade_status)
            {
                case 0:
                    return TransferStatus.Fail;
                case 1:
                    return TransferStatus.Withdrawals;
                case 2:
                    return TransferStatus.Complete;
                default:
                    return TransferStatus.Fail;
            }
        }

        public string Uploadimage(byte[] files,string mimeType,string suffix)
        {
            var imageUrl = string.Format("{0}{1}", url, "/openapi/mch/uploadimage");
            var hash = new Hashtable();
            AddCommonParams(hash);
            hash.Add("sign", Sign(hash));
            var result = PostFile(imageUrl, hash, files, mimeType, suffix);

            var resultModel = JsonConvert.DeserializeObject<ImageResultModel>(result);
            if (resultModel.status == 0)
                return resultModel.path; //return string.Format("{0}{1}", resultModel.assets_domain, resultModel.path);
            return "";        
        }

        public PaymentResultModel Payment(int sysmch_id, decimal total, string orderNo, int pay_type = 1)
        {
            var payUrl = string.Format("{0}{1}", url, "/openapi/pay/webpay");
            var hash = new Hashtable();
            hash.Add("sysmch_id", sysmch_id);
            hash.Add("amount", total.ToString("#0.00"));
            hash.Add("out_trade_no", orderNo);
            hash.Add("pay_type", pay_type);

            AddCommonParams(hash);
            hash.Add("sign", Sign(hash));
            var result = Post(payUrl, hash);
            var resultModel = JsonConvert.DeserializeObject<PaymentResultModel>(result);
            return resultModel;
        }

        public RateResultModel QueryRate(int sysmch_id, int product_type, int pay_type = 1)
        {
            var rateUrl = string.Format("{0}{1}", url, "/openapi/pay/ratequery");
            var hash = new Hashtable();
            hash.Add("sysmch_id", sysmch_id);
            hash.Add("product_type", product_type);
            hash.Add("pay_type", pay_type);
            AddCommonParams(hash);
            hash.Add("sign", Sign(hash));

            var result = Post(rateUrl, hash);
            var resultModel = JsonConvert.DeserializeObject<RateResultModel>(result);
            return resultModel;
        }

        public RateResultModel SetRate(int sysmch_id, int product_type, decimal rate, int pay_type = 1)
        {
            var rateUrl = string.Format("{0}{1}", url, "/openapi/pay/setrate ");
            var hash = new Hashtable();
            hash.Add("sysmch_id", sysmch_id);
            hash.Add("product_type", product_type);
            hash.Add("pay_type", pay_type);
            hash.Add("rate", (rate * 1000).ToString("#0.00"));
            AddCommonParams(hash);
            hash.Add("sign", Sign(hash));

            var result = Post(rateUrl, hash);
            var resultModel = JsonConvert.DeserializeObject<RateResultModel>(result);
            return resultModel;
        }

        public DefaultQueryResultModel SetDefaultRate(decimal def_rate, int def_transferrate, decimal tf_def_rate, int tf_def_transferrate)
        {
            var defaultRateUrl = string.Format("{0}{1}", url, "/openapi/mch/defconfigset");
            var hash = new Hashtable();
            hash.Add("def_rate", def_rate.ToString("#0.00"));
            hash.Add("def_transferrate", def_transferrate);
            hash.Add("def_rate7", def_rate.ToString("#0.00"));
            hash.Add("def_transferrate7", def_transferrate);
            hash.Add("tf_def_rate", tf_def_rate.ToString("#0.00"));
            hash.Add("tf_def_transferrate", tf_def_transferrate);
            hash.Add("pay_type", 7);
            AddCommonParams(hash);
            hash.Add("sign", Sign(hash));
            var result = Post(defaultRateUrl, hash);
            var resultModel = JsonConvert.DeserializeObject<DefaultQueryResultModel>(result);
            return resultModel;

        }

        public DefaultQueryResultModel QueryDefaultRate()
        {
            var defaultQueryRateUrl = string.Format("{0}{1}", url, "/openapi/mch/defconfigquery");
            var hash = new Hashtable();
            AddCommonParams(hash);
            hash.Add("sign", Sign(hash));
            var result = Post(defaultQueryRateUrl, hash);
            var resultModel = JsonConvert.DeserializeObject<DefaultQueryResultModel>(result);
            return resultModel;
        }

        public MchStatusResultModel MchQuery(int mchId, int customerId)
        {
            var mchurl = string.Format("{0}{1}", url, "/openapi/mch/mchquery");
            Hashtable hash = new Hashtable();
            hash.Add("sysmch_id", mchId);
            hash.Add("out_mch_id", customerId);
            AddCommonParams(hash);//公共参数
            hash.Add("sign", Sign(hash));//排序+签名

            var result = Post(mchurl, hash);

            var model = JsonConvert.DeserializeObject<MchStatusResultModel>(result);
            return model;
        }

        public BalanceResultModel Balance(int sysmch_id, int pay_type)
        {
            var mchurl = string.Format("{0}{1}", url, "/openapi/mch/profit_balance");
            Hashtable hash = new Hashtable();
            hash.Add("sysmch_id", sysmch_id);
            hash.Add("pay_type", pay_type);
            AddCommonParams(hash);//公共参数
            hash.Add("sign", Sign(hash));//排序+签名

            var result = Post(mchurl, hash);

            var model = JsonConvert.DeserializeObject<BalanceResultModel>(result);
            return model;
        }
        #endregion
    }
}
