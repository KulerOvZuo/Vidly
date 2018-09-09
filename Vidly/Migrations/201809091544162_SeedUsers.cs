namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6bd36449-a4dc-42a2-b814-9db6bc5160e4', N'guest@gmail.com', 0, N'ALNEWN+OghQau0TMwEEfvwXtPydliY/eui6BOUe11Uilftp/KZvy2oF93O/qhRdngQ==', N'ed234277-1cd6-490b-ac97-90e731469831', NULL, 0, 0, NULL, 1, 0, N'guest@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'cbbd508a-35f4-4285-abfe-12c7b5d813ea', N'admin@gmail.com', 0, N'AACFCAID2iSgrjsaa+LFO8v0ReroCX9awvRfS1rQ01k64pdBXvuy5Qx/ElMpIuJhcw==', N'7421fe42-98d3-46a0-abf6-3d8274105a3a', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'dcb8005f-d52c-4d1d-b73d-4620c6ac591e', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cbbd508a-35f4-4285-abfe-12c7b5d813ea', N'dcb8005f-d52c-4d1d-b73d-4620c6ac591e')");
        }
        
        public override void Down()
        {
        }
    }
}
