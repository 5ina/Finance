namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Customer_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        No = c.String(nullable: false, maxLength: 30),
                        Name = c.String(nullable: false, maxLength: 10),
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
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mobile = c.String(maxLength: 15),
                        NickName = c.String(maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 60),
                        OpenId = c.String(maxLength: 60),
                        CustomerRoleId = c.Int(nullable: false),
                        PasswordSalt = c.String(nullable: false, maxLength: 6),
                        LastModificationTime = c.DateTime(),
                        CreationTime = c.DateTime(nullable: false),
                        VerificationCode = c.String(maxLength: 200),
                        IsSubscribe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Key = c.String(nullable: false, maxLength: 200),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomerAttributes");
            DropTable("dbo.Customers");
            DropTable("dbo.BankCards",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BankCard_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
