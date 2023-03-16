namespace CRMS1.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roommodelupdate : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Rooms", newName: "ConferenceRooms");
            AddColumn("dbo.ConferenceRooms", "Capacity", c => c.Int(nullable: false));
            DropColumn("dbo.ConferenceRooms", "RoomName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConferenceRooms", "RoomName", c => c.String());
            DropColumn("dbo.ConferenceRooms", "Capacity");
            RenameTable(name: "dbo.ConferenceRooms", newName: "Rooms");
        }
    }
}
