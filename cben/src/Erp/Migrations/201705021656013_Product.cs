namespace Erp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Product : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProcessRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Long(nullable: false),
                        ProcessId = c.Int(nullable: false),
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
                    { "DynamicFilter_ProcessRecord_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CbenUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.CbenUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.CbenUsers", t => t.LastModifierUserId)
                .ForeignKey("dbo.Processes", t => t.ProcessId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ProcessId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductBatchId = c.Int(nullable: false),
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
                    { "DynamicFilter_Product_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CbenUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.CbenUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.CbenUsers", t => t.LastModifierUserId)
                .ForeignKey("dbo.ProductBatches", t => t.ProductBatchId, cascadeDelete: true)
                .Index(t => t.ProductBatchId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.ProductBatches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatchNo = c.String(nullable: false, maxLength: 20),
                        Spec = c.String(nullable: false),
                        TechNo = c.String(nullable: false, maxLength: 20),
                        Diameter = c.Double(nullable: false),
                        Pressure = c.Double(nullable: false),
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
                    { "DynamicFilter_ProductBatch_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CbenUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.CbenUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.CbenUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.ProcessRecordEmployees",
                c => new
                    {
                        ProcessRecord_Id = c.Guid(nullable: false),
                        Employee_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProcessRecord_Id, t.Employee_Id })
                .ForeignKey("dbo.ProcessRecords", t => t.ProcessRecord_Id, cascadeDelete: true)
                .ForeignKey("dbo.Erp_Employee", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.ProcessRecord_Id)
                .Index(t => t.Employee_Id);
            
            AddColumn("dbo.Processes", "OrderNum", c => c.Int(nullable: false));
            AddColumn("dbo.Processes", "GuidePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ProcessCategories", "OrderNum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductBatchId", "dbo.ProductBatches");
            DropForeignKey("dbo.ProductBatches", "LastModifierUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.ProductBatches", "DeleterUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.ProductBatches", "CreatorUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.ProcessRecords", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "LastModifierUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.Products", "DeleterUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.Products", "CreatorUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.ProcessRecords", "ProcessId", "dbo.Processes");
            DropForeignKey("dbo.ProcessRecordEmployees", "Employee_Id", "dbo.Erp_Employee");
            DropForeignKey("dbo.ProcessRecordEmployees", "ProcessRecord_Id", "dbo.ProcessRecords");
            DropForeignKey("dbo.ProcessRecords", "LastModifierUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.ProcessRecords", "DeleterUserId", "dbo.CbenUsers");
            DropForeignKey("dbo.ProcessRecords", "CreatorUserId", "dbo.CbenUsers");
            DropIndex("dbo.ProcessRecordEmployees", new[] { "Employee_Id" });
            DropIndex("dbo.ProcessRecordEmployees", new[] { "ProcessRecord_Id" });
            DropIndex("dbo.ProductBatches", new[] { "CreatorUserId" });
            DropIndex("dbo.ProductBatches", new[] { "LastModifierUserId" });
            DropIndex("dbo.ProductBatches", new[] { "DeleterUserId" });
            DropIndex("dbo.Products", new[] { "CreatorUserId" });
            DropIndex("dbo.Products", new[] { "LastModifierUserId" });
            DropIndex("dbo.Products", new[] { "DeleterUserId" });
            DropIndex("dbo.Products", new[] { "ProductBatchId" });
            DropIndex("dbo.ProcessRecords", new[] { "CreatorUserId" });
            DropIndex("dbo.ProcessRecords", new[] { "LastModifierUserId" });
            DropIndex("dbo.ProcessRecords", new[] { "DeleterUserId" });
            DropIndex("dbo.ProcessRecords", new[] { "ProcessId" });
            DropIndex("dbo.ProcessRecords", new[] { "ProductId" });
            DropColumn("dbo.ProcessCategories", "OrderNum");
            DropColumn("dbo.Processes", "GuidePrice");
            DropColumn("dbo.Processes", "OrderNum");
            DropTable("dbo.ProcessRecordEmployees");
            DropTable("dbo.ProductBatches",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ProductBatch_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Products",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Product_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ProcessRecords",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ProcessRecord_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
