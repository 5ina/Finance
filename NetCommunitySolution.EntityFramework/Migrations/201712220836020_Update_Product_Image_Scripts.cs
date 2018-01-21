namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Product_Image_Scripts : DbMigration
    {
        public override void Up()
        {
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
            
            DropColumn("dbo.Products", "ProductImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductImage", c => c.String(maxLength: 500));
            DropTable("dbo.ProductImages");
        }
    }
}
