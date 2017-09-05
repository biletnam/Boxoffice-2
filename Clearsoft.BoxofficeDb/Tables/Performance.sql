CREATE TABLE [dbo].[Performance] (
	[PerformanceId]		BIGINT IDENTITY(1,1)	NOT NULL,
	[StatusId]		BIGINT					NOT NULL,
	[PerformanceDate]	DATETIME2(7)			NOT NULL,
	[CreatedDate]	DATETIME2(7)			NOT NULL,
	[CreatedUserId]	BIGINT					NOT NULL,
	[ts]			rowversion				NOT NULL,

	PRIMARY KEY CLUSTERED ([PerformanceId] ASC),
	FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([StatusId]),
	FOREIGN KEY (CreatedUserId) REFERENCES [dbo].[User] ([UserId])
);
