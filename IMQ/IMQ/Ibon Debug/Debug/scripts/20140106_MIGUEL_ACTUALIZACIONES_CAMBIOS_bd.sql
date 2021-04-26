USE HCDISTesting
GO
PRINT N'Dropping DF_CustomerProcess_CustomerEpisodeID...';


GO
ALTER TABLE [dbo].[CustomerProcess] DROP CONSTRAINT [DF_CustomerProcess_CustomerEpisodeID];


GO
PRINT N'Dropping DF_Person_ImageID...';


GO
ALTER TABLE [dbo].[Person] DROP CONSTRAINT [DF_Person_ImageID];


GO
PRINT N'Dropping DF_SensitiveData_BirthLocation...';


GO
ALTER TABLE [dbo].[SensitiveData] DROP CONSTRAINT [DF_SensitiveData_BirthLocation];


GO
PRINT N'Dropping DF_SensitiveData_Citizenship...';


GO
ALTER TABLE [dbo].[SensitiveData] DROP CONSTRAINT [DF_SensitiveData_Citizenship];


GO
PRINT N'Dropping DF_SensitiveData_CitizenshipComments...';


GO
ALTER TABLE [dbo].[SensitiveData] DROP CONSTRAINT [DF_SensitiveData_CitizenshipComments];


GO
PRINT N'Starting rebuilding table [dbo].[CustomerAssistancePlan]...';


GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_CustomerAssistancePlan] (
    [ID]                         INT            IDENTITY (1, 1) NOT NULL,
    [CustomerID]                 INT            NOT NULL,
    [AdmissionID]                INT            NULL,
    [EpisodeID]                  INT            NULL,
    [AssistanceProcessChartID]   INT            NULL,
    [Precautions]                NVARCHAR (MAX) NULL,
    [Preferences]                NVARCHAR (MAX) NULL,
    [Comments]                   NVARCHAR (MAX) NULL,
    [InitDateTime]               DATETIME       NULL,
    [EndDateTime]                DATETIME       NULL,
    [PrimaryCarePractitionerID]  INT            NULL,
    [SecondayCarePractitionerID] INT            NULL,
    [Status]                     SMALLINT       NULL,
    [LastUpdated]                DATETIME       NULL,
    [ModifiedBy]                 NVARCHAR (MAX) NULL,
    [DBTimeStamp]                TIMESTAMP      NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_CustomerAssistancePlan]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_CustomerAssistancePlan] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[CustomerAssistancePlan])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CustomerAssistancePlan] ON;
        INSERT INTO [dbo].[tmp_ms_xx_CustomerAssistancePlan] ([ID], [CustomerID], [AdmissionID], [EpisodeID], [AssistanceProcessChartID], [Precautions], [Preferences], [Comments], [InitDateTime], [EndDateTime], [Status], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [CustomerID],
                 [AdmissionID],
                 [EpisodeID],
                 [AssistanceProcessChartID],
                 [Precautions],
                 [Preferences],
                 [Comments],
                 [InitDateTime],
                 [EndDateTime],
                 [Status],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[CustomerAssistancePlan]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CustomerAssistancePlan] OFF;
    END

DROP TABLE [dbo].[CustomerAssistancePlan];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_CustomerAssistancePlan]', N'CustomerAssistancePlan';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_CustomerAssistancePlan]', N'PK_CustomerAssistancePlan', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[CustomerAssistancePlan].[IX_CustomerAssistancePlan_EpisodeID]...';


