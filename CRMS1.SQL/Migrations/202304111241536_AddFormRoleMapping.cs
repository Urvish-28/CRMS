namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFormRoleMapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormRoleMappings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FormId = c.Guid(),
                        RoleId = c.Guid(),
                        AllowInsert = c.Boolean(nullable: false),
                        AllowView = c.Boolean(nullable: false),
                        AllowUpdate = c.Boolean(nullable: false),
                        AllowDelete = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        UpdatedOn = c.DateTime(),
                        UpdatedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FormRoleMappings");
        }
    }
}
