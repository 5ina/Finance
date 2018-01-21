using Abp.Application.Services;
using NetCommunitySolution.Domain.Orders;
using NetCommunitySolution.Security.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCommunitySolution.Security
{
    public interface IPaymentService : IApplicationService
    {
        /// <summary>
        ///  套现并且转账到储蓄卡
        /// </summary>
        /// <param name="model"></param>
        /// <param name="key">加密秘钥</param>
        /// <returns></returns>
        PaymentModelDto Payment(PaymentModelDto model, out string signature);

        /// <summary>
        /// 套现
        /// </summary>
        /// <param name="model"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string Payment(PaymentcditossModel model, string key);
                
        string PutOn(string putOnUrl);
    }
}
