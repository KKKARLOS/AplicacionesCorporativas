

/****** Object:  Table [dbo].[IB_IMQ_FrecuenciasEfarmaco]    Script Date: 02/17/2016 09:38:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  TABLE [dbo].[IB_IMQ_ViasAdministracion](
	[ID] [int]IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL,
	[AdministrationRouteID] [int] NOT NULL,
	[ViaPrincipal] [bit] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_IB_IMQ_ViasAdministracion] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
