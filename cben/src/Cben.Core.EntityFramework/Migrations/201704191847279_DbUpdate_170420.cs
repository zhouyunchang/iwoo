namespace Cben.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate_170420 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientAuthorizations", "UserId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClientAuthorizations", "UserId", c => c.Int(nullable: false));
        }
    }
}
