CREATE TABLE [dbo].[Event] (
	[EventId]		BIGINT IDENTITY(1,1)	NOT NULL,
	[Name]			NVARCHAR(100)			NOT NULL,
	[StatusId]		BIGINT					NOT NULL,
	[CreatedDate]	DATETIME2(7)			NOT NULL,
	[CreatedUserId]	BIGINT					NOT NULL,
	[ts]			rowversion				NOT NULL,

	PRIMARY KEY CLUSTERED ([EventId] ASC),
	FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([StatusId]),
	FOREIGN KEY (CreatedUserId) REFERENCES [dbo].[User] ([UserId])
);
