
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PrescriptionRequestFreeDoseTimeQuantity](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PrescriptionRequestID] [int] NOT NULL,
	[PrescriptionRequestTime] [datetime] NOT NULL,
	[PrescriptionRequestQuantity] [float] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL
) ON [PRIMARY]

GO


