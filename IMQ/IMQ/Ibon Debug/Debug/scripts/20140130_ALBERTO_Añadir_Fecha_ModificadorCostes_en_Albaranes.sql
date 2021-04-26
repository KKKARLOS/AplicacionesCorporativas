SET XACT_ABORT ON;

BEGIN TRANSACTION;

DECLARE @AssistanceAgreementID int
DECLARE @InsurerCoverAgreementID int
DECLARE @AgreementID int
DECLARE @InsurerAgreementID int
DECLARE @AgreeConditionID int
DECLARE @InsurerConditionID int

SET @AssistanceAgreementID = ISNULL((SELECT TOP 1 E.ID FROM EACElement E WHERE (E.Name = 'AssistanceAgreementEntity')), 0)
SET @InsurerCoverAgreementID = ISNULL((SELECT TOP 1 E.ID FROM EACElement E WHERE (E.Name = 'InsurerCoverAgreementEntity')), 0)
SET @AgreementID = ISNULL((SELECT TOP 1 E.ID FROM EACElement E WHERE (E.Name = 'AgreementEntity')), 0)
SET @InsurerAgreementID = ISNULL((SELECT TOP 1 E.ID FROM EACElement E WHERE (E.Name = 'InsurerAgreementEntity')), 0)
SET @AgreeConditionID = ISNULL((SELECT TOP 1 E.ID FROM EACElement E WHERE (E.Name = 'AgreeConditionEntity')), 0)
SET @InsurerConditionID = ISNULL((SELECT TOP 1 E.ID FROM EACElement E WHERE (E.Name = 'InsurerConditionEntity')), 0)

PRINT N'Dropping DF_DeliveryNote_GuarantorID...';
ALTER TABLE [dbo].[DeliveryNote] DROP CONSTRAINT [DF_DeliveryNote_GuarantorID];

PRINT N'Dropping DF_DeliveryNote_Manual...';
ALTER TABLE [dbo].[DeliveryNote] DROP CONSTRAINT [DF_DeliveryNote_Manual];

PRINT N'Dropping DF_DeliveryNote_TaxTypeID...';
ALTER TABLE [dbo].[DeliveryNote] DROP CONSTRAINT [DF_DeliveryNote_TaxTypeID];

PRINT N'Dropping DF_DeliveryNote_CalculationMode...';
ALTER TABLE [dbo].[DeliveryNote] DROP CONSTRAINT [DF_DeliveryNote_CalculationMode];

PRINT N'Dropping DF_DeliveryNote_Export...';
ALTER TABLE [dbo].[DeliveryNote] DROP CONSTRAINT [DF_DeliveryNote_Export];

PRINT N'Dropping DF_DeliveryNoteCondEntry_DeliveryNoteEntryID...';
ALTER TABLE [dbo].[DeliveryNoteCondEntry] DROP CONSTRAINT [DF_DeliveryNoteCondEntry_DeliveryNoteEntryID];

PRINT N'Dropping DF_DeliveryNoteCondEntry_AssignedCode...';
ALTER TABLE [dbo].[DeliveryNoteCondEntry] DROP CONSTRAINT [DF_DeliveryNoteCondEntry_AssignedCode];

PRINT N'Dropping DF_DeliveryNoteCondEntry_DiscountType...';
ALTER TABLE [dbo].[DeliveryNoteCondEntry] DROP CONSTRAINT [DF_DeliveryNoteCondEntry_DiscountType];

PRINT N'Dropping DF_DeliveryNoteCondEntry_TaxTypeID...';
ALTER TABLE [dbo].[DeliveryNoteCondEntry] DROP CONSTRAINT [DF_DeliveryNoteCondEntry_TaxTypeID];

PRINT N'Dropping DF_DeliveryNoteCondEntry_PriceUnit...';
ALTER TABLE [dbo].[DeliveryNoteCondEntry] DROP CONSTRAINT [DF_DeliveryNoteCondEntry_PriceUnit];

PRINT N'Dropping DF_DeliveryNoteCondEntry_Unit...';
ALTER TABLE [dbo].[DeliveryNoteCondEntry] DROP CONSTRAINT [DF_DeliveryNoteCondEntry_Unit];

