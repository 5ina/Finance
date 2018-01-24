using Abp.AutoMapper;
using NetCommunitySolution.Common;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Configuration;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Security;
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
        private readonly IEncryptionService _encryptionService;
        private readonly ICustomerAttributeService _attributeService;
        private readonly IYeeSevice _yeeService;
        private readonly AccountSetting rateSetting;

        public CustomerController(ICustomerService customerService,
            ICustomerAttributeService attributeService,
            IEncryptionService encryptionService,
            IYeeSevice yeeService,
            ISettingService settingService)
        {
            this._customerService = customerService;
            this._attributeService = attributeService;
            this.rateSetting = settingService.GetAccountSettings();
            this._yeeService = yeeService;
            this._encryptionService = encryptionService;
        }
        #endregion

        #region Utilities
        [NonAction]
        protected void PrepareCustomerRateModel(CustomerRateModel model)
        {
            if (model == null)
                model = new CustomerRateModel();

            model.MinPayment = rateSetting.Payment;
            model.MinRate = rateSetting.BaseRate;
        }
            #endregion

        #region Method
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

        public ActionResult Password()
        {
            var model = new CustomerPasswordModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Password(CustomerPasswordModel model)
        {
            var customer = _customerService.GetCustomerId(this.CustomerId);
            var psd = _encryptionService.CreatePasswordHash(model.OldPassword, customer.PasswordSalt);
            if (model.OldPassword != psd)
                ModelState.AddModelError("", "旧密码输入错误");
            if (!model.NewPassword.Equals(model.ConfirmPassword))
                ModelState.AddModelError("", "两次密码输入的不相同");
            if (ModelState.IsValid)
            {
                var newPsd = _encryptionService.CreatePasswordHash(model.NewPassword, customer.PasswordSalt);

                customer.Password = newPsd;
                _customerService.UpdateCustomer(customer);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        /// <summary>
        /// 设置费率
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetRate(int id)
        {
            var model = new CustomerRateModel();
            PrepareCustomerRateModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult SetRate(CustomerRateModel model)
        {
            if (model.MinPayment < rateSetting.Payment)
                ModelState.AddModelError("", "您设置的单笔费用低于公共费率");
            if (model.MinPayment > rateSetting.Payment)
                ModelState.AddModelError("", "您设置的交易费率低于公共费率");
            
            if (ModelState.IsValid)
            {
                var customer = _customerService.GetCustomerId(model.Id);
                var mchId = customer.GetCustomerAttributeValue<int>(CustomerAttributeNames.SysMchId);
                var rate = model.Rate * 10;
                _yeeService.SetRate(mchId, 1, rate);
                _yeeService.SetRate(mchId, 3, model.Payment);
                return RedirectToAction("Index");
            }
            PrepareCustomerRateModel(model);
            return View(model);

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