namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Setting_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Settings");
        }
    }
}
