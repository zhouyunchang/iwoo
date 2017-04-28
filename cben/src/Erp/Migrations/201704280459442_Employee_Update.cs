namespace Erp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employee_Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Erp_Employee", "User_Id", "dbo.CbenUsers");
            DropIndex("dbo.Erp_Employee", new[] { "User_Id" });
            RenameColumn(table: "dbo.Erp_Employee", name: "UserId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Erp_Employee", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Erp_Employee", name: "__mig_tmp__0", newName: "Id");
            AlterColumn("dbo.Erp_Employee", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Erp_Employee", "UserId", c => c.Long(nullable: false));
            CreateIndex("dbo.Erp_Employee", "UserId");
            AddForeignKey("dbo.Erp_Employee", "UserId", "dbo.CbenUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Erp_Employee", "UserId", "dbo.CbenUsers");
            DropIndex("dbo.Erp_Employee", new[] { "UserId" });
            AlterColumn("dbo.Erp_Employee", "UserId", c => c.Long());
            AlterColumn("dbo.Erp_Employee", "UserId", c => c.Long(nullable: false, identity: true));
            RenameColumn(table: "dbo.Erp_Employee", name: "Id", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Erp_Employee", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Erp_Employee", name: "__mig_tmp__0", newName: "UserId");
            CreateIndex("dbo.Erp_Employee", "User_Id");
            AddForeignKey("dbo.Erp_Employee", "User_Id", "dbo.CbenUsers", "Id");
        }
    }
}
