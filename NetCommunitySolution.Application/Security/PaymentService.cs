using NetCommunitySolution.Common;
using NetCommunitySolution.Domain.Configuration;
using NetCommunitySolution.Security.dto;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace NetCommunitySolution.Security
{

    public class PaymentService : NetCommunitySolutionAppServiceBase, IPaymentService
    {
        #region Fields &&Ctor
        private const string contenttype = "application/json;charset=UTF-8";

        private readonly AccountSetting accountSetting;
        private readonly IEncryptionService _encryptionService;

        public PaymentService(ISettingService settingService,
            IEncryptionService encryptionService)
        {
            this.accountSetting = settingService.GetAccountSettings();
            this._encryptionService = encryptionService;
        }

        public string Cash()
        {
            throw new NotImplementedException();
        }
        #endregion


        public PaymentModelDto Payment(PaymentModelDto model, out string signature)
        {
            var pa = _encryptionService.ConvertParamaters<PaymentModelDto>(model, false);
            Logger.Debug("加密前的参数: " + pa);

            model.payBankCard = _encryptionService.Encrypt(model.payBankCard);
            model.payPreMobile = _encryptionService.Encrypt(model.payPreMobile);
            model.name = _encryptionService.Encrypt(model.name);
            model.idcard = _encryptionService.Encrypt(model.idcard);
            model.bankCard = _encryptionService.Encrypt(model.bankCard);
            model.preMobile = _encryptionService.Encrypt(model.preMobile);

            var paramaters = _encryptionService.ConvertParamaters<PaymentModelDto>(model, false);
            signature = _encryptionService.Sign(paramaters);
            Logger.Debug("加密后的参数: " + paramaters + "&signature=" + signature);
            return model;

        }


        public string Payment(PaymentcditossModel model, string key)
        {
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;


            #region Encryp 加密params
            var total = model.total_fee.ToString("#0.00");// string.Format() Math.Round(model.total_fee, 2).ToString();
            var paramaters = "version=1.0&customerid=1265&total_fee=" + total + "&sdorderno=" + model.sdorderno + "&notifyurl=" + model.notifyurl + "&returnurl=" + model.returnurl;
            var sign = CommonHelper.GetMD5(paramaters + "&" + key);

            var p = _encryptionService.ConvertParamaters<PaymentcditossModel>(model);
            p = p + "&sign=" + sign;

            #endregion
            var url = "http://www.cditoss.com/apisubmit";
            url = url + "?" + p;

            httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";

            string bodys = "";
            //根据API的要求，定义相对应的Content-Type
            //httpRequest.ContentType = "application/x-www-form-urlencoded";
            
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

        public string SetDefaultRate(string putOnUrl)
        {
            return null;

        }
    }
}
