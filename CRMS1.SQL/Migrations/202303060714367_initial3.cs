namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Roles", "IsActive");
            DropColumn("dbo.Roles", "IsDelete");
            DropColumn("dbo.Roles", "CreatedOn");
            DropColumn("dbo.Roles", "CreatedBy");
            DropColumn("dbo.Roles", "UpdatedOn");
            DropColumn("dbo.Roles", "UpdatedBy");
            DropColumn("dbo.UserRoles", "IsActive");
            DropColumn("dbo.UserRoles", "IsDelete");
            DropColumn("dbo.UserRoles", "CreatedOn");
            DropColumn("dbo.UserRoles", "CreatedBy");
            DropColumn("dbo.UserRoles", "UpdatedOn");
            DropColumn("dbo.UserRoles", "UpdatedBy");
            DropColumn("dbo.Users", "IsActive");
            DropColumn("dbo.Users", "IsDelete");
            DropColumn("dbo.Users", "CreatedOn");
            DropColumn("dbo.Users", "CreatedBy");
            DropColumn("dbo.Users", "UpdatedOn");
            DropColumn("dbo.Users", "UpdatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UpdatedBy", c => c.String());
            AddColumn("dbo.Users", "UpdatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CreatedBy", c => c.String());
            AddColumn("dbo.Users", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserRoles", "UpdatedBy", c => c.String());
            AddColumn("dbo.UserRoles", "UpdatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserRoles", "CreatedBy", c => c.String());
            AddColumn("dbo.UserRoles", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserRoles", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserRoles", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "UpdatedBy", c => c.String());
            AddColumn("dbo.Roles", "UpdatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Roles", "CreatedBy", c => c.String());
            AddColumn("dbo.Roles", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Roles", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "IsActive", c => c.Boolean(nullable: false));
        }
    }
}
