DECLARE @EACEID INT
DECLARE @ATTRNotasEnfermeriaID INT
DECLARE @ATTRNotasMedicasID INT
DECLARE @ATTROtrasNotasID INT

DECLARE @EACElementIndigoInteropAttributesID INT
SET @EACElementIndigoInteropAttributesID = (SELECT ID FROM [EACElement] WHERE Name = 'IndigoInteropAttributes')

DECLARE @PlantillaAlergia NVARCHAR(50)
SET @PlantillaAlergia = (SELECT [DefaultValue] FROM EACAttribute WHERE EACElementID = @EACElementIndigoInteropAttributesID AND Name = 'Alergia')
DECLARE @PlantillaAlerta NVARCHAR(50)
SET @PlantillaAlerta = (SELECT [DefaultValue] FROM EACAttribute WHERE EACElementID = @EACElementIndigoInteropAttributesID AND Name = 'Alerta')
DECLARE @PlantillaAltaMedica NVARCHAR(50)
SET @PlantillaAltaMedica = (SELECT [DefaultValue] FROM EACAttribute WHERE EACElementID = @EACElementIndigoInteropAttributesID AND Name = 'AltaMedica')
DECLARE @PlantillaConstantes NVARCHAR(50)
SET @PlantillaConstantes = (SELECT [DefaultValue] FROM EACAttribute WHERE EACElementID = @EACElementIndigoInteropAttributesID AND Name = 'Constantes')
DECLARE @PlantillaDiagnosticoMedico NVARCHAR(50)
SET @PlantillaDiagnosticoMedico = (SELECT [DefaultValue] FROM EACAttribute WHERE EACElementID = @EACElementIndigoInteropAttributesID AND Name = 'DiagnosticoMedico')
DECLARE @PlantillaEvolutivoEnfermeria NVARCHAR(50)
SET @PlantillaEvolutivoEnfermeria = (SELECT [DefaultValue] FROM EACAttribute WHERE EACElementID = @EACElementIndigoInteropAttributesID AND Name = 'EvolutivoEnfermeria')
DECLARE @PlantillaEvolutivo NVARCHAR(50)
SET @PlantillaEvolutivo = (SELECT [DefaultValue] FROM EACAttribute WHERE EACElementID = @EACElementIndigoInteropAttributesID AND Name = 'Evolutivo')
DECLARE @PlantillaConstantesTriaje NVARCHAR(50)
SET @PlantillaConstantesTriaje = (SELECT [DefaultValue] FROM EACAttribute WHERE EACElementID = @EACElementIndigoInteropAttributesID AND Name = 'ConstantesTriaje')
DECLARE @PlantillaInformeAlta NVARCHAR(50)
SET @PlantillaInformeAlta = (SELECT [DefaultValue] FROM EACAttribute WHERE EACElementID = @EACElementIndigoInteropAttributesID AND Name = 'InformeAlta')

---------------------------------------------------------------------------------------------
---- METADATO: NurseStationEpisodeNotesEditViewDTO
---------------------------------------------------------------------------------------------
SET @EACEID = (SELECT ID FROM [EACElement] WHERE Name = 'NurseStationEpisodeNotesEditViewDTO')
IF (@EACEID IS NULL) OR (@EACEID <= 0)
BEGIN
	INSERT INTO [EACElement]
			([ElementType],
			[Name],
			[ModuleName],
			[Description],
			[Status],
			[LastUpdated],
			[ModifiedBy])
	VALUES (2, 
			'NurseStationEpisodeNotesEditViewDTO',
			'',
			'La configuración de los metadatos DTO para la vista NurseStationEpisodeNotesEditView',
			2, 
			GETDATE(), 
			'Administrador')
	SET @EACEID = (SELECT @@IDENTITY)
END

