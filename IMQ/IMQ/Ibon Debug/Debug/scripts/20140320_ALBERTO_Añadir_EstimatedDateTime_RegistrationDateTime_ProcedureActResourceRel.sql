SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

PRINT N'Dropping DF_ProcedureActResourceRel_ServiceChargeID...';
ALTER TABLE [dbo].[ProcedureActResourceRel] DROP CONSTRAINT [DF_ProcedureActResourceRel_ServiceChargeID];

PRINT N'Dropping DF_ProcedureActResourceRel_ReasonID...';
ALTER TABLE [dbo].[ProcedureActResourceRel] DROP CONSTRAINT [DF_ProcedureActResourceRel_ReasonID];

PRINT N'Starting rebuilding table [dbo].[ProcedureActResourceRel]...';

CREATE TABLE [dbo].[tmp_ms_xx_ProcedureActResourceRel] (
    [ID]                      INT            IDENTITY (1, 1) NOT NULL,
    [ProcedureActID]          INT            NOT NULL,
    [ItemID]                  INT            NOT NULL,
    [CustomerAccountChargeID] INT            CONSTRAINT [DF_ProcedureActResourceRel_ServiceChargeID] DEFAULT ((0)) NOT NULL,
    [Quantity]                FLOAT          NOT NULL,
    [SpendQuantity]           FLOAT          NOT NULL,
    [RealizationDateTime]     DATETIME       NOT NULL,
    [EstimatedDateTime]       DATETIME       NOT NULL,
    [ReasonChangeID]          INT            CONSTRAINT [DF_ProcedureActResourceRel_ReasonID] DEFAULT ((0)) NOT NULL,
    [Explanation]             NVARCHAR (MAX) NULL,
    [Status]                  SMALLINT       NOT NULL,
    [RegistrationDateTime]    DATETIME       NOT NULL,
    [LastUpdated]             DATETIME       NOT NULL,
    [ModifiedBy]              NVARCHAR (256) NOT NULL,
    [DBTimeStamp]             TIMESTAMP      NOT NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_ProcedureActResourceRel]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_ProcedureActResourceRel] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[ProcedureActResourceRel])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ProcedureActResourceRel] ON;
        INSERT INTO [dbo].[tmp_ms_xx_ProcedureActResourceRel] ([ID], [ProcedureActID], [ItemID], [CustomerAccountChargeID], 
			[Quantity], [SpendQuantity], [RealizationDateTime], [EstimatedDateTime], [ReasonChangeID], [Explanation], 
			[Status], [RegistrationDateTime], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [ProcedureActID],
                 [ItemID],
                 [CustomerAccountChargeID],
                 [Quantity],
                 [SpendQuantity],
                 [RealizationDateTime],
                 [RealizationDateTime],
                 [ReasonChangeID],
                 [Explanation],
                 [Status],
                 [RealizationDateTime],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[ProcedureActResourceRel]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ProcedureActResourceRel] OFF;
    END

DROP TABLE [dbo].[ProcedureActResourceRel];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_ProcedureActResourceRel]', N'ProcedureActResourceRel';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_ProcedureActResourceRel]', N'PK_ProcedureActResourceRel', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;