namespace Cben.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cben_OAuth2_Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientAuthorizations",
                c => new
                    {
                        AuthorizationId = c.Int(nullable: false, identity: true),
                        CreatedOnUtc = c.DateTime(nullable: false),
                        ClientId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Scope = c.String(),
                        ExpirationDateUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.AuthorizationId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        ClientIdentifier = c.String(nullable: false, maxLength: 50),
                        ClientSecret = c.String(maxLength: 50),
                        Callback = c.String(),
                        Name = c.String(),
                        ClientType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientAuthorizations", "ClientId", "dbo.Clients");
            DropIndex("dbo.ClientAuthorizations", new[] { "ClientId" });
            DropTable("dbo.Clients");
            DropTable("dbo.ClientAuthorizations");
        }
    }
}
