using Abp.Application.Services;
using NetCommunitySolution.Domain.Customers;
using System.Collections.Generic;

namespace NetCommunitySolution.Customers
{
    /// <summary>
    /// 用户属性服务接口
    /// </summary>
    public interface ICustomerAttributeService : IApplicationService
    {
        /// <summary>
        /// 保存属性
        /// </summary>
        /// <param name="attribute"></param>
        void SaveAttribute(CustomerAttribute attribute);

        /// <summary>
        /// 获取用户的所有属性
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        IList<CustomerAttribute> GetAttributeByCustomerId(int customerId);

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <typeparam name="TPropType"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int GetCustomerId<TPropType>(string key , TPropType value);

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <typeparam name="TPropType"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int GetCountByValue<TPropType>(string key, TPropType value);
        
        /// <summary>
        /// 删除熟悉
        /// </summary>
        /// <param name="attributeId"></param>
        void DeleteAttribute(int attributeId);

        /// <summary>
        /// 清空用户所有属性
        /// </summary>
        /// <param name="customerId"></param>
        void ClearAttribute(int customerId);

        /// <summary>
        /// 根据属性值获取用户ID
        /// </summary>
        /// <typeparam name="TPropType"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        int GetCustomerIdByAttributeValue<TPropType>(TPropType value);
        
    }
}