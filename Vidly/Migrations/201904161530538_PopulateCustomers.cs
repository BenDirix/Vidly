namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCustomers : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Customers] ON
INSERT INTO [dbo].[Customers] ([Id], [Name], [IsSubscribedToNewsLetter], [MembershipTypeId], [BirthDate]) VALUES (1, N'Mary Williams', 1, 1, N'1990-01-01 00:00:00')
INSERT INTO [dbo].[Customers] ([Id], [Name], [IsSubscribedToNewsLetter], [MembershipTypeId], [BirthDate]) VALUES (2, N'John Smith', 1, 1, N'1973-01-01 00:00:00')
INSERT INTO [dbo].[Customers] ([Id], [Name], [IsSubscribedToNewsLetter], [MembershipTypeId], [BirthDate]) VALUES (3, N'Benjamin Dirix', 1, 2, N'1988-01-03 00:00:00')
SET IDENTITY_INSERT [dbo].[Customers] OFF");
        }
        
        public override void Down()
        {
        }
    }
}
