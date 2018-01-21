using Abp.Web.Mvc.Controllers.Results;

namespace NetCommunitySolution.Web.Framework.Mvc
{
    public class NullJsonResult : AbpJsonResult
    {
        public NullJsonResult() : base(null)
        {
            //TODO test
        }
    }
}