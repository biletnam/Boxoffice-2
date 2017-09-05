﻿/*
Deployment script for Clearsoft.Boxoffice

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Clearsoft.Boxoffice"
:setvar DefaultFilePrefix "Clearsoft.Boxoffice"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL13.SQL2016\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL13.SQL2016\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
USE [$(DatabaseName)];


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating [dbo].[Event]...';


GO
CREATE TABLE [dbo].[Event] (
    [EventId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (100) NOT NULL,
    [StatusId]      BIGINT         NOT NULL,
    [CreatedDate]   DATETIME2 (7)  NOT NULL,
    [CreatedUserId] BIGINT         NOT NULL,
    [ts]            ROWVERSION     NOT NULL,
    PRIMARY KEY CLUSTERED ([EventId] ASC)
);


GO
PRINT N'Creating [dbo].[EventPerformance]...';


GO
CREATE TABLE [dbo].[EventPerformance] (
    [EventId]       BIGINT     NOT NULL,
    [PerformanceId] BIGINT     NOT NULL,
    [ts]            ROWVERSION NOT NULL,
    PRIMARY KEY CLUSTERED ([EventId] ASC, [PerformanceId] ASC)
);


GO
PRINT N'Creating [dbo].[EventPerformance].[ix_EventPerformance_PerformanceId]...';


GO
CREATE NONCLUSTERED INDEX [ix_EventPerformance_PerformanceId]
    ON [dbo].[EventPerformance]([PerformanceId] ASC);


GO
PRINT N'Creating [dbo].[Performance]...';


GO
CREATE TABLE [dbo].[Performance] (
    [PerformanceId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [StatusId]        BIGINT        NOT NULL,
    [PerformanceDate] DATETIME2 (7) NOT NULL,
    [CreatedDate]     DATETIME2 (7) NOT NULL,
    [CreatedUserId]   BIGINT        NOT NULL,
    [ts]              ROWVERSION    NOT NULL,
    PRIMARY KEY CLUSTERED ([PerformanceId] ASC)
);


GO
PRINT N'Creating [dbo].[Status]...';


GO
CREATE TABLE [dbo].[Status] (
    [StatusId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (100) NOT NULL,
    [Ordinal]  INT            NOT NULL,
    [ts]       ROWVERSION     NOT NULL,
    PRIMARY KEY CLUSTERED ([StatusId] ASC)
);


GO
PRINT N'Creating [dbo].[User]...';


GO
CREATE TABLE [dbo].[User] (
    [UserId]    BIGINT        IDENTITY (1, 1) NOT NULL,
    [Firstname] NVARCHAR (50) NOT NULL,
    [Lastname]  NVARCHAR (50) NOT NULL,
    [Username]  NVARCHAR (50) NOT NULL,
    [ts]        ROWVERSION    NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);


GO
PRINT N'Creating unnamed constraint on [dbo].[Event]...';


GO
ALTER TABLE [dbo].[Event]
    ADD FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([StatusId]);


GO
PRINT N'Creating unnamed constraint on [dbo].[Event]...';


GO
ALTER TABLE [dbo].[Event]
    ADD FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[User] ([UserId]);


GO
PRINT N'Creating unnamed constraint on [dbo].[EventPerformance]...';


GO
ALTER TABLE [dbo].[EventPerformance]
    ADD FOREIGN KEY ([PerformanceId]) REFERENCES [dbo].[Performance] ([PerformanceId]);


GO
PRINT N'Creating unnamed constraint on [dbo].[EventPerformance]...';


GO
ALTER TABLE [dbo].[EventPerformance]
    ADD FOREIGN KEY ([PerformanceId]) REFERENCES [dbo].[Performance] ([PerformanceId]);


GO
PRINT N'Creating unnamed constraint on [dbo].[Performance]...';


GO
ALTER TABLE [dbo].[Performance]
    ADD FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([StatusId]);


GO
PRINT N'Creating unnamed constraint on [dbo].[Performance]...';


GO
ALTER TABLE [dbo].[Performance]
    ADD FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[User] ([UserId]);


GO
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
	insert into dob.Status(Name,Ordinal) values ('Not Published', 0);
if not exists(select * from dbo.Status where Name = 'Published')
	insert into dob.Status(Name,Ordinal) values ('Not Started', 1);
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
GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO
