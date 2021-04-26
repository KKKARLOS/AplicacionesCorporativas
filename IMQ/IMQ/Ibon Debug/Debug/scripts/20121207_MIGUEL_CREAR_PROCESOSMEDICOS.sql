IF NOT(EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES AS c1 
		WHERE c1.table_name = 'CustomerMedProcess'))
BEGIN
	--/////////////////////////////////////////////////////////////////
	-- CREAR Table [CustomerMedProcess]
	--/////////////////////////////////////////////////////////////////
	CREATE TABLE [CustomerMedProcess](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[MedEpisodeProcessChartID] [int] NOT NULL,
	[OpeningProcessPhysicianID] [int] NOT NULL,
	[ClosingProcessPhysicianID] [int] NULL,
	[RegistrationDateTime] [datetime] NOT NULL,
	[OpenDateTime] [datetime] NOT NULL,
	[CloseDateTime] [datetime] NULL,
	[CustomerMedProcessChangeReasonID] [int] NULL,
	[Status] [smallint] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
	CONSTRAINT [PK_CustomerMedProcess] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	ALTER TABLE [dbo].[CustomerMedProcess] ADD  CONSTRAINT [DF_CustomerMedProcess_ClosingProcessPhysicianID]  DEFAULT ((0)) FOR [ClosingProcessPhysicianID]
	ALTER TABLE [dbo].[CustomerMedProcess] ADD  CONSTRAINT [DF_CustomerMedProcess_CustomerMedProcessChangeReason]  DEFAULT ((0)) FOR [CustomerMedProcessChangeReasonID]
END
GO

--/////////////////////////////////////////////////////////////////
-- Alter Table [CustomerMedEpisodeAct]SOLO SIN FALTAN COLUMNAS 
--/////////////////////////////////////////////////////////////////
IF NOT(EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS AS c1 
		WHERE c1.column_name = 'MedEpSpecialtyID' 
		AND c1.table_name = 'CustomerMedEpisodeAct'))
BEGIN
	ALTER TABLE CustomerMedEpisodeAct ADD [MedEpSpecialtyID] INT NOT NULL DEFAULT 0
END
GO
IF NOT(EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS AS c1 
		WHERE c1.column_name = 'WorkGroupID' 
		AND c1.table_name = 'CustomerMedEpisodeAct'))
BEGIN
	ALTER TABLE CustomerMedEpisodeAct ADD [WorkGroupID] INT NOT NULL DEFAULT 0
END
GO
--/////////////////////////////////////////////////////////////////
 --Alter Table [CustomerMedEpisodeAct]SOLO SIN FALTAN COLUMNAS 
--/////////////////////////////////////////////////////////////////
IF NOT(EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS AS c1 
		WHERE c1.column_name = 'CustomerMedProcessID' 
		AND c1.table_name = 'MedicalEpisode'))
BEGIN
	ALTER TABLE [MedicalEpisode] ADD [CustomerMedProcessID] INT NOT NULL DEFAULT 0
END
GO
IF NOT(EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS AS c1 
		WHERE c1.column_name = 'MedEpSpecialtyID' 
		AND c1.table_name = 'MedicalEpisode'))
BEGIN
	ALTER TABLE [MedicalEpisode] ADD [MedEpSpecialtyID] INT NOT NULL DEFAULT 0
END
GO
IF NOT(EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS AS c1 
		WHERE c1.column_name = 'WorkGroupID' 
		AND c1.table_name = 'MedicalEpisode'))
BEGIN
	ALTER TABLE [MedicalEpisode] ADD [WorkGroupID] INT NOT NULL DEFAULT 0
END
GO

