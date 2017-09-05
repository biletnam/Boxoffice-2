declare @statusId int,
@eventId int,
@userId int

if not exists (select * from [User] where Username = 'nbarlow')
	insert into [dbo].[User] ([Firstname],[Lastname],[Username]) values (N'Boss',N'Hogg',N'nbarlow')
if not exists (select * from [User] where Username = 'jbob')
	insert into [dbo].[User] ([Firstname],[Lastname],[Username]) values (N'Jim',N'Bob',N'jbob')
if not exists (select * from [User] where Username = 'jdoe')
	insert into [dbo].[User] ([Firstname],[Lastname],[Username]) values (N'John',N'Doe',N'jdoe')

if not exists (select * from dbo.[Event] where [Name] = 'Test Event')
	begin
		select top 1 @statusId = StatusId from [Status] order by StatusId;
		select top 1 @userId = UserId from [User] order by UserId;
		insert into dbo.[Event]([Name],StatusId,CreatedDate,CreatedUserId) values ('Test Event', @statusId,GETDATE(),@userId);
end

if not exists (select * from dbo.[Performance] where PerformanceId = 1)
	begin
		select top 1 @statusId = StatusId from [Status] order by StatusId;
		select top 1 @userId = UserId from [User] order by UserId;
		insert into dbo.[Performance](StatusId,CreatedDate,CreatedUserId,PerformanceDate) values (@statusId,GETDATE(),@userId,GETDATE());
end

if not exists (select * from dbo.[Performance] where PerformanceId = 2)
	begin
		select top 1 @statusId = StatusId from [Status] order by StatusId;
		select top 1 @userId = UserId from [User] order by UserId;
		insert into dbo.[Performance](StatusId,CreatedDate,CreatedUserId,PerformanceDate) values (@statusId,GETDATE(),@userId,dateadd(DD,1,getdate()));
end

if not exists (select * from dbo.[Performance] where PerformanceId = 3)
	begin
		select top 1 @statusId = StatusId from [Status] order by StatusId;
		select top 1 @userId = UserId from [User] order by UserId;
		insert into dbo.[Performance](StatusId,CreatedDate,CreatedUserId,PerformanceDate) values (@statusId,GETDATE(),@userId,dateadd(DD,2,getdate()));
end