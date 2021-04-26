
SET XACT_ABORT ON
		
BEGIN TRANSACTION
PRINT 'BEGIN TRANSACTION' 
		
PRINT N'Dropping DF_CustomerCitation_CustomerProcessID...';
ALTER TABLE [dbo].[CustomerCitation] DROP CONSTRAINT [DF_CustomerCitation_CustomerProcessID];

PRINT N'Dropping DF_CustomerCitation_RequestingAssistanceServiceID...';
ALTER TABLE [dbo].[CustomerCitation] DROP CONSTRAINT [DF_CustomerCitation_RequestingAssistanceServiceID];

PRINT N'Dropping DF_CustomerCitation_CustomerPolicyID...';
ALTER TABLE [dbo].[CustomerCitation] DROP CONSTRAINT [DF_CustomerCitation_CustomerPolicyID];

PRINT N'Dropping DF_CustomerCitation_AppointmentRequestedPhysicianID...';
ALTER TABLE [dbo].[CustomerCitation] DROP CONSTRAINT [DF_CustomerCitation_AppointmentRequestedPhysicianID];

PRINT N'Dropping DF_CustomerCitation_ReceptionAssistanceServiceID...';
ALTER TABLE [dbo].[CustomerCitation] DROP CONSTRAINT [DF_CustomerCitation_ReceptionAssistanceServiceID];

PRINT N'Dropping DF_CustomerCitation_RequestingMedicalSpecialtyID...';
ALTER TABLE [dbo].[CustomerCitation] DROP CONSTRAINT [DF_CustomerCitation_RequestingMedicalSpecialtyID];

PRINT N'Starting rebuilding table [dbo].[CustomerCitation]...';
CREATE TABLE [dbo].[tmp_ms_xx_CustomerCitation] (
    [ID]                              INT            IDENTITY (1, 1) NOT NULL,
    [CustomerID]                      INT            NOT NULL,
    [ProcessChartID]                  INT            NOT NULL,
    [CustomerProcessID]               INT            CONSTRAINT [DF_CustomerCitation_CustomerProcessID] DEFAULT ((0)) NOT NULL,
    [RequestingAssistanceServiceID]   INT            CONSTRAINT [DF_CustomerCitation_RequestingAssistanceServiceID] DEFAULT ((0)) NOT NULL,
    [ReceptionAssistanceServiceID]    INT            CONSTRAINT [DF_CustomerCitation_ReceptionAssistanceServiceID] DEFAULT ((0)) NOT NULL,
    [InsurerID]                       INT            NOT NULL,
    [PolicyTypeID]                    INT            CONSTRAINT [DF_CustomerCitation_CustomerPolicyID] DEFAULT ((0)) NOT NULL,
    [CareCenterID]                    INT            NOT NULL,
    [AppointmentRequestedPhysicianID] INT            CONSTRAINT [DF_CustomerCitation_AppointmentRequestedPhysicianID] DEFAULT ((0)) NOT NULL,
    [RequestingMedicalSpecialtyID]    INT            CONSTRAINT [DF_CustomerCitation_RequestingMedicalSpecialtyID] DEFAULT ((0)) NOT NULL,
    [AppointmentRequestedLocationID]  INT            NOT NULL,
    [AppointmentPriority]             SMALLINT       NOT NULL,
    [CitationExplanation]             NVARCHAR (MAX) NULL,
    [RequestDate]                     DATETIME       NULL,
    [RealizeFrom]                     DATETIME       NULL,
    [ConfirmedDate]                   DATETIME       NULL,
    [RegistrationDate]                DATETIME       NOT NULL,
    [PrecautionComments]              NVARCHAR (MAX) NULL,
    [ContraindicationComments]        NVARCHAR (MAX) NULL,
    [CustomerOrderRequestID]          INT            CONSTRAINT [DF_CustomerCitation_CustomerOrderRequestID] DEFAULT ((0)) NOT NULL,
    [Status]                          SMALLINT       NOT NULL,
    [LastUpdated]                     DATETIME       NOT NULL,
    [ModifiedBy]                      NVARCHAR (256) NOT NULL,
    [DBTimeStamp]                     TIMESTAMP      NOT NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_CustomerCitation]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_CustomerCitation] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[CustomerCitation])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CustomerCitation] ON;
        INSERT INTO [dbo].[tmp_ms_xx_CustomerCitation] ([ID], [CustomerID], [ProcessChartID], [CustomerProcessID], [RequestingAssistanceServiceID], [ReceptionAssistanceServiceID], [InsurerID], [PolicyTypeID], [CareCenterID], [AppointmentRequestedPhysicianID], [RequestingMedicalSpecialtyID], [AppointmentRequestedLocationID], [AppointmentPriority], [CitationExplanation], [RequestDate], [RealizeFrom], [ConfirmedDate], [RegistrationDate], [PrecautionComments], [ContraindicationComments], [Status], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [CustomerID],
                 [ProcessChartID],
                 [CustomerProcessID],
                 [RequestingAssistanceServiceID],
                 [ReceptionAssistanceServiceID],
                 [InsurerID],
                 [PolicyTypeID],
                 [CareCenterID],
                 [AppointmentRequestedPhysicianID],
                 [RequestingMedicalSpecialtyID],
                 [AppointmentRequestedLocationID],
                 [AppointmentPriority],
                 [CitationExplanation],
                 [RequestDate],
                 [RealizeFrom],
                 [ConfirmedDate],
                 [RegistrationDate],
                 [PrecautionComments],
                 [ContraindicationComments],
                 [Status],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[CustomerCitation]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CustomerCitation] OFF;
    END

DROP TABLE [dbo].[CustomerCitation];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_CustomerCitation]', N'CustomerCitation';
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_CustomerCitation]', N'PK_CustomerCitation', N'OBJECT';

COMMIT TRANSACTION
PRINT 'COMMIT TRANSACTION' 
