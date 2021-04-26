USE [nombre bd]

GO
PRINT N'Starting rebuilding table [dbo].[InvoiceAgreement]...';

GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_InvoiceAgreement](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[AccountInformation] [nvarchar](200) NULL,
	[AccountNumber] [nvarchar](20) NULL,
	[GoesDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NULL,
	[InvSendAddressID] [int] NULL,
	[InvAddressType] [smallint] NOT NULL,
	[PaymentMethod] [smallint] NOT NULL,
	[InvoiceMode] [smallint] NOT NULL,
	[BillingDayID] [int] NOT NULL,
	[RemittanceDayID] [int] NULL,
	[PaymentDueID] [int] NOT NULL,
	[GuarantorType] [smallint] NOT NULL,
	[AncestorID] [int] NOT NULL,
	[Status] [smallint] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
);

ALTER TABLE [dbo].[tmp_ms_xx_InvoiceAgreement]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_InvoiceAgreement] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[InvoiceAgreement])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_InvoiceAgreement] ON;
        INSERT INTO [dbo].[tmp_ms_xx_InvoiceAgreement] ([ID],[Name],[Description],
				[AccountInformation],[AccountNumber],[GoesDate],[ExpirationDate],[InvSendAddressID],[InvAddressType],
				[PaymentMethod],[InvoiceMode],[BillingDayID],[RemittanceDayID],[PaymentDueID],[GuarantorType],[AncestorID],
				[Status],[LastUpdated],[ModifiedBy])
        SELECT  IA.[ID],IAB.[Name],IAB.[Description],
				IAB.[AccountInformation],IAB.[AccountNumber],IAB.[GoesDate],IAB.[ExpirationDate],IAB.[InvSendAddressID],IAB.[InvAddressType],
				IAB.[PaymentMethod],IAB.[InvoiceMode],IAB.[BillingDayID],IAB.[RemittanceDayID],IAB.[PaymentDueID],IA.[GuarantorType],IA.[AncestorID],
				IA.[Status],IA.[LastUpdated],IA.[ModifiedBy]
        FROM [dbo].[InvoiceAgreement] IA
        JOIN [dbo].[InvoiceAgreementBase] IAB ON IA.InvoiceAgreementBaseID=IAB.[ID]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_InvoiceAgreement] OFF;
    END

DROP TABLE [dbo].[InvoiceAgreement];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_InvoiceAgreement]', N'InvoiceAgreement';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_InvoiceAgreement]', N'PK_InvoiceAgreement', N'OBJECT';


COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

GO
PRINT N'Starting rebuilding table [dbo].[InsurerInvoiceAgreement]...';

GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_InsurerInvoiceAgreement](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InsurerID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[AccountInformation] [nvarchar](200) NULL,
	[AccountNumber] [nvarchar](20) NULL,
	[GoesDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NULL,
	[InvSendAddressID] [int] NULL,
	[InvAddressType] [smallint] NOT NULL,
	[PaymentMethod] [smallint] NOT NULL,
	[InvoiceMode] [smallint] NOT NULL,
	[BillingDayID] [int] NOT NULL,
	[RemittanceDayID] [int] NULL,
	[PaymentDueID] [int] NOT NULL,
	[AncestorID] [int] NOT NULL,
	[Status] [smallint] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
);

ALTER TABLE [dbo].[tmp_ms_xx_InsurerInvoiceAgreement]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_InsurerInvoiceAgreement] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[InsurerInvoiceAgreement])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_InsurerInvoiceAgreement] ON;
        INSERT INTO [dbo].[tmp_ms_xx_InsurerInvoiceAgreement] ([ID],[InsurerID],[Name],[Description],
				[AccountInformation],[AccountNumber],[GoesDate],[ExpirationDate],[InvSendAddressID],[InvAddressType],
				[PaymentMethod],[InvoiceMode],[BillingDayID],[RemittanceDayID],[PaymentDueID],[AncestorID],
				[Status],[LastUpdated],[ModifiedBy])
        SELECT  IA.[ID],IA.[InsurerID],IAB.[Name],IAB.[Description],
				IAB.[AccountInformation],IAB.[AccountNumber],IAB.[GoesDate],IAB.[ExpirationDate],IAB.[InvSendAddressID],IAB.[InvAddressType],
				IAB.[PaymentMethod],IAB.[InvoiceMode],IAB.[BillingDayID],IAB.[RemittanceDayID],IAB.[PaymentDueID],IA.[AncestorID],
				IA.[Status],IA.[LastUpdated],IA.[ModifiedBy]
        FROM [dbo].[InsurerInvoiceAgreement] IA
        JOIN [dbo].[InvoiceAgreementBase] IAB ON IA.InvoiceAgreementBaseID=IAB.[ID]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_InsurerInvoiceAgreement] OFF;
    END

DROP TABLE [dbo].[InsurerInvoiceAgreement];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_InsurerInvoiceAgreement]', N'InsurerInvoiceAgreement';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_InsurerInvoiceAgreement]', N'PK_InsurerInvoiceAgreement', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

DROP TABLE [dbo].[InvoiceAgreementBase];
