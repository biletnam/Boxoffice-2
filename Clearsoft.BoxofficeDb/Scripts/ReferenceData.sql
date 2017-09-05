if not exists(select * from dbo.Status where Name = 'Not Published')
	insert into dbo.Status(Name,Ordinal) values ('Not Published', 0);
if not exists(select * from dbo.Status where Name = 'Published')
	insert into dbo.Status(Name,Ordinal) values ('Published', 1);