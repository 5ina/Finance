using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.BankCards;

namespace NetCommunitySolution.BankCards
{
    public class BankCardService : NetCommunitySolutionAppServiceBase, IBankCardService
    {
        #region Fields
        private readonly IRepository<BankCard> _bankRepository;
        private readonly ICacheManager _cacheManager;


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : bank ID
        /// </remarks>
        private const string BANKS_BY_ID_KEY = "net.bank.id-{0}";


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : CUSTOMER ID
        /// {1} : BANK MODE 
        /// </remarks>
        private const string BANKS_BY_CUSTOMER_MODE_KEY = "net.bank.bycustomer-{0}-{1}";

        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string BANKS_PATTERN_KEY = "net.bank.";

        #endregion

        #region Ctor


        public BankCardService(IRepository<BankCard> bankRepository, ICacheManager cacheManager)
        {
            this._bankRepository = bankRepository;
            this._cacheManager = cacheManager;
        }
        #endregion


        #region Method
        public int CreateCard(BankCard card)
        {
            if (card == null)
                throw new ArgumentNullException("bankCard");

            var bankId = _bankRepository.InsertAndGetId(card);

            //cache
            _cacheManager.RemoveByPattern(BANKS_PATTERN_KEY);

            return bankId;
        }

        public void DeleteCardById(int cardId)
        {
            _bankRepository.Delete(cardId);
            //cache
            _cacheManager.RemoveByPattern(BANKS_PATTERN_KEY);
        }

        public IPagedResult<BankCard> GetAllCards(int customerId = 0, BankCardMode? cardMode = null, 
            int? bank = null, 
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _bankRepository.GetAll();

            if (customerId > 0)
                query = query.Where(b => b.CustomerId == customerId);

            if (cardMode.HasValue)
                query = query.Where(b => b.BankCardModeId == (int)cardMode.Value);

            if (bank.HasValue)
                query = query.Where(b => b.Bank == bank.Value.ToString());

            query = query.OrderBy(b => b.CustomerId).ThenBy(b => b.CreationTime);

            return new PagedResult<BankCard>(query, pageIndex, pageSize);
        }

        public BankCard GetCardById(int cardId)
        {
            var key = string.Format(BANKS_BY_ID_KEY, cardId);
            return _cacheManager.GetCache(key).Get(key, () =>
            {
                return _bankRepository.Get(cardId);
            });
        }

        public IList<BankCard> GetMyCards(int customerId, BankCardMode? cardMode = null)
        {
            var key = string.Format(BANKS_BY_CUSTOMER_MODE_KEY, customerId, cardMode);
            return _cacheManager.GetCache(key).Get(key, () =>
            {
                var query = _bankRepository.GetAll();
                query = query.Where(b => b.CustomerId == customerId);

                if (cardMode.HasValue)
                    query = query.Where(b => b.BankCardModeId == (int)cardMode.Value);

                query = query.OrderByDescending(b => b.CreationTime);
                return query.ToList();
            });
        }

        public void UpdateCard(BankCard card)
        {
            if (card == null)
                throw new ArgumentNullException("bankCard");

            _bankRepository.Update(card);

            //cache
            _cacheManager.RemoveByPattern(BANKS_PATTERN_KEY);
        }
        #endregion
    }
}
