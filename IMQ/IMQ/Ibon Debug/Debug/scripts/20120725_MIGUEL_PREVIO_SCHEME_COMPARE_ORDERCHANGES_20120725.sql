USE [NOMBREBD]
GO

--//////////////////////////////////////////////////////////
--//
--//  CAMBIOS EN DRUGINFO
--//
--//////////////////////////////////////////////////////////
PRINT N'Starting rebuilding table [dbo].[DrugInfo]...';
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
SET XACT_ABORT ON;
BEGIN TRANSACTION;
CREATE TABLE [dbo].[tmp_ms_xx_DrugInfo] (
    [ID]                     INT            IDENTITY (1, 1) NOT NULL,
    [ItemID]                 INT            NOT NULL,
    [UnitaryDose]            FLOAT          NULL,
    [GiveUnitsID]            INT            NULL,
    [NationalCode]           NVARCHAR (50)  NOT NULL,
    [EquivalentNationalCode] NVARCHAR (50)  NULL,
    [LaboratoryName]         NVARCHAR (100) NULL,
    [PharmaceuticalForm]     NVARCHAR (100) NULL,
    [TherapeuticGroup]       NVARCHAR (100) NULL,
    [AdministrationRoute]    NVARCHAR (100) NULL,
    [CountryName]            NVARCHAR (100) NULL,
    [UnitsPerPack]           INT            NOT NULL,
    [LastUpdated]            DATETIME       NOT NULL,
    [ModifiedBy]             NVARCHAR (256) NOT NULL,
    [DBTimeStamp]            TIMESTAMP      NOT NULL
);
ALTER TABLE [dbo].[tmp_ms_xx_DrugInfo]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_DrugInfo] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[DrugInfo])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_DrugInfo] ON;
        INSERT INTO [dbo].[tmp_ms_xx_DrugInfo] ([ID], [ItemID], [UnitaryDose], [GiveUnitsID], 
					[NationalCode], [EquivalentNationalCode], [LaboratoryName], [PharmaceuticalForm], 
					[TherapeuticGroup], [AdministrationRoute], [CountryName], [UnitsPerPack], 
					[LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [ItemID],
                 0,
                 0,
                 [NationalCode],
                 [EquivalentNationalCode],
                 [LaboratoryName],
                 [PharmaceuticalForm],
                 [TherapeuticGroup],
                 [AdministrationRoute],
                 [CountryName],
                 [UnitsPerPack],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[DrugInfo]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_DrugInfo] OFF;
    END
DROP TABLE [dbo].[DrugInfo];
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_DrugInfo]', N'DrugInfo';
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_DrugInfo]', N'PK_DrugInfo', N'OBJECT';
COMMIT TRANSACTION;
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO
PRINT N'Creating [dbo].[DrugInfo].[IX_DrugInfo_ItemID]...';
GO
CREATE NONCLUSTERED INDEX [IX_DrugInfo_ItemID]
    ON [dbo].[DrugInfo]([ItemID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];
GO
--//////////////////////////////////////////////////////////
--//
--//  FIN DRUGINFO
--//
--//////////////////////////////////////////////////////////


--//////////////////////////////////////////////////////////
--//
--//  CAMBIOS EN UnidosisInfo
--//
--//////////////////////////////////////////////////////////
PRINT N'Starting rebuilding table [dbo].[UnidosisInfo]...';
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
SET XACT_ABORT ON;
BEGIN TRANSACTION;
CREATE TABLE [dbo].[tmp_ms_xx_UnidosisInfo] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [ItemID]      INT            NOT NULL,
    [UnitaryDose] FLOAT          NULL,
    [GiveUnitsID] INT            NULL,
    [LastUpdated] DATETIME       NOT NULL,
    [ModifiedBy]  NVARCHAR (256) NOT NULL,
    [DBTimeStamp] TIMESTAMP      NOT NULL
);
ALTER TABLE [dbo].[tmp_ms_xx_UnidosisInfo]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_UnidosisInfo] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[UnidosisInfo])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_UnidosisInfo] ON;
        INSERT INTO [dbo].[tmp_ms_xx_UnidosisInfo] ([ID], [ItemID], [UnitaryDose], [GiveUnitsID], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [ItemID],
                 0,
                 0,
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[UnidosisInfo]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_UnidosisInfo] OFF;
    END
