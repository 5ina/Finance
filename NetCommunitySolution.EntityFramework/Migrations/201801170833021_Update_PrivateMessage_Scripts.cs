namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_PrivateMessage_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "IsRead", c => c.Boolean(nullable: false));
            DropColumn("dbo.Messages", "IsDeletedByRecipient");
            DropColumn("dbo.Messages", "IsDeletedByAuthor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "IsDeletedByAuthor", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "IsDeletedByRecipient", c => c.Boolean(nullable: false));
            DropColumn("dbo.Messages", "IsRead");
        }
    }
}
