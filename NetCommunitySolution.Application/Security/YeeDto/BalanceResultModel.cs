namespace NetCommunitySolution.Security.YeeDto
{
    /// <summary>
    /// 总分润查询返回实体
    /// </summary>
    public class BalanceResultModel
    {
        public int status { get; set; }
        public string msg { get; set; }

        public int sysmch_id { get; set; }

        public decimal profit { get; set; }
        public int pay_type { get; set; }
    }
}
