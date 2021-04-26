USE [HCDISTesting]
GO

/****** Object:  Table [dbo].[Invoice]    Script Date: 15/06/2015 12:09:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Invoice_Temp](
       [ID] [int] IDENTITY(1,1) NOT NULL,
       [CareCenterID] [int] NOT NULL CONSTRAINT [DF_Invoice_CareCenterID_Temp]  DEFAULT ((0)),
       [GuarantorID] [int] NOT NULL CONSTRAINT [DF_Invoice_GuarantorID_Temp]  DEFAULT ((0)),
       [GuarantorType] [smallint] NOT NULL CONSTRAINT [DF_Invoice_GuarantorType_Temp]  DEFAULT ((0)),
       [InvoiceType] [smallint] NOT NULL,
       [InvoiceNumber] [nvarchar](50) NOT NULL,
       [InvoiceAddressID] [int] NOT NULL,
       [Comments] [nvarchar](max) NULL,
       [InvoiceDateTime] [datetime] NOT NULL,
       [RegistrationDateTime] [datetime] NOT NULL,
       [TotalQty] [money] NOT NULL,
       [TaxQty] [money] NOT NULL,
       [GuarantorInvoiceAgreementID] [int] NOT NULL CONSTRAINT [DF_Invoice_GuarantorInvoiceAgreementID_Temp]  DEFAULT ((0)),
       [InvoicePrintRuleID] [int] NOT NULL,
       [PrintedDateTime] [datetime] NULL,
       [PrintedBy] [nvarchar](256) NOT NULL,
       [InvoiceStatus] [smallint] NOT NULL,
       [PaymentStatus] [smallint] NOT NULL,
       [RemittanceStatus] [smallint] NOT NULL CONSTRAINT [DF_Invoice_RemittanceStatus_Temp]  DEFAULT ((0)),
       [LastUpdated] [datetime] NOT NULL,
       [ModifiedBy] [nvarchar](256) NOT NULL,
       [DBTimeStamp] [timestamp] NOT NULL,
CONSTRAINT [PK_Invoice_Temp] PRIMARY KEY CLUSTERED 
(
       [ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET IDENTITY_INSERT [dbo].[Invoice_Temp] ON

INSERT INTO [dbo].[Invoice_Temp] 
       (      [ID],
             [CareCenterID],
             [GuarantorID],
             [GuarantorType],
             [InvoiceType],
             [InvoiceNumber],
             [InvoiceAddressID],
             [Comments],
             [InvoiceDateTime],
             [RegistrationDateTime],
             [TotalQty],
             [TaxQty],
             [GuarantorInvoiceAgreementID],
             [InvoicePrintRuleID],
             [PrintedDateTime],
             [PrintedBy],
             [InvoiceStatus],
             [PaymentStatus],
             [RemittanceStatus],
             [LastUpdated],
             [ModifiedBy]
       )
SELECT  INV.[ID],
             INV.[CareCenterID],
             INV.[GuarantorID],
             INV.[GuarantorType],
             INV.[InvoiceType],
             INV.[InvoiceNumber],
             INV.[InvoiceAddressID],
             INV.[Comments],
             INV.[InvoiceDateTime],
             INV.[RegistrationDateTime],
             INV.[TotalQty],
             INV.[TaxQty],
             IPPR.[InvoiceAgreementID],
             INV.[InvoicePrintRuleID],
             INV.[PrintedDateTime],
             INV.[PrintedBy],
             INV.[InvoiceStatus],
             INV.[PaymentStatus],
             INV.[RemittanceStatus],
             INV.[LastUpdated],
             INV.[ModifiedBy]
FROM [dbo].[Invoice] INV
JOIN [dbo].[InvoiceAgreeInvPrintRuleRel] IPPR ON IPPR.[ID]=INV.[GuarantorInvoiceAgreePrintRuleID] AND INV.GuarantorType<>1
UNION 
SELECT  INV.[ID],
             INV.[CareCenterID],
             INV.[GuarantorID],
             INV.[GuarantorType],
             INV.[InvoiceType],
             INV.[InvoiceNumber],
             INV.[InvoiceAddressID],
             INV.[Comments],
             INV.[InvoiceDateTime],
             INV.[RegistrationDateTime],
             INV.[TotalQty],
             INV.[TaxQty],
             IIPPR.[InsurerInvoiceAgreementID],
             INV.[InvoicePrintRuleID],
             INV.[PrintedDateTime],
             INV.[PrintedBy],
             INV.[InvoiceStatus],
             INV.[PaymentStatus],
             INV.[RemittanceStatus],
             INV.[LastUpdated],
             INV.[ModifiedBy]
FROM [dbo].[Invoice] INV
JOIN [dbo].InsurerInvAgreeInvPrintRuleRel IIPPR ON IIPPR.[ID]=INV.[GuarantorInvoiceAgreePrintRuleID] AND INV.GuarantorType=1

SET IDENTITY_INSERT [dbo].[Invoice_Temp] OFF
GO

DROP TABLE [dbo].[Invoice]

GO

EXEC sp_rename 'Invoice_Temp', 'Invoice';
EXEC sp_rename 'Invoice.PK_Invoice_Temp', 'PK_Invoice';