GO
CREATE NONCLUSTERED INDEX [IX_CustomerAssistancePlan_EpisodeID]
    ON [dbo].[CustomerAssistancePlan]([EpisodeID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];


GO
PRINT N'Creating [dbo].[CustomerAssistancePlan].[IX_CustomerAssistancePlan_CustomerID]...';


GO
CREATE NONCLUSTERED INDEX [IX_CustomerAssistancePlan_CustomerID]
    ON [dbo].[CustomerAssistancePlan]([CustomerID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];


GO
PRINT N'Starting rebuilding table [dbo].[CustomerProcess]...';


GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_CustomerProcess] (
    [ID]                             INT            IDENTITY (1, 1) NOT NULL,
    [ProcessChartID]                 INT            NOT NULL,
    [PersonID]                       INT            NOT NULL,
    [CustomerID]                     INT            NOT NULL,
    [CareCenterID]                   INT            NOT NULL,
    [CustomerEpisodeID]              INT            CONSTRAINT [DF_CustomerProcess_CustomerEpisodeID] DEFAULT ((0)) NOT NULL,
    [RegistrationDateTime]           DATETIME       NOT NULL,
    [CloseDateTime]                  DATETIME       NULL,
    [RelatedMedEpisodeDischarge]     BIT            NULL,
    [MedEpisodeDischargeDateTime]    DATETIME       NULL,
    [MedEpisodePhysicianDischargeID] INT            NULL,
    [AvailableProcessSteps]          BIGINT         NOT NULL,
    [Status]                         SMALLINT       NOT NULL,
    [LastUpdated]                    DATETIME       NOT NULL,
    [ModifiedBy]                     NVARCHAR (256) NOT NULL,
    [DBTimeStamp]                    TIMESTAMP      NOT NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_CustomerProcess]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_CustomerProcess] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[CustomerProcess])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CustomerProcess] ON;
        INSERT INTO [dbo].[tmp_ms_xx_CustomerProcess] ([ID], [ProcessChartID], [PersonID], [CustomerID], [CareCenterID], [CustomerEpisodeID], [RegistrationDateTime], [CloseDateTime], [AvailableProcessSteps], [Status], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [ProcessChartID],
                 [PersonID],
                 [CustomerID],
                 [CareCenterID],
                 [CustomerEpisodeID],
                 [RegistrationDateTime],
                 [CloseDateTime],
                 [AvailableProcessSteps],
                 [Status],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[CustomerProcess]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CustomerProcess] OFF;
    END

DROP TABLE [dbo].[CustomerProcess];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_CustomerProcess]', N'CustomerProcess';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_CustomerProcess]', N'PK_CustomerProcess', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[CustomerProcess].[IX_CustomerProcess_CustomerEpisodeID]...';


GO
CREATE NONCLUSTERED INDEX [IX_CustomerProcess_CustomerEpisodeID]
    ON [dbo].[CustomerProcess]([CustomerEpisodeID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];


GO
PRINT N'Creating [dbo].[CustomerProcess].[IX_CustomerProcess_PersonID]...';


GO
CREATE NONCLUSTERED INDEX [IX_CustomerProcess_PersonID]
    ON [dbo].[CustomerProcess]([PersonID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];


GO
PRINT N'Starting rebuilding table [dbo].[CustomerProcessStepsRel]...';


GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_CustomerProcessStepsRel] (
    [ID]                INT            IDENTITY (1, 1) NOT NULL,
    [CustomerProcessID] INT            NOT NULL,
    [Step]              BIGINT         NOT NULL,
    [CurrentStepID]     INT            NOT NULL,
    [StepStatus]        SMALLINT       NOT NULL,
    [StepDateTime]      DATETIME       NULL,
    [CloseDateTime]     DATETIME       NULL,
    [LastUpdated]       DATETIME       NOT NULL,
    [ModifiedBy]        NVARCHAR (256) NOT NULL,
    [DBTimeStamp]       TIMESTAMP      NOT NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_CustomerProcessStepsRel]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_CustomerProcessStepsRel] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[CustomerProcessStepsRel])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CustomerProcessStepsRel] ON;
        INSERT INTO [dbo].[tmp_ms_xx_CustomerProcessStepsRel] ([ID], [CustomerProcessID], [Step], [CurrentStepID], [StepStatus], [StepDateTime], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [CustomerProcessID],
                 [Step],
                 [CurrentStepID],
                 [StepStatus],
                 [StepDateTime],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[CustomerProcessStepsRel]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CustomerProcessStepsRel] OFF;
    END

