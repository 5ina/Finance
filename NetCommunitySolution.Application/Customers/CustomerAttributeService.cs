using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Customers;
using System.Linq;

namespace NetCommunitySolution.Customers
{
    public class CustomerAttributeService : NetCommunitySolutionAppServiceBase,ICustomerAttributeService
    {

        #region Ctor && Field
        /// <summary>
        /// 用户属性{0} 用户主键
        /// </summary>
        private const string CUSTOMER_ATTRIBUTES_BY_CUSTOMERID = "net.customerattributes.bycustomerid-{0}";

        private readonly IRepository<CustomerAttribute> _attributeRepository;
        private readonly ICacheManager _cacheManager;

        public CustomerAttributeService(IRepository<CustomerAttribute> attributeRepository,
            ICacheManager cacheManager)
        {
            this._attributeRepository = attributeRepository;
            this._cacheManager = cacheManager;
        }


        #endregion

        #region Method
        public void ClearAttribute(int customerId)
        {
            _attributeRepository.Delete(x => x.CustomerId == customerId);
            var key = string.Format(CUSTOMER_ATTRIBUTES_BY_CUSTOMERID, customerId);
            _cacheManager.GetCache(key).Remove(customerId.ToString());
        }

        public void DeleteAttribute(int attributeId)
        {
            if (attributeId > 0)
                _attributeRepository.Delete(attributeId);
        }

        public IList<CustomerAttribute> GetAttributeByCustomerId(int customerId)
        {
            var ilist = _attributeRepository.GetAllList(x => x.CustomerId == customerId);
            var key = string.Format(CUSTOMER_ATTRIBUTES_BY_CUSTOMERID, customerId);
            return _cacheManager.GetCache(key).Get(customerId.ToString(), () =>
            {
                return _attributeRepository.GetAllList(x => x.CustomerId == customerId);
            });            
        }

        public int GetCountByValue<TPropType>(string key, TPropType value)
        {
            var query = _attributeRepository.GetAll();
            query = query.Where(a => a.Key.Equals(key) && a.Value.Equals(value.ToString()));

            return query.ToList().Count;
        }

        public int GetCustomerId<TPropType>(string key, TPropType value)
        {
            var query = _attributeRepository.GetAll();
            query = query.Where(a => a.Key == key);
            query = query.Where(a => a.Value == value.ToString());

            if (query.Count() > 0)
                return query.FirstOrDefault().CustomerId;
            else
                return 0;
        }

        public int GetCustomerIdByAttributeValue<TPropType>(TPropType value)
        {
            var query = _attributeRepository.GetAll();
            var entity = query.FirstOrDefault(a => a.Value.Equals(value));

            if (entity == null)
                return 0;
            else
                return entity.CustomerId;
        }

        public void SaveAttribute(CustomerAttribute attribute)
        {
            if (attribute == null || attribute.CustomerId <= 0)
                return;


            if (attribute.Id == 0)
            {
                _attributeRepository.InsertAndGetId(attribute);
            }

            else
            {
                var value = attribute.Value;
                attribute = _attributeRepository.Get(attribute.Id);
                attribute.Value = value;
                _attributeRepository.Update(attribute);
            }
            var key = string.Format(CUSTOMER_ATTRIBUTES_BY_CUSTOMERID, attribute.CustomerId);
            _cacheManager.GetCache(key).Remove(attribute.CustomerId.ToString());
            
        }
        #endregion
    }
}
