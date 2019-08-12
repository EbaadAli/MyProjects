namespace Assignment8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tracks", "Album_Id", "dbo.Albums");
            DropIndex("dbo.Tracks", new[] { "Album_Id" });
            DropIndex("dbo.Tracks", new[] { "Album_Id1" });
            DropColumn("dbo.Tracks", "Album_Id");
            RenameColumn(table: "dbo.Tracks", name: "Album_Id1", newName: "Album_Id");
            CreateTable(
                "dbo.MediaItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        StringId = c.String(nullable: false, maxLength: 100),
                        Content = c.Binary(),
                        ContentType = c.String(maxLength: 200),
                        Caption = c.String(nullable: false, maxLength: 100),
                        Artist_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.Artist_Id)
                .Index(t => t.Artist_Id);
            
            CreateTable(
                "dbo.RoleClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tracks", "AudioContentType", c => c.String(maxLength: 200));
            AddColumn("dbo.Tracks", "Audio", c => c.Binary());
            AlterColumn("dbo.Tracks", "Album_Id", c => c.Int());
            CreateIndex("dbo.Tracks", "Album_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MediaItems", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.Tracks", new[] { "Album_Id" });
            DropIndex("dbo.MediaItems", new[] { "Artist_Id" });
            AlterColumn("dbo.Tracks", "Album_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Tracks", "Audio");
            DropColumn("dbo.Tracks", "AudioContentType");
            DropTable("dbo.RoleClaims");
            DropTable("dbo.MediaItems");
            RenameColumn(table: "dbo.Tracks", name: "Album_Id", newName: "Album_Id1");
            AddColumn("dbo.Tracks", "Album_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Tracks", "Album_Id1");
            CreateIndex("dbo.Tracks", "Album_Id");
            AddForeignKey("dbo.Tracks", "Album_Id", "dbo.Albums", "Id");
        }
    }
}
