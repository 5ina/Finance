using Abp.Web.Mvc.Controllers;
using System;

namespace NetCommunitySolution.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class NetCommunitySolutionControllerBase : AbpController
    {
        public int CustomerId { get { return Convert.ToInt32(AbpSession.UserId); } }

        //public int CustomerId { get { return 1; } }

        protected NetCommunitySolutionControllerBase()
        {
            LocalizationSourceName = NetCommunitySolutionConsts.LocalizationSourceName;
        }
        
    }


}