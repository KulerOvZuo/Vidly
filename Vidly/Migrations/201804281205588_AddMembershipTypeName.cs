namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershipTypeName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "Name", c => c.String(nullable: true, maxLength: 255));

            Sql("UPDATE dbo.MembershipTypes SET [name] = 'Pay as You go' WHERE ID = 1");
            Sql("UPDATE dbo.MembershipTypes SET [name] = 'Monthly' WHERE ID = 2");
            Sql("UPDATE dbo.MembershipTypes SET [name] = 'Quater yearly' WHERE ID = 3");
            Sql("UPDATE dbo.MembershipTypes SET [name] = 'Yearly' WHERE ID = 4");

            AlterColumn("dbo.MembershipTypes", "Name", c => c.String(nullable: false, maxLength: 255));           
        }
        
        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "Name");
        }
    }
}
