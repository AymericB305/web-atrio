CREATE TABLE [dbo].[PersonJob]
(
	[Id_Person] INT NOT NULL,
	[Id_Job] INT NOT NULL,
	[Start_Date] DATE NOT NULL,
	[End_Date] DATE NULL,
	PRIMARY KEY ([Id_Person], [Id_Job], [Start_Date]),
	FOREIGN KEY ([Id_Person]) REFERENCES [dbo].[Person](Id) ON DELETE CASCADE,
	FOREIGN KEY ([Id_Job]) REFERENCES [dbo].[Job](Id) ON DELETE CASCADE
)