DROP TABLE [dbo].[UnidosisInfo];
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_UnidosisInfo]', N'UnidosisInfo';
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_UnidosisInfo]', N'PK_UnidosisInfo', N'OBJECT';
COMMIT TRANSACTION;
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO
PRINT N'Creating [dbo].[UnidosisInfo].[IX_UnidosisInfo_ItemID]...';
GO
CREATE NONCLUSTERED INDEX [IX_UnidosisInfo_ItemID]
    ON [dbo].[UnidosisInfo]([ItemID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];
GO
--//////////////////////////////////////////////////////////
--//
--//  FIN UnidosisInfo
--//
--//////////////////////////////////////////////////////////




--//////////////////////////////////////////////////////////
--//
--//  CAMBIOS EN ITEM
--//
--//////////////////////////////////////////////////////////
PRINT N'Starting rebuilding table [dbo].[Item]...';
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
SET XACT_ABORT ON;
BEGIN TRANSACTION;
CREATE TABLE [dbo].[tmp_ms_xx_Item] (
    [ID]                         INT            IDENTITY (1, 1) NOT NULL,
    [Code]                       NVARCHAR (50)  NOT NULL,
    [GenericName]                NVARCHAR (100) NOT NULL,
    [ComercialName]              NVARCHAR (100) NULL,
    [Description]                NVARCHAR (200) NULL,
    [ItemType]                   SMALLINT       NOT NULL,
    [FamilyID]                   INT            NOT NULL,
    [SubFamilyID]                INT            NOT NULL,
    [StartDateDB]                DATETIME       NOT NULL,
    [EndDateDB]                  DATETIME       NULL,
    [Price]                      MONEY          NOT NULL,
    [ProrateApplied]             MONEY          NOT NULL,
    [AverageCost]                MONEY          NOT NULL,
    [ProrateAverageCost]         MONEY          NOT NULL,
    [LastCost]                   MONEY          NOT NULL,
    [ProrateLastCost]            MONEY          NOT NULL,
    [AverageStockCost]           MONEY          NOT NULL,
    [ProrateAverageStockCost]    MONEY          NOT NULL,
    [AverageWeightedCost]        MONEY          NOT NULL,
    [ProrateAverageWeightedCost] MONEY          NOT NULL,
    [TaxID]                      INT            NOT NULL,
    [BarCode]                    NVARCHAR (30)  NULL,
    [DefaultReplenishmentUnits]  INT            CONSTRAINT [DF_Item_ReplenishmentUnits] DEFAULT ((0)) NULL,
    [Status]                     SMALLINT       NOT NULL,
    [LastUpdated]                DATETIME       NOT NULL,
    [ModifiedBy]                 NVARCHAR (256) NOT NULL,
    [DBTimeStamp]                TIMESTAMP      NOT NULL
);
ALTER TABLE [dbo].[tmp_ms_xx_Item]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_Item] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[Item])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Item] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Item] ([ID], [Code], [GenericName], [ComercialName], [Description], 
					[ItemType], [FamilyID], [SubFamilyID], [StartDateDB], [EndDateDB], [Price], 
					[ProrateApplied], [AverageCost], [ProrateAverageCost], [LastCost], [ProrateLastCost], 
					[AverageStockCost], [ProrateAverageStockCost], [AverageWeightedCost], 
					[ProrateAverageWeightedCost], [TaxID], [BarCode], [DefaultReplenishmentUnits], 
					[Status], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [Code],
                 [GenericName],
                 [ComercialName],
                 [Description],
                 [ItemType],
                 [FamilyID],
                 [SubFamilyID],
                 [StartDateDB],
                 [EndDateDB],
                 [Price],
                 [ProrateApplied],
                 [AverageCost],
                 [ProrateAverageCost],
                 [LastCost],
                 [ProrateLastCost],
                 [AverageStockCost],
                 [ProrateAverageStockCost],
                 [AverageWeightedCost],
                 [ProrateAverageWeightedCost],
                 [TaxID],
                 [BarCode],
                 1,
                 [Status],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[Item]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Item] OFF;
    END
DROP TABLE [dbo].[Item];
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Item]', N'Item';
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_Item]', N'PK_Item', N'OBJECT';
COMMIT TRANSACTION;
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO
PRINT N'Creating IX_Item_Code...';
GO
ALTER TABLE [dbo].[Item]
    ADD CONSTRAINT [IX_Item_Code] UNIQUE NONCLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];
