CREATE TABLE [dbo].[EventPerformance](
	[EventId]	BIGINT	NOT NULL,
	[PerformanceId]	BIGINT	NOT NULL,
	[ts]	rowversion	NOT NULL,
	PRIMARY KEY (EventId, PerformanceId),
	FOREIGN KEY ([PerformanceId]) REFERENCES [dbo].[Performance] ([PerformanceId]),
	FOREIGN KEY (PerformanceId) REFERENCES [dbo].[Performance] ([PerformanceId])
);
go

create index ix_EventPerformance_PerformanceId on EventPerformance(PerformanceId)
go