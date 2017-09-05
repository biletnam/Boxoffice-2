CREATE TABLE [dbo].[User](
	[UserId] BIGINT IDENTITY(1,1) NOT NULL,
	[Firstname] nvarchar(50) not null,
	[Lastname] nvarchar(50) not null,
	[Username] nvarchar(50) not null,
	[ts] rowversion not null,
	PRIMARY KEY (UserId ASC)
);
