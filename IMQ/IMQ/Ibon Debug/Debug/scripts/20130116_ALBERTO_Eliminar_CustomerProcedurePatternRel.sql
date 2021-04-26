
PRINT N'Dropping FK_CustomerProcedurePatternRel_CustomerProcedure...';
GO
ALTER TABLE [dbo].[CustomerProcedurePatternRel] DROP CONSTRAINT [FK_CustomerProcedurePatternRel_CustomerProcedure];
GO

PRINT N'Dropping FK_CustomerProcedureRoutineRel_CustomerProcedure...';
GO
ALTER TABLE [dbo].[CustomerProcedureRoutineRel] DROP CONSTRAINT [FK_CustomerProcedureRoutineRel_CustomerProcedure];
GO

PRINT N'Dropping FK_CustomerProcedure_Procedure...';
GO
ALTER TABLE [dbo].[CustomerProcedure] DROP CONSTRAINT [FK_CustomerProcedure_Procedure];
GO

PRINT N'Dropping FK_CustomerProcedureTime_CustomerProcedurePatternRel...';
GO
ALTER TABLE [dbo].[CustomerProcedureTime] DROP CONSTRAINT [FK_CustomerProcedureTime_CustomerProcedurePatternRel];
GO

PRINT N'Starting rebuilding table [dbo].[CustomerProcedure]...';
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
SET XACT_ABORT ON;
BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_CustomerProcedure] (
    [ID]                       INT            IDENTITY (1, 1) NOT NULL,
    [AssistancePlanID]         INT            NOT NULL,
    [CustomerID]               INT            NOT NULL,
    [EpisodeID]                INT            NULL,
    [ProcedureID]              INT            NULL,
    [Precautions]              NVARCHAR (MAX) NULL,
    [NoticeRequires]           BIT            NULL,
    [NoticePreviousTimeID]     INT            NULL,
    [NoticeMessage]            NVARCHAR (MAX) NULL,
    [StartAt]                  DATETIME       NULL,
    [LatestSchedule]           DATETIME       NULL,
    [EndingTo]                 DATETIME       NULL,
    [AvailPattern]             SMALLINT       NULL,
    [EstimatedDurationID]      INT            NULL,
    [FrequencyOfApplicationID] INT            NULL,
    [LocationID]               INT            NULL,
    [Meaning]                  NVARCHAR (MAX) NULL,
    [Status]                   SMALLINT       NULL,
    [LastUpdated]              DATETIME       NULL,
    [ModifiedBy]               NVARCHAR (256) NULL,
    [DBTimeStamp]              TIMESTAMP      NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_CustomerProcedure]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_CustomerProcedure] 
		PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[CustomerProcedure])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CustomerProcedure] ON;
        INSERT INTO [dbo].[tmp_ms_xx_CustomerProcedure] ([ID], [AssistancePlanID], [CustomerID], [EpisodeID], [ProcedureID], [Precautions], 
			[NoticeRequires], [NoticePreviousTimeID], [NoticeMessage], [StartAt], [LatestSchedule], [EndingTo], [AvailPattern], [EstimatedDurationID], 
			[FrequencyOfApplicationID], [LocationID], [Meaning], [Status], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [AssistancePlanID],
                 [CustomerID],
                 [EpisodeID],
                 [ProcedureID],
                 [Precautions],
                 [NoticeRequires],
                 [NoticePreviousTimeID],
                 [NoticeMessage],
                 [StartAt],
                 [LatestSchedule],
                 [EndingTo],
                 [AvailPattern],
                 [EstimatedDurationID],
                 ISNULL((SELECT TOP 1 CPPR.TimePatternID FROM CustomerProcedurePatternRel CPPR WHERE CPPR.CustomerProcedureID=[CustomerProcedure].[ID]), 0),
                 0,
                 '',
                 [Status],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[CustomerProcedure]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CustomerProcedure] OFF;
    END

DROP TABLE [dbo].[CustomerProcedure];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_CustomerProcedure]', N'CustomerProcedure';
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_CustomerProcedure]', N'PK_CustomerProcedure', N'OBJECT';

COMMIT TRANSACTION;
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

GO
PRINT N'Creating [dbo].[CustomerProcedure].[IX_CustomerProcedure_EpisodeID]...';
GO
CREATE NONCLUSTERED INDEX [IX_CustomerProcedure_EpisodeID]
    ON [dbo].[CustomerProcedure]([EpisodeID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];

GO
PRINT N'Starting rebuilding table [dbo].[CustomerProcedureTime]...';
GO
/*
The column [dbo].[CustomerProcedureTime].[CustomerProcedurePatternRelID] is being dropped, data loss could occur.
The column [dbo].[CustomerProcedureTime].[CustomerProcedureID] on table [dbo].[CustomerProcedureTime] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue, you must add a default value to the column or mark it as allowing NULL values.
*/
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
SET XACT_ABORT ON;
BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_CustomerProcedureTime] (
    [ID]                  INT            IDENTITY (1, 1) NOT NULL,
    [CustomerProcedureID] INT            NOT NULL,
    [Time]                DATETIME       NULL,
    [LastUpdated]         DATETIME       NULL,
    [ModifiedBy]          NVARCHAR (256) NULL,
    [DBTimeStamp]         TIMESTAMP      NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_CustomerProcedureTime]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_CustomerProcedureTime] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[CustomerProcedureTime])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CustomerProcedureTime] ON;
        INSERT INTO [dbo].[tmp_ms_xx_CustomerProcedureTime] ([ID], [CustomerProcedureID], [Time], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
				 ISNULL((SELECT TOP 1 CPPR.CustomerProcedureID FROM CustomerProcedurePatternRel CPPR WHERE CPPR.[ID]=[CustomerProcedureTime].[CustomerProcedurePatternRelID] ), 0),
                 [Time],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[CustomerProcedureTime]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CustomerProcedureTime] OFF;
    END

DROP TABLE [dbo].[CustomerProcedureTime];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_CustomerProcedureTime]', N'CustomerProcedureTime';
EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_CustomerProcedureTime]', N'PK_CustomerProcedureTime', N'OBJECT';

COMMIT TRANSACTION;
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

GO
PRINT N'Creating FK_CustomerProcedureRoutineRel_CustomerProcedure...';
GO
ALTER TABLE [dbo].[CustomerProcedureRoutineRel] WITH NOCHECK
    ADD CONSTRAINT [FK_CustomerProcedureRoutineRel_CustomerProcedure] FOREIGN KEY ([CustomerProcedureID]) REFERENCES [dbo].[CustomerProcedure] ([ID]) ON DELETE CASCADE ON UPDATE NO ACTION;

GO
PRINT N'Creating FK_CustomerProcedure_Procedure...';
GO
ALTER TABLE [dbo].[CustomerProcedure] WITH NOCHECK
    ADD CONSTRAINT [FK_CustomerProcedure_Procedure] FOREIGN KEY ([ProcedureID]) REFERENCES [dbo].[Procedure] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

GO
PRINT N'Checking existing data against newly created constraints';
GO
ALTER TABLE [dbo].[CustomerProcedureRoutineRel] WITH CHECK CHECK CONSTRAINT [FK_CustomerProcedureRoutineRel_CustomerProcedure];
ALTER TABLE [dbo].[CustomerProcedure] WITH CHECK CHECK CONSTRAINT [FK_CustomerProcedure_Procedure];
GO

PRINT N'Dropping [dbo].[CustomerProcedurePatternRel]...';
GO
DROP TABLE [dbo].[CustomerProcedurePatternRel];
GO
