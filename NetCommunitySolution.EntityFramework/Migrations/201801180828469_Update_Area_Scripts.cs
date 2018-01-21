namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Area_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Areas", "Province", c => c.String());
            AddColumn("dbo.Areas", "City", c => c.String());
            AddColumn("dbo.Areas", "County", c => c.String());
            AddColumn("dbo.Areas", "areaCode", c => c.String());
            DropColumn("dbo.Areas", "Name");
            DropColumn("dbo.Areas", "Code");
            DropColumn("dbo.Areas", "Level");
            DropColumn("dbo.Areas", "ParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Areas", "ParentId", c => c.Int(nullable: false));
            AddColumn("dbo.Areas", "Level", c => c.Int(nullable: false));
            AddColumn("dbo.Areas", "Code", c => c.String());
            AddColumn("dbo.Areas", "Name", c => c.String());
            DropColumn("dbo.Areas", "areaCode");
            DropColumn("dbo.Areas", "County");
            DropColumn("dbo.Areas", "City");
            DropColumn("dbo.Areas", "Province");
        }
    }
}
