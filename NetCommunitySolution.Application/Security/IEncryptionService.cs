using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Collections;

namespace NetCommunitySolution.Security
{
    /// <summary>
    /// 加解密服务
    /// </summary>
    public interface IEncryptionService: IApplicationService
    {
        /// <summary>
        /// 创建秘钥键
        /// </summary>
        /// <param name="size">长度</param>
        /// <returns>Salt key</returns>
        string CreateSaltKey(int size);

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="saltkey">秘钥键</param>
        /// <param name="passwordFormat">密码加密方式，散列算法</param>
        /// <returns>Password hash</returns>
        string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1");

        
        /// <summary>
        /// 实体转换参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        string ConvertParamaters<T>(T model, bool formatDecimal = true) where T : IParamater;

        /// <summary>
        /// 实体转换参数并签名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        string ConvertParamatersSign<T>(T model, string key) where T : IParamater;

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string Sign(string data);
        

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string Encrypt(string data);

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="text">未签名的数据</param>
        /// <param name="signature">已签名的64位字符串</param>
        /// <returns></returns>
        bool Verify(string text, string signature);

    }
}
