CREATE TABLE [Candy]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	[Name] NVARCHAR(255) not null,
	[TypeId] INT not null,
	[Price] DECIMAL(5,2) not null
)
CREATE TABLE [Type]
(
	[Id] INT PRIMARY KEY IDENTITY (1,1),
	[Name] NVARCHAR(255) not null
)