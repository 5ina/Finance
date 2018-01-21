namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Create_PrivateMessage_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromCustomerId = c.Int(nullable: false),
                        ToCustomerId = c.Int(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 50),
                        Text = c.String(nullable: false, maxLength: 500),
                        IsDeletedByRecipient = c.Boolean(nullable: false),
                        IsDeletedByAuthor = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Message_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Messages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Message_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
