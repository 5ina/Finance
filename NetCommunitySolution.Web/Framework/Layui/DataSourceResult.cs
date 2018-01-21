using System.Collections;

namespace NetCommunitySolution.Web.Framework.Layui
{
    public class DataSourceResult
    {
        public DataSourceResult()
        {
            this.msg = "";
        }
        public int code { get; set; }
        public string msg { get; set; }

        public int count { get; set; }

        public IEnumerable data { get; set; }        
    }
}