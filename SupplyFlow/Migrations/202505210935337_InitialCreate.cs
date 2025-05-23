namespace SupplyFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemMasters",
                c => new
                    {
                        Item_Id = c.Int(nullable: false, identity: true),
                        Item_Name = c.String(),
                        Item_Description = c.String(),
                    })
                .PrimaryKey(t => t.Item_Id);
            
            CreateTable(
                "dbo.PurchaseOrderItems",
                c => new
                    {
                        Item_ID = c.Int(nullable: false, identity: true),
                        PO_ID = c.Int(nullable: false),
                        ItemMasterId = c.Int(),
                        Product_Code = c.String(maxLength: 50),
                        Description = c.String(),
                        Quantity = c.Int(nullable: false),
                        Unit_Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax_Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Item_ID)
                .ForeignKey("dbo.ItemMasters", t => t.ItemMasterId)
                .ForeignKey("dbo.PurchaseOrders", t => t.PO_ID, cascadeDelete: true)
                .Index(t => t.PO_ID)
                .Index(t => t.ItemMasterId);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        PO_ID = c.Int(nullable: false, identity: true),
                        PO_Number = c.String(maxLength: 50),
                        Supplier_ID = c.Int(nullable: false),
                        PO_Date = c.DateTime(nullable: false),
                        Status = c.String(maxLength: 20),
                        Delivery_Date = c.DateTime(nullable: false),
                        Created_By = c.String(maxLength: 50),
                        Total_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(maxLength: 10),
                        Payment_Terms = c.String(maxLength: 100),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.PO_ID)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_ID, cascadeDelete: true)
                .Index(t => t.Supplier_ID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Supplier_ID = c.Int(nullable: false, identity: true),
                        Company_Name = c.String(maxLength: 100),
                        Address = c.String(),
                        Contact_Person = c.String(),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 100),
                        VAT_Number = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Supplier_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseOrders", "Supplier_ID", "dbo.Suppliers");
            DropForeignKey("dbo.PurchaseOrderItems", "PO_ID", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrderItems", "ItemMasterId", "dbo.ItemMasters");
            DropIndex("dbo.PurchaseOrders", new[] { "Supplier_ID" });
            DropIndex("dbo.PurchaseOrderItems", new[] { "ItemMasterId" });
            DropIndex("dbo.PurchaseOrderItems", new[] { "PO_ID" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.PurchaseOrderItems");
            DropTable("dbo.ItemMasters");
        }
    }
}
