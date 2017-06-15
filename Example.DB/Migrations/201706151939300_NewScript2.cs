namespace Example.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewScript2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Contact", "Birfsday");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contact", "Birfsday", c => c.DateTime());
        }
    }
}
