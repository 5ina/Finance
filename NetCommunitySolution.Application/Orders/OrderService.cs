using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Domain.Orders;

namespace NetCommunitySolution.Orders
{
    public class OrderService : NetCommunitySolutionAppServiceBase, IOrderService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// order id
        /// </remarks>
        private const string ORDERS_BY_ID = "net.orders.id-{0}";

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// order id
        /// </remarks>
        private const string ORDERS_BY_SN = "net.orders.sn-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// customer ID
        /// </remarks>
        private const string ORDERS_BY_CUSTOMER_ID = "net.orders.customer-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string ORDERS_PATTERN_KEY = "net.orders.";

        #endregion
        #region Fields

        private readonly IRepository<Order> _orderRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public OrderService(IRepository<Order> orderRepository,ICacheManager cacheManager)
        {
            this._orderRepository = orderRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region  Method
        public int CreateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            var orderId = _orderRepository.InsertAndGetId(order);

            var key = string.Format(ORDERS_BY_CUSTOMER_ID, order.CustomerId);
            _cacheManager.GetCache(key).Remove(key);
            return orderId;
        }

        public IPagedResult<Order> GetAllOrders(int customerId = 0, int agentId = 0,
            AccountMode? orderMode = null, OrderStatus? status = null,
            DateTime? createdFrom = null, DateTime? createdTo = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _orderRepository.GetAll();
            if (createdFrom.HasValue)
                query = query.Where(o => createdFrom.Value <= o.CreationTime);
            if (createdTo.HasValue)
                query = query.Where(o => createdTo.Value >= o.CreationTime);

            if (orderMode.HasValue)
                query = query.Where(o => o.OrderModeId == (int)orderMode.Value);

            if (status.HasValue)
                query = query.Where(o => o.OrderStatusId == (int)status.Value);

            if (customerId > 0)
                query = query.Where(o => customerId == o.CustomerId);
            if (agentId > 0)
                query = query.Where(o => agentId == o.AgentId);

            query = query.OrderByDescending(o => o.OrderStatusId).ThenByDescending(o => o.CreationTime).ThenBy(o => o.CustomerId);

            return new PagedResult<Order>(query, pageIndex, pageSize);
        }

        public IList<Order> GetCustomerOrder(int customerId, int? status = null)
        {
            var key = string.Format(ORDERS_BY_CUSTOMER_ID, customerId);
            return _cacheManager.GetCache(key).Get(key, () =>
            {
                return _orderRepository.GetAllList(o => o.CustomerId == customerId);
            });
        }

        public Order GetOrderByExternal(string external)
        {
            var key = string.Format(ORDERS_BY_SN, external);
            return _cacheManager.GetCache(key).Get(key, () =>
            {
                return _orderRepository.FirstOrDefault(o => o.Serial == external);
            });
        }

        public Order GetOrderById(int orderId)
        {
            var key = string.Format(ORDERS_BY_ID, orderId);
            return _cacheManager.GetCache(key).Get(key, () =>
            {
                return _orderRepository.Get(orderId);
            });
        }

        public Order GetOrderBySn(string orderSn)
        {
            var key = string.Format(ORDERS_BY_SN, orderSn);
            return _cacheManager.GetCache(key).Get(key, () => {
                return _orderRepository.FirstOrDefault(o => o.OrderSn == orderSn);
            });
        }

        public void UpdateOrder(Order order)
        {
            if (order != null)
                _orderRepository.Update(order);

            _cacheManager.RemoveByPattern(ORDERS_PATTERN_KEY);
        }
        #endregion
    }
}
