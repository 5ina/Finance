using System.Collections.Generic;

namespace NetCommunitySolution.Web.Models.Customers
{
    public class CustomerAgentListModel
    {
        public CustomerAgentListModel()
        {
            this.Customers = new List<MyCustomer>();
        }
        /// <summary>
        /// 推广总人数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 当月推广人数
        /// </summary>
        public int MonthTotal { get; set; }

        public IList<MyCustomer> Customers { get; set; }

        public class MyCustomer
        {
            public int CustomerId { get; set; }

            public string NickName { get; set; }

            public string Mobile { get; set; }

            public string CreateTime { get; set; }

        }
    }
}