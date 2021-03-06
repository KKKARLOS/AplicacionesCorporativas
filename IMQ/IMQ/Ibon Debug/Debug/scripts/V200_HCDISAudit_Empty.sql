USE [master]
GO
/****** Object:  Database [HCDAuditLog]    Script Date: 20/12/2012 11:01:27 ******/
CREATE DATABASE [HCDAuditLog] ON  PRIMARY 
( NAME = N'HCDAuditLog', FILENAME = N'HCDAuditLog.mdf' , SIZE = 8665088KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'HCDAuditLog_log', FILENAME = N'HCDAuditLog_log.ldf' , SIZE = 15993536KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'HCDAuditLog', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HCDAuditLog].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [HCDAuditLog] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HCDAuditLog] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HCDAuditLog] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HCDAuditLog] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HCDAuditLog] SET ARITHABORT OFF 
GO
ALTER DATABASE [HCDAuditLog] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HCDAuditLog] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [HCDAuditLog] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HCDAuditLog] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HCDAuditLog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HCDAuditLog] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HCDAuditLog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HCDAuditLog] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HCDAuditLog] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HCDAuditLog] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HCDAuditLog] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HCDAuditLog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HCDAuditLog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HCDAuditLog] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HCDAuditLog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HCDAuditLog] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HCDAuditLog] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HCDAuditLog] SET RECOVERY FULL 
GO
ALTER DATABASE [HCDAuditLog] SET  MULTI_USER 
GO
ALTER DATABASE [HCDAuditLog] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HCDAuditLog] SET DB_CHAINING OFF 
GO
USE [HCDAuditLog]
GO
/****** Object:  User [sii]    Script Date: 20/12/2012 11:01:27 ******/
CREATE USER [sii] FOR LOGIN [sii] WITH DEFAULT_SCHEMA=[dbo]
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'sii'
GO
/****** Object:  StoredProcedure [dbo].[SII.Framework.Services.AddLOPDAuditEntry]    Script Date: 20/12/2012 11:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Roberto Muñoz
-- Create date: 10/06/2008
-- Description:	Add a new entry to AuditLog
-- =============================================
CREATE PROCEDURE [dbo].[SII.Framework.Services.AddLOPDAuditEntry]
	@Category NVARCHAR(128) = '',
	@Message NVARCHAR(1024) = '',
	@EntityID INT = 0,
	@Entity NVARCHAR(128) = '',
	@Action INT = 0,
	@Module NVARCHAR(128) = '',
	@Method NVARCHAR(128) = '',
	@User NVARCHAR(128) = ''
AS
BEGIN

	SET NOCOUNT ON;

	INSERT INTO AuditLog
		([Category], [Message], [DateTime], [EntityID], [Entity], [Action], 
		 [Module], [Method], [User])
	VALUES
		(@Category, @Message, GetDate(), @EntityID, @Entity, @Action, 
	     @Module, @Method, @User)
END

GO
/****** Object:  Table [dbo].[AuditLog]    Script Date: 20/12/2012 11:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](128) NOT NULL,
	[Message] [nvarchar](1024) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[EntityID] [int] NOT NULL,
	[Entity] [nvarchar](128) NOT NULL,
	[Action] [int] NOT NULL,
	[Module] [nvarchar](128) NOT NULL,
	[Method] [nvarchar](128) NOT NULL,
	[User] [nvarchar](128) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DeletedLog]    Script Date: 20/12/2012 11:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeletedLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](128) NOT NULL,
	[IDRecordFieldName] [nvarchar](128) NOT NULL,
	[RecordID] [int] NOT NULL,
	[DeletionDate] [datetime] NOT NULL,
	[DeletedBy] [nvarchar](128) NOT NULL,
	[EntityType] [nvarchar](512) NULL,
	[EntityContent] [nvarchar](max) NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_RecordDeletedLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Version]    Script Date: 20/12/2012 11:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Version](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MajorVersion] [smallint] NOT NULL,
	[MinorVersion] [smallint] NOT NULL,
	[Release] [smallint] NOT NULL,
	[Status] [smallint] NOT NULL,
	[UpgradeDateTime] [datetime] NOT NULL,
	[Output] [nvarchar](max) NULL,
 CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [HCDAuditLog] SET  READ_WRITE 
GO

/* ------------------- DATA -------------------- */
SET IDENTITY_INSERT [dbo].[Version] ON 
INSERT [dbo].[Version] ([ID], [MajorVersion], [MinorVersion], [Release], [Status], [UpgradeDateTime], [Output]) VALUES (2, 0, 0, 0, 0, CAST(0x00009F7F012DE4CE AS DateTime), N'')
SET IDENTITY_INSERT [dbo].[Version] OFF