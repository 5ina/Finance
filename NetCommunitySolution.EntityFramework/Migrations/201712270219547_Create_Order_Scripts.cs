namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Order_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderSn = c.String(nullable: false, maxLength: 50),
                        CustomerId = c.Int(nullable: false),
                        OrderStatusId = c.Int(nullable: false),
                        PaymentBankId = c.Int(nullable: false),
                        ReceivableId = c.Int(nullable: false),
                        OrderTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderModeId = c.Int(nullable: false),
                        agentId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Order_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Order_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
