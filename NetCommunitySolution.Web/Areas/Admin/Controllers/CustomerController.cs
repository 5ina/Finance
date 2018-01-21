using Abp.AutoMapper;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Web.Areas.Admin.Models.Customers;
using NetCommunitySolution.Web.Framework.Layui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Controllers
{
    public class CustomerController : AdminBaseController
    {
        #region ctor && Fields
        private readonly ICustomerService _customerService;
        private readonly ICustomerAttributeService _attributeService;

        public CustomerController(ICustomerService customerService, 
            ICustomerAttributeService attributeService)
        {
            this._customerService = customerService;
            this._attributeService = attributeService;
        }
        #endregion

        #region
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, CustomerListModel model)
        {
            var customers = _customerService.GetAllCustomers(keywords: model.Keywords,
                                            isAgent: model.IsAgent,
                                            pageIndex: command.page - 1,
                                            pageSize: command.limit);


            var jsonData = new DataSourceResult
            {
                data = customers.Items.Select(c => new
                {
                    Id = c.Id,
                    NickName = c.NickName,
                    Mobile = c.Mobile,
                    CreateTime = c.CreationTime.ToString("yyyy/MM/dd"),
                }).ToList(),
                count = customers.TotalCount,
            };

            return AbpJson(jsonData);
        }
        #endregion


        #region Report 

        [ChildActionOnly]
        public ActionResult CustomerReport()
        {
            var count = _attributeService.GetCountByValue(CustomerAttributeNames.Agent, true);
            var model = new CustomerReportModel();
            model.AgentCount = count;
            return PartialView(model);
        }
        #endregion

        #region 分部视图
        [ChildActionOnly]
        public ActionResult CustomerHeader()
        {
            var customer = _customerService.GetCustomerId(this.CustomerId);
            var model = customer.MapTo<CustomerModel>();
            model.CustomerAvatar = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.Avatar);
            model.Authentication = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.Authentication);
            model.Agent = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.Agent);
            model.Account = customer.GetCustomerAttributeValue<decimal>(CustomerAttributeNames.Account);
            model.Promoter = customer.Agent == 0;
            return PartialView(model);
        }


        #endregion

        #region customer Echarts
        [ChildActionOnly]
        public ActionResult CustomerCharts()
        {
            return PartialView();
        }
        #endregion

    }
}