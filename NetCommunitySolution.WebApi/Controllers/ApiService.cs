using Abp.Application.Services;
using Abp.WebApi.Controllers;
using System;
using System.Web.Http;

namespace NetCommunitySolution.Controllers
{
    [Authorize]
    public class ApiService : AbpApiController, IApplicationService
    {
        public int CustomerId
        {
            get
            {
                return Convert.ToInt32(User.Identity.Name);
            }
        }
    }
}
