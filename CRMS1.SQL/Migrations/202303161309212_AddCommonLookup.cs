namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommonLookup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommonLookups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ConfigName = c.String(),
                        ConfigKey = c.String(),
                        ConfigValue = c.String(),
                        DisplayOrder = c.Int(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CommonLookups");
        }
    }
}
