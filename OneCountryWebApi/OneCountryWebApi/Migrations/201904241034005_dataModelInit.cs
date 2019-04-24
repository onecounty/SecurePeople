namespace OneCountryWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataModelInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OneAction",
                c => new
                    {
                        ActionId = c.Int(nullable: false, identity: true),
                        ActionName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ActionId);
            
            CreateTable(
                "dbo.OneReport",
                c => new
                    {
                        OneReportId = c.Int(nullable: false, identity: true),
                        CaseId = c.String(maxLength: 150),
                        Description = c.String(nullable: false, maxLength: 500),
                        UploadedLat = c.Double(nullable: false),
                        UploadedLong = c.Double(nullable: false),
                        LocationLat = c.Double(nullable: false),
                        LocationLong = c.Double(nullable: false),
                        MobileNumber = c.String(nullable: false, maxLength: 10),
                        ActionId = c.Int(nullable: false),
                        PhotoUrl = c.String(maxLength: 225),
                        LastActionDescription = c.String(),
                        LastActionTakenBy = c.String(),
                        ApproximateArea = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        LastActionTakenDate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OneReportId)
                .ForeignKey("dbo.OneAction", t => t.ActionId, cascadeDelete: true)
                .Index(t => t.ActionId);
            
            CreateTable(
                "dbo.OneRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.OneUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.OneRole", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.OneUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.OneUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UniqueDeviceId = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.OneUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OneUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OneUserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.OneUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OneUserRole", "UserId", "dbo.OneUser");
            DropForeignKey("dbo.OneUserLogin", "UserId", "dbo.OneUser");
            DropForeignKey("dbo.OneUserClaim", "UserId", "dbo.OneUser");
            DropForeignKey("dbo.OneUserRole", "RoleId", "dbo.OneRole");
            DropForeignKey("dbo.OneReport", "ActionId", "dbo.OneAction");
            DropIndex("dbo.OneUserLogin", new[] { "UserId" });
            DropIndex("dbo.OneUserClaim", new[] { "UserId" });
            DropIndex("dbo.OneUser", "UserNameIndex");
            DropIndex("dbo.OneUserRole", new[] { "RoleId" });
            DropIndex("dbo.OneUserRole", new[] { "UserId" });
            DropIndex("dbo.OneRole", "RoleNameIndex");
            DropIndex("dbo.OneReport", new[] { "ActionId" });
            DropTable("dbo.OneUserLogin");
            DropTable("dbo.OneUserClaim");
            DropTable("dbo.OneUser");
            DropTable("dbo.OneUserRole");
            DropTable("dbo.OneRole");
            DropTable("dbo.OneReport");
            DropTable("dbo.OneAction");
        }
    }
}
