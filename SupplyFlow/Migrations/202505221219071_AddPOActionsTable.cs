namespace SupplyFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPOActionsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PO_Actions",
                c => new
                    {
                        Action_ID = c.Int(nullable: false, identity: true),
                        PO_ID = c.Int(nullable: false),
                        Action_Timestamp = c.DateTime(nullable: false),
                        Before_Edit = c.String(),
                        After_Edit = c.String(),
                    })
                .PrimaryKey(t => t.Action_ID)
                .ForeignKey("dbo.PurchaseOrders", t => t.PO_ID, cascadeDelete: true)
                .Index(t => t.PO_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PO_Actions", "PO_ID", "dbo.PurchaseOrders");
            DropIndex("dbo.PO_Actions", new[] { "PO_ID" });
            DropTable("dbo.PO_Actions");
        }
    }
}
