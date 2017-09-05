/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

if not exists(select * from dbo.Status where Name = 'Not Published')
	insert into dbo.Status(Name,Ordinal) values ('Not Published', 0);
if not exists(select * from dbo.Status where Name = 'Published')
	insert into dbo.Status(Name,Ordinal) values ('Published', 1);
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
GO
