namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roomtableupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConferenceRooms", "Name", c => c.String());
            DropColumn("dbo.ConferenceRooms", "RoomName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConferenceRooms", "RoomName", c => c.String());
            DropColumn("dbo.ConferenceRooms", "Name");
        }
    }
}
