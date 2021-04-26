/****** Object:  Table [dbo].[IB_IMQ_ReplenishmentOrderEntryDiff]    Script Date: 01/14/2016 15:36:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IB_IMQ_ReplenishmentOrderEntryDiff](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReplenishmentOrderID] [int] NOT NULL,
	[ItemID] [int] NOT NULL,
	[QuantityDiff] [float] NOT NULL,
	[ServedQuantity] [float] NOT NULL,
	[NurseRequestEntryID] [int] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_IB_IMQ_ReplenishmentOrderEntryDiff] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

