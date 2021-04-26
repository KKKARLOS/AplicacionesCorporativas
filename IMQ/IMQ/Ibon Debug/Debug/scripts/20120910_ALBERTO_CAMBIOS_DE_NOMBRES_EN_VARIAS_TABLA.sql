

GO
PRINT N'Starting rebuilding table [dbo].[AssistanceProcessChart]...';

GO
/*
The column [dbo].[AssistanceProcessChart].[OpenAssistacePlan] is being dropped, data loss could occur.

The column [dbo].[AssistanceProcessChart].[OpenAssistancePlan] on table [dbo].[AssistanceProcessChart] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue, you must add a default value to the column or mark it as allowing NULL values.
*/
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_AssistanceProcessChart] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (100) NOT NULL,
    [Description]        NVARCHAR (200) NULL,
    [DataFlowID]         INT            NOT NULL,
    [OpenAssistancePlan] BIT            NOT NULL,
    [Status]             SMALLINT       NOT NULL,
    [LastUpdated]        DATETIME       NOT NULL,
    [ModifiedBy]         NVARCHAR (256) NOT NULL,
    [DBTimeStamp]        TIMESTAMP      NOT NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_AssistanceProcessChart]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_AssistanceProcessChart] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[AssistanceProcessChart])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AssistanceProcessChart] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AssistanceProcessChart] ([ID], [Name], [Description], [DataFlowID], [OpenAssistancePlan], [Status], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [Name],
                 [Description],
                 [DataFlowID],
                 [OpenAssistacePlan],
                 [Status],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[AssistanceProcessChart]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AssistanceProcessChart] OFF;
    END

DROP TABLE [dbo].[AssistanceProcessChart];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AssistanceProcessChart]', N'AssistanceProcessChart';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_AssistanceProcessChart]', N'PK_AssistanceProcessChart', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[AssistanceProcessChart].[IX_AssistanceProcessChart_Name]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_AssistanceProcessChart_Name]
    ON [dbo].[AssistanceProcessChart]([Name] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];



GO
PRINT N'Starting rebuilding table [dbo].[InvoiceAgreement]...';


GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_InvoiceAgreement] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [CareCenterID]       INT            NULL,
    [Name]               NVARCHAR (100) NOT NULL,
    [Description]        NVARCHAR (200) NULL,
    [AccountInformation] NVARCHAR (200) NULL,
    [AccountNumber]      NVARCHAR (20)  NULL,
    [GoesDate]           DATETIME       NOT NULL,
    [ExpirationDate]     DATETIME       NULL,
    [InvSendAddressID]   INT            NULL,
    [InvAddressType]     SMALLINT       NOT NULL,
    [PaymentMethod]      SMALLINT       NOT NULL,
    [InvoiceMode]        SMALLINT       NOT NULL,
    [BillingDayID]       INT            NOT NULL,
    [RemittanceDayID]    INT            NULL,
    [PaymentDueID]       INT            NOT NULL,
    [GuarantorType]      SMALLINT       NOT NULL,
    [AncestorID]         INT            NOT NULL,
    [Status]             SMALLINT       NOT NULL,
    [LastUpdated]        DATETIME       NOT NULL,
    [ModifiedBy]         NVARCHAR (256) NOT NULL,
    [DBTimeStamp]        TIMESTAMP      NOT NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_InvoiceAgreement]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_InvoiceAgreement] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[InvoiceAgreement])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_InvoiceAgreement] ON;
        INSERT INTO [dbo].[tmp_ms_xx_InvoiceAgreement] ([ID], [CareCenterID], [Name], [Description], [AccountInformation], [AccountNumber], [GoesDate], [ExpirationDate], [InvSendAddressID], [InvAddressType], [PaymentMethod], [InvoiceMode], [BillingDayID], [RemittanceDayID], [PaymentDueID], [GuarantorType], [AncestorID], [Status], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
				 ISNULL((SELECT TOP 1 IACCR.[CareCenterID] FROM InvoiceAgreeCareCenterRel IACCR WHERE IACCR.InvoiceAgreementID=[ID]), 0) as [CareCenterID],
                 [Name],
                 [Description],
                 [AccountInformation],
                 [AccountNumber],
                 [GoesDate],
                 [ExpirationDate],
                 [InvSendAddressID],
                 [InvAddressType],
                 [PaymentMethod],
                 [InvoiceMode],
                 [BillingDayID],
                 [RemittanceDayID],
                 [PaymentDueID],
                 [GuarantorType],
                 [AncestorID],
                 [Status],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[InvoiceAgreement]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_InvoiceAgreement] OFF;
    END

DROP TABLE [dbo].[InvoiceAgreement];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_InvoiceAgreement]', N'InvoiceAgreement';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_InvoiceAgreement]', N'PK_InvoiceAgreement', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

GO
PRINT N'Starting rebuilding table [dbo].[InvoiceElementPrintRule]...';


GO
PRINT N'Rename [dbo].[InsurerInvAgeeProcessChartRel] to [InsurerInvAgreeProcessChartRel]...';

EXECUTE sp_rename N'[dbo].[InsurerInvAgeeProcessChartRel]', N'InsurerInvAgreeProcessChartRel';

