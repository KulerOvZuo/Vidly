namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMoviesToCustomer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovieCustomers", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.MovieCustomers", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.MovieCustomers", new[] { "Movie_Id" });
            DropIndex("dbo.MovieCustomers", new[] { "Customer_Id" });
            CreateTable(
                "dbo.Movies2Customers",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.CustomerId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.CustomerId);
            
            DropTable("dbo.MovieCustomers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MovieCustomers",
                c => new
                    {
                        Movie_Id = c.Int(nullable: false),
                        Customer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_Id, t.Customer_Id });
            
            DropForeignKey("dbo.Movies2Customers", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Movies2Customers", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Movies2Customers", new[] { "CustomerId" });
            DropIndex("dbo.Movies2Customers", new[] { "MovieId" });
            DropTable("dbo.Movies2Customers");
            CreateIndex("dbo.MovieCustomers", "Customer_Id");
            CreateIndex("dbo.MovieCustomers", "Movie_Id");
            AddForeignKey("dbo.MovieCustomers", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MovieCustomers", "Movie_Id", "dbo.Movies", "Id", cascadeDelete: true);
        }
    }
}