PRINT N'Dropping DF_DeliveryNoteCondEntry_DiscountQty...';
ALTER TABLE [dbo].[DeliveryNoteCondEntry] DROP CONSTRAINT [DF_DeliveryNoteCondEntry_DiscountQty];

PRINT N'Dropping DF_DeliveryNoteCondEntry_PhysUnitID...';
ALTER TABLE [dbo].[DeliveryNoteCondEntry] DROP CONSTRAINT [DF_DeliveryNoteCondEntry_PhysUnitID];

PRINT N'Dropping DF_DeliveryNoteCondEntry_FactorCode...';
ALTER TABLE [dbo].[DeliveryNoteCondEntry] DROP CONSTRAINT [DF_DeliveryNoteCondEntry_FactorCode];

PRINT N'Dropping DF_DeliveryNoteCondEntry_ModificationFactor...';
ALTER TABLE [dbo].[DeliveryNoteCondEntry] DROP CONSTRAINT [DF_DeliveryNoteCondEntry_ModificationFactor];

PRINT N'Dropping DF_DeliveryNoteCondEntry_Manual...';
ALTER TABLE [dbo].[DeliveryNoteCondEntry] DROP CONSTRAINT [DF_DeliveryNoteCondEntry_Manual];

PRINT N'Dropping DF_DeliveryNoteCondEntry_CalculationCostMode...';
ALTER TABLE [dbo].[DeliveryNoteCondEntry] DROP CONSTRAINT [DF_DeliveryNoteCondEntry_CalculationCostMode];

PRINT N'Dropping DF_DeliveryNoteEntry_DiscountType...';
ALTER TABLE [dbo].[DeliveryNoteEntry] DROP CONSTRAINT [DF_DeliveryNoteEntry_DiscountType];

PRINT N'Dropping DF_DeliveryNoteEntry_DiscountQty...';
ALTER TABLE [dbo].[DeliveryNoteEntry] DROP CONSTRAINT [DF_DeliveryNoteEntry_DiscountQty];

PRINT N'Dropping DF_DeliveryNoteEntry_FactorCode...';
ALTER TABLE [dbo].[DeliveryNoteEntry] DROP CONSTRAINT [DF_DeliveryNoteEntry_FactorCode];

PRINT N'Dropping DF_DeliveryNoteEntry_CalculationMode...';
ALTER TABLE [dbo].[DeliveryNoteEntry] DROP CONSTRAINT [DF_DeliveryNoteEntry_CalculationMode];

PRINT N'Dropping DF_DeliveryNoteEntry_TaxTypeID...';
ALTER TABLE [dbo].[DeliveryNoteEntry] DROP CONSTRAINT [DF_DeliveryNoteEntry_TaxTypeID];

PRINT N'Dropping DF_DeliveryNoteEntry_CalculationCostMode...';
ALTER TABLE [dbo].[DeliveryNoteEntry] DROP CONSTRAINT [DF_DeliveryNoteEntry_CalculationCostMode];

PRINT N'Dropping DF_DeliveryNoteEntry_TaxQty...';
ALTER TABLE [dbo].[DeliveryNoteEntry] DROP CONSTRAINT [DF_DeliveryNoteEntry_TaxQty];

PRINT N'Dropping DF_DeliveryNoteEntry_ExclusiveConditions...';
ALTER TABLE [dbo].[DeliveryNoteEntry] DROP CONSTRAINT [DF_DeliveryNoteEntry_ExclusiveConditions];

PRINT N'Dropping DF_DeliveryNoteEntry_ModificationFactor...';
ALTER TABLE [dbo].[DeliveryNoteEntry] DROP CONSTRAINT [DF_DeliveryNoteEntry_ModificationFactor];

PRINT N'Dropping DF_DeliveryNoteEntry_Unit...';
ALTER TABLE [dbo].[DeliveryNoteEntry] DROP CONSTRAINT [DF_DeliveryNoteEntry_Unit];

PRINT N'Dropping DF_DeliveryNoteEntry_PriceUnit...';
ALTER TABLE [dbo].[DeliveryNoteEntry] DROP CONSTRAINT [DF_DeliveryNoteEntry_PriceUnit];

