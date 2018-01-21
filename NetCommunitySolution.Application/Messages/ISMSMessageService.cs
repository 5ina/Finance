using Abp.Application.Services;

namespace NetCommunitySolution.Messages
{
    /// <summary>
    /// 短信服务接口
    /// </summary>
    public interface ISMSMessageService : IApplicationService
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        bool SendMessage(string mobile, string signName, string tempCode, string param);
    }
}
