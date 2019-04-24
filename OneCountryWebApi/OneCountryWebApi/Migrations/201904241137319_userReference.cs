namespace OneCountryWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userReference : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OneReport", "CreatedUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.OneReport", "CreatedUserId");
            AddForeignKey("dbo.OneReport", "CreatedUserId", "dbo.OneUser", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OneReport", "CreatedUserId", "dbo.OneUser");
            DropIndex("dbo.OneReport", new[] { "CreatedUserId" });
            DropColumn("dbo.OneReport", "CreatedUserId");
        }
    }
}