DROP TABLE [dbo].[CustomerProcessStepsRel];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_CustomerProcessStepsRel]', N'CustomerProcessStepsRel';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_CustomerProcessStepsRel]', N'PK_CustomerProcessStepsRel', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[CustomerProcessStepsRel].[I_CPS_CustomerProcessID]...';


GO
CREATE NONCLUSTERED INDEX [I_CPS_CustomerProcessID]
    ON [dbo].[CustomerProcessStepsRel]([CustomerProcessID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];


GO
PRINT N'Creating [dbo].[CustomerProcessStepsRel].[IX_CustomerProcessStepsRel_Step]...';


GO
CREATE NONCLUSTERED INDEX [IX_CustomerProcessStepsRel_Step]
    ON [dbo].[CustomerProcessStepsRel]([Step] ASC)
    INCLUDE([CustomerProcessID], [CurrentStepID]) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];


GO
PRINT N'Altering [dbo].[InsurerInvoiceAgreement]...';


GO
ALTER TABLE [dbo].[InsurerInvoiceAgreement] ALTER COLUMN [AccountNumber] NVARCHAR (24) NULL;


GO
PRINT N'Altering [dbo].[InvoiceAgreement]...';


GO
ALTER TABLE [dbo].[InvoiceAgreement] ALTER COLUMN [AccountNumber] NVARCHAR (24) NULL;


GO
PRINT N'Starting rebuilding table [dbo].[Person]...';


GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_Person] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]          NVARCHAR (50)  NOT NULL,
    [LastName]           NVARCHAR (50)  NULL,
    [LastName2]          NVARCHAR (50)  NULL,
    [AsUser]             BIT            NOT NULL,
    [EmailAddress]       NVARCHAR (250) NULL,
    [Picture]            NVARCHAR (250) NULL,
    [AddressID]          INT            NULL,
    [Address2ID]         INT            NULL,
    [RegistrationDate]   DATETIME       NOT NULL,
    [ImageID]            INT            CONSTRAINT [DF_Person_ImageID] DEFAULT ((0)) NOT NULL,
    [HasDuplicate]       BIT            NULL,
    [RecordMerged]       INT            NULL,
    [HasMergedRegisters] BIT            NULL,
    [LastUpdated]        DATETIME       NOT NULL,
    [ModifiedBy]         NVARCHAR (256) NOT NULL,
    [Status]             SMALLINT       NOT NULL,
    [DBTimeStamp]        TIMESTAMP      NOT NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_Person]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_Person] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[Person])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Person] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Person] ([ID], [FirstName], [LastName], [LastName2], [AsUser], [EmailAddress], [Picture], [AddressID], [Address2ID], [RegistrationDate], [ImageID], [LastUpdated], [ModifiedBy], [Status])
        SELECT   [ID],
                 [FirstName],
                 [LastName],
                 [LastName2],
                 [AsUser],
                 [EmailAddress],
                 [Picture],
                 [AddressID],
                 [Address2ID],
                 [RegistrationDate],
                 [ImageID],
                 [LastUpdated],
                 [ModifiedBy],
                 [Status]
        FROM     [dbo].[Person]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Person] OFF;
    END

DROP TABLE [dbo].[Person];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Person]', N'Person';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_Person]', N'PK_Person', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Person].[IX_Person_AddressID]...';


