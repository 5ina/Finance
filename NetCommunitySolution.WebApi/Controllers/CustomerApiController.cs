using Abp.AutoMapper;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Models;
using NetCommunitySolution.Security;
using System.Web.Http;

namespace NetCommunitySolution.Controllers
{
    public class CustomerApiController : ApiService
    {

        #region Ctor && Field

        private readonly ICustomerService _customerService;
        private readonly IEncryptionService _encryptionService;

        public CustomerApiController(ICustomerService customerService,
            IEncryptionService encryptionService)
        {
            this._customerService = customerService;
            this._encryptionService = encryptionService;
        }
        #endregion


        #region Method

        [HttpPost]
        public CustomerInfoModel Index()
        {
            var customer = _customerService.GetCustomerId(this.CustomerId);
            var model = customer.MapTo<CustomerInfoModel>();
            model.CustomerAvatar = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.Avatar);
            model.Authentication = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.Authentication);
            model.Agent = customer.GetCustomerAttributeValue<bool>(CustomerAttributeNames.Agent);
            model.Account = customer.GetCustomerAttributeValue<decimal>(CustomerAttributeNames.Account);
            model.Promoter = customer.Agent == 0;

            return model;
        }
        #endregion

    }
}