PRINT N'Dropping DF_DeliveryNoteEntry_Export...';
ALTER TABLE [dbo].[DeliveryNoteEntry] DROP CONSTRAINT [DF_DeliveryNoteEntry_Export];

PRINT N'Dropping DF_DeliveryNoteEntry_Manual...';
ALTER TABLE [dbo].[DeliveryNoteEntry] DROP CONSTRAINT [DF_DeliveryNoteEntry_Manual];

PRINT N'Starting rebuilding table [dbo].[DeliveryNote]...';
CREATE TABLE [dbo].[tmp_ms_xx_DeliveryNote] (
    [ID]                     INT            IDENTITY (1, 1) NOT NULL,
    [GuarantorID]            INT            CONSTRAINT [DF_DeliveryNote_GuarantorID] DEFAULT ((0)) NOT NULL,
    [RegistrationDateTime]   DATETIME       NOT NULL,
    [DateTime]               DATETIME       NOT NULL,
    [DeliveryNoteNumber]     NVARCHAR (50)  NOT NULL,
    [CoverElementID]         INT            NOT NULL,
    [CoverEntityID]          INT            NOT NULL,
    [AssignedCode]           NVARCHAR (50)  NULL,
    [Name]                   NVARCHAR (100) NULL,
    [Description]            NVARCHAR (MAX) NULL,
    [CalculationMode]        SMALLINT       CONSTRAINT [DF_DeliveryNote_CalculationMode] DEFAULT ((0)) NOT NULL,
    [CustomerDeliveryNoteID] INT            NOT NULL,
    [Comments]               NVARCHAR (MAX) NULL,
    [TotalTI]                MONEY          NOT NULL,
    [TaxTypeID]              INT            CONSTRAINT [DF_DeliveryNote_TaxTypeID] DEFAULT ((0)) NOT NULL,
    [TaxQty]                 MONEY          NOT NULL,
    [CalculatedCost]         MONEY          NOT NULL,
    [Status]                 SMALLINT       NOT NULL,
    [Manual]                 BIT            CONSTRAINT [DF_DeliveryNote_Manual] DEFAULT ((0)) NOT NULL,
    [Exported]               BIT            CONSTRAINT [DF_DeliveryNote_Export] DEFAULT ((0)) NOT NULL,
    [LastUpdated]            DATETIME       NOT NULL,
    [ModifiedBy]             NVARCHAR (256) NOT NULL,
    [DBTimeStamp]            TIMESTAMP      NOT NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_DeliveryNote]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_DeliveryNote] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[DeliveryNote])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_DeliveryNote] ON;
        INSERT INTO [dbo].[tmp_ms_xx_DeliveryNote] ([ID], [GuarantorID], [RegistrationDateTime], [DateTime], [DeliveryNoteNumber], 
			[CoverElementID], [CoverEntityID], [AssignedCode], [Name], [Description], [CalculationMode], [CustomerDeliveryNoteID], 
			[Comments], [TotalTI], [TaxTypeID], [TaxQty], [CalculatedCost], [Status], [Manual], [Exported], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [GuarantorID],
                 [RegistrationDateTime],
                 CAST(FLOOR(CAST([RegistrationDateTime] AS FLOAT)) AS DATETIME),
                 [DeliveryNoteNumber],
                 [CoverElementID],
                 [CoverEntityID],
                 [AssignedCode],
                 [Name],
                 [Description],
                 [CalculationMode],
                 [CustomerDeliveryNoteID],
                 [Comments],
                 [TotalTI],
                 [TaxTypeID],
                 [TaxQty],
                 [CalculatedCost],
                 [Status],
                 [Manual],
                 [Exported],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[DeliveryNote]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_DeliveryNote] OFF;
    END

DROP TABLE [dbo].[DeliveryNote];
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_DeliveryNote]', N'DeliveryNote';
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_DeliveryNote]', N'PK_DeliveryNote', N'OBJECT';

PRINT N'Creating [dbo].[DeliveryNote].[IX_DeliveryNote_CustomerDeliveryNoteID]...';
CREATE NONCLUSTERED INDEX [IX_DeliveryNote_CustomerDeliveryNoteID]
    ON [dbo].[DeliveryNote]([CustomerDeliveryNoteID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];

PRINT N'Creating [dbo].[DeliveryNote].[IX_DeliveryNote_Exported]...';
CREATE NONCLUSTERED INDEX [IX_DeliveryNote_Exported]
    ON [dbo].[DeliveryNote]([Exported] ASC)
    INCLUDE([CustomerDeliveryNoteID]) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];

