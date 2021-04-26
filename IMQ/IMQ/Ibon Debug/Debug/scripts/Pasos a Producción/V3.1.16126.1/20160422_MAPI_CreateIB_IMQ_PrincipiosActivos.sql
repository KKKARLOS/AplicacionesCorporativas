SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  TABLE [dbo].[IB_IMQ_PrincipiosActivos](
	[ID] [int]IDENTITY(1,1) NOT NULL,
	[Code] nvarchar(100) NOT NULL,
	[CodCas] nvarchar(100) NOT NULL,
	[Name] nvarchar(400) NOT NULL,
	[Description] nvarchar(400) NOT NULL,
	[CodeATC] nvarchar(100) NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_IB_IMQ_PrincipiosActivos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
