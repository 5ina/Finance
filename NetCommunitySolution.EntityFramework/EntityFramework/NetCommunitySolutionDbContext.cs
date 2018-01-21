using System.Data.Common;
using System.Data.Entity;
using Abp.EntityFramework;
using NetCommunitySolution.Domain.Articles;
using NetCommunitySolution.Domain.Configuration;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Domain.Directory;
using NetCommunitySolution.Domain.Messages;
using NetCommunitySolution.Domain.Orders;
using NetCommunitySolution.Domain.Seo;

namespace NetCommunitySolution.EntityFramework
{
    public class NetCommunitySolutionDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...
        

        /* setting */

        public virtual IDbSet<Setting> Setting { get; set; }

        /* Customer */
        public virtual IDbSet<Customer> Customer { get; set; }
        public virtual IDbSet<CustomerAttribute> CustomerAttribute { get; set; }

        /* BankCards */

        //public virtual IDbSet<BankCard> BankCard { get; set; }

        /* UrlRecord */
        public virtual IDbSet<UrlRecord> UrlRecord { get; set; }

        /* Articles */
        public virtual IDbSet<Topic> Topic { get; set; }
        
        /* Ordedr */

        public virtual IDbSet<AccountLog> AccountLog { get; set; }
        public virtual IDbSet<Order> Order { get; set; }

        /* Message */
        public virtual IDbSet<Message> Message { get; set; }
        public virtual IDbSet<Area> Area { get; set; }
        
        public NetCommunitySolutionDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in NetCommunitySolutionDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of NetCommunitySolutionDbContext since ABP automatically handles it.
         */
        public NetCommunitySolutionDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public NetCommunitySolutionDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public NetCommunitySolutionDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