IF NOT(@EACEID IS NULL) AND (@EACEID > 0)
BEGIN
	---------------------------------------------------------------------------------------------
	---- ATRIBUTO NotasEnfermeria
	---------------------------------------------------------------------------------------------
	SET @ATTRNotasEnfermeriaID = (SELECT ID FROM [EACAttribute] WHERE [EACElementID]=@EACEID AND Name = 'NotasEnfermeria')
	IF (@ATTRNotasEnfermeriaID IS NULL) OR (@ATTRNotasEnfermeriaID <= 0)
		BEGIN
			INSERT INTO [EACAttribute]
			([EACElementID],
			[Name],
			[Description],
			[Type],[TypeName],[HasOptions],[Length],[Status],[LastUpdated],[ModifiedBy])        
			VALUES(@EACEID,
			'NotasEnfermeria',
			'Listado de Plantillas de Observaciones de Indigo que voy a permitir agregar para su edición en esta vista.',
			0,'',1,0,2,GETDATE(),'Administrador')
			SET @ATTRNotasEnfermeriaID = (SELECT @@IDENTITY)
		END
	IF NOT(@ATTRNotasEnfermeriaID IS NULL) AND (@ATTRNotasEnfermeriaID > 0)
		BEGIN
			---------------------------------------------------------------------------------------------
			---- OPCIONES
			---------------------------------------------------------------------------------------------
			
			---------------------------------------------------------------------------------------------
			---- EvolutivoEnfermeria
			IF NOT(@PlantillaEvolutivoEnfermeria IS NULL) 
				AND EXISTS(SELECT ID FROM ObservationTemplate WHERE Name = @PlantillaEvolutivoEnfermeria)
				AND NOT EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRNotasEnfermeriaID AND Value = @PlantillaEvolutivoEnfermeria)
				BEGIN
					INSERT INTO [EACAttributeOption]
					([EACAttributeID],
					[Value],
					[Description],
					[LastUpdated],[ModifiedBy],[Status])
					VALUES(@ATTRNotasEnfermeriaID,
					@PlantillaEvolutivoEnfermeria,
					@PlantillaEvolutivoEnfermeria,
					GETDATE(),'Administrador',2)
				END
			---------------------------------------------------------------------------------------------
		END
	---------------------------------------------------------------------------------------------
	---- FIN ATRIBUTO NotasEnfermeria
	---------------------------------------------------------------------------------------------

	---------------------------------------------------------------------------------------------
	---- ATRIBUTO NotasMedicas
	---------------------------------------------------------------------------------------------
	SET @ATTRNotasMedicasID = (SELECT ID FROM [EACAttribute] WHERE [EACElementID]=@EACEID AND Name = 'NotasMedicas')
	IF (@ATTRNotasMedicasID IS NULL) OR (@ATTRNotasMedicasID <= 0)
		BEGIN
			INSERT INTO [EACAttribute]
			([EACElementID],
			[Name],
			[Description],
			[Type],[TypeName],[HasOptions],[Length],[Status],[LastUpdated],[ModifiedBy])        
			VALUES(@EACEID,
			'NotasMedicas',
			'Listado de Plantillas de Observaciones de Indigo que se van a mostrar en la pestaña de Notas Medicas.',
			0,'',1,0,2,GETDATE(),'Administrador')
			SET @ATTRNotasMedicasID = (SELECT @@IDENTITY)
		END
	IF NOT(@ATTRNotasMedicasID IS NULL) AND (@ATTRNotasMedicasID > 0)
		BEGIN
			---------------------------------------------------------------------------------------------
			---- OPCIONES
			---------------------------------------------------------------------------------------------
			
			---------------------------------------------------------------------------------------------
			---- Evolutivo
			IF NOT(@PlantillaEvolutivo IS NULL) 
				AND EXISTS(SELECT ID FROM ObservationTemplate WHERE Name = @PlantillaEvolutivo)
				AND NOT EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRNotasMedicasID AND Value = @PlantillaEvolutivo)
				BEGIN
					INSERT INTO [EACAttributeOption]
					([EACAttributeID],
					[Value],
					[Description],
					[LastUpdated],[ModifiedBy],[Status])
					VALUES(@ATTRNotasMedicasID,
					@PlantillaEvolutivo,
					@PlantillaEvolutivo,
					GETDATE(),'Administrador',2)
				END
			---------------------------------------------------------------------------------------------
			---------------------------------------------------------------------------------------------
			---- DiagnosticoMedico
			--IF NOT(@PlantillaDiagnosticoMedico IS NULL) 
			--	AND EXISTS(SELECT ID FROM ObservationTemplate WHERE Name = @PlantillaDiagnosticoMedico)
			--	AND NOT EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRNotasMedicasID AND Value = @PlantillaDiagnosticoMedico)
			--	BEGIN
			--		INSERT INTO [EACAttributeOption]
			--		([EACAttributeID],
			--		[Value],
			--		[Description],
			--		[LastUpdated],[ModifiedBy],[Status])
			--		VALUES(@ATTRNotasMedicasID,
			--		@PlantillaDiagnosticoMedico,
			--		@PlantillaDiagnosticoMedico,
			--		GETDATE(),'Administrador',2)
			--	END
			---------------------------------------------------------------------------------------------
		END
	---------------------------------------------------------------------------------------------
	---- FIN ATRIBUTO NotasMedicas
	---------------------------------------------------------------------------------------------

	---------------------------------------------------------------------------------------------
	---- ATRIBUTO OtrasNotas
	---------------------------------------------------------------------------------------------
	SET @ATTROtrasNotasID = (SELECT ID FROM [EACAttribute] WHERE [EACElementID]=@EACEID AND Name = 'OtrasNotas')
	IF (@ATTROtrasNotasID IS NULL) OR (@ATTROtrasNotasID <= 0)
		BEGIN
			INSERT INTO [EACAttribute]
			([EACElementID],
			[Name],
			[Description],
			[Type],[TypeName],[HasOptions],[Length],[Status],[LastUpdated],[ModifiedBy])        
			VALUES(@EACEID,
			'OtrasNotas',
			'Listado de Plantillas de Observaciones de Indigo que se van a mostrar en la pestaña de Otras Notas.',
			0,'',1,0,2,GETDATE(),'Administrador')
			SET @ATTROtrasNotasID = (SELECT @@IDENTITY)
		END
	--IF NOT(@ATTROtrasNotasID IS NULL) AND (@ATTROtrasNotasID > 0)
		--BEGIN
			---------------------------------------------------------------------------------------------
			---- OPCIONES
			---------------------------------------------------------------------------------------------
			
			---------------------------------------------------------------------------------------------
			---- Alergia
			--IF NOT(@PlantillaAlergia IS NULL) 
			--	AND EXISTS(SELECT ID FROM ObservationTemplate WHERE Name = @PlantillaAlergia)
			--	AND NOT EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTROtrasNotasID AND Value = @PlantillaAlergia)
			--	BEGIN
			--		INSERT INTO [EACAttributeOption]
			--		([EACAttributeID],
			--		[Value],
			--		[Description],
			--		[LastUpdated],[ModifiedBy],[Status])
			--		VALUES(@ATTROtrasNotasID,
			--		@PlantillaAlergia,
			--		@PlantillaAlergia,
			--		GETDATE(),'Administrador',2)
			--	END
			---------------------------------------------------------------------------------------------
			---------------------------------------------------------------------------------------------
			---- Alerta
			--IF NOT(@PlantillaAlerta IS NULL) 
			--	AND EXISTS(SELECT ID FROM ObservationTemplate WHERE Name = @PlantillaAlerta)
			--	AND NOT EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTROtrasNotasID AND Value = @PlantillaAlerta)
			--	BEGIN
			--		INSERT INTO [EACAttributeOption]
			--		([EACAttributeID],
			--		[Value],
			--		[Description],
			--		[LastUpdated],[ModifiedBy],[Status])
			--		VALUES(@ATTROtrasNotasID,
			--		@PlantillaAlerta,
			--		@PlantillaAlerta,
			--		GETDATE(),'Administrador',2)
			--	END
			---------------------------------------------------------------------------------------------
			---------------------------------------------------------------------------------------------
			---- AltaMedica
			--IF NOT(@PlantillaAltaMedica IS NULL) 
			--	AND EXISTS(SELECT ID FROM ObservationTemplate WHERE Name = @PlantillaAltaMedica)
			--	AND NOT EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTROtrasNotasID AND Value = @PlantillaAltaMedica)
			--	BEGIN
			--		INSERT INTO [EACAttributeOption]
			--		([EACAttributeID],
			--		[Value],
			--		[Description],
			--		[LastUpdated],[ModifiedBy],[Status])
			--		VALUES(@ATTROtrasNotasID,
			--		@PlantillaAltaMedica,
			--		@PlantillaAltaMedica,
			--		GETDATE(),'Administrador',2)
			--	END
			---------------------------------------------------------------------------------------------
			---------------------------------------------------------------------------------------------
			---- Constantes
			--IF NOT(@PlantillaConstantes IS NULL) 
			--	AND EXISTS(SELECT ID FROM ObservationTemplate WHERE Name = @PlantillaConstantes)
			--	AND NOT EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTROtrasNotasID AND Value = @PlantillaConstantes)
			--	BEGIN
			--		INSERT INTO [EACAttributeOption]
			--		([EACAttributeID],
			--		[Value],
			--		[Description],
			--		[LastUpdated],[ModifiedBy],[Status])
			--		VALUES(@ATTROtrasNotasID,
			--		@PlantillaConstantes,
			--		@PlantillaConstantes,
			--		GETDATE(),'Administrador',2)
			--	END
			---------------------------------------------------------------------------------------------
			---------------------------------------------------------------------------------------------
			---- InformeAlta
			--IF NOT(@PlantillaInformeAlta IS NULL) 
			--	AND EXISTS(SELECT ID FROM ObservationTemplate WHERE Name = @PlantillaInformeAlta)
			--	AND NOT EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTROtrasNotasID AND Value = @PlantillaInformeAlta)
			--	BEGIN
			--		INSERT INTO [EACAttributeOption]
			--		([EACAttributeID],
			--		[Value],
			--		[Description],
			--		[LastUpdated],[ModifiedBy],[Status])
			--		VALUES(@ATTROtrasNotasID,
			--		@PlantillaInformeAlta,
			--		@PlantillaInformeAlta,
			--		GETDATE(),'Administrador',2)
			--	END
			---------------------------------------------------------------------------------------------
		--END
	---------------------------------------------------------------------------------------------
	---- FIN ATRIBUTO OtrasNotas
	---------------------------------------------------------------------------------------------
END
