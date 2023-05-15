namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableUserId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AuditLogs", "UserId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AuditLogs", "UserId", c => c.Guid(nullable: false));
        }
    }
}
