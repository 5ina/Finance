using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Domain.Orders;
using NetCommunitySolution.Orders;
using NetCommunitySolution.Web.Areas.Admin.Models.Orders;
using NetCommunitySolution.Web.Framework.Htmls;
using NetCommunitySolution.Web.Framework.Layui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Controllers
{
    public class OrderController : AdminBaseController
    {

        #region ctor && Fields
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(IOrderService orderService, 
            ICustomerService customerService)
        {
            this._orderService = orderService;
            this._customerService = customerService;
        }
        #endregion

        #region Utilities

        [NonAction]
        public string CustomerAgent(int customerId)
        {
            if (customerId == 0)
                return "无代理商";
            var customer = _customerService.GetCustomerId(customerId);
            if (customer == null)
                return "无代理商";
            return customer.NickName;

        }
        #endregion

        #region Method

        public ActionResult WithdrawalsList()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult WithdrawalsList(DataSourceRequest command)
        {
            var categories = _orderService.GetAllOrders(orderMode: AccountMode.Extract,
                status: OrderStatus.Pending,
                 pageIndex: command.page - 1,
                 pageSize: command.limit);
            var jsonData = new DataSourceResult
            {
                data = categories.Items.Select(o => 
                {
                    var customer = _customerService.GetCustomerId(o.CustomerId);
                    var item = new
                    {
                        Total = o.OrderTotal,
                        CreateTime = o.CreationTime.ToString("yyyy/MM/dd hh:mm"),
                        NickName = customer.NickName,
                        Id = o.Id,
                    };
                    return item;
                }).ToList(),
                count = categories.TotalCount,
            };
            return AbpJson(jsonData);
        }

        public ActionResult CashList()
        {
            var model = new CashListModel();
            model.AvailableOrderStatuses = OrderStatus.Arrival.EnumToDictionary(e => e.GetDescription(), false).ToList();
            model.AvailableOrderStatuses.Insert(0, new SelectListItem {
                Selected =true,
                Text = "全部状态",
                Value = ""
            });
            return View(model);
        }

        [HttpPost]
        public ActionResult CashList(DataSourceRequest command, CashListModel model)
        {
            var categories = _orderService.GetAllOrders(orderMode: AccountMode.Cash,
                status: model.OrderStatusId,
                 pageIndex: command.page - 1,
                 pageSize: command.limit);
            var jsonData = new DataSourceResult
            {
                data = categories.Items.Select(o =>
                {
                    var customer = _customerService.GetCustomerId(o.CustomerId);
                    var agent = _customerService.GetCustomerId(o.AgentId);
                    var item = new
                    {
                        Total = o.OrderTotal,
                        CreateTime = o.CreationTime.ToString("yyyy/MM/dd hh:mm"),
                        NickName = customer.NickName,
                        OrderSn = o.OrderSn,
                        Id = o.Id,
                        AgentName = agent == null ? "-- / --" : agent.NickName
                    };
                    return item;
                }).ToList(),
                count = categories.TotalCount,
            };
            return AbpJson(jsonData);
        }

        #endregion

        #region Report
        [ChildActionOnly]
        public ActionResult AgentTotal()
        {
            var model = new AgentReportModel();
            var orders = _orderService.GetAllOrders(orderMode: AccountMode.Agent);
            model.AgentTotal = orders.Items.Sum(o => o.OrderTotal);
            return PartialView(model);
        }


        [ChildActionOnly]
        public ActionResult Trading()
        {
            var model = new TradingModel();
            var orders = _orderService.GetAllOrders(orderMode: AccountMode.Cash,
                                                    createdFrom: DateTime.Now.AddDays(-1),
                                                    createdTo: DateTime.Now);
            model.Total = orders.Items.Sum(o => o.OrderTotal);
            model.Number = orders.Items.Count();
            return PartialView(model);
        }


        //套现报表

        [ChildActionOnly]
        public ActionResult CashReport()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult CashReport(DataSourceRequest command)
        {
            var fromDate = DateTime.Now.Date;

            var orders = _orderService.GetAllOrders(orderMode: AccountMode.Cash,
                                        createdFrom: fromDate);

            var jsonData = new DataSourceResult
            {
                data = orders.Items.Select(x => new
                {
                    Id = x.Id,
                    OrderSn = x.OrderSn,
                    Agent = CustomerAgent(x.AgentId),
                    DateTime = x.CreationTime.ToString("yyyy/MM/dd HH:mm:ss"),
                    Account = x.OrderTotal,
                }).ToList(),
                count = orders.TotalCount,
            };

            return AbpJson(jsonData);
        }

        /// <summary>
        /// 代付报表
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PayReport()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult PayReport(DataSourceRequest command)
        { 
            var fromDate = DateTime.Now.Date;

            var orders = _orderService.GetAllOrders(orderMode: AccountMode.Extract,
                                        createdFrom: fromDate);

            var jsonData = new DataSourceResult
            {
                data = orders.Items.Select(x => new
                {
                    Id = x.Id,
                    OrderSn = x.OrderSn,
                    DateTime = x.CreationTime,
                    Account = x.OrderTotal,
                }).ToList(),
                count = orders.TotalCount,
            };

            return AbpJson(jsonData);
        }
        #endregion

    }
}