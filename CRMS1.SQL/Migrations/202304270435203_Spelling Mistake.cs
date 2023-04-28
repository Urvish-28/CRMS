namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpellingMistake : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "StatusId", c => c.Guid(nullable: false));
            DropColumn("dbo.Tickets", "StstusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "StstusId", c => c.Guid(nullable: false));
            DropColumn("dbo.Tickets", "StatusId");
        }
    }
}
