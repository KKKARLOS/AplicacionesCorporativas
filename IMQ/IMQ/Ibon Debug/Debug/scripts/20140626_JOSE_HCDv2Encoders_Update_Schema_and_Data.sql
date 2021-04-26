sp_rename DIAGNOSTICO, DIAGNOSTICO_OLD
GO
sp_rename INTERVEN, INTERVEN_OLD
GO
sp_rename MORFOLOGIAS, MORFOLOGIAS_OLD
GO

CREATE TABLE [dbo].[DIAGNOSTICO](
      [ID] [int] IDENTITY(1,1) NOT NULL,
      [Code] [nvarchar](200) NOT NULL,
      [Name] [nvarchar](1024) NULL,
      [Description] [nvarchar](max) NULL,
      [Area] [nvarchar](10) NULL,
      [Classification] [nvarchar](10) NULL,
      [OP] [nvarchar](10) NULL,
      [Level] [int] NULL,
      [AncestorID] [int] NULL,
      [Version] [nvarchar](50) NULL,
      [VersionDT] [datetime] NOT NULL,
      [Status] [smallint] NOT NULL,
      [LastUpdated] [datetime] NOT NULL,
      [ModifiedBy] [nvarchar](256) NOT NULL,
      [DBTimeStamp] [timestamp] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[INTERVEN](
      [ID] [int] IDENTITY(1,1) NOT NULL,
      [Code] [nvarchar](200) NOT NULL,
      [Name] [nvarchar](1024) NULL,
      [Description] [nvarchar](max) NULL,
      [Area] [nvarchar](10) NULL,
      [Classification] [nvarchar](10) NULL,
      [OP] [nvarchar](10) NULL,
      [Level] [int] NULL,
      [AncestorID] [int] NULL,
      [Version] [nvarchar](50) NULL,
      [VersionDT] [datetime] NOT NULL,
      [Status] [smallint] NOT NULL,
      [LastUpdated] [datetime] NOT NULL,
      [ModifiedBy] [nvarchar](256) NOT NULL,
      [DBTimeStamp] [timestamp] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MORFOLOGIAS](
      [ID] [int] IDENTITY(1,1) NOT NULL,
      [Code] [nvarchar](200) NOT NULL,
      [Name] [nvarchar](1024) NULL,
      [Description] [nvarchar](max) NULL,
      [Area] [nvarchar](10) NULL,
      [Classification] [nvarchar](10) NULL,
      [OP] [nvarchar](10) NULL,
      [Level] [int] NULL,
      [AncestorID] [int] NULL,
      [Version] [nvarchar](50) NULL,
      [VersionDT] [datetime] NOT NULL,
      [Status] [smallint] NOT NULL,
      [LastUpdated] [datetime] NOT NULL,
      [ModifiedBy] [nvarchar](256) NOT NULL,
      [DBTimeStamp] [timestamp] NOT NULL
) ON [PRIMARY]
GO

DECLARE @ActualDate DateTime
SET @ActualDate = GetDate()
INSERT INTO DIAGNOSTICO ([Code], [Name], [Description], [Version], VersionDT, [Status], LastUpdated, ModifiedBy)
SELECT DIACodigo, DIANombre, DIANombre, '1' [Version], @ActualDate, 1, @ActualDate, 'Administrator' FROM DIAGNOSTICO_OLD

INSERT INTO INTERVEN ([Code], [Name], [Description], [Version], VersionDT, [Status], LastUpdated, ModifiedBy)
SELECT INTCodigo, INTDescripcion, INTDescripcion, '1' [Version], @ActualDate, 1, @ActualDate, 'Administrator' FROM INTERVEN_OLD
WHERE INTCodigo IS NOT NULL

INSERT INTO MORFOLOGIAS ([Code], [Name], [Description], [Version], VersionDT, [Status], LastUpdated, ModifiedBy)
SELECT MORCodigo, MORDescripcion, MORDescripcion, '1' [Version], @ActualDate, 1, @ActualDate, 'Administrator' FROM MORFOLOGIAS_OLD
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DIAGNOSTICO_OLD]') AND type in (N'U'))
DROP TABLE [dbo].[DIAGNOSTICO_OLD]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[INTERVEN_OLD]') AND type in (N'U'))
DROP TABLE [dbo].[INTERVEN_OLD]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MORFOLOGIAS_OLD]') AND type in (N'U'))
DROP TABLE [dbo].[MORFOLOGIAS_OLD]
GO