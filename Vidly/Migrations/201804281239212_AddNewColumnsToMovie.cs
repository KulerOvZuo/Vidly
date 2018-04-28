namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumnsToMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "GenreTypeId", c => c.Int());
            AddColumn("dbo.Movies", "ReleaseDate", c => c.DateTime());
            AddColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));
            CreateIndex("dbo.Movies", "GenreTypeId");
            AddForeignKey("dbo.Movies", "GenreTypeId", "dbo.GenreTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "GenreTypeId", "dbo.GenreTypes");
            DropIndex("dbo.Movies", new[] { "GenreTypeId" });
            DropColumn("dbo.Movies", "NumberInStock");
            DropColumn("dbo.Movies", "DateAdded");
            DropColumn("dbo.Movies", "ReleaseDate");
            DropColumn("dbo.Movies", "GenreTypeId");
        }
    }
}