PRINT N'Starting rebuilding table [dbo].[DeliveryNoteCondEntry]...';
CREATE TABLE [dbo].[tmp_ms_xx_DeliveryNoteCondEntry] (
    [ID]                     INT            IDENTITY (1, 1) NOT NULL,
    [RegistrationDateTime]   DATETIME       NOT NULL,
    [DateTime]               DATETIME       NOT NULL,
    [CondElementID]          INT            NOT NULL,
    [CondEntityID]           INT            NOT NULL,
    [AssignedCode]           NVARCHAR (50)  CONSTRAINT [DF_DeliveryNoteCondEntry_AssignedCode] DEFAULT (N' ') NULL,
    [Name]                   NVARCHAR (100) NULL,
    [Description]            NVARCHAR (MAX) NULL,
    [DeliveryNoteEntryID]    INT            CONSTRAINT [DF_DeliveryNoteCondEntry_DeliveryNoteEntryID] DEFAULT ((0)) NOT NULL,
    [DiscountType]           SMALLINT       CONSTRAINT [DF_DeliveryNoteCondEntry_DiscountType] DEFAULT ((0)) NOT NULL,
    [DiscountQty]            MONEY          CONSTRAINT [DF_DeliveryNoteCondEntry_DiscountQty] DEFAULT ((0)) NOT NULL,
    [PhysUnitID]             INT            CONSTRAINT [DF_DeliveryNoteCondEntry_PhysUnitID] DEFAULT ((0)) NOT NULL,
    [FactorCode]             SMALLINT       CONSTRAINT [DF_DeliveryNoteCondEntry_FactorCode] DEFAULT ((0)) NOT NULL,
    [ModificationFactor]     FLOAT          CONSTRAINT [DF_DeliveryNoteCondEntry_ModificationFactor] DEFAULT ((0)) NOT NULL,
    [Units]                  FLOAT          CONSTRAINT [DF_DeliveryNoteCondEntry_Unit] DEFAULT ((0)) NOT NULL,
    [PriceUnit]              MONEY          CONSTRAINT [DF_DeliveryNoteCondEntry_PriceUnit] DEFAULT ((0)) NOT NULL,
    [TotalTI]                MONEY          NOT NULL,
    [TaxTypeID]              INT            CONSTRAINT [DF_DeliveryNoteCondEntry_TaxTypeID] DEFAULT ((0)) NOT NULL,
    [TaxQty]                 MONEY          NOT NULL,
    [CalculationCostMode]    SMALLINT       CONSTRAINT [DF_DeliveryNoteCondEntry_CalculationCostMode] DEFAULT ((0)) NOT NULL,
    [ModificationFactorCost] FLOAT          CONSTRAINT [DF_DeliveryNoteCondEntry_ModificationFactorCost] DEFAULT ((0)) NOT NULL,
    [CalculatedCost]         MONEY          NOT NULL,
    [Comments]               NVARCHAR (MAX) NULL,
    [Status]                 SMALLINT       NOT NULL,
    [Manual]                 BIT            CONSTRAINT [DF_DeliveryNoteCondEntry_Manual] DEFAULT ((0)) NOT NULL,
    [Exported]               BIT            NOT NULL,
    [LastUpdated]            DATETIME       NOT NULL,
    [ModifiedBy]             NVARCHAR (256) NOT NULL,
    [DBTimeStamp]            TIMESTAMP      NOT NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_DeliveryNoteCondEntry]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_DeliveryNoteCondEntry] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[DeliveryNoteCondEntry])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_DeliveryNoteCondEntry] ON;
        INSERT INTO [dbo].[tmp_ms_xx_DeliveryNoteCondEntry] ([ID], [RegistrationDateTime], [DateTime], [CondElementID], [CondEntityID], 
			[AssignedCode], [Name], [Description], [DeliveryNoteEntryID], [DiscountType], [DiscountQty], [PhysUnitID], [FactorCode], 
			[ModificationFactor], [Units], [PriceUnit], [TotalTI], [TaxTypeID], [TaxQty], 
			[CalculationCostMode], [ModificationFactorCost], [CalculatedCost], [Comments], 
			[Status], [Manual], [Exported], [LastUpdated], [ModifiedBy])
        SELECT   DNCE.[ID],
                 DNCE.[RegistrationDateTime],
                 CAST(FLOOR(CAST(DNCE.[RegistrationDateTime] AS FLOAT)) AS DATETIME),
                 DNCE.[CondElementID],
                 DNCE.[CondEntityID],
                 DNCE.[AssignedCode],
                 DNCE.[Name],
                 DNCE.[Description],
                 DNCE.[DeliveryNoteEntryID],
                 DNCE.[DiscountType],
                 DNCE.[DiscountQty],
                 DNCE.[PhysUnitID],
                 DNCE.[FactorCode],
                 DNCE.[ModificationFactor],
                 DNCE.[Units],
                 DNCE.[PriceUnit],
                 DNCE.[TotalTI],
                 DNCE.[TaxTypeID],
                 DNCE.[TaxQty],
                 DNCE.[CalculationCostMode],
                 CASE HAC.CalculationCostMode WHEN 1 THEN HAC.CalculationCostQty ELSE 0 END,
                 DNCE.[CalculatedCost],
                 DNCE.[Comments],
                 DNCE.[Status],
                 DNCE.[Manual],
                 DNCE.[Exported],
                 DNCE.[LastUpdated],
                 DNCE.[ModifiedBy]
        FROM     [dbo].[DeliveryNoteCondEntry] DNCE
					JOIN HistoryAgreeCondition HAC ON HAC.ID=DNCE.CondEntityID AND DNCE.CondElementID=@AgreeConditionID
        ORDER BY [ID] ASC;
        
        INSERT INTO [dbo].[tmp_ms_xx_DeliveryNoteCondEntry] ([ID], [RegistrationDateTime], [DateTime], [CondElementID], [CondEntityID], 
			[AssignedCode], [Name], [Description], [DeliveryNoteEntryID], [DiscountType], [DiscountQty], [PhysUnitID], [FactorCode], 
			[ModificationFactor], [Units], [PriceUnit], [TotalTI], [TaxTypeID], [TaxQty], 
			[CalculationCostMode], [ModificationFactorCost], [CalculatedCost], [Comments], 
			[Status], [Manual], [Exported], [LastUpdated], [ModifiedBy])
        SELECT   DNCE.[ID],
                 DNCE.[RegistrationDateTime],
                 CAST(FLOOR(CAST(DNCE.[RegistrationDateTime] AS FLOAT)) AS DATETIME),
                 DNCE.[CondElementID],
                 DNCE.[CondEntityID],
                 DNCE.[AssignedCode],
                 DNCE.[Name],
                 DNCE.[Description],
                 DNCE.[DeliveryNoteEntryID],
                 DNCE.[DiscountType],
                 DNCE.[DiscountQty],
                 DNCE.[PhysUnitID],
                 DNCE.[FactorCode],
                 DNCE.[ModificationFactor],
                 DNCE.[Units],
                 DNCE.[PriceUnit],
                 DNCE.[TotalTI],
                 DNCE.[TaxTypeID],
                 DNCE.[TaxQty],
                 DNCE.[CalculationCostMode],
                 CASE HIC.CalculationCostMode WHEN 1 THEN HIC.CalculationCostQty ELSE 0 END,
                 DNCE.[CalculatedCost],
                 DNCE.[Comments],
                 DNCE.[Status],
                 DNCE.[Manual],
                 DNCE.[Exported],
                 DNCE.[LastUpdated],
                 DNCE.[ModifiedBy]
        FROM     [dbo].[DeliveryNoteCondEntry] DNCE
					JOIN HistoryInsurerCondition HIC ON HIC.ID=DNCE.CondEntityID AND DNCE.CondElementID=@InsurerConditionID 
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_DeliveryNoteCondEntry] OFF;
    END

