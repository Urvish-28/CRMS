namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roomtableupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConferenceRooms", "RoomName", c => c.String());
            DropColumn("dbo.ConferenceRooms", "RoomNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConferenceRooms", "RoomNo", c => c.String());
            DropColumn("dbo.ConferenceRooms", "RoomName");
        }
    }
}
