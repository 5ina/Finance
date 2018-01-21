namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Product_Scripts : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        ProductCode = c.String(maxLength: 20),
                        ProductImage = c.String(maxLength: 500),
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