DROP TABLE [dbo].[DeliveryNoteCondEntry];
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_DeliveryNoteCondEntry]', N'DeliveryNoteCondEntry';
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_DeliveryNoteCondEntry]', N'PK_DeliveryNoteCondEntry', N'OBJECT';

PRINT N'Starting rebuilding table [dbo].[DeliveryNoteEntry]...';
CREATE TABLE [dbo].[tmp_ms_xx_DeliveryNoteEntry] (
    [ID]                     INT            IDENTITY (1, 1) NOT NULL,
    [RegistrationDateTime]   DATETIME       NOT NULL,
    [DateTime]               DATETIME       NOT NULL,
    [AgreeElementID]         INT            NOT NULL,
    [AgreeEntityID]          INT            NOT NULL,
    [AssignedCode]           NVARCHAR (50)  NULL,
    [Name]                   NVARCHAR (100) NULL,
    [Description]            NVARCHAR (MAX) NULL,
    [DeliveryNoteID]         INT            NOT NULL,
    [DiscountType]           SMALLINT       CONSTRAINT [DF_DeliveryNoteEntry_DiscountType] DEFAULT ((0)) NOT NULL,
    [DiscountQty]            MONEY          CONSTRAINT [DF_DeliveryNoteEntry_DiscountQty] DEFAULT ((0)) NOT NULL,
    [CalculationMode]        SMALLINT       CONSTRAINT [DF_DeliveryNoteEntry_CalculationMode] DEFAULT ((0)) NOT NULL,
    [FactorCode]             SMALLINT       CONSTRAINT [DF_DeliveryNoteEntry_FactorCode] DEFAULT ((0)) NOT NULL,
    [ModificationFactor]     FLOAT          CONSTRAINT [DF_DeliveryNoteEntry_ModificationFactor] DEFAULT ((0)) NOT NULL,
    [Units]                  FLOAT          CONSTRAINT [DF_DeliveryNoteEntry_Unit] DEFAULT ((0)) NOT NULL,
    [PriceUnit]              MONEY          CONSTRAINT [DF_DeliveryNoteEntry_PriceUnit] DEFAULT ((0)) NOT NULL,
    [TotalTI]                MONEY          NOT NULL,
    [TaxTypeID]              INT            CONSTRAINT [DF_DeliveryNoteEntry_TaxTypeID] DEFAULT ((0)) NOT NULL,
    [TaxQty]                 MONEY          CONSTRAINT [DF_DeliveryNoteEntry_TaxQty] DEFAULT ((0)) NOT NULL,
    [CalculationCostMode]    SMALLINT       CONSTRAINT [DF_DeliveryNoteEntry_CalculationCostMode] DEFAULT ((0)) NOT NULL,
    [ModificationFactorCost] FLOAT          CONSTRAINT [DF_DeliveryNoteEntry_ModificationFactorCost] DEFAULT ((0)) NOT NULL,
    [CalculatedCost]         MONEY          NOT NULL,
    [Comments]               NVARCHAR (MAX) NULL,
    [ExclusiveConditions]    BIT            CONSTRAINT [DF_DeliveryNoteEntry_ExclusiveConditions] DEFAULT ((0)) NOT NULL,
    [Status]                 SMALLINT       NOT NULL,
    [Manual]                 BIT            CONSTRAINT [DF_DeliveryNoteEntry_Manual] DEFAULT ((0)) NOT NULL,
    [Exported]               BIT            CONSTRAINT [DF_DeliveryNoteEntry_Export] DEFAULT ((0)) NOT NULL,
    [LastUpdated]            DATETIME       NOT NULL,
    [ModifiedBy]             NVARCHAR (256) NOT NULL,
    [DBTimeStamp]            TIMESTAMP      NOT NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_DeliveryNoteEntry]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_DeliveryNoteEntry] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[DeliveryNoteEntry])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_DeliveryNoteEntry] ON;
        INSERT INTO [dbo].[tmp_ms_xx_DeliveryNoteEntry] ([ID], [RegistrationDateTime], [DateTime], [AgreeElementID], [AgreeEntityID], 
			[AssignedCode], [Name], [Description], [DeliveryNoteID], [DiscountType], [DiscountQty], [CalculationMode], [FactorCode], 
			[ModificationFactor], [Units], [PriceUnit], [TotalTI], [TaxTypeID], [TaxQty], 
			[CalculationCostMode], [ModificationFactorCost], [CalculatedCost], [Comments], 
			[ExclusiveConditions], [Status], [Manual], [Exported], [LastUpdated], [ModifiedBy])
        SELECT   DNE.[ID],
                 DNE.[RegistrationDateTime],
                 CAST(FLOOR(CAST(DNE.[RegistrationDateTime] AS FLOAT)) AS DATETIME),
                 DNE.[AgreeElementID],
                 DNE.[AgreeEntityID],
                 DNE.[AssignedCode],
                 DNE.[Name],
                 DNE.[Description],
                 DNE.[DeliveryNoteID],
                 DNE.[DiscountType],
                 DNE.[DiscountQty],
                 DNE.[CalculationMode],
                 DNE.[FactorCode],
                 DNE.[ModificationFactor],
                 DNE.[Units],
                 DNE.[PriceUnit],
                 DNE.[TotalTI],
                 DNE.[TaxTypeID],
                 DNE.[TaxQty],
                 DNE.[CalculationCostMode],
                 CASE HA.CalculationCostMode WHEN 1 THEN HA.CalculationCostQty ELSE 0 END,
                 DNE.[CalculatedCost],
                 DNE.[Comments],
                 DNE.[ExclusiveConditions],
                 DNE.[Status],
                 DNE.[Manual],
                 DNE.[Exported],
                 DNE.[LastUpdated],
                 DNE.[ModifiedBy]
        FROM     [dbo].[DeliveryNoteEntry] DNE
					JOIN HistoryAgreement HA ON HA.ID=DNE.AgreeEntityID AND DNE.AgreeElementID=@AgreementID
        ORDER BY [ID] ASC;
        
        INSERT INTO [dbo].[tmp_ms_xx_DeliveryNoteEntry] ([ID], [RegistrationDateTime], [DateTime], [AgreeElementID], [AgreeEntityID], 
			[AssignedCode], [Name], [Description], [DeliveryNoteID], [DiscountType], [DiscountQty], [CalculationMode], [FactorCode], 
			[ModificationFactor], [Units], [PriceUnit], [TotalTI], [TaxTypeID], [TaxQty], 
			[CalculationCostMode], [ModificationFactorCost], [CalculatedCost], [Comments], 
			[ExclusiveConditions], [Status], [Manual], [Exported], [LastUpdated], [ModifiedBy])
        SELECT   DNE.[ID],
                 DNE.[RegistrationDateTime],
                 CAST(FLOOR(CAST(DNE.[RegistrationDateTime] AS FLOAT)) AS DATETIME),
                 DNE.[AgreeElementID],
                 DNE.[AgreeEntityID],
                 DNE.[AssignedCode],
                 DNE.[Name],
                 DNE.[Description],
                 DNE.[DeliveryNoteID],
                 DNE.[DiscountType],
                 DNE.[DiscountQty],
                 DNE.[CalculationMode],
                 DNE.[FactorCode],
                 DNE.[ModificationFactor],
                 DNE.[Units],
                 DNE.[PriceUnit],
                 DNE.[TotalTI],
                 DNE.[TaxTypeID],
                 DNE.[TaxQty],
                 DNE.[CalculationCostMode],
                 CASE HIA.CalculationCostMode WHEN 1 THEN HIA.CalculationCostQty ELSE 0 END,
                 DNE.[CalculatedCost],
                 DNE.[Comments],
                 DNE.[ExclusiveConditions],
                 DNE.[Status],
                 DNE.[Manual],
                 DNE.[Exported],
                 DNE.[LastUpdated],
                 DNE.[ModifiedBy]
        FROM     [dbo].[DeliveryNoteEntry] DNE
					JOIN HistoryInsurerAgreement HIA ON HIA.ID=DNE.AgreeEntityID AND DNE.AgreeElementID=@InsurerAgreementID
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_DeliveryNoteEntry] OFF;
    END

DROP TABLE [dbo].[DeliveryNoteEntry];
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_DeliveryNoteEntry]', N'DeliveryNoteEntry';
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_DeliveryNoteEntry]', N'PK_DeliveryNoteEntry', N'OBJECT';

COMMIT TRANSACTION
