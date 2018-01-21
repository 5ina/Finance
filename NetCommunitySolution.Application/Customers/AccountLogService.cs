using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Customers;

namespace NetCommunitySolution.Customers
{
    public class AccountLogService : NetCommunitySolutionAppServiceBase, IAccountLogService
    {
        #region Fields
        private readonly IRepository<AccountLog> _logRepository;
        private readonly ICacheManager _cacheManager;


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : CUSTOMER ID
        /// </remarks>
        private const string ACCOUNT_LOG_BY_CUSTOMER_ID = "net.accountlog.by.customerid-{0}";

        
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string ACCOUNT_LOG_PATTERN_KEY = "net.accountlog.";


        #endregion

        #region Ctor


        public AccountLogService(IRepository<AccountLog> logRepository, ICacheManager cacheManager)
        {
            this._logRepository = logRepository;
            this._cacheManager = cacheManager;
        }
        #endregion
        #region Method
        public void Delete(int logId)
        {
            _logRepository.Delete(logId);
            _cacheManager.RemoveByPattern(ACCOUNT_LOG_PATTERN_KEY);
        }

        public IList<AccountLog> GetAccountByCustomerId(int customerId, Int16? mode = null)
        {
            var key = string.Format(ACCOUNT_LOG_BY_CUSTOMER_ID, customerId);
            var list = _cacheManager.GetCache(key).Get(key, () => {
                var query = _logRepository.GetAll();

                query = query.Where(a => a.CustomerId == customerId);

                query = query.OrderByDescending(a => a.CreationTime);

                return query.ToList();
            });

            if (mode.HasValue)
                list = list.Where(a => a.AccountModeId == mode.Value).ToList();

            return list;

        }

        public AccountLog GetAccountLogById(int logId)
        {
            return _logRepository.Get(logId);
        }

        public void InsertLog(AccountLog log)
        {
            if (log == null)
                throw new ArgumentNullException("accountLog");

            _logRepository.Insert(log);

            //cache
            _cacheManager.RemoveByPattern(ACCOUNT_LOG_PATTERN_KEY);
        }

        public void UpdateLog(AccountLog log)
        {
            if (log == null)
                throw new ArgumentNullException("accountLog");

            _logRepository.Update(log);

            //cache
            _cacheManager.RemoveByPattern(ACCOUNT_LOG_PATTERN_KEY);
        }
        #endregion
    }
}
