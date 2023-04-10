namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFormMst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormMsts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        NavigateURL = c.String(),
                        ParentFormId = c.Guid(),
                        FormAccessCode = c.String(),
                        DisplayOrder = c.Int(nullable: false),
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
            DropTable("dbo.FormMsts");
        }
    }
}
