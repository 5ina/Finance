namespace NetCommunitySolution.Security.YeeDto
{
    public class RateResultModel
    {
        public int status { get; set; }
        public string msg { get; set; }
        public int sysmch_id { get; set; }
        public int product_type { get; set; }
        public decimal rate { get; set; }
        public int pay_type { get; set; }
    }
}
