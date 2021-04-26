SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CoverAnalysisInProcess](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerEpisodeID] [int] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[DBTimeStamp] [timestamp] NOT NULL
) ON [PRIMARY]

GO