namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Db_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Serial", c => c.String(maxLength: 50));
            DropColumn("dbo.Orders", "PaymentBankId");
            DropColumn("dbo.Orders", "ReceivableId");
            DropTable("dbo.BankCards",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BankCard_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.ProductImages");
            DropTable("dbo.TopicTemplates");
        }
        
        public override void Down()
        {
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
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        DefaultImage = c.Boolean(nullable: false),
                        Url = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        ProductCode = c.String(maxLength: 20),
                        ShortDescription = c.String(maxLength: 500),
                        FullDescription = c.String(),
                        StockQuantity = c.Int(nullable: false),
                        WithOrder = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Market = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SpecialPrice = c.Decimal(precision: 18, scale: 2),
                        SpecialPriceStartDateTime = c.DateTime(),
                        SpecialPriceEndDateTime = c.DateTime(),
                        AllowReward = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        Published = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentId = c.Int(nullable: false),
                        Published = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        BankCode = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        No = c.String(nullable: false, maxLength: 30),
                        Name = c.String(nullable: false, maxLength: 10),
                        Mobile = c.String(nullable: false, maxLength: 15),
                        Bank = c.String(nullable: false, maxLength: 30),
                        BankCardModeId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BankCard_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "ReceivableId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "PaymentBankId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Serial");
        }
    }
}
