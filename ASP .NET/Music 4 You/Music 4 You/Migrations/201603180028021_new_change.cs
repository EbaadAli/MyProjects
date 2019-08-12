namespace Assignment8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_change : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Genre = c.String(),
                        Coordinator = c.String(),
                        Name = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        UrlAlbum = c.String(),
                        Artist_Id = c.Int(),
                        Artist_Id1 = c.Int(nullable: false),
                        Track_Id = c.Int(),
                        Track_Id1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.Artist_Id)
                .ForeignKey("dbo.Artists", t => t.Artist_Id1)
                .ForeignKey("dbo.Tracks", t => t.Track_Id)
                .ForeignKey("dbo.Tracks", t => t.Track_Id1)
                .Index(t => t.Artist_Id)
                .Index(t => t.Artist_Id1)
                .Index(t => t.Track_Id)
                .Index(t => t.Track_Id1);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BirthName = c.String(),
                        BirthOrStartDate = c.DateTime(nullable: false),
                        Executive = c.String(),
                        Genre = c.String(),
                        UrlArtist = c.String(),
                        Name = c.String(nullable: false, maxLength: 120),
                        Album_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.Album_Id)
                .Index(t => t.Album_Id);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Clerk = c.String(),
                        Composers = c.String(),
                        Name = c.String(),
                        Genre = c.String(),
                        Album_Id = c.Int(nullable: false),
                        Album_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.Album_Id)
                .ForeignKey("dbo.Albums", t => t.Album_Id1)
                .Index(t => t.Album_Id)
                .Index(t => t.Album_Id1);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tracks", "Album_Id1", "dbo.Albums");
            DropForeignKey("dbo.Albums", "Track_Id1", "dbo.Tracks");
            DropForeignKey("dbo.Albums", "Track_Id", "dbo.Tracks");
            DropForeignKey("dbo.Tracks", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.Artists", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.Albums", "Artist_Id1", "dbo.Artists");
            DropForeignKey("dbo.Albums", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.Tracks", new[] { "Album_Id1" });
            DropIndex("dbo.Tracks", new[] { "Album_Id" });
            DropIndex("dbo.Artists", new[] { "Album_Id" });
            DropIndex("dbo.Albums", new[] { "Track_Id1" });
            DropIndex("dbo.Albums", new[] { "Track_Id" });
            DropIndex("dbo.Albums", new[] { "Artist_Id1" });
            DropIndex("dbo.Albums", new[] { "Artist_Id" });
            DropTable("dbo.Genres");
            DropTable("dbo.Tracks");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
        }
    }
}