GO    
--//////////////////////////////////////////////////////////
--//
--//  FIN ITEM
--//
--//////////////////////////////////////////////////////////




--//////////////////////////////////////////////////////////
--//
--//  CAMBIOS EN [ORDER]
--//
--//////////////////////////////////////////////////////////
PRINT N'Dropping DF_Order_AncestorID...';
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [DF_Order_AncestorID];
GO
PRINT N'Dropping DF_Order_InUse...';
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [DF_Order_InUse];
GO
PRINT N'Dropping DF_Order_ClinicalTrialIdentificationRequired...';
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [DF_Order_ClinicalTrialIdentificationRequired];
GO
PRINT N'Dropping DF_Order_ResultInterpretationRequired...';
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [DF_Order_ResultInterpretationRequired];
GO
PRINT N'Dropping DF_Order_IsCodified...';
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [DF_Order_IsCodified];
GO
PRINT N'Dropping DF_Order_RequestedLocationRequired...';
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [DF_Order_RequestedLocationRequired];
GO
PRINT N'Dropping DF_Order_RequestedPhysicianRequired...';
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [DF_Order_RequestedPhysicianRequired];
GO
PRINT N'Dropping DF_Order_OrderTypeID...';
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [DF_Order_OrderTypeID];
GO
PRINT N'Dropping DF_Order_RequestedInsurerRequired...';
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [DF_Order_RequestedInsurerRequired];
GO
PRINT N'Dropping DF_Order_RequestedCareCenterRequired...';
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [DF_Order_RequestedCareCenterRequired];
GO
PRINT N'Dropping DF_Order_ScheduleRequired...';
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [DF_Order_ScheduleRequired];
GO
PRINT N'Dropping DF_Order_OrderClassType...';
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [DF_Order_OrderClassType];
GO
PRINT N'Dropping FK_OrderPatternRel_Order...';
GO
ALTER TABLE [dbo].[OrderPatternRel] DROP CONSTRAINT [FK_OrderPatternRel_Order];
GO
PRINT N'Starting rebuilding table [dbo].[Order]...';
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
SET XACT_ABORT ON;
BEGIN TRANSACTION;
CREATE TABLE [dbo].[tmp_ms_xx_Order] (
    [ID]                   INT            IDENTITY (1, 1) NOT NULL,
    [OrderTypeID]          INT            CONSTRAINT [DF_Order_OrderTypeID] DEFAULT ((0)) NOT NULL,
    [OrderClassType]       SMALLINT       CONSTRAINT [DF_Order_OrderClassType] DEFAULT ((0)) NOT NULL,
    [IsCodified]           BIT            CONSTRAINT [DF_Order_IsCodified] DEFAULT ((0)) NOT NULL,
    [Encoder]              NVARCHAR (256) NULL,
    [AssignedCode]         NVARCHAR (50)  NOT NULL,
    [Name]                 NVARCHAR (100) NOT NULL,
    [Description]          NVARCHAR (200) NULL,
    [RegistrationDateTime] DATETIME       NOT NULL,
    [ToRequestOrderFlags]  BIGINT         CONSTRAINT [DF_Order_RequestedPhysicianRequired] DEFAULT ((0)) NOT NULL,
    [ADTRequestAction]     SMALLINT       CONSTRAINT [DF_Order_ADTActionCode] DEFAULT ((0)) NOT NULL,
    [AncestorID]           INT            CONSTRAINT [DF_Order_AncestorID] DEFAULT ((0)) NOT NULL,
    [InUse]                BIT            CONSTRAINT [DF_Order_InUse] DEFAULT ((0)) NOT NULL,
    [Status]               SMALLINT       NOT NULL,
    [ModifiedBy]           NVARCHAR (256) NOT NULL,
    [LastUpdated]          DATETIME       NOT NULL,
    [DBTimeStamp]          TIMESTAMP      NOT NULL
);
ALTER TABLE [dbo].[tmp_ms_xx_Order]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_Order] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[Order])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Order] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Order] ([ID], [OrderTypeID], [OrderClassType], [IsCodified], [Encoder], 
					[AssignedCode], [Name], [Description], [RegistrationDateTime], [AncestorID], [InUse], 
					[Status], [ModifiedBy], [LastUpdated])
        SELECT   [ID],
                 [OrderTypeID],
                 [OrderClassType],
                 [IsCodified],
                 [Encoder],
                 [AssignedCode],
                 [Name],
                 [Description],
                 [RegistrationDateTime],
                 [AncestorID],
                 [InUse],
                 [Status],
                 [ModifiedBy],
                 [LastUpdated]
        FROM     [dbo].[Order]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Order] OFF;
    END
