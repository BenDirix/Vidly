namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'605e3398-cdae-448b-baab-987beb17eb6d', N'admin@test.com', 0, N'AFALnVZK4XGCu2fpRwPc7GUJ5T5mPFU7n4tjml4JLJM3x07dFXNITfdW+NPZyI34tg==', N'247d426a-8428-4eec-b538-6f81d8bda062', NULL, 0, 0, NULL, 1, 0, N'admin@test.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6a8cf7eb-797c-404b-bc48-7bbfb773d5f5', N'guest@test.com', 0, N'AL4pV9NAogio2IZGoA22IPTKW2Y6xVGVYNk+VGu0+ktls3nCsFYkmZVV0sgqZrIEXA==', N'a0c6c6ab-161f-4a08-91f5-e221067d435a', NULL, 0, 0, NULL, 1, 0, N'guest@test.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a366fea4-3774-44ee-93ef-46811c89c196', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'605e3398-cdae-448b-baab-987beb17eb6d', N'a366fea4-3774-44ee-93ef-46811c89c196')
");
            
        }

        public override void Down()
        {
        }
    }
}
