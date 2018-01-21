using Abp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCommunitySolution
{
    public class WebBaseApiController: AbpApiController
    {

        //protected override AbpJsonResult AbpJson(object data, string contentType = null,
        //    Encoding contentEncoding = null, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet,
        //    bool wrapResult = true, bool camelCase = false, bool indented = false)
        //{
        //    return new AbpJsonResult
        //    {
        //        Data = data,
        //        ContentType = contentType,
        //        ContentEncoding = contentEncoding,
        //        JsonRequestBehavior = behavior,
        //        MaxJsonLength = int.MaxValue,
        //        CamelCase = camelCase,
        //        Indented = indented,
        //    };
        //}
    }
}
