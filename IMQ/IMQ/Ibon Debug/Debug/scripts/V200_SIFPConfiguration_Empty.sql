USE [master]
GO
/****** Object:  Database [SIFPConfiguration]    Script Date: 20/12/2012 11:00:42 ******/
CREATE DATABASE [SIFPConfiguration] ON  PRIMARY 
( NAME = N'SIFPConfiguration', FILENAME = N'SIFPConfiguration.mdf' , SIZE = 2240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SIFPConfiguration_log', FILENAME = N'SIFPConfiguration_log.LDF' , SIZE = 6272KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'SIFPConfiguration', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SIFPConfiguration].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SIFPConfiguration] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET ARITHABORT OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SIFPConfiguration] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SIFPConfiguration] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SIFPConfiguration] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SIFPConfiguration] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SIFPConfiguration] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SIFPConfiguration] SET RECOVERY FULL 
GO
ALTER DATABASE [SIFPConfiguration] SET  MULTI_USER 
GO
ALTER DATABASE [SIFPConfiguration] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SIFPConfiguration] SET DB_CHAINING OFF 
GO
USE [SIFPConfiguration]
GO
/****** Object:  Table [dbo].[ApplicationCatalog]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationCatalog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Title] [nvarchar](256) NULL,
	[Default] [bit] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_ApplicationCatalog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ApplicationCatalogApplicationItem]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationCatalogApplicationItem](
	[ApplicationCatalogID] [int] NOT NULL,
	[ApplicationItemID] [int] NOT NULL,
 CONSTRAINT [PK_ApplicationCatalogApplicationItem] PRIMARY KEY CLUSTERED 
(
	[ApplicationCatalogID] ASC,
	[ApplicationItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ApplicationItem]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Title] [nvarchar](256) NULL,
	[Description] [nvarchar](1024) NULL,
	[SmallImageID] [int] NULL,
	[LargeImageID] [int] NULL,
	[ExplorerCaption] [nvarchar](256) NULL,
	[PrimaryWorkspace] [nvarchar](256) NOT NULL,
	[ShowImagesExplorerWorkspace] [nvarchar](max) NULL,
	[ExplorerWorkspace] [nvarchar](256) NOT NULL,
	[ToolbarManagerWorkspace] [nvarchar](256) NOT NULL,
	[MainMenuElementName] [nvarchar](256) NOT NULL,
	[ShowImages] [bit] NOT NULL,
	[ShowText] [bit] NOT NULL,
	[TabHeight] [int] NOT NULL,
	[HideSingleTab] [bit] NOT NULL,
	[MergeOrder] [int] NOT NULL,
	[AllowFloatingPointExplorer] [bit] NOT NULL,
	[AllowFloatingMainMenuConfig] [bit] NOT NULL,
	[AllowFloatingNameToolbar] [bit] NOT NULL,
	[AllowMainMenuConfig] [bit] NOT NULL,
	[AllowMainToolbarConfig] [bit] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_ApplicationItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ApplicationItemExplorerTab]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationItemExplorerTab](
	[ApplicationItemID] [int] NOT NULL,
	[ExplorerTabID] [int] NOT NULL,
 CONSTRAINT [PK_ApplicationItemExplorerTab] PRIMARY KEY CLUSTERED 
(
	[ApplicationItemID] ASC,
	[ExplorerTabID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ApplicationItemMenu]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationItemMenu](
	[ApplicationItemID] [int] NOT NULL,
	[MenuID] [int] NOT NULL,
 CONSTRAINT [PK_ApplicationItemMenu] PRIMARY KEY CLUSTERED 
(
	[ApplicationItemID] ASC,
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ApplicationItemToolbar]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationItemToolbar](
	[ApplicationItemID] [int] NOT NULL,
	[ToolbarID] [int] NOT NULL,
 CONSTRAINT [PK_ApplicationItemToolbar] PRIMARY KEY CLUSTERED 
(
	[ApplicationItemID] ASC,
	[ToolbarID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Command]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Command](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Title] [nvarchar](256) NULL,
	[Description] [nvarchar](1024) NULL,
	[SmallImageID] [int] NULL,
	[LargeImageID] [int] NULL,
	[CommandType] [smallint] NOT NULL,
	[CommandName] [nvarchar](256) NULL,
	[CommandParameters] [nvarchar](1024) NULL,
	[Enabled] [bit] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_Command] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExplorerGroup]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExplorerGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ExplorerTabID] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Title] [nvarchar](256) NULL,
	[Description] [nvarchar](1024) NULL,
	[SmallImageID] [int] NULL,
	[LargeImageID] [int] NULL,
	[GroupType] [smallint] NOT NULL,
	[MergeOrder] [int] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_ExplorerGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExplorerItem]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExplorerItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ExplorerGroupID] [int] NOT NULL,
	[ParentExplorerItemID] [int] NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Title] [nvarchar](256) NULL,
	[Description] [nvarchar](1024) NULL,
	[CommandID] [int] NULL,
	[SmallImageID] [int] NULL,
	[LargeImageID] [int] NULL,
	[MergeOrder] [int] NOT NULL,
	[Separator] [bit] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_ExplorerItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExplorerTab]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExplorerTab](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Title] [nvarchar](256) NULL,
	[Description] [nvarchar](1024) NULL,
	[SmallImageID] [int] NULL,
	[LargeImageID] [int] NULL,
	[Style] [nvarchar](64) NULL,
	[ViewStyle] [nvarchar](64) NULL,
	[GroupStyle] [nvarchar](64) NULL,
	[ItemStyle] [nvarchar](64) NULL,
	[MergeOrder] [int] NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_ExplorerTab] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Image]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Image](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Source] [smallint] NOT NULL,
	[Reference] [nvarchar](1024) NULL,
	[Content] [varbinary](max) NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Title] [nvarchar](256) NULL,
	[Description] [nvarchar](1024) NULL,
	[MergeOrder] [int] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuItem]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MenuID] [int] NOT NULL,
	[ParentMenuItemID] [int] NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Title] [nvarchar](256) NULL,
	[Description] [nvarchar](1024) NULL,
	[CommandID] [int] NULL,
	[SmallImageID] [int] NULL,
	[LargeImageID] [int] NULL,
	[MergeOrder] [int] NOT NULL,
	[Separator] [bit] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_MenuItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Toolbar]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Toolbar](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Title] [nvarchar](256) NULL,
	[Description] [nvarchar](1024) NULL,
	[Row] [int] NULL,
	[Column] [int] NULL,
	[Position] [int] NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_Toolbar] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ToolItem]    Script Date: 20/12/2012 11:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToolItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ToolbarID] [int] NOT NULL,
	[ParentToolItemID] [int] NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Title] [nvarchar](256) NULL,
	[Description] [nvarchar](1024) NULL,
	[CommandID] [int] NULL,
	[SmallImageID] [int] NULL,
	[LargeImageID] [int] NULL,
	[MergeOrder] [int] NOT NULL,
	[Separator] [bit] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_ToolItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO

/****** Object:  Table [dbo].[Version]    Script Date: 20/12/2012 10:59:52 ******/
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
SET ANSI_PADDING ON

