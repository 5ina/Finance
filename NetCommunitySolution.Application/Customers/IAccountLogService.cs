using Abp.Application.Services;
using NetCommunitySolution.Domain.Customers;
using System;
using System.Collections.Generic;

namespace NetCommunitySolution.Customers
{
    public interface IAccountLogService : IApplicationService
    {
        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="log"></param>
        void InsertLog(AccountLog log);

        /// <summary>
        /// 更新日志
        /// </summary>
        /// <param name="log"></param>
        void UpdateLog(AccountLog log);


        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="logId"></param>
        void Delete(int logId);

        /// <summary>
        /// 根据主键获取日志
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        AccountLog GetAccountLogById(int logId);


        /// <summary>
        /// 根据用户获取日志
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="mode">日志类型</param>
        /// <returns></returns>
        IList<AccountLog> GetAccountByCustomerId(int customerId, Int16? mode = null);
    }
}
