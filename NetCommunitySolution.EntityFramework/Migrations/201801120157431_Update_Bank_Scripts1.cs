namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Bank_Scripts1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankCards", "BankCode", c => c.String());
            AddColumn("dbo.BankCards", "Province", c => c.String());
            AddColumn("dbo.BankCards", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankCards", "City");
            DropColumn("dbo.BankCards", "Province");
            DropColumn("dbo.BankCards", "BankCode");
        }
    }
}
