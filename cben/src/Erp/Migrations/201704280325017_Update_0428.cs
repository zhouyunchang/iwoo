namespace Erp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Update_0428 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Erp_Employee");
            AlterTableAnnotations(
                "dbo.Erp_Employee",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        SerialNumber = c.String(nullable: false, maxLength: 20),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        User_Id = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Employee_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.Erp_Employee", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Erp_Employee", "DeleterUserId", c => c.Long());
            AddColumn("dbo.Erp_Employee", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.Erp_Employee", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.Erp_Employee", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.Erp_Employee", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Erp_Employee", "CreatorUserId", c => c.Long());
            AlterColumn("dbo.Erp_Employee", "UserId", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Erp_Employee", "SerialNumber", c => c.String(nullable: false, maxLength: 20));
            AddPrimaryKey("dbo.Erp_Employee", "UserId");
            CreateIndex("dbo.Erp_Employee", "DeleterUserId");
            CreateIndex("dbo.Erp_Employee", "LastModifierUserId");
            CreateIndex("dbo.Erp_Employee", "CreatorUserId");
            AddForeignKey("dbo.Erp_Employee", "CreatorUserId", "dbo.CbenUsers", "Id");
            AddForeignKey("dbo.Erp_Employee", "DeleterUserId", "dbo.CbenUsers", "Id");
            AddForeignKey("dbo.Erp_Employee", "LastModifierUserId", "dbo.CbenUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Erp_Employee", "LastModifierUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.Erp_Employee", "DeleterUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.Erp_Employee", "CreatorUserId", "dbo.CbenUsers");
            DropIndex("dbo.Erp_Employee", new[] { "CreatorUserId" });
            DropIndex("dbo.Erp_Employee", new[] { "LastModifierUserId" });
            DropIndex("dbo.Erp_Employee", new[] { "DeleterUserId" });
            DropPrimaryKey("dbo.Erp_Employee");
            AlterColumn("dbo.Erp_Employee", "SerialNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Erp_Employee", "UserId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Erp_Employee", "CreatorUserId");
            DropColumn("dbo.Erp_Employee", "CreationTime");
            DropColumn("dbo.Erp_Employee", "LastModifierUserId");
            DropColumn("dbo.Erp_Employee", "LastModificationTime");
            DropColumn("dbo.Erp_Employee", "DeletionTime");
            DropColumn("dbo.Erp_Employee", "DeleterUserId");
            DropColumn("dbo.Erp_Employee", "IsDeleted");
            AlterTableAnnotations(
                "dbo.Erp_Employee",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        SerialNumber = c.String(nullable: false, maxLength: 20),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        User_Id = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Employee_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AddPrimaryKey("dbo.Erp_Employee", "UserId");
        }
    }
}
