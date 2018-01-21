using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Domain.Orders;
using System;
using System.Collections.Generic;

namespace NetCommunitySolution.Orders
{
    /// <summary>
    /// 订单服务接口
    /// </summary>
    public interface IOrderService: IApplicationService
    {
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        int CreateOrder(Order order);

        /// <summary>
        /// 更新订单
        /// </summary>
        /// <param name="order"></param>
        void UpdateOrder(Order order);

        /// <summary>
        /// 根据主键查询订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Order GetOrderById(int orderId);

        /// <summary>
        /// 根据主键查询订单
        /// </summary>
        /// <param name="orderSn">订单sn</param>
        /// <returns></returns>
        Order GetOrderBySn(string orderSn);

        /// <summary>
        /// 根据易宝交易流水号获取订单
        /// </summary>
        /// <param name="external"></param>
        /// <returns></returns>
        Order GetOrderByExternal(string external);

        /// <summary>
        /// 查询用户订单
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        IList<Order> GetCustomerOrder(int customerId, int? status = null);

        /// <summary>
        /// 查询所有订单
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="agentId"></param>
        /// <param name="orderMode"></param>
        /// <param name="status"></param>
        /// <param name="createdFrom"></param>
        /// <param name="createdTo"></param>\
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedResult<Order> GetAllOrders(int customerId = 0, int agentId = 0,
            AccountMode? orderMode = null, OrderStatus? status = null,
            DateTime? createdFrom = null, DateTime? createdTo = null,
            int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
