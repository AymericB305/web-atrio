CREATE TABLE [dbo].[Person] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [LastName] NVARCHAR(100) NOT NULL,
    [FirstName] NVARCHAR(100) NOT NULL,
    [BirthDate] DATE NOT NULL
)