GO

/****** Object:  Index [IX_ApplicationCatalogName]    Script Date: 20/12/2012 11:00:42 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ApplicationCatalogName] ON [dbo].[ApplicationCatalog]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ApplicationCatalogID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationCatalogID] ON [dbo].[ApplicationCatalogApplicationItem]
(
	[ApplicationCatalogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ApplicationItemID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationItemID] ON [dbo].[ApplicationCatalogApplicationItem]
(
	[ApplicationItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ApplicationItemName]    Script Date: 20/12/2012 11:00:42 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ApplicationItemName] ON [dbo].[ApplicationItem]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LargeImageID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_LargeImageID] ON [dbo].[ApplicationItem]
(
	[LargeImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SmallImageID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_SmallImageID] ON [dbo].[ApplicationItem]
(
	[SmallImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ApplicationItemID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationItemID] ON [dbo].[ApplicationItemExplorerTab]
(
	[ApplicationItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExplorerTabID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_ExplorerTabID] ON [dbo].[ApplicationItemExplorerTab]
(
	[ExplorerTabID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ApplicationItemID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationItemID] ON [dbo].[ApplicationItemMenu]
(
	[ApplicationItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MenuID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_MenuID] ON [dbo].[ApplicationItemMenu]
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ApplicationItemID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_ApplicationItemID] ON [dbo].[ApplicationItemToolbar]
(
	[ApplicationItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ToolbarID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_ToolbarID] ON [dbo].[ApplicationItemToolbar]
(
	[ToolbarID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_CommandName]    Script Date: 20/12/2012 11:00:42 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CommandName] ON [dbo].[Command]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LargeImageID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_LargeImageID] ON [dbo].[Command]
(
	[LargeImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SmallImageID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_SmallImageID] ON [dbo].[Command]
(
	[SmallImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ExplorerGroupName]    Script Date: 20/12/2012 11:00:42 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ExplorerGroupName] ON [dbo].[ExplorerGroup]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExplorerTabID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_ExplorerTabID] ON [dbo].[ExplorerGroup]
(
	[ExplorerTabID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LargeImageID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_LargeImageID] ON [dbo].[ExplorerGroup]
(
	[LargeImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SmallImageID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_SmallImageID] ON [dbo].[ExplorerGroup]
(
	[SmallImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CommandID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_CommandID] ON [dbo].[ExplorerItem]
(
	[CommandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExplorerGroupID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_ExplorerGroupID] ON [dbo].[ExplorerItem]
(
	[ExplorerGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ExplorerItemName]    Script Date: 20/12/2012 11:00:42 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ExplorerItemName] ON [dbo].[ExplorerItem]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LargeImageID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_LargeImageID] ON [dbo].[ExplorerItem]
(
	[LargeImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ParentExplorerItemID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_ParentExplorerItemID] ON [dbo].[ExplorerItem]
(
	[ParentExplorerItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SmallImageID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_SmallImageID] ON [dbo].[ExplorerItem]
(
	[SmallImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ExplorerTabName]    Script Date: 20/12/2012 11:00:42 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ExplorerTabName] ON [dbo].[ExplorerTab]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LargeImageID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_LargeImageID] ON [dbo].[ExplorerTab]
(
	[LargeImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SmallImageID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_SmallImageID] ON [dbo].[ExplorerTab]
(
	[SmallImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_MenuName]    Script Date: 20/12/2012 11:00:42 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_MenuName] ON [dbo].[Menu]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CommandID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_CommandID] ON [dbo].[MenuItem]
(
	[CommandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LargeImageID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_LargeImageID] ON [dbo].[MenuItem]
(
	[LargeImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MenuID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_MenuID] ON [dbo].[MenuItem]
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_MenuItemName]    Script Date: 20/12/2012 11:00:42 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_MenuItemName] ON [dbo].[MenuItem]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ParentMenuItemID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_ParentMenuItemID] ON [dbo].[MenuItem]
(
	[ParentMenuItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SmallImageID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_SmallImageID] ON [dbo].[MenuItem]
(
	[SmallImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ToolbarName]    Script Date: 20/12/2012 11:00:42 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ToolbarName] ON [dbo].[Toolbar]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CommandID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_CommandID] ON [dbo].[ToolItem]
(
	[CommandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LargeImageID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_LargeImageID] ON [dbo].[ToolItem]
(
	[LargeImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ParentToolItemID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_ParentToolItemID] ON [dbo].[ToolItem]
(
	[ParentToolItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SmallImageID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_SmallImageID] ON [dbo].[ToolItem]
(
	[SmallImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ToolbarID]    Script Date: 20/12/2012 11:00:42 ******/
