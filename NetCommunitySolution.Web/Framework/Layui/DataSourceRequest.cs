namespace NetCommunitySolution.Web.Framework.Layui
{
    /// <summary>
    /// layui的请求类
    /// </summary>
    public class DataSourceRequest
    {
        public int page { get; set; }

        public int limit { get; set; }

        public DataSourceRequest()
        {
            this.page = 1;
            this.limit = 20;
        }
    }
}