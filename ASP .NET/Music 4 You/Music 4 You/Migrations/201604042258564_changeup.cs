namespace Assignment8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeup : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Albums", new[] { "Artist_Id1" });
            DropIndex("dbo.Albums", new[] { "Track_Id1" });
            AlterColumn("dbo.Albums", "Artist_Id1", c => c.Int());
            AlterColumn("dbo.Albums", "Track_Id1", c => c.Int());
            CreateIndex("dbo.Albums", "Artist_Id1");
            CreateIndex("dbo.Albums", "Track_Id1");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Albums", new[] { "Track_Id1" });
            DropIndex("dbo.Albums", new[] { "Artist_Id1" });
            AlterColumn("dbo.Albums", "Track_Id1", c => c.Int(nullable: false));
            AlterColumn("dbo.Albums", "Artist_Id1", c => c.Int(nullable: false));
            CreateIndex("dbo.Albums", "Track_Id1");
            CreateIndex("dbo.Albums", "Artist_Id1");
        }
    }
}
