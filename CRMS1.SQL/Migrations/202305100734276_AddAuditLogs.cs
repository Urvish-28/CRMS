namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditLogs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ExecutionTime = c.DateTime(nullable: false),
                        ExecutionDuration = c.Int(nullable: false),
                        ClientIpAddress = c.String(),
                        BrowserInfo = c.String(),
                        HttpMethod = c.String(),
                        Url = c.String(),
                        Exception = c.String(),
                        HttpStatusCode = c.Int(nullable: false),
                        Comments = c.String(),
                        Parameters = c.String(),
                        Headers = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditLogs");
        }
    }
}
