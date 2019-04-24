namespace OneCountryWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class peronaldetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OneUser", "FullName", c => c.String(nullable: false, maxLength: 1500));
            AddColumn("dbo.OneUser", "Nic", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OneUser", "Nic");
            DropColumn("dbo.OneUser", "FullName");
        }
    }
}
