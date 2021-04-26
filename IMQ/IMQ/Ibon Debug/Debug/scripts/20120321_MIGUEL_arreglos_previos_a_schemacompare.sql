USE --VUESTRABD

GO
ALTER TABLE [dbo].[ProcessChartHierarchyRel] DROP CONSTRAINT [DF_ProcessChartHierarchyRel_PlacementChargeIsMandatory];
GO
ALTER TABLE [dbo].[ProcessChartHierarchyRel] DROP CONSTRAINT [DF_ProcessChartHierarchyRel_MinimumTimeIntervalID];
GO
PRINT N'Starting rebuilding table [dbo].[ProcessChartAssistProcessRel]...';
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_ProcessChartAssistProcessRel] (
    [ID]                       INT            IDENTITY (1, 1) NOT NULL,
    [ProcessChartID]           INT            NOT NULL,
    [CareCenterID]             INT            CONSTRAINT [DF_ProcessChartAssistProcessRel_CareCenterID] DEFAULT ((0)) NOT NULL,
    [AssistanceProcessChartID] INT            NOT NULL,
    [StartDateTime]            DATETIME       NULL,
    [EndDateTime]              DATETIME       NULL,
    [LastUpdated]              DATETIME       NOT NULL,
    [ModifiedBy]               NVARCHAR (256) NOT NULL,
    [DBTimeStamp]              TIMESTAMP      NOT NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_ProcessChartAssistProcessRel]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_ProcessChartAssistProcessRel] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[ProcessChartAssistProcessRel])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ProcessChartAssistProcessRel] ON;
        INSERT INTO [dbo].[tmp_ms_xx_ProcessChartAssistProcessRel] ([ID], [AssistanceProcessChartID], [ProcessChartID], 
        [CareCenterID], 
        
        [StartDateTime], [EndDateTime], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [AssistanceProcessChartID],
                 [ProcessChartID],
				 ISNULL((SELECT TOP 1 PCC.CareCenterID 
					FROM ProcessChartCareCenterRel PCC 
					WHERE PCC.ProcessChartID = [ProcessChartID]),0),        
                 [StartDateTime],
                 [EndDateTime],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[ProcessChartAssistProcessRel]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ProcessChartAssistProcessRel] OFF;
    END

DROP TABLE [dbo].[ProcessChartAssistProcessRel];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_ProcessChartAssistProcessRel]', N'ProcessChartAssistProcessRel';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_ProcessChartAssistProcessRel]', N'PK_ProcessChartAssistProcessRel', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[ProcessChartGuarTypeRel]...';


GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_ProcessChartGuarTypeRel] (
    [ID]                   INT            IDENTITY (1, 1) NOT NULL,
    [ProcessChartID]       INT            NOT NULL,
    [CareCenterID]         INT            CONSTRAINT [DF_ProcessChartGuarTypeRel_CareCenterID] DEFAULT ((0)) NOT NULL,
    [GuarantorType]        SMALLINT       NOT NULL,
    [Required]             BIT            NOT NULL,
    [OnlyOne]              BIT            NOT NULL,
    [InvoiceAgreeRequired] BIT            NOT NULL,
    [AsDefaultGuarantor]   BIT            NOT NULL,
    [AdmitSrvOutsideAgree] BIT            NOT NULL,
    [CoverOrder]           INT            NOT NULL,
    [Status]               SMALLINT       NOT NULL,
    [LastUpdated]          DATETIME       NOT NULL,
    [ModifiedBy]           NVARCHAR (256) NOT NULL,
    [DBTimeStamp]          TIMESTAMP      NOT NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_ProcessChartGuarTypeRel]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_ProcessChartGuarTypeRel] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[ProcessChartGuarTypeRel])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ProcessChartGuarTypeRel] ON;
        INSERT INTO [dbo].[tmp_ms_xx_ProcessChartGuarTypeRel] ([ID], [ProcessChartID], [CareCenterID], [GuarantorType], [Required], [OnlyOne], [InvoiceAgreeRequired], [AsDefaultGuarantor], [AdmitSrvOutsideAgree], [CoverOrder], [Status], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [ProcessChartID],
  				 ISNULL((SELECT TOP 1 PCC.CareCenterID 
					FROM ProcessChartCareCenterRel PCC 
					WHERE PCC.ProcessChartID = [ProcessChartID]),0),        
                 [GuarantorType],
                 [Required],
                 [OnlyOne],
                 [InvoiceAgreeRequired],
                 [AsDefaultGuarantor],
                 [AdmitSrvOutsideAgree],
                 [CoverOrder],
                 [Status],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[ProcessChartGuarTypeRel]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ProcessChartGuarTypeRel] OFF;
    END

