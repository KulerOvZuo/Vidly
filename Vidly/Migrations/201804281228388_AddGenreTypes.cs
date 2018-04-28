namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenreTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GenreTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);

            Sql(@"
            SET IDENTITY_INSERT dbo.GenreTypes ON

            INSERT INTO dbo.GenreTypes
                (Id, Name)
                VALUES
                (1, 'Action'),
                (2, 'Comedy'),
                (3, 'Criminal'),
                (4, 'Drama')

            SET IDENTITY_INSERT dbo.GenreTypes OFF");            
        }
        
        public override void Down()
        {
            DropTable("dbo.GenreTypes");
        }
    }
}
