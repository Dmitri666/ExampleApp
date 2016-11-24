namespace Example.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rolle", t => t.Role_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Rolle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Contact", "CreatedBy_Id", c => c.Int());
            CreateIndex("dbo.Contact", "CreatedBy_Id");
            AddForeignKey("dbo.Contact", "CreatedBy_Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contact", "CreatedBy_Id", "dbo.User");
            DropForeignKey("dbo.UserRole", "User_Id", "dbo.User");
            DropForeignKey("dbo.UserRole", "Role_Id", "dbo.Rolle");
            DropIndex("dbo.UserRole", new[] { "User_Id" });
            DropIndex("dbo.UserRole", new[] { "Role_Id" });
            DropIndex("dbo.Contact", new[] { "CreatedBy_Id" });
            DropColumn("dbo.Contact", "CreatedBy_Id");
            DropTable("dbo.Rolle");
            DropTable("dbo.UserRole");
        }
    }
}
