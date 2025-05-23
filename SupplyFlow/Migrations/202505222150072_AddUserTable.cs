namespace SupplyFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        User_ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 255),
                        Password_Hash = c.String(nullable: false),
                        Full_Name = c.String(maxLength: 200),
                        Role = c.String(maxLength: 50),
                        Created_At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.User_ID)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Email" });
            DropTable("dbo.Users");
        }
    }
}
