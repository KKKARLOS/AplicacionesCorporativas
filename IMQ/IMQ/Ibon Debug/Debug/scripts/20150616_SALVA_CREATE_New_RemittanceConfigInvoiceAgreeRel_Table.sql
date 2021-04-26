
/****** Object:  Table [dbo].[RemittanceConfigInvoiceAgreeRel]    Script Date: 06/10/2015 16:24:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RemittanceConfigInvoiceAgreeRelCopy]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RemittanceConfigID] [int] NOT NULL,
	[GuarantorType] [smallint] NOT NULL,
	[InvoiceAgreementID] [int] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
	CONSTRAINT [PK_RemittanceConfigInvoiceAgreeRelCopy] PRIMARY KEY CLUSTERED 
	(
	[ID] ASC
	)
	WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET IDENTITY_INSERT [dbo].[RemittanceConfigInvoiceAgreeRelCopy] ON
INSERT INTO [dbo].[RemittanceConfigInvoiceAgreeRelCopy] ([ID], [RemittanceConfigID], [GuarantorType], [InvoiceAgreementID], [LastUpdated], [ModifiedBy])
SELECT ICIAR.[ID], ICIAR.[RemittanceConfigID], ICA.[GuarantorType], ICA.[InvoiceAgreementID], ICIAR.[LastUpdated], ICIAR.[ModifiedBy]
FROM [dbo].[RemittanceConfigInvoiceAgreeRemittanceRel] ICIAR
JOIN [dbo].[InvoiceAgreeRemittanceConfig] ICA ON ICIAR.InvoiceAgreeRemittanceConfigID=ICA.[ID]
SET IDENTITY_INSERT [dbo].[RemittanceConfigInvoiceAgreeRelCopy] OFF
GO

DROP TABLE [dbo].[InvoiceAgreeRemittanceConfig]
DROP TABLE [dbo].[RemittanceConfigInvoiceAgreeRemittanceRel]

GO

EXEC sp_rename 'RemittanceConfigInvoiceAgreeRelCopy', 'RemittanceConfigInvoiceAgreeRel';
EXEC sp_rename 'RemittanceConfigInvoiceAgreeRel.PK_RemittanceConfigInvoiceAgreeRelCopy', 'PK_RemittanceConfigInvoiceAgreeRel';

GO