DROP TABLE [dbo].[Order];
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Order]', N'Order';
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_Order]', N'PK_Order', N'OBJECT';
COMMIT TRANSACTION;
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO
PRINT N'Creating [dbo].[Order].[IX_Order]...';
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Order]
    ON [dbo].[Order]([AssignedCode] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = ON, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];
GO
PRINT N'Creating FK_OrderPatternRel_Order...';
GO
ALTER TABLE [dbo].[OrderPatternRel] WITH NOCHECK
    ADD CONSTRAINT [FK_OrderPatternRel_Order] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Order] ([ID]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO
PRINT N'Checking existing data against newly created constraints';
GO
ALTER TABLE [dbo].[OrderPatternRel] WITH CHECK CHECK CONSTRAINT [FK_OrderPatternRel_Order];
GO
--//////////////////////////////////////////////////////////
--//
--//  FIN CAMBIOS EN [ORDER]
--//
--//////////////////////////////////////////////////////////



--//////////////////////////////////////////////////////////
--//
--//  CAMBIOS EN PrescriptionRequest
--//
--//////////////////////////////////////////////////////////
PRINT N'Dropping DF_PrescriptionRequest_UnitaryQty...';
GO
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_UnitaryQty];
GO
PRINT N'Dropping DF_PrescriptionRequest_CurrentLocationID...';
GO
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_CurrentLocationID];
GO
PRINT N'Dropping DF_PrescriptionRequest_TimesPerDay...';
GO
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_TimesPerDay];
GO
PRINT N'Dropping DF_PrescriptionRequest_Dispatchment...';
GO
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_Dispatchment];
go

