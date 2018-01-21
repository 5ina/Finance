namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Topic_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SystemName = c.String(),
                        IncludeInTopMenu = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        AccessibleWhenStoreClosed = c.Boolean(nullable: false),
                        Title = c.String(),
                        Body = c.String(),
                        Published = c.Boolean(nullable: false),
                        TopicTemplateId = c.Int(nullable: false),
                        MetaKeywords = c.String(),
                        MetaDescription = c.String(),
                        MetaTitle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TopicTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ViewPath = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UrlRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityId = c.Int(nullable: false),
                        EntityName = c.String(),
                        Slug = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UrlRecords");
            DropTable("dbo.TopicTemplates");
            DropTable("dbo.Topics");
        }
    }
}
