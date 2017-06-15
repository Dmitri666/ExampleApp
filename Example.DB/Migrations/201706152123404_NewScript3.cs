namespace Example.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewScript3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contact", "Birfsday", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contact", "Birfsday");
        }
    }
}
