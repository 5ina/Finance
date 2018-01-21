using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.EntityFramework;
using System;
using System.Linq;

namespace NetCommunitySolution.Migrations.SeedData
{
    public class DefaultCustomerData
    {
        private readonly NetCommunitySolutionDbContext _context;

        public DefaultCustomerData(NetCommunitySolutionDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateCustomer();
        }

        private void CreateCustomer()
        {
            var admin = _context.Customer.FirstOrDefault(e => e.Mobile == "18503223172");
            if (admin == null)
            {
                admin = new Domain.Customers.Customer
                {
                    CustomerRoleId = (int)CustomerRole.Vendor,
                    Mobile = "18503223172",
                    PasswordSalt = "WPIUEZ",
                    Password = "D7CB286678A76D14D69AD990709D74E51B293DC4",
                    CreationTime = DateTime.Now,
                    LastModificationTime = DateTime.Now,
                    OpenId = "oJroL1nU1ytsHi0jt0nMzIWr9RcI",
                    NickName = "",
                    IsSubscribe =true,
                };
            }

            _context.Customer.Add(admin);
            _context.SaveChanges();

        }

    }
}
