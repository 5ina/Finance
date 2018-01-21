namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_AccountLog_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        AccountModeId = c.Short(nullable: false),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Message = c.String(nullable: false, maxLength: 200),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountLogs");
        }
    }
}
