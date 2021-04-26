USE [database]
GO

PRINT N'Starting rebuilding table [dbo].[ItemHistoryInformation]...';
GO

/*
The column [dbo].[ItemHistoryInformation].[ItemQuantity] is being dropped, data loss could occur.
The column [dbo].[ItemHistoryInformation].[TotalQuantity] on table [dbo].[ItemHistoryInformation] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue, you must add a default value to the column or mark it as allowing NULL values.
The type for column AverageCost in table [dbo].[ItemHistoryInformation] is currently  FLOAT NOT NULL but is being changed to  MONEY NOT NULL. Data loss could occur.
The type for column AverageStockCost in table [dbo].[ItemHistoryInformation] is currently  FLOAT NOT NULL but is being changed to  MONEY NOT NULL. Data loss could occur.
The type for column AverageWeightedCost in table [dbo].[ItemHistoryInformation] is currently  FLOAT NOT NULL but is being changed to  MONEY NOT NULL. Data loss could occur.
The type for column ItemLastCost in table [dbo].[ItemHistoryInformation] is currently  FLOAT NOT NULL but is being changed to  MONEY NOT NULL. Data loss could occur.
The type for column ProrateAverageCost in table [dbo].[ItemHistoryInformation] is currently  FLOAT NOT NULL but is being changed to  MONEY NOT NULL. Data loss could occur.
The type for column ProrateAverageStockCost in table [dbo].[ItemHistoryInformation] is currently  FLOAT NOT NULL but is being changed to  MONEY NOT NULL. Data loss could occur.
The type for column ProrateAverageWeightedCost in table [dbo].[ItemHistoryInformation] is currently  FLOAT NOT NULL but is being changed to  MONEY NOT NULL. Data loss could occur.
The type for column ProrateItemLastCost in table [dbo].[ItemHistoryInformation] is currently  FLOAT NOT NULL but is being changed to  MONEY NOT NULL. Data loss could occur.
The type for column SalesPrice in table [dbo].[ItemHistoryInformation] is currently  FLOAT NOT NULL but is being changed to  MONEY NOT NULL. Data loss could occur.
*/
GO

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
SET XACT_ABORT ON;
BEGIN TRANSACTION;

DROP TABLE [dbo].[ItemHistoryInformation];

CREATE TABLE [dbo].[ItemHistoryInformation] (
    [ID]                         INT            IDENTITY (1, 1) NOT NULL,
    [ItemType]                   SMALLINT       NOT NULL,
    [ItemID]                     INT            NOT NULL,
    [ItemCode]                   NVARCHAR (50)  NOT NULL,
    [CurrentName]                NVARCHAR (100) NOT NULL,
    [LocationID]                 INT            NOT NULL,
    [PODateTime]                 DATETIME       NOT NULL,
    [POEntryID]                  INT            NOT NULL,
    [ProrateApplied]             FLOAT          NOT NULL,
    [ProrateAverageCost]         MONEY          NOT NULL,
    [AverageCost]                MONEY          NOT NULL,
    [ItemLastCost]               MONEY          NOT NULL,
    [Quantity]                   FLOAT          NOT NULL,
    [TotalQuantity]              FLOAT          NOT NULL,
    [SalesPrice]                 MONEY          NOT NULL,
    [ProrateItemLastCost]        MONEY          NOT NULL,
    [AverageStockCost]           MONEY          NOT NULL,
    [ProrateAverageStockCost]    MONEY          NOT NULL,
    [AverageWeightedCost]        MONEY          NOT NULL,
    [ProrateAverageWeightedCost] MONEY          NOT NULL,
    [TaxTypeID]                  INT            NOT NULL,
    [CalculationDateTime]        DATETIME       NOT NULL,
    [Status]                     SMALLINT       NOT NULL,
    [LastUpdated]                DATETIME       NOT NULL,
    [ModifiedBy]                 NVARCHAR (256) NOT NULL
);

PRINT N'Creating PK_ItemHistoryInformation...';
ALTER TABLE [dbo].[ItemHistoryInformation]
    ADD CONSTRAINT [PK_ItemHistoryInformation] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

COMMIT TRANSACTION;
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO