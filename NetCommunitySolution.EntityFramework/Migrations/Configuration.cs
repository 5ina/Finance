using NetCommunitySolution.Migrations.SeedData;
using System.Data.Entity.Migrations;

namespace NetCommunitySolution.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<NetCommunitySolution.EntityFramework.NetCommunitySolutionDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "NetCommunitySolution";
        }

        protected override void Seed(NetCommunitySolution.EntityFramework.NetCommunitySolutionDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...
            //new InitialHostDbBuilder(context).Create();
        }
    }
}
