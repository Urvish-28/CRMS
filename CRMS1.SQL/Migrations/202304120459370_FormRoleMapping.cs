namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormRoleMapping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormRoleMappings", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.FormRoleMappings", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FormRoleMappings", "IsDelete");
            DropColumn("dbo.FormRoleMappings", "IsActive");
        }
    }
}
