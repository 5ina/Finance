namespace NetCommunitySolution.Security.YeeDto
{
    public class MchCreateResultModel
    {
        public int status { get; set; }
        public string msg { get; set; }

        public int sysmch_id { get; set; }
        public string username { get; set; }
        public string mobile { get; set; }
        public int out_mch_id { get; set; }
    }
}
