namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class baseentityadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditLogs", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.AuditLogs", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.AuditLogs", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.AuditLogs", "CreatedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.AuditLogs", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.AuditLogs", "UpdatedBy", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuditLogs", "UpdatedBy");
            DropColumn("dbo.AuditLogs", "UpdatedOn");
            DropColumn("dbo.AuditLogs", "CreatedBy");
            DropColumn("dbo.AuditLogs", "CreatedOn");
            DropColumn("dbo.AuditLogs", "IsDelete");
            DropColumn("dbo.AuditLogs", "IsActive");
        }
    }
}