PRINT N'Starting rebuilding table [dbo].[PrescriptionRequest]...';
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
SET XACT_ABORT ON;
BEGIN TRANSACTION;
CREATE TABLE [dbo].[tmp_ms_xx_PrescriptionRequest] (
    [ID]                     INT            IDENTITY (1, 1) NOT NULL,
    [CustomerOrderRequestID] INT            CONSTRAINT [DF_PrescriptionRequest_CustomerOrderRequestID] DEFAULT ((0)) NULL,
    [CustomerProcedureID]    INT            CONSTRAINT [DF_PrescriptionRequest_CustomerProcedureID] DEFAULT ((0)) NULL,
    [RequestedPersonID]      INT            CONSTRAINT [DF_PrescriptionRequest_RequestedPersonID] DEFAULT ((0)) NULL,
    [CurrentLocationID]      INT            CONSTRAINT [DF_PrescriptionRequest_CurrentLocationID] DEFAULT ((0)) NOT NULL,
    [LocationID]             INT            NOT NULL,
    [ItemID]                 INT            NOT NULL,
    [UnitaryQty]             FLOAT          CONSTRAINT [DF_PrescriptionRequest_UnitaryQty] DEFAULT ((1)) NOT NULL,
    [TimesPerDay]            INT            CONSTRAINT [DF_PrescriptionRequest_TimesPerDay] DEFAULT ((1)) NOT NULL,
    [FrequencyID]            INT            NULL,
    [UnitaryDose]            FLOAT          NULL,
    [DayDose]                FLOAT          NULL,
    [TotalDose]              FLOAT          NULL,
    [SupplySupervised]       BIT            CONSTRAINT [DF_PrescriptionRequest_SupplySupervised] DEFAULT ((0)) NOT NULL,
    [StartDateTime]          DATETIME       NOT NULL,
    [EndDateTime]            DATETIME       NULL,
    [Dispatchment]           SMALLINT       CONSTRAINT [DF_PrescriptionRequest_Dispatchment] DEFAULT ((0)) NOT NULL,
    [Status]                 SMALLINT       NOT NULL,
    [LastUpdated]            DATETIME       NOT NULL,
    [ModifiedBy]             NVARCHAR (256) NOT NULL,
    [DBTimeStamp]            TIMESTAMP      NOT NULL
);
ALTER TABLE [dbo].[tmp_ms_xx_PrescriptionRequest]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_PrescriptionRequest] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[PrescriptionRequest])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_PrescriptionRequest] ON;
        INSERT INTO [dbo].[tmp_ms_xx_PrescriptionRequest] ([ID], [CustomerOrderRequestID], [CustomerProcedureID], [RequestedPersonID], [CurrentLocationID], [LocationID], 
					[ItemID], [UnitaryQty], [TimesPerDay], [FrequencyID], [UnitaryDose], [DayDose], [TotalDose], [SupplySupervised], [StartDateTime], [EndDateTime], [Dispatchment], [Status], [LastUpdated], [ModifiedBy])
        SELECT   PR.[ID],
				 ISNULL((SELECT TOP 1 ORSP.CustomerOrderRequestID FROM OrderRequestSchPlanning ORSP JOIN OrderRequestPrescriptionRel ORPR ON ORSP.ID=ORPR.OrderRequestSchPlanningID WHERE ORPR.PrescriptionRequestID=PR.[ID]),0),	
                 PR.[CustomerProcedureID],
                 PR.[RequestedPersonID],
                 PR.[CurrentLocationID],
                 PR.[LocationID],
                 PR.[ItemID],
                 PR.[UnitaryQty],
                 PR.[TimesPerDay],
                 PR.[FrequencyID],
				 ISNULL((SELECT TOP 1 ORSP.UnitaryDose FROM OrderRequestSchPlanning ORSP JOIN OrderRequestPrescriptionRel ORPR ON ORSP.ID=ORPR.OrderRequestSchPlanningID WHERE ORPR.PrescriptionRequestID=PR.[ID]),0),
				 ISNULL((SELECT TOP 1 ORSP.DayDose FROM OrderRequestSchPlanning ORSP JOIN OrderRequestPrescriptionRel ORPR ON ORSP.ID=ORPR.OrderRequestSchPlanningID WHERE ORPR.PrescriptionRequestID=PR.[ID]),0),	
				 ISNULL((SELECT TOP 1 ORSP.TotalDose FROM OrderRequestSchPlanning ORSP JOIN OrderRequestPrescriptionRel ORPR ON ORSP.ID=ORPR.OrderRequestSchPlanningID WHERE ORPR.PrescriptionRequestID=PR.[ID]),0),	
				 ISNULL((SELECT TOP 1 ORSP.SupplySupervised FROM OrderRequestSchPlanning ORSP JOIN OrderRequestPrescriptionRel ORPR ON ORSP.ID=ORPR.OrderRequestSchPlanningID WHERE ORPR.PrescriptionRequestID=PR.[ID]),0),	
                 PR.[StartDateTime],
                 PR.[EndDateTime],
                 PR.[Dispatchment],
                 PR.[Status],
                 PR.[LastUpdated],
                 PR.[ModifiedBy]
        FROM     [dbo].[PrescriptionRequest]PR
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_PrescriptionRequest] OFF;
    END
DROP TABLE [dbo].[PrescriptionRequest];
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_PrescriptionRequest]', N'PrescriptionRequest';
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_PrescriptionRequest]', N'PK_PrescriptionRequest', N'OBJECT';
COMMIT TRANSACTION;
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO

--//////////////////////////////////////////////////////////
--//
--//  QUITAR LAS COLUMNAS SOBRANTES EN OrderRequestSchPlanning
--//
--//////////////////////////////////////////////////////////
PRINT N'Altering [dbo].[OrderRequestSchPlanning]...';
GO
ALTER TABLE [dbo].[OrderRequestSchPlanning] DROP COLUMN [DayDose], COLUMN [ItemID], COLUMN [SupplySupervised], COLUMN [TotalDose], COLUMN [UnitaryDose], COLUMN [UnitaryQuantity];
GO
--//////////////////////////////////////////////////////////
--//
--//  FINALMNETE BORRAR [OrderRequestPrescriptionRel]
--//
--//////////////////////////////////////////////////////////
PRINT N'Dropping [dbo].[OrderRequestPrescriptionRel]...';
GO
DROP TABLE [dbo].[OrderRequestPrescriptionRel];
GO
PRINT N'Altering [dbo].[CustomerOrderRequest]...';
GO
ALTER TABLE [dbo].[CustomerOrderRequest] DROP COLUMN [ResultExpected];
GO
--//////////////////////////////////////////////////////////
--//
--//  FIN CAMBIOS EN CUSTOMERORDERREQUEST, PrescriptionRequest, [OrderRequestSchPlanning]
--//
--//////////////////////////////////////////////////////////
