namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMovies : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Movies] ON
INSERT INTO [dbo].[Movies] ([Id], [Name], [GenreId], [ReleaseDate], [DateAdded], [NumberInStock], [NumberAvailable]) VALUES (1, N'Jurassic Park', 2, N'1993-06-09 00:00:00', N'2019-04-16 00:00:00', 7, 7)
INSERT INTO [dbo].[Movies] ([Id], [Name], [GenreId], [ReleaseDate], [DateAdded], [NumberInStock], [NumberAvailable]) VALUES (2, N'Deadpool', 5, N'2016-02-10 00:00:00', N'2019-04-16 00:00:00', 14, 14)
INSERT INTO [dbo].[Movies] ([Id], [Name], [GenreId], [ReleaseDate], [DateAdded], [NumberInStock], [NumberAvailable]) VALUES (3, N'Ace Ventura: Pet Detective', 5, N'1994-02-04 00:00:00', N'2019-04-16 00:00:00', 13, 13)
INSERT INTO [dbo].[Movies] ([Id], [Name], [GenreId], [ReleaseDate], [DateAdded], [NumberInStock], [NumberAvailable]) VALUES (4, N'Die Hard', 1, N'1988-07-12 00:00:00', N'2019-04-16 00:00:00', 6, 6)
INSERT INTO [dbo].[Movies] ([Id], [Name], [GenreId], [ReleaseDate], [DateAdded], [NumberInStock], [NumberAvailable]) VALUES (5, N'Dragon Ball Super: Broly', 1, N'2018-09-12 00:00:00', N'2019-04-16 00:00:00', 12, 12)
INSERT INTO [dbo].[Movies] ([Id], [Name], [GenreId], [ReleaseDate], [DateAdded], [NumberInStock], [NumberAvailable]) VALUES (6, N'John Wick', 1, N'2015-01-01 00:00:00', N'2019-04-16 00:00:00', 6, 6)
SET IDENTITY_INSERT [dbo].[Movies] OFF");
        }
        
        public override void Down()
        {
        }
    }
}
