namespace SupplyFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActionTypeToPOAction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PO_Actions", "Action_Type", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PO_Actions", "Action_Type");
        }
    }
}
