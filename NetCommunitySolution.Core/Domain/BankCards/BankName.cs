using System.Collections.Generic;

namespace NetCommunitySolution.Domain.BankCards
{
    public class BankName
    {
        public string Name { get; set; }


        public List<BankName> GetList()
        {
            var names = new List<BankName>();
            names.Add(new BankName { Name = "工商银行" });
            names.Add(new BankName { Name = "农业银行" });
            names.Add(new BankName { Name = "招商银行" });
            names.Add(new BankName { Name = "建设银行" });
            names.Add(new BankName { Name = "交通银行" });
            names.Add(new BankName { Name = "中信银行" });
            names.Add(new BankName { Name = "光大银行" });
            names.Add(new BankName { Name = "平安银行" });
            names.Add(new BankName { Name = "北京银行" });
            names.Add(new BankName { Name = "兴业银行" });
            names.Add(new BankName { Name = "民生银行" });
            names.Add(new BankName { Name = "华夏银行" });
            return names;
        }
        
    }
}
