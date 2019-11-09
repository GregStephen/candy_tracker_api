
IF not exists (SELECT * FROM sys.tables WHERE [name] = 'Candy')
	BEGIN
	CREATE TABLE [Candy]
	(
		[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
		[Name] NVARCHAR(255) not null,
		[TypeId] INT not null,
		[Size] INT not null,
		[ImgUrl] NVARCHAR(max) not null
	)
	END
ELSE
	PRINT 'Candy table already exists'

IF not exists (SELECT * FROM sys.tables WHERE [name] = 'Type')
	BEGIN
	CREATE TABLE [Type]
	(
		[Id] INT PRIMARY KEY IDENTITY (1,1),
		[Name] NVARCHAR(255) not null
	)
	END
ELSE
	PRINT 'Type table already exists!'

IF not exists (SELECT * FROM sys.tables WHERE [name] = 'Trade')
	BEGIN
	CREATE TABLE [Trade]
	(
		[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
		[UserCandyId] UNIQUEIDENTIFIER not null
	)
	END
ELSE
	PRINT 'Trade table already exists!'

IF not exists (SELECT * FROM sys.tables WHERE [name] = 'User')
	BEGIN
	CREATE TABLE [User]
	(
		[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
		[FirstName] NVARCHAR(255) not null,
		[LastName] NVARCHAR(255) not null,
		[FavoriteTypeOfCandyId] INT not null,
		[AmountOfCandyEaten] INT not null default(0),
		[AmountOfCandyDonated] INT not null default(0)
	)
	END
ELSE
	PRINT 'User table already exists'

IF not exists (SELECT * FROM sys.tables WHERE [name] = 'UserCandy')
	BEGIN
	CREATE TABLE [UserCandy]
	(
		[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
		[UserId] UNIQUEIDENTIFIER not null,
		[CandyId] UNIQUEIDENTIFIER not null
	)
	END
ELSE
	PRINT 'UserCandy table already exists'

IF not exists (SELECT * FROM sys.foreign_keys WHERE [name] = 'FK_Candy_Type')
	BEGIN
	ALTER TABLE [Candy]
	ADD CONSTRAINT FK_Candy_Type 
		FOREIGN KEY (TypeId) 
		REFERENCES [Type] (Id)
	END
ELSE
	PRINT 'Foreign key FK_Candy_Type already exists'

IF not exists (SELECT * FROM sys.foreign_keys WHERE [name] = 'FK_UserCandy_User')
	BEGIN
	ALTER TABLE [UserCandy]
	ADD CONSTRAINT FK_UserCandy_User
		FOREIGN KEY (UserId)
		REFERENCES [User] (Id)
	END
ELSE
	PRINT 'Foregin key FK_UserCandy_User already eists'

IF not exists (SELECT * FROM sys.foreign_keys WHERE [name] = 'FK_UserCandy_Candy')
	BEGIN
	ALTER TABLE [UserCandy]
	ADD CONSTRAINT FK_UserCandy_Candy
		FOREIGN KEY (CandyId)
		REFERENCES [Candy] (Id)
	END
ELSE
	PRINT 'Foregin key FK_UserCandy_Candy already eists'

IF not exists (SELECT * FROM sys.foreign_keys WHERE [name] = 'FK_User_Type')
	BEGIN
	ALTER TABLE [User]
	ADD CONSTRAINT FK_User_Type
		FOREIGN KEY (FavoriteTypeOfCandyId)
		REFERENCES [Type] (Id)
	END
ELSE
	PRINT 'Foregin key FK_User_Type already eists'

IF not exists (SELECT * FROM sys.foreign_keys WHERE [name] = 'FK_Trade_UserCandy')
	BEGIN
	ALTER TABLE [Trade]
	ADD CONSTRAINT FK_Trade_UserCandy
		FOREIGN KEY (UserCandyId)
		REFERENCES [UserCandy] (Id)
	END
ELSE
	PRINT 'Foregin key FK_Trade_UserCandy already eists'

/************************************
INSERT INTO [Type]([Name])
VALUES ('Gummy'),
		('Jelly Beans'),
		('Lollipops and Suckers'),
		('Rock Candy'),
		('Taffy'),
		('Asian'),
		('Bite Size'),
		('Bubble Gum'),
		('Bulk Chocolates'),
		('Mini Packs'),
		('Candy Bars'),
		('Candy Coated Popcorn'),
		('Candy Sticks'),
		('CBD Candy'),
		('Chewing Gum'),
		('Chewy Candy'),
		('Fizzies'),
		('Fruit Drops'),
		('Giant Candy'),
		('Gumballs'),
		('Gumdrops'),
		('Hard Candy'),
		('Jawbreakers'),
		('Liquid Candy'),
		('Little Candy'),
		('Movie Theatre Candy'),
		('Novelty Candy'),
		('Powder & Particle'),
		('Ribbon Candy'),
		('Spicy Candy'),
		('Sweet Tarts Candy')
***********************************/

/*
INSERT INTO [User]([FirstName], [LastName], [FavoriteTypeOfCandyId])
	VALUES ('Greg', 'Stephen', 2)
*/

SELECT u.FirstName + ' ' + u.LastName as FullName, t.Name AS FavoriteCandy
FROM [User] u
	JOIN [Type] t
	ON u.FavoriteTypeOfCandyId = t.Id

SELECT c.Id, c.Name, c.Price, t.Name 
FROM Candy c
	JOIN [Type] t
		ON c.TypeId = t.Id