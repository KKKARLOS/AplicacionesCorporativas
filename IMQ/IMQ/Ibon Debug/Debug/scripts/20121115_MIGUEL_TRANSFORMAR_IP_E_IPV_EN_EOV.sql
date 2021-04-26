--//////////////////////////////////////////////////////////
-- el script sólo se ejecuta si existe la columna value en la tabla InterpretationValue
--//////////////////////////////////////////////////////////
IF (EXISTS(SELECT * FROM sys.columns 
		WHERE Name = N'Value' 
		AND Object_ID = Object_ID(N'InterpretationValue')))
BEGIN
	--/////////////////////////////////////////////////////////////////
	-- Alter Table SOLO SIN FALTAN COLUMNAS
	--/////////////////////////////////////////////////////////////////
	IF NOT(EXISTS(SELECT * FROM sys.columns  
			WHERE Name = N'Order' 
			AND Object_ID = Object_ID(N'InterpretationParamConfig')))
	BEGIN
		ALTER TABLE InterpretationParamConfig ADD [Order] INT NOT NULL DEFAULT 0
	END
	IF NOT(EXISTS(SELECT * FROM sys.columns  
			WHERE Name = N'Order' 
			AND Object_ID = Object_ID(N'InterpretationValue')))
	BEGIN
		ALTER TABLE InterpretationValue ADD [Order] INT NOT NULL DEFAULT 0
	END
	IF NOT(EXISTS(SELECT * FROM sys.columns  
			WHERE Name = N'ObservationValueID' 
			AND Object_ID = Object_ID(N'InterpretationValue')))
	BEGIN
		ALTER TABLE InterpretationValue ADD ObservationValueID INT NOT NULL DEFAULT 0
	END
	IF NOT(EXISTS(SELECT * FROM sys.columns  
			WHERE Name = N'ExtObservationValueID' 
			AND Object_ID = Object_ID(N'InterpretationValue')))
	BEGIN
		ALTER TABLE InterpretationValue ADD ExtObservationValueID INT NOT NULL DEFAULT 0
	END
END

GO

IF (EXISTS(SELECT * FROM sys.columns 
		WHERE Name = N'Value' 
		AND Object_ID = Object_ID(N'InterpretationValue')))
