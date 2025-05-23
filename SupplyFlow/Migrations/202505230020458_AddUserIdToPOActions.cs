namespace SupplyFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToPOActions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PO_Actions", "User_ID", c => c.Int(nullable: false, defaultValue: 2));
            CreateIndex("dbo.PO_Actions", "User_ID");
            AddForeignKey("dbo.PO_Actions", "User_ID", "dbo.Users", "User_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PO_Actions", "User_ID", "dbo.Users");
            DropIndex("dbo.PO_Actions", new[] { "User_ID" });
            DropColumn("dbo.PO_Actions", "User_ID");
        }
    }
}
