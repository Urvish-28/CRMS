namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormMsts", "IsMenu", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FormMsts", "IsMenu");
        }
    }
}
