namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Movies2Customers_ProperIndentity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies2Customers", "Id", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies2Customers", "Id", c => c.Int(nullable: false));
        }
    }
}
