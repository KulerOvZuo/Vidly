namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Movies2Customers_addDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies2Customers", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Movies2Customers", "DateRented", c => c.DateTime(nullable: true));
            AddColumn("dbo.Movies2Customers", "DateReturned", c => c.DateTime());

            var date = DateTime.Now.ToString("yyyy-MM-dd");
            Sql($"UPDATE [dbo].[Movies2Customers] SET [DateRented] = '{date}'");

            AlterColumn("dbo.Movies2Customers", "DateRented", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies2Customers", "DateReturned");
            DropColumn("dbo.Movies2Customers", "DateRented");
            DropColumn("dbo.Movies2Customers", "Id");
        }
    }
}
