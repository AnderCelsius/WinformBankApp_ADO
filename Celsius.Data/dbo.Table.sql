CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [FullName] NVARCHAR(125) NOT NULL, 
    [Email] NVARCHAR(125) NOT NULL, 
    [PasswordHash] NVARCHAR(125) NOT NULL, 
    [PasswordKey] NVARCHAR(125) NOT NULL, 
    [DateCreated] DATETIME NOT NULL
)
