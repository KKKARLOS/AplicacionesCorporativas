--USE [HCDISIMQ]
--GO

/****** Object:  Table [dbo].[InvoiceConfigInvoiceAgreeRel]    Script Date: 06/10/2015 16:24:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InvoiceConfigInvoiceAgreeRelCopy]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceConfigID] [int] NOT NULL,
	[GuarantorType] [smallint] NOT NULL,
	[InvoiceAgreementID] [int] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
	CONSTRAINT [PK_InvoiceConfigInvoiceAgreeRelCopy] PRIMARY KEY CLUSTERED 
	(
	[ID] ASC
	)
	WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET IDENTITY_INSERT [dbo].[InvoiceConfigInvoiceAgreeRelCopy] ON
INSERT INTO [dbo].[InvoiceConfigInvoiceAgreeRelCopy] ([ID], [InvoiceConfigID], [GuarantorType], [InvoiceAgreementID], [LastUpdated], [ModifiedBy])
SELECT ICIAR.[ID], ICIAR.[InvoiceConfigID], ICA.[GuarantorType], ICA.[InvoiceAgreementID], ICIAR.[LastUpdated], ICIAR.[ModifiedBy]
FROM [dbo].[InvoiceConfigInvoiceAgreeRel] ICIAR
JOIN [dbo].[InvoiceConfigAgreement] ICA ON ICIAR.InvoiceConfigAgreementID=ICA.[ID]
SET IDENTITY_INSERT [dbo].[InvoiceConfigInvoiceAgreeRelCopy] OFF
GO

DROP TABLE [dbo].[InvoiceConfigAgreement]
DROP TABLE [dbo].[InvoiceConfigInvoiceAgreeRel]

GO

EXEC sp_rename 'InvoiceConfigInvoiceAgreeRelCopy', 'InvoiceConfigInvoiceAgreeRel';
EXEC sp_rename 'InvoiceConfigInvoiceAgreeRel.PK_InvoiceConfigInvoiceAgreeRelCopy', 'PK_InvoiceConfigInvoiceAgreeRel';

GO