--/////////////////////////////////////////////////////////////////
-- ciclo de creacion de registros en CustomerMedProcess
--/////////////////////////////////////////////////////////////////
DECLARE @MedEpID INT
DECLARE @Old_MedEpID INT
DECLARE @LastMepEpID INT
DECLARE @CustomerMedProcessID INT
DECLARE @CloseProcess BIT
DECLARE @TransferVisit INT
DECLARE @ClosedStatus INT
DECLARE @CancelledStatus INT
DECLARE @ClosingProcessPhysicianID INT
DECLARE @CloseDateTime DATETIME
SET @TransferVisit = 16
SET @ClosedStatus = 4
SET @CancelledStatus = 2
DECLARE @MedEpisode TABLE(MedicalEpisodeID INT, MedEpisodeNumber nvarchar(50))
SET @MedEpID = 0
SET @Old_MedEpID = -1
WHILE (@MedEpID > @Old_MedEpID)
BEGIN
	SET @Old_MedEpID = @MedEpID
	SET @LastMepEpID = 0
	SET @MedEpID = ISNULL((SELECT TOP 1 [ID] FROM MedicalEpisode 
		WHERE [ID]> @Old_MedEpID AND PreviousMedEpisodeID = 0
		ORDER BY [ID]),0)
		DELETE @MedEpisode
		
		;WITH RecursiveMedEp (MedicalEpisodeID,PreviousMedEpisodeID, MedEpisodeProcessChartID, CustomerID, MedEpisodeNumber) AS
		(
			SELECT ME1.[ID] MedicalEpisodeID, ME1.PreviousMedEpisodeID, 
				ME1.MedEpisodeProcessChartID, ME1.CustomerID, ME1.MedEpisodeNumber
			FROM MedicalEpisode ME1
			WHERE ME1.ID=@MedEpID
			UNION ALL
			SELECT ME2.[ID] MedicalEpisodeID, ME2.PreviousMedEpisodeID, 
				ME2.MedEpisodeProcessChartID, ME2.CustomerID, ME2.MedEpisodeNumber
			FROM MedicalEpisode ME2
			INNER JOIN RecursiveMedEp RE ON ME2.PreviousMedEpisodeID = RE.MedicalEpisodeID 
			AND ME2.MedEpisodeProcessChartID = RE.MedEpisodeProcessChartID AND ME2.CustomerID = RE.CustomerID
		)
	INSERT INTO @MedEpisode (MedicalEpisodeID,MedEpisodeNumber)
	SELECT MedicalEpisodeID, MedEpisodeNumber FROM RecursiveMedEp
	
	SELECT TOP 1 @LastMepEpID=MedicalEpisodeID FROM @MedEpisode ORDER BY MedEpisodeNumber DESC 
	
	IF (@MedEpID > @Old_MedEpID) AND NOT(@MedEpID = 0) AND NOT(@LastMepEpID = 0)
	BEGIN
		INSERT INTO [CustomerMedProcess] ([CustomerID],[MedEpisodeProcessChartID],[OpeningProcessPhysicianID],
			[ClosingProcessPhysicianID],[RegistrationDateTime],[OpenDateTime],[CloseDateTime],
			[CustomerMedProcessChangeReasonID],[Status],[ModifiedBy],[LastUpdated])
		SELECT CustomerID,MedEpisodeProcessChartID,PhysicianID,0,
			ISNULL(OpenDateTime,LastUpdated),ISNULL(OpenDateTime,LastUpdated), NULL,0,1,ModifiedBy,LastUpdated
		FROM MedicalEpisode 
		WHERE [ID]=@MedEpID
		SET @CustomerMedProcessID = (SELECT @@Identity) 
		
		UPDATE MedicalEpisode SET CustomerMedProcessID=@CustomerMedProcessID
		WHERE [ID] IN (SELECT MedicalEpisodeID FROM @MedEpisode)
		
		IF EXISTS(SELECT DISTINCT ME.[ID]
			FROM MedicalEpisode ME
			JOIN MedEpisodeType MET ON ME.MedEpisodeTypeID = MET.[ID]
			WHERE ME.ID IN (SELECT MedicalEpisodeID FROM @MedEpisode) 
			AND NOT(MET.OnCreateEpisode = @TransferVisit) 
			AND EXISTS(SELECT ME1.[ID]
			FROM MedicalEpisode ME1
			JOIN BasicStepsInMedEpProcess BSMEP ON ME1.MedEpisodeTypeID = BSMEP.MedEpisodeTypeID
			JOIN MedEpisodeProcessChart MEPC ON ME1.MedEpisodeProcessChartID = MEPC.[ID]
			WHERE ME.MedEpisodeProcessChartID = MEPC.[ID] 
			AND ME1.[Status] IN (@ClosedStatus, @CancelledStatus)
			AND BSMEP.[ID] = (SELECT TOP 1 BSMEP1.[ID] FROM BasicStepsInMedEpProcess BSMEP1
			JOIN MedEpisodeType MET1 ON BSMEP1.MedEpisodeTypeID = MET1.[ID]
			WHERE BSMEP1.MedEpProcessChartID = MEPC.[ID] 
			AND NOT(MET1.OnCreateEpisode = @TransferVisit) 
			ORDER BY BSMEP1.Position DESC)
			AND ME1.ID IN (SELECT MedicalEpisodeID FROM @MedEpisode)))
			SET @CloseProcess = 1
		ELSE	
			SET @CloseProcess = 0
		IF (@CloseProcess = 1)
		BEGIN
			SELECT TOP 1 @CloseDateTime = CloseDateTime,
			@ClosingProcessPhysicianID = PhysicianID
			FROM MedicalEpisode WHERE ID = @LastMepEpID
			UPDATE CustomerMedProcess SET [ClosingProcessPhysicianID] = @ClosingProcessPhysicianID,
				[CloseDateTime] = @CloseDateTime, [Status]= @ClosedStatus WHERE ID=@CustomerMedProcessID
		END	

	END