GO
CREATE NONCLUSTERED INDEX [IX_Person_AddressID]
    ON [dbo].[Person]([AddressID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];


GO
PRINT N'Starting rebuilding table [dbo].[SensitiveData]...';


GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_SensitiveData] (
    [ID]                  INT            IDENTITY (1, 1) NOT NULL,
    [PersonID]            INT            NOT NULL,
    [BirthDate]           DATETIME       NULL,
    [EducationLevel]      SMALLINT       NULL,
    [Language]            SMALLINT       NULL,
    [MaritalStatus]       SMALLINT       NULL,
    [ReligiousPreference] SMALLINT       NULL,
    [Sex]                 SMALLINT       NULL,
    [BirthPlace]          NVARCHAR (100) CONSTRAINT [DF_SensitiveData_BirthLocation] DEFAULT ('') NOT NULL,
    [Citizenship]         NVARCHAR (100) CONSTRAINT [DF_SensitiveData_Citizenship] DEFAULT ('') NOT NULL,
    [CitizenshipComments] NVARCHAR (200) CONSTRAINT [DF_SensitiveData_CitizenshipComments] DEFAULT ('') NOT NULL,
    [DeathDateTime]       DATETIME       NULL,
    [DeathReason]         NVARCHAR (200) NULL,
    [LastUpdated]         DATETIME       NOT NULL,
    [ModifiedBy]          NVARCHAR (256) NOT NULL,
    [DBTimeStamp]         TIMESTAMP      NOT NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_SensitiveData]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_SensitiveData] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[SensitiveData])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_SensitiveData] ON;
        INSERT INTO [dbo].[tmp_ms_xx_SensitiveData] ([ID], [PersonID], [BirthDate], 
        [EducationLevel], [Language], [MaritalStatus], [ReligiousPreference], [Sex], [BirthPlace], 
        [Citizenship], [CitizenshipComments], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [PersonID],
                 [BirthDate],
                 [EducationLevel],
                 [Language],
                 [MaritalStatus],
                 [ReligiousPreference],
                 [Sex],
                 [BirthPlace],
                 [Citizenship],
                 [CitizenshipComments],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[SensitiveData]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_SensitiveData] OFF;
    END

DROP TABLE [dbo].[SensitiveData];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_SensitiveData]', N'SensitiveData';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_SensitiveData]', N'PK_SensitiveData', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[SensitiveData].[IX_SensitiveData_PersonID]...';


GO
CREATE NONCLUSTERED INDEX [IX_SensitiveData_PersonID]
    ON [dbo].[SensitiveData]([PersonID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];

--//////////////////////////////////////////////////////
--// UPDATES DE FECHAS DE FALLECIMIENTO Y RAZON
--//////////////////////////////////////////////////////
DECLARE @CID INT
DECLARE @COLDID INT
DECLARE @PersonID INT
DECLARE @DeathDateTime DATETIME
DECLARE @DeathReason NVARCHAR(200)
SET @CID = 0;
SET @COLDID = -1;
WHILE (@COLDID < @CID)
BEGIN
	SET @COLDID = @CID
	SET @CID = ISNULL((SELECT TOP 1 [ID] FROM Customer WHERE [ID] > @COLDID ORDER BY [ID]),0)
	IF (@COLDID < @CID) AND NOT(@CID = 0)
	BEGIN
		(SELECT TOP 1 @PersonID = PersonID, @DeathDateTime = DeathDateTime, @DeathReason = DeathReason
			FROM Customer WHERE [ID] = @CID)
		UPDATE SensitiveData
		SET DeathDateTime = @DeathDateTime, DeathReason = @DeathReason
		WHERE PersonID = @PersonID 
	END
END
--//////////////////////////////////////////////////////
--// FIN UPDATES DE FECHAS DE FALLECIMIENTO Y RAZON
--//////////////////////////////////////////////////////

GO
PRINT N'Creating [dbo].[CustomerConfidentialityComment]...';


GO
CREATE TABLE [dbo].[CustomerConfidentialityComment] (
    [ID]                      INT            NOT NULL,
    [CustomerID]              INT            NOT NULL,
    [Comments]                NVARCHAR (MAX) NOT NULL,
    [RegistrationDateTime]    DATETIME       NOT NULL,
    [CustomerConfidentiality] SMALLINT       NOT NULL,
    [LastUpdated]             DATETIME       NOT NULL,
    [ModifiedBy]              NVARCHAR (256) NOT NULL,
    [DBTimeStamp]             TIMESTAMP      NOT NULL
);


GO
PRINT N'Creating PK_CustomerConfidentialityComment...';


GO
ALTER TABLE [dbo].[CustomerConfidentialityComment]
    ADD CONSTRAINT [PK_CustomerConfidentialityComment] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);



GO
PRINT N'Altering [dbo].[Customer]...';


GO
ALTER TABLE [dbo].[Customer] DROP COLUMN [DeathDateTime], COLUMN [DeathReason];


GO