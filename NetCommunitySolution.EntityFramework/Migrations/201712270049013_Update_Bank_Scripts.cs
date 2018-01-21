namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Bank_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankCards", "Mobile", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankCards", "Mobile");
        }
    }
}