CREATE NONCLUSTERED INDEX [IX_ToolbarID] ON [dbo].[ToolItem]
(
	[ToolbarID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ToolItemName]    Script Date: 20/12/2012 11:00:42 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ToolItemName] ON [dbo].[ToolItem]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApplicationCatalogApplicationItem]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationCatalogApplicationItem_ApplicationCatalog_ApplicationCatalogID] FOREIGN KEY([ApplicationCatalogID])
REFERENCES [dbo].[ApplicationCatalog] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationCatalogApplicationItem] CHECK CONSTRAINT [FK_ApplicationCatalogApplicationItem_ApplicationCatalog_ApplicationCatalogID]
GO
ALTER TABLE [dbo].[ApplicationCatalogApplicationItem]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationCatalogApplicationItem_ApplicationItem_ApplicationItemID] FOREIGN KEY([ApplicationItemID])
REFERENCES [dbo].[ApplicationItem] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationCatalogApplicationItem] CHECK CONSTRAINT [FK_ApplicationCatalogApplicationItem_ApplicationItem_ApplicationItemID]
GO
ALTER TABLE [dbo].[ApplicationItem]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationItem_Image_LargeImageID] FOREIGN KEY([LargeImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[ApplicationItem] CHECK CONSTRAINT [FK_ApplicationItem_Image_LargeImageID]
GO
ALTER TABLE [dbo].[ApplicationItem]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationItem_Image_SmallImageID] FOREIGN KEY([SmallImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[ApplicationItem] CHECK CONSTRAINT [FK_ApplicationItem_Image_SmallImageID]
GO
ALTER TABLE [dbo].[ApplicationItemExplorerTab]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationItemExplorerTab_ApplicationItem_ApplicationItemID] FOREIGN KEY([ApplicationItemID])
REFERENCES [dbo].[ApplicationItem] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationItemExplorerTab] CHECK CONSTRAINT [FK_ApplicationItemExplorerTab_ApplicationItem_ApplicationItemID]
GO
ALTER TABLE [dbo].[ApplicationItemExplorerTab]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationItemExplorerTab_ExplorerTab_ExplorerTabID] FOREIGN KEY([ExplorerTabID])
REFERENCES [dbo].[ExplorerTab] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationItemExplorerTab] CHECK CONSTRAINT [FK_ApplicationItemExplorerTab_ExplorerTab_ExplorerTabID]
GO
ALTER TABLE [dbo].[ApplicationItemMenu]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationItemMenu_ApplicationItem_ApplicationItemID] FOREIGN KEY([ApplicationItemID])
REFERENCES [dbo].[ApplicationItem] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationItemMenu] CHECK CONSTRAINT [FK_ApplicationItemMenu_ApplicationItem_ApplicationItemID]
GO
ALTER TABLE [dbo].[ApplicationItemMenu]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationItemMenu_Menu_MenuID] FOREIGN KEY([MenuID])
REFERENCES [dbo].[Menu] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationItemMenu] CHECK CONSTRAINT [FK_ApplicationItemMenu_Menu_MenuID]
GO
ALTER TABLE [dbo].[ApplicationItemToolbar]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationItemToolbar_ApplicationItem_ApplicationItemID] FOREIGN KEY([ApplicationItemID])
REFERENCES [dbo].[ApplicationItem] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationItemToolbar] CHECK CONSTRAINT [FK_ApplicationItemToolbar_ApplicationItem_ApplicationItemID]
GO
ALTER TABLE [dbo].[ApplicationItemToolbar]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationItemToolbar_Toolbar_ToolbarID] FOREIGN KEY([ToolbarID])
REFERENCES [dbo].[Toolbar] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationItemToolbar] CHECK CONSTRAINT [FK_ApplicationItemToolbar_Toolbar_ToolbarID]
GO
ALTER TABLE [dbo].[Command]  WITH CHECK ADD  CONSTRAINT [FK_Command_Image_LargeImageID] FOREIGN KEY([LargeImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[Command] CHECK CONSTRAINT [FK_Command_Image_LargeImageID]
GO
ALTER TABLE [dbo].[Command]  WITH CHECK ADD  CONSTRAINT [FK_Command_Image_SmallImageID] FOREIGN KEY([SmallImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[Command] CHECK CONSTRAINT [FK_Command_Image_SmallImageID]
GO
ALTER TABLE [dbo].[ExplorerGroup]  WITH CHECK ADD  CONSTRAINT [FK_ExplorerGroup_ExplorerTab_ExplorerTabID] FOREIGN KEY([ExplorerTabID])
REFERENCES [dbo].[ExplorerTab] ([ID])
GO
ALTER TABLE [dbo].[ExplorerGroup] CHECK CONSTRAINT [FK_ExplorerGroup_ExplorerTab_ExplorerTabID]
GO
ALTER TABLE [dbo].[ExplorerGroup]  WITH CHECK ADD  CONSTRAINT [FK_ExplorerGroup_Image_LargeImageID] FOREIGN KEY([LargeImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[ExplorerGroup] CHECK CONSTRAINT [FK_ExplorerGroup_Image_LargeImageID]
GO
ALTER TABLE [dbo].[ExplorerGroup]  WITH CHECK ADD  CONSTRAINT [FK_ExplorerGroup_Image_SmallImageID] FOREIGN KEY([SmallImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[ExplorerGroup] CHECK CONSTRAINT [FK_ExplorerGroup_Image_SmallImageID]
GO
ALTER TABLE [dbo].[ExplorerItem]  WITH CHECK ADD  CONSTRAINT [FK_ExplorerItem_Command_CommandID] FOREIGN KEY([CommandID])
REFERENCES [dbo].[Command] ([ID])
GO
ALTER TABLE [dbo].[ExplorerItem] CHECK CONSTRAINT [FK_ExplorerItem_Command_CommandID]
GO
ALTER TABLE [dbo].[ExplorerItem]  WITH CHECK ADD  CONSTRAINT [FK_ExplorerItem_ExplorerGroup_ExplorerGroupID] FOREIGN KEY([ExplorerGroupID])
REFERENCES [dbo].[ExplorerGroup] ([ID])
GO
ALTER TABLE [dbo].[ExplorerItem] CHECK CONSTRAINT [FK_ExplorerItem_ExplorerGroup_ExplorerGroupID]
GO
ALTER TABLE [dbo].[ExplorerItem]  WITH CHECK ADD  CONSTRAINT [FK_ExplorerItem_ExplorerItem_ParentExplorerItemID] FOREIGN KEY([ParentExplorerItemID])
REFERENCES [dbo].[ExplorerItem] ([ID])
GO
ALTER TABLE [dbo].[ExplorerItem] CHECK CONSTRAINT [FK_ExplorerItem_ExplorerItem_ParentExplorerItemID]
GO
ALTER TABLE [dbo].[ExplorerItem]  WITH CHECK ADD  CONSTRAINT [FK_ExplorerItem_Image_LargeImageID] FOREIGN KEY([LargeImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[ExplorerItem] CHECK CONSTRAINT [FK_ExplorerItem_Image_LargeImageID]
GO
ALTER TABLE [dbo].[ExplorerItem]  WITH CHECK ADD  CONSTRAINT [FK_ExplorerItem_Image_SmallImageID] FOREIGN KEY([SmallImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[ExplorerItem] CHECK CONSTRAINT [FK_ExplorerItem_Image_SmallImageID]
GO
ALTER TABLE [dbo].[ExplorerTab]  WITH CHECK ADD  CONSTRAINT [FK_ExplorerTab_Image_LargeImageID] FOREIGN KEY([LargeImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[ExplorerTab] CHECK CONSTRAINT [FK_ExplorerTab_Image_LargeImageID]
GO
ALTER TABLE [dbo].[ExplorerTab]  WITH CHECK ADD  CONSTRAINT [FK_ExplorerTab_Image_SmallImageID] FOREIGN KEY([SmallImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[ExplorerTab] CHECK CONSTRAINT [FK_ExplorerTab_Image_SmallImageID]
GO
ALTER TABLE [dbo].[MenuItem]  WITH CHECK ADD  CONSTRAINT [FK_MenuItem_Command_CommandID] FOREIGN KEY([CommandID])
REFERENCES [dbo].[Command] ([ID])
GO
ALTER TABLE [dbo].[MenuItem] CHECK CONSTRAINT [FK_MenuItem_Command_CommandID]
GO
ALTER TABLE [dbo].[MenuItem]  WITH CHECK ADD  CONSTRAINT [FK_MenuItem_Image_LargeImageID] FOREIGN KEY([LargeImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[MenuItem] CHECK CONSTRAINT [FK_MenuItem_Image_LargeImageID]
GO
ALTER TABLE [dbo].[MenuItem]  WITH CHECK ADD  CONSTRAINT [FK_MenuItem_Image_SmallImageID] FOREIGN KEY([SmallImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[MenuItem] CHECK CONSTRAINT [FK_MenuItem_Image_SmallImageID]
GO
ALTER TABLE [dbo].[MenuItem]  WITH CHECK ADD  CONSTRAINT [FK_MenuItem_Menu_MenuID] FOREIGN KEY([MenuID])
REFERENCES [dbo].[Menu] ([ID])
GO
ALTER TABLE [dbo].[MenuItem] CHECK CONSTRAINT [FK_MenuItem_Menu_MenuID]
GO
ALTER TABLE [dbo].[MenuItem]  WITH CHECK ADD  CONSTRAINT [FK_MenuItem_MenuItem_ParentMenuItemID] FOREIGN KEY([ParentMenuItemID])
REFERENCES [dbo].[MenuItem] ([ID])
GO
ALTER TABLE [dbo].[MenuItem] CHECK CONSTRAINT [FK_MenuItem_MenuItem_ParentMenuItemID]
GO
ALTER TABLE [dbo].[ToolItem]  WITH CHECK ADD  CONSTRAINT [FK_ToolItem_Command_CommandID] FOREIGN KEY([CommandID])
REFERENCES [dbo].[Command] ([ID])
GO
ALTER TABLE [dbo].[ToolItem] CHECK CONSTRAINT [FK_ToolItem_Command_CommandID]
GO
ALTER TABLE [dbo].[ToolItem]  WITH CHECK ADD  CONSTRAINT [FK_ToolItem_Image_LargeImageID] FOREIGN KEY([LargeImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[ToolItem] CHECK CONSTRAINT [FK_ToolItem_Image_LargeImageID]
GO
ALTER TABLE [dbo].[ToolItem]  WITH CHECK ADD  CONSTRAINT [FK_ToolItem_Image_SmallImageID] FOREIGN KEY([SmallImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[ToolItem] CHECK CONSTRAINT [FK_ToolItem_Image_SmallImageID]
GO
ALTER TABLE [dbo].[ToolItem]  WITH CHECK ADD  CONSTRAINT [FK_ToolItem_Toolbar_ToolbarID] FOREIGN KEY([ToolbarID])
REFERENCES [dbo].[Toolbar] ([ID])
GO
ALTER TABLE [dbo].[ToolItem] CHECK CONSTRAINT [FK_ToolItem_Toolbar_ToolbarID]
GO
ALTER TABLE [dbo].[ToolItem]  WITH CHECK ADD  CONSTRAINT [FK_ToolItem_ToolItem_ParentToolItemID] FOREIGN KEY([ParentToolItemID])
REFERENCES [dbo].[ToolItem] ([ID])
GO
ALTER TABLE [dbo].[ToolItem] CHECK CONSTRAINT [FK_ToolItem_ToolItem_ParentToolItemID]
GO
USE [master]
GO
ALTER DATABASE [SIFPConfiguration] SET  READ_WRITE 
GO


/* ------------------- DATA -------------------- */
SET IDENTITY_INSERT [dbo].[Version] ON 
INSERT [dbo].[Version] ([ID], [MajorVersion], [MinorVersion], [Release], [Status], [UpgradeDateTime], [Output]) VALUES (2, 0, 0, 0, 0, CAST(0x00009F7F012DE4CE AS DateTime), N'')
SET IDENTITY_INSERT [dbo].[Version] OFF