END
GO
--/////////////////////////////////////////////////////////////////
-- fin ciclo de creacion de registros en CustomerMedProcess
--/////////////////////////////////////////////////////////////////


--////////////////////////////////////////////////////////////////////////////////////////
-- ciclo de asociación de especialidad y grupo de trabajo a los episodio y visitas médicas
--////////////////////////////////////////////////////////////////////////////////////////
DECLARE @MedEpID INT
DECLARE @Old_MedEpID INT
DECLARE @CMEAID INT
DECLARE @Old_CMEAID INT
DECLARE @SpecialtyID INT
DECLARE @WorkGroupID INT
DECLARE @NumPhysicians INT
SET @MedEpID = 0
SET @Old_MedEpID = -1
WHILE (@MedEpID > @Old_MedEpID)
BEGIN
	SET @Old_MedEpID = @MedEpID
	SET @MedEpID = ISNULL((SELECT TOP 1 [ID] FROM MedicalEpisode 
		WHERE [ID]> @Old_MedEpID ORDER BY [ID]),0)
	IF (@MedEpID > @Old_MedEpID) AND NOT(@MedEpID = 0)
	BEGIN
		SET @NumPhysicians = ISNULL((SELECT COUNT(DISTINCT PhysicianID) FROM CustomerMedEpisodeAct WHERE MedicalEpisodeID = @MedEpID),0)
		IF (@NumPhysicians = 1)
		BEGIN
			SET @SpecialtyID = ISNULL((SELECT TOP 1  PMSR.MedicalSpecialtyID
						FROM PhysicianMedicalSpecialtyRel PMSR
						JOIN CustomerMedEpisodeAct CMEA ON PMSR.PhysicianID = CMEA.PhysicianID
						WHERE CMEA.MedicalEpisodeID = @MedEpID AND PMSR.IsDefault=1),0)
			SET @WorkGroupID = 0
		END
		IF (@NumPhysicians > 1)
		BEGIN
			SET @SpecialtyID = 0
			SET @WorkGroupID = ISNULL((SELECT TOP 1 PWGR.WorkGroupID
						FROM PhysicianWorkGroupRel PWGR
						JOIN MedicalEpisode ME ON PWGR.PhysicianID = ME.PhysicianID
						WHERE ME.ID = @MedEpID),0)
		END
		IF (@NumPhysicians = 0)
		BEGIN
			SET @SpecialtyID = ISNULL((SELECT TOP 1  PMSR.MedicalSpecialtyID
						FROM PhysicianMedicalSpecialtyRel PMSR
						JOIN MedicalEpisode ME ON PMSR.PhysicianID = ME.PhysicianID
						WHERE ME.ID = @MedEpID AND PMSR.IsDefault=1),0)
			SET @WorkGroupID = ISNULL((SELECT TOP 1 PWGR.WorkGroupID
						FROM PhysicianWorkGroupRel PWGR
						JOIN MedicalEpisode ME ON PWGR.PhysicianID = ME.PhysicianID
						WHERE ME.ID = @MedEpID AND PWGR.IsDefault=1),0)
		END
		UPDATE MedicalEpisode 
			SET MedEpSpecialtyID = @SpecialtyID,WorkGroupID = @WorkGroupID 
			WHERE ID=@MedEpID
		SET @CMEAID = 0
		SET @Old_CMEAID = -1
		WHILE (@CMEAID > @Old_CMEAID)
		BEGIN
			SET @Old_CMEAID = @CMEAID
			SET @CMEAID = ISNULL((SELECT TOP 1 [ID] FROM CustomerMedEpisodeAct 
				WHERE [ID]> @Old_CMEAID AND MedicalEpisodeID = @MedEpID ORDER BY [ID]),0)
			IF (@CMEAID > @Old_CMEAID) AND NOT(@CMEAID = 0)
			BEGIN
				SET @SpecialtyID = ISNULL((SELECT TOP 1 PMSR.MedicalSpecialtyID
						FROM PhysicianMedicalSpecialtyRel PMSR
						JOIN CustomerMedEpisodeAct CMEA ON PMSR.PhysicianID = CMEA.PhysicianID
						WHERE CMEA.ID = @CMEAID AND PMSR.IsDefault=1),0)
				UPDATE CustomerMedEpisodeAct
					SET MedEpSpecialtyID = @SpecialtyID, WorkGroupID=@WorkGroupID
					WHERE ID = @CMEAID
			END		
		END
	END
END
GO
--/////////////////////////////////////////////////////////////////////////////////////////////
-- fin ciclo de asociación de especialidad y grupo de trabajo a los episodio y visitas médicas
--/////////////////////////////////////////////////////////////////////////////////////////////
