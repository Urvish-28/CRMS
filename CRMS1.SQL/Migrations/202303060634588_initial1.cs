namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Password", c => c.String());
            DropColumn("dbo.Users", "Contact");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Contact", c => c.String());
            DropColumn("dbo.Users", "Password");
        }
    }
}
