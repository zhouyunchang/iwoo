namespace Erp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Process : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Processes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        CategoryId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Process_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProcessCategories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.CbenUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.CbenUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.CbenUsers", t => t.LastModifierUserId)
                .Index(t => t.CategoryId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.ProcessCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ProcessCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CbenUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.CbenUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.CbenUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Processes", "LastModifierUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.Processes", "DeleterUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.Processes", "CreatorUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.Processes", "CategoryId", "dbo.ProcessCategories");
            DropForeignKey("dbo.ProcessCategories", "LastModifierUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.ProcessCategories", "DeleterUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.ProcessCategories", "CreatorUserId", "dbo.CbenUsers");
            DropIndex("dbo.ProcessCategories", new[] { "CreatorUserId" });
            DropIndex("dbo.ProcessCategories", new[] { "LastModifierUserId" });
            DropIndex("dbo.ProcessCategories", new[] { "DeleterUserId" });
            DropIndex("dbo.Processes", new[] { "CreatorUserId" });
            DropIndex("dbo.Processes", new[] { "LastModifierUserId" });
            DropIndex("dbo.Processes", new[] { "DeleterUserId" });
            DropIndex("dbo.Processes", new[] { "CategoryId" });
            DropTable("dbo.ProcessCategories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ProcessCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Processes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Process_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
