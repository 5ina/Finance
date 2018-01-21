using System.Collections.Generic;

namespace NetCommunitySolution.Domain.BankCards
{
    public class AccountOpenCode
    {
        /// <summary>
        /// 获取地址
        /// </summary>
        /// <returns></returns>
        public static List<Address> GetAddressCode()
        {
            var address=  new List<Address>();
            //北京
            address.Add(new Address
            {
                Cities = new List<Address> { new Address { Name = "北京市"} },
                Name = "北京市"
            });
            //天津
            address.Add(new Address
            {
                Cities = new List<Address> { new Address { Name = "天津市" } },
                Name = "天津市"
            });
            //河北
            address.Add(new Address
            {
                Cities = new List<Address> {
                    new Address { Name = "石家庄市" },
                    new Address { Name = "唐山市" },
                    new Address { Name = "秦皇岛市" },
                    new Address { Name = "邯郸市" },
                    new Address { Name = "邢台市" },
                    new Address { Name = "保定市" },
                    new Address { Name = "张家口市" },
                    new Address { Name = "承德市" },
                    new Address { Name = "沧州市" },
                    new Address { Name = "廊坊市" },
                    new Address { Name = "衡水市" },
                },
                Name = "河北省"
            });
            //山西省
            address.Add(new Address
            {
                Cities = new List<Address> {
                    new Address { Name = "太原市" },
                    new Address { Name = "大同市" },
                    new Address { Name = "阳泉市" },
                    new Address { Name = "长治市" },
                    new Address { Name = "晋城市" },
                    new Address { Name = "朔州市" },
                    new Address { Name = "忻州市" },
                    new Address { Name = "吕梁市" },
                    new Address { Name = "晋中市" },
                    new Address { Name = "临汾市" },
                    new Address { Name = "运城市" },
                },
                Name = "山西省"
            });
            return address;
        }

        public class Address
        {
            public Address()
            {
                this.Cities = new List<Address>();
            }
            public string Name { get; set; }

            public List<Address> Cities { get; set; }
        }
    }

}
