use [DBName]
-- ///////////////////////////////////////////////////////////////////////
-- EL OBJETIVO ES CREAR EL REGISTRO EN LA TABLA CustomerEpisodeServiceRel 
-- CUANDO EL EPSIDIO HA SIDO DATO DE ATLA
-- EL SEGUNDO OBJETIVO ES INCLUIR EN EL REGISTRO CustomerEpisodeServiceRel 
-- DE ADMISIÓN EL PHYSICIANID SI NO SE HA INCLUIDO
-- ///////////////////////////////////////////////////////////////////////
DECLARE @EpisodeLeaveServices TABLE (ID INT IDENTITY,
	CustomerEpisodeID int,
	AdmiServiceID int,
	AdmPhysicianID int)

DECLARE @ID INT
DECLARE @OLDID INT
DECLARE @CustomerEpisodeID INT
DECLARE @AdmiServiceID INT
DECLARE @AdmPhysicianID INT
DECLARE @StartAt DATETIME
DECLARE @EndingTo DATETIME
DECLARE @LeaveStep BIGINT
DECLARE @AdmStep BIGINT
DECLARE @RcpStep BIGINT
SET @ID = 0
SET @OLDID = -1
SET @LeaveStep = 16384
SET @AdmStep = 2048
SET @RcpStep = 256
-- ///////////////////////////////////////////////////////////////////////
-- NO SE UTILIZAN CURSORES POR COMPATIBILIDAD
-- ///////////////////////////////////////////////////////////////////////

-- ///////////////////////////////////////////////////////////////////////
-- VERIFICAR QUE CustomerEpisodeServiceRel PARA LA ADM TIENE EL PHYSICIAN DE ADM
-- ///////////////////////////////////////////////////////////////////////
SET @ID = 0
SET @OLDID = -1
WHILE @OLDID < @ID
BEGIN
	SET @OLDID = @ID
	SET @ID = ISNULL((SELECT TOP 1 CE.[ID] FROM CustomerEpisode CE
							JOIN CustomerEpisodeServiceRel CESR ON CESR.CustomerEpisodeID=CE.[ID]
							WHERE (CESR.PhysicianID <=0) AND
								(CESR.Step = @AdmStep OR CESR.Step = @RcpStep)
						AND NOT(CE.PhysicianID IS NULL) AND CE.PhysicianID > 0
					    AND CE.ID > @OLDID
						ORDER BY CE.ID),0)
	IF NOT(@ID=0) AND (@OLDID < @ID)
	BEGIN
		SET @AdmPhysicianID = ISNULL((SELECT TOP 1 PhysicianID FROM CustomerEpisode WHERE ID=@ID),0)
		IF NOT(@AdmPhysicianID =0)
		BEGIN
			UPDATE CustomerEpisodeServiceRel SET PhysicianID = @AdmPhysicianID
			WHERE (PhysicianID = 0 OR PhysicianID IS NULL) AND  CustomerEpisodeID = @ID
		END
	END						
END
-- ///////////////////////////////////////////////////////////////////////
-- CREAR EL CustomerEpisodeServiceRel PARA EL ALTA
-- ///////////////////////////////////////////////////////////////////////
SET @ID = 0
SET @OLDID = -1
WHILE @OLDID < @ID
BEGIN
	SET @OLDID = @ID
	SET @ID = ISNULL((SELECT TOP 1 CE.[ID] FROM CustomerEpisode CE
						JOIN CustomerLeave CL ON CE.[ID]=CL.EpisodeID
						WHERE NOT(EXISTS(SELECT CESR.[ID] 
							FROM CustomerEpisodeServiceRel CESR 
							WHERE CESR.CustomerEpisodeID=CE.[ID] AND
								CESR.Step = @LeaveStep))
					    AND CE.ID > @OLDID
						ORDER BY CE.ID),0)
	IF NOT(@ID=0) AND (@OLDID < @ID)
	BEGIN
		SET @AdmiServiceID = 0
		SET @AdmPhysicianID = 0
		SELECT TOP 1 @AdmiServiceID = CESR.AssistanceServiceID,
			@AdmPhysicianID = CESR.PhysicianID,
			@StartAt = CE.StartDateTime,
			@EndingTo = CE.EndDateTime
		FROM CustomerEpisodeServiceRel CESR 
		JOIN CustomerEpisode CE ON CESR.CustomerEpisodeID = CE.ID
		WHERE CE.ID = @ID AND 
		 (CESR.Step = @AdmStep OR CESR.Step = @RcpStep)
		IF NOT(@AdmiServiceID = 0)
		BEGIN
			INSERT INTO [CustomerEpisodeServiceRel]
					([CustomerEpisodeID],[AssistanceServiceID],[UnitServiceID],[Step],
					[StartAt],[EndingTo],[PhysicianID],[ModifiedBy],[LastUpdated])
			VALUES (@ID,@AdmiServiceID, 0, @LeaveStep, @StartAt, @EndingTo, @AdmPhysicianID,
					'ADMINISTRADOR', GETDATE())
		END
	END
END
