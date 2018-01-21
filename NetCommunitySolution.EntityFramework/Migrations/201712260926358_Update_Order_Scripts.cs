namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Order_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Agent", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Agent");
        }
    }
}
