namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "CreatedOn", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Roles", "CreatedBy", c => c.String());
            AddColumn("dbo.Roles", "UpdatedOn", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Roles", "UpdatedBy", c => c.String());
            AddColumn("dbo.UserRoles", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserRoles", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserRoles", "CreatedOn", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.UserRoles", "CreatedBy", c => c.String());
            AddColumn("dbo.UserRoles", "UpdatedOn", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.UserRoles", "UpdatedBy", c => c.String());
            AddColumn("dbo.Users", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "CreatedOn", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Users", "CreatedBy", c => c.String());
            AddColumn("dbo.Users", "UpdatedOn", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Users", "UpdatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UpdatedBy");
            DropColumn("dbo.Users", "UpdatedOn");
            DropColumn("dbo.Users", "CreatedBy");
            DropColumn("dbo.Users", "CreatedOn");
            DropColumn("dbo.Users", "IsDelete");
            DropColumn("dbo.Users", "IsActive");
            DropColumn("dbo.UserRoles", "UpdatedBy");
            DropColumn("dbo.UserRoles", "UpdatedOn");
            DropColumn("dbo.UserRoles", "CreatedBy");
            DropColumn("dbo.UserRoles", "CreatedOn");
            DropColumn("dbo.UserRoles", "IsDelete");
            DropColumn("dbo.UserRoles", "IsActive");
            DropColumn("dbo.Roles", "UpdatedBy");
            DropColumn("dbo.Roles", "UpdatedOn");
            DropColumn("dbo.Roles", "CreatedBy");
            DropColumn("dbo.Roles", "CreatedOn");
            DropColumn("dbo.Roles", "IsDelete");
            DropColumn("dbo.Roles", "IsActive");
        }
    }
}