BEGIN
	DECLARE @ObsID INT
	DECLARE @Old_ObsID INT

	DECLARE @IPID INT
	DECLARE @Old_IPID INT

	DECLARE @IPVID INT
	DECLARE @Old_IPVID INT

	DECLARE @ObsValueID INT
	DECLARE @ExtObsValueID INT

	DECLARE @ObsIsString SMALLINT
	DECLARE @ObsIsRichText SMALLINT
	SET @ObsIsString = 8
	SET @ObsIsRichText = 256

	DECLARE @IPOrder INT
	DECLARE @IPVOrder INT
	DECLARE @stVALUE NVARCHAR(MAX)
	DECLARE @imgVALUE VARBINARY(MAX)
	DECLARE @IsRichText INT
	--/////////////////////////////////////////////////////////////////
	--// ciclo de observaciones con IP
	--/////////////////////////////////////////////////////////////////
	SET @ObsID = 0
	SET @Old_ObsID = -1
	WHILE (@ObsID > @Old_ObsID)
	BEGIN
		SET @Old_ObsID = @ObsID
		SET @ObsID = ISNULL((SELECT TOP 1 O.[ID] FROM Observation O
							JOIN InterpretationParamConfig IPC ON O.[ID] = IPC.ObservationID
							WHERE O.[ID]> @Old_ObsID AND O.KindOf=1 AND
							 (O.BasicType=@ObsIsString OR O.BasicType=@ObsIsRichText) 
							ORDER BY O.[ID]),0)
		IF (@ObsID > @Old_ObsID) AND (@ObsID > 0)
		BEGIN
			SET @IsRichText = ISNULL((SELECT TOP 1 [ID] FROM Observation
							WHERE [ID]=@ObsID AND BasicType=@ObsIsRichText),0)
			--/////////////////////////////////////////////////////////////////
			-- ciclo de InterpretationParamConfig
			--/////////////////////////////////////////////////////////////////
			SET @IPOrder=1
			SET @IPID=0
			SET @Old_IPID=-1
			WHILE (@IPID > @Old_IPID)
			BEGIN
				SET @Old_IPID = @IPID
				SET @IPID = ISNULL((SELECT TOP 1 IPC.[ID] FROM InterpretationParamConfig IPC
									WHERE IPC.[ID]> @Old_IPID AND IPC.ObservationID=@ObsID
									ORDER BY IPC.[ID]),0)
				IF (@IPID > @Old_IPID) AND (@IPID > 0)
				BEGIN
					UPDATE InterpretationParamConfig SET [Order]=@IPOrder WHERE [ID]=@IPID
					SET @IPOrder=@IPOrder+1
					--/////////////////////////////////////////////////////////////////
					-- ciclo de InterpretationValue
					--/////////////////////////////////////////////////////////////////
					SET @IPVOrder=1
					SET @IPVID=0
					SET @Old_IPVID=-1
					WHILE (@IPVID > @Old_IPVID)
					BEGIN
						SET @Old_IPVID = @IPVID
						SET @IPVID = ISNULL((SELECT TOP 1 IPV.[ID] FROM InterpretationValue IPV
											WHERE IPV.[ID]> @Old_IPVID 
											AND IPV.InterpretationParamConfigID=@IPID
											ORDER BY IPV.[ID]),0)
						IF (@IPVID > @Old_IPVID) AND (@IPVID > 0)
						BEGIN
							IF (@IsRichText > 0)
							BEGIN
								SET @imgVALUE = CAST((SELECT TOP 1 CAST([Value] AS VARCHAR(MAX))
									FROM InterpretationValue WHERE [ID]=@IPVID) AS VARBINARY(MAX))
								INSERT INTO [ExtObservationValue]
									([Value],[ModifiedBy],[LastUpdated])
								VALUES (@imgVALUE,'Administrador',GETDATE())
								SET @ExtObsValueID = (SELECT @@Identity)
								SET @ObsValueID=0
							END	
							ELSE	
							BEGIN
								SET @stVALUE = (SELECT TOP 1 [Value]
									FROM InterpretationValue WHERE [ID]=@IPVID)
								INSERT INTO [ObservationValue]
									([DTValue],[BoolValue],[IntValue],[DecValue],
									 [DbValue],[StValue],[ModifiedBy],[LastUpdated])
								VALUES (NULL, NULL, NULL, NULL, NULL, @stVALUE,'Administrador',GETDATE())
								SET @ObsValueID=(SELECT @@Identity)
								SET @ExtObsValueID = 0
							END	
 							UPDATE InterpretationValue
								SET [Order]=@IPVOrder,ObservationValueID=@ObsValueID,
									ExtObservationValueID=@ExtObsValueID
								WHERE [ID]=@IPVID
							SET @IPVOrder=@IPVOrder+1
						END
					END
					--/////////////////////////////////////////////////////////////////
					-- FIN ciclo de InterpretationValue
					--/////////////////////////////////////////////////////////////////
				END
			END
			--/////////////////////////////////////////////////////////////////
			-- FIN ciclo de InterpretationParamConfig
			--/////////////////////////////////////////////////////////////////
		END
	END
	--/////////////////////////////////////////////////////////////////
	-- fin ciclo de observaciones con IP
	--/////////////////////////////////////////////////////////////////

	--/////////////////////////////////////////////////////////////////
	-- Alter Table QUITAR LA COLUMNA VALUE DE LA TABLA InterpretationValue
	--/////////////////////////////////////////////////////////////////
	IF (EXISTS(SELECT * FROM sys.columns 
		WHERE Name = N'Value' 
		AND Object_ID = Object_ID(N'InterpretationValue')))
	BEGIN
		ALTER TABLE InterpretationValue DROP COLUMN [Value]
	END
END