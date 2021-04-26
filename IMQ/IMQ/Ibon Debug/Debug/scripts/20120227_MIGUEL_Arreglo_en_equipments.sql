USE [HCDXXXX]
GO

/****** Object:  Table [dbo].[EquipmentParentLocation]    Script Date: 02/27/2012 11:29:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EquipmentParentLocation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EquipmentID] [int] NOT NULL,
	[LocationID] [int] NOT NULL,
	[DefaultLocation] [bit] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_EquipmentParentLocation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EquipmentParentLocation] ADD  CONSTRAINT [DF_EquipmentParentLocation_DefaultLocation]  DEFAULT ((0)) FOR [DefaultLocation]
GO


insert into [EquipmentParentLocation] ([EquipmentID],[LocationID],[DefaultLocation],[LastUpdated],[ModifiedBy])
select [ID],[ParentLocationID],1,[LastUpdated],[ModifiedBy] from Equipment


GO
ALTER TABLE [dbo].[Equipment] DROP COLUMN [ParentLocationID];

GO