DROP TABLE [dbo].[ProcessChartGuarTypeRel];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_ProcessChartGuarTypeRel]', N'ProcessChartGuarTypeRel';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_ProcessChartGuarTypeRel]', N'PK_ProcessChartGuarTypeRel', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[ProcessChartHierarchyRel]...';


GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_ProcessChartHierarchyRel] (
    [ID]                              INT            IDENTITY (1, 1) NOT NULL,
    [ProcessChartID]                  INT            NOT NULL,
    [CareCenterID]                    INT            CONSTRAINT [DF_ProcessChartHierarchyRel_CareCenterID] DEFAULT ((0)) NOT NULL,
    [LocationID]                      INT            NOT NULL,
    [LocationTypeID]                  INT            NOT NULL,
    [LocationClassID]                 INT            NOT NULL,
    [StartDateTime]                   DATETIME       NOT NULL,
    [EndDateTime]                     DATETIME       NULL,
    [IsDefault]                       BIT            NOT NULL,
    [DefaultPlacementChargeID]        INT            NOT NULL,
    [PlacementChargeIsMandatory]      BIT            CONSTRAINT [DF_ProcessChartHierarchyRel_PlacementChargeIsMandatory] DEFAULT ((0)) NOT NULL,
    [MinimumTimeIntervalID]           INT            CONSTRAINT [DF_ProcessChartHierarchyRel_MinimumTimeIntervalID] DEFAULT ((0)) NOT NULL,
    [FractionationUnit]               SMALLINT       NOT NULL,
    [PlacementChargeFractionation]    BIT            NOT NULL,
    [ResourceChargeAsPlacementCharge] BIT            NOT NULL,
    [Status]                          SMALLINT       NOT NULL,
    [LastUpdated]                     DATETIME       NOT NULL,
    [ModifiedBy]                      NVARCHAR (256) NOT NULL,
    [DBTimeStamp]                     TIMESTAMP      NOT NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_ProcessChartHierarchyRel]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_ProcessChartHierarchyRel] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[ProcessChartHierarchyRel])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ProcessChartHierarchyRel] ON;
        INSERT INTO [dbo].[tmp_ms_xx_ProcessChartHierarchyRel] ([ID], [ProcessChartID], [CareCenterID], [LocationID], [LocationTypeID], [LocationClassID], [StartDateTime], [EndDateTime], [IsDefault], [DefaultPlacementChargeID], [PlacementChargeIsMandatory], [MinimumTimeIntervalID], [FractionationUnit], [PlacementChargeFractionation], [ResourceChargeAsPlacementCharge], [Status], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [ProcessChartID],
  				 ISNULL((SELECT TOP 1 PCC.CareCenterID 
					FROM ProcessChartCareCenterRel PCC 
					WHERE PCC.ProcessChartID = [ProcessChartID]),0),        
                 [LocationID],
                 [LocationTypeID],
                 [LocationClassID],
                 [StartDateTime],
                 [EndDateTime],
                 [IsDefault],
                 [DefaultPlacementChargeID],
                 [PlacementChargeIsMandatory],
                 [MinimumTimeIntervalID],
                 [FractionationUnit],
                 [PlacementChargeFractionation],
                 [ResourceChargeAsPlacementCharge],
                 [Status],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[ProcessChartHierarchyRel]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ProcessChartHierarchyRel] OFF;
    END

DROP TABLE [dbo].[ProcessChartHierarchyRel];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_ProcessChartHierarchyRel]', N'ProcessChartHierarchyRel';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_ProcessChartHierarchyRel]', N'PK_ProcessChartHierarchyRel', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
