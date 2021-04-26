
--REGISTRO DEL METADATO PARA INTERFACE EFARMACO-HCDIS
--PRIMERO EL ELEMENT
DECLARE @EACEID INT
DECLARE @ATTRID INT

SET @EACEID = ISNULL((SELECT TOP 1 ID FROM [EACElement] WHERE Name = 'CPOEInteropDTO'),0)
IF (@EACEID = 0)  
BEGIN
	--//SI NO EXISTE SE REGISTRA EL METADATO
	INSERT INTO [EACElement]
	([ElementType],[Name],[ModuleName],[Description],[Status],[LastUpdated],[ModifiedBy])
	VALUES (2, 'CPOEInteropDTO','',
		'Para la configuración de valores de Interoperabilidad en procesos CPOE de Prescripción',
		2, GETDATE(), 'Administrador')
	SET @EACEID = (SELECT @@IDENTITY)	
END
IF NOT(@EACEID IS NULL) AND @EACEID > 0
BEGIN
	--///////////////////////////////////////////////////////////////////////////////////
	--//
	--//								ACTIVE MODE
	--//
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ActiveMode' AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		---REGISTRO DE LOS ATRIBUTOS
		--/// ActiveMode
		--/// Modo activo para cada tipo de mensaje. De conformidad todos los mensajes tipo QRY son siempre síncronos. 
		--/// Description: ReceiveMode o SendMode. 
		--/// Value: Synchronous, Asynchronous, SynchronousACK
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'ActiveMode',
			'Modo activo para cada tipo de mensaje. De conformidad todos los mensajes tipo QRY son siempre síncronos. Description: ReceiveMode o SendMode. Value: Synchronous, SynchronousACK o Asynchronous',
			0,'',0,0,
			1,1,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	IF NOT(@ATTRID = 0)
	BEGIN
		--// SENDMODE
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='SendMode'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'SendMode','SynchronousACK',GETDATE(),'Administrador',2)
		END
		--// ReceiveMode
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='ReceiveMode'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'ReceiveMode','SynchronousACK',GETDATE(),'Administrador',2)
		END
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//
	--//								CPOEMessageHandler
	--//
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'CPOEMessageHandler' AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		--/// CPOEMessageHandler
		--/// En las opciones se indicarán los manejadores de cada mensaje. 
		--/// Descripcion el nombre exacto del manejador en HCDIS
		--/// Value el código HCDIS de ese mensaje.
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'CPOEMessageHandler',
			'En las opciones se indicarán los manejadores de cada mensaje. Descripcion el nombre exacto del manejador en HCDIS, en Value el código HCDIS de ese mensaje.',
			0,'',0,0,
			1,1,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	IF NOT(@ATTRID = 0)
	BEGIN
		--// DecodeHL7OMP09MessageHandler
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='DecodeHL7OMP09MessageHandler'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'DecodeHL7OMP09MessageHandler','HL7_OMP09',GETDATE(),'Administrador',2)
		END			
		--// DecodeHL7RDE11MessageHandler
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='DecodeHL7RDE11MessageHandler'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'DecodeHL7RDE11MessageHandler','HL7_RDE11',GETDATE(),'Administrador',2)
		END
		--// DecodeHL7ROR26MessageHandler
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='DecodeHL7ROR26MessageHandler'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'DecodeHL7ROR26MessageHandler','HL7_ROR26',GETDATE(),'Administrador',2)
		END
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//
	--//								ExpectedAppActors
	--//
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ExpectedAppActors' AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		--/// ExpectedAppActors
		--/// En opciones Nombres de los actores de interoperabilidad y la ruta de sus servicios. 
		--/// Descripcion: Identificación del actor en el sistema. 
		--/// Value: Ruta del servicio
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'ExpectedAppActors',
			'En opciones Nombres de los actores de interoperabilidad y la ruta de sus servicios. Descripcion: Identificación del actor en el sistema. Value: Ruta del servicio',
			0,'',0,0,
			1,1,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	IF NOT(@ATTRID = 0)
	BEGIN
		--// eFarmaco
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='eFarmaco'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','eFarmaco',GETDATE(),'Administrador',2)
		END
		--// HCDIS
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HCDIS'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','HCDIS',GETDATE(),'Administrador',2)
		END
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//
	--//								AppActorMessageHandler
	--//
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'AppActorMessageHandler' AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		--/// AppActorMessageHandler
		--/// En las opciones se indicarán los manejadores de cada appActor. 
		--/// Descripcion: Identificación del actor en el sistema. 
		--/// Value: El nombre exacto del manejador en HCDIS
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'AppActorMessageHandler',
			'En las opciones se indicarán los manejadores de cada appActor. Descripcion: Identificación del actor en el sistema. Value: El nombre exacto del manejador en HCDIS',
			0,'',0,0,
			1,1,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	IF NOT(@ATTRID = 0)
	BEGIN
		--// eFarmacoMessageHandler
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='eFarmacoMessageHandler'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmacoMessageHandler','eFarmaco',GETDATE(),'Administrador',2)
		END
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//
	--//								AppActorRol
	--//
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'AppActorRol' AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		--/// AppActorRol
		--/// En opciones rol de los actores de interoperabilidad. 
		--/// Descripcion: Identificación del actor en el sistema. 
		--/// Value: Rol del actor. ADT, MPI, PRESCRIBER (PRESCRIPTOR), DISPENSER (DISPENSADOR), SUPPLIER (ADMINISTRADOR), REPOSITORY (REPOSITORIO)
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'AppActorRol',
			'En opciones rol de los actores de interoperabilidad. Descripcion: Identificación del actor en el sistema. Value: Rol del actor. ADT, MPI, PrescriptionPlacer, PharmaceuticalAdviser, MedicationDispenser, MedicationAdministration.',
			0,'',0,0,
			1,1,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	IF NOT(@ATTRID = 0)
	BEGIN
		--// eFarmaco as PrescriptionPlacer
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='eFarmaco' AND [Value] = 'PrescriptionPlacer'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','PrescriptionPlacer',GETDATE(),'Administrador',2)
		END
		--// eFarmaco as PharmaceuticalAdviser
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='eFarmaco' AND [Value] = 'PharmaceuticalAdviser'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','PharmaceuticalAdviser',GETDATE(),'Administrador',2)
		END
		--// eFarmaco as PharmaceuticalAdviser
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='eFarmaco' AND [Value] = 'PharmaceuticalAdviser'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','ADT',GETDATE(),'Administrador',2)
		END
		--// HCDIS as MedicationDispenser
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HCDIS' AND [Value] = 'MedicationDispenser'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','MedicationDispenser',GETDATE(),'Administrador',2)
		END
		--// HCDIS as MedicationAdministration
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HCDIS' AND [Value] = 'MedicationAdministration'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','MedicationAdministration',GETDATE(),'Administrador',2)
		END
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//
	--//								ExpectedFacilityActors
	--//
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ExpectedFacilityActors' AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		--/// ExpectedFacilityActors
		--/// En opciones Centros de cada actor. 
		--/// Descripcion: Identificación del actor en el sistema. 
		--/// Value: Identificación del Centro desde el que se envía/recibe el mensaje para dicho actor.
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'ExpectedFacilityActors',
			'En opciones Centros de cada actor. Descripcion: Identificación del actor en el sistema. Value: Identificación del Centro desde el que se envía/recibe el mensaje para dicho actor.',
			0,'',0,0,
			1,1,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	IF NOT(@ATTRID = 0)
	BEGIN
		--// eFarmaco EN CVB
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='eFarmaco' AND [Value] = 'CVB'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','CVB',GETDATE(),'Administrador',2)
		END
		--// HCDIS EN CVB
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HCDIS' AND [Value] = 'CVB'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','CVB',GETDATE(),'Administrador',2)
		END
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//
	--//								MessageReceiveExpected
	--//
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'MessageReceiveExpected' AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		--/// MessageReceiveExpected
		--/// En opciones se indican los mensajes por cada actor que se esperan recibir. 
		--/// Descripción codigo del mensaje en HCDIS
		--/// Value identificación del actor, la que esté en ExpectedAppActors.
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'MessageReceiveExpected',
			'En opciones se indican los mensajes por cada actor que se esperan recibir. En descripción codigo del mensaje en HCDIS, en value identificación del actor, la que esté en ExpectedAppActors.',
			0,'',0,0,
			1,1,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	IF NOT(@ATTRID = 0)
	BEGIN
		--// HL7_OMP09 DE eFarmaco
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HL7_OMP09' AND [Value] = 'eFarmaco'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HL7_OMP09','eFarmaco',GETDATE(),'Administrador',2)
		END
		--// HL7_RDE11 DE eFarmaco
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HL7_RDE11' AND [Value] = 'eFarmaco'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HL7_RDE11','eFarmaco',GETDATE(),'Administrador',2)
		END
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//
	--//								MessageSendExpected
	--//
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'MessageSendExpected' AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		--/// MessageSendExpected
		--/// En opciones se indican los mensajes que se esperan envian y a qué actores. 
		--/// Descripción codigo del mensaje en HCDIS
		--/// Value identificación del actor, la que esté en ExpectedAppActors.
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'MessageSendExpected',
			'En opciones se indican los mensajes que se esperan envian y a qué actores. En descripción codigo del mensaje en HCDIS, en value identificación del actor, la que esté en ExpectedAppActors.',
			0,'',0,0,
			1,1,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	IF NOT(@ATTRID = 0)
	BEGIN
		--// HL7_ORP10 DE HCDIS
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HL7_ORP10' AND [Value] = 'HCDIS'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HL7_ORP10','HCDIS',GETDATE(),'Administrador',2)
		END		
		--// HL7_RRE12 DE HCDIS
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HL7_RRE12' AND [Value] = 'HCDIS'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HL7_RRE12','HCDIS',GETDATE(),'Administrador',2)
		END
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//
	--//								ReceiveControlCode
	--//
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ReceiveControlCode' AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		--/// ReceiveControlCode
		--/// En las opciones se identifican las diferentes acciones esperadas para el ORC.1. 
		--/// Description el código exacto del actor tomado de ExpectedAppActors.
		--/// Value el código de HCDIS para la acción
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'ReceiveControlCode',
			'En las opciones se identifican las diferentes acciones esperadas para el ORC.1. En Value el código de HCDIS para la acción y en description el código exacto del actor tomado de ExpectedAppActors.',
			0,'',0,0,
			1,1,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	IF NOT(@ATTRID = 0)
	BEGIN
		--// eFarmaco  NW
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='eFarmaco' AND [Value] = 'NW'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','NW',GETDATE(),'Administrador',2)
		END
		--// eFarmaco  RP
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='eFarmaco' AND [Value] = 'RP'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','RP',GETDATE(),'Administrador',2)
		END
		--// eFarmaco  XO
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='eFarmaco' AND [Value] = 'XO'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','XO',GETDATE(),'Administrador',2)
		END
		--// eFarmaco  HD
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='eFarmaco' AND [Value] = 'HD'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','HD',GETDATE(),'Administrador',2)
		END
		--// eFarmaco  DC
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='eFarmaco' AND [Value] = 'DC'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','DC',GETDATE(),'Administrador',2)
		END
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//
	--//								SendControlCode
	--//
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'SendControlCode' AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		--/// SendControlCode
		--/// En las opciones se identifican las diferentes acciones que se enviarán para el ORC.1. 
		--/// Description el código exacto del actor tomado de ExpectedAppActors.
		--/// Value el código de HCDIS para la acción 
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'SendControlCode',
			'En las opciones se identifican las diferentes acciones que se enviarán en el ORC.1. En Value el código de HCDIS para la acción y en description el código exacto del actor tomado de ExpectedAppActors.',
			0,'',0,0,
			1,1,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	IF NOT(@ATTRID = 0)
	BEGIN
		--// HCDIS NW
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HCDIS' AND [Value] = 'NW'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','NW',GETDATE(),'Administrador',2)
		END
		--// HCDIS  RP
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HCDIS' AND [Value] = 'RP'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','RP',GETDATE(),'Administrador',2)
		END
		--// HCDIS  XO
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HCDIS' AND [Value] = 'XO'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','XO',GETDATE(),'Administrador',2)
		END
		--// HCDIS  HD
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HCDIS' AND [Value] = 'HD'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','HD',GETDATE(),'Administrador',2)
		END
		--// HCDIS  DC
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HCDIS' AND [Value] = 'DC'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','DC',GETDATE(),'Administrador',2)
		END
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//
	--//								ReceiveEventCode
	--//
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ReceiveEventCode' AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		--/// ReceiveEventCode
		--/// En las opciones se identifican las diferentes eventos esperadas para el ORC.16.1 combinado con el ORC.1. 
		--/// Description el código exacto del evento.
		--/// Value el código de HCDIS para el ORC.1
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'ReceiveEventCode',
			'En las opciones se identifican las diferentes acciones esperadas para el ORC.1. En Value el código de HCDIS para la acción y en description el código exacto del actor tomado de ExpectedAppActors.',
			0,'',0,0,
			1,1,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	IF NOT(@ATTRID = 0)
	BEGIN
		--// POR AHORA LO QUE SE RECIBE DE eFarmaco
		--// de NW OK
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='NW' AND [Value] = 'OK'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'NW','OK',GETDATE(),'Administrador',2)
		END
		--// de NW UA
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='NW' AND [Value] = 'UA'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'NW','UA',GETDATE(),'Administrador',2)
		END
		--// de ROR OK
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='ROR' AND [Value] = 'OK'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'ROR','OK',GETDATE(),'Administrador',2)
		END
		--// de ROR UX
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='ROR' AND [Value] = 'UX'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'ROR','UX',GETDATE(),'Administrador',2)
		END
		--// de COR OK
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='COR' AND [Value] = 'OK'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'COR','OK',GETDATE(),'Administrador',2)
		END
		--// de COR UX
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='COR' AND [Value] = 'UX'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'COR','UX',GETDATE(),'Administrador',2)
		END
		--// de HO OH
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HO' AND [Value] = 'OH'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HO','OH',GETDATE(),'Administrador',2)
		END
		--// de HO UH
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HO' AND [Value] = 'UH'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HO','UH',GETDATE(),'Administrador',2)
		END
		--// de XO CR
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='XO' AND [Value] = 'CR'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'XO','CR',GETDATE(),'Administrador',2)
		END
		--// de XO UC
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='XO' AND [Value] = 'UC'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'XO','UC',GETDATE(),'Administrador',2)
		END
		--// de AUNO OK
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='AUNO' AND [Value] = 'OK'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'AUNO','OK',GETDATE(),'Administrador',2)
		END
		--// de AUNO UA
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='AUNO' AND [Value] = 'UA'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'AUNO','UA',GETDATE(),'Administrador',2)
		END
		--// de DO OD
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='DO' AND [Value] = 'OD'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'DO','OD',GETDATE(),'Administrador',2)
		END
		--// de DO UD
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='DO' AND [Value] = 'UD'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'DO','UD',GETDATE(),'Administrador',2)
		END
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//
	--//								SendEventCode
	--//
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'SendEventCode' AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		--/// SendEventCode
		--/// En las opciones se identifican las diferentes eventos que se enviarán en el ORC.16.1 combinado con el ORC.1. 
		--/// Description el código exacto del evento.
		--/// Value el código de HCDIS para el ORC.1
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'SendEventCode',
			'En las opciones se identifican las diferentes eventos salientes para el ORC.16.1 combinado con el ORC.1. En Value el código de HCDIS para el ORC.1 tomado de SendControlCode y en description el código del evento.',
			0,'',0,0,
			1,1,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	IF NOT(@ATTRID = 0)
	BEGIN
		--// POR AHORA LO QUE ENVIA HCDIS
		--// de NW UA
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='NW' AND [Value] = 'UA'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'NW','UA',GETDATE(),'Administrador',2)
		END
		--// de ROR UX
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='ROR' AND [Value] = 'UX'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'ROR','UX',GETDATE(),'Administrador',2)
		END
		--// de COR UX
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='COR' AND [Value] = 'UX'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'COR','UX',GETDATE(),'Administrador',2)
		END
		--// de HO UH
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HO' AND [Value] = 'UH'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HO','UH',GETDATE(),'Administrador',2)
		END
		--// de AUNO UA
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='AUNO' AND [Value] = 'UA'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'AUNO','UA',GETDATE(),'Administrador',2)
		END
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//
	--//								MessageReason
	--//
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'MessageReason' AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		--/// MessageReason
		--/// En las opciones se indicarán los diferentes motivos configurados en HCDIS para cada una de las acciones que provoquen la interrupción del circuito normal. 
		--/// Description código de la accion en HCDIS tomado de los Control Code del ORC.1.
		--/// Value código de la razón en HCDIS. 
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'MessageReason',
			'En las opciones se indicarán los diferentes motivos configurados en HCDIS para cada una de las acciones que provoquen la interrupción del circuito normal. En Value código de la razón en HCDIS. En description código de la accion en HCDIS.',
			0,'',0,0,
			1,1,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	IF NOT(@ATTRID = 0)
	BEGIN
		-- POR AHORA NO HAY RAZONES PERO TENGO QUE PONER UNA POR DEFECTO por cada accion
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='COR' AND [Value] = 'UndoOrder'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'COR','UndoOrder',GETDATE(),'Administrador',2)
		END
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='DO' AND [Value] = 'AbortOrder'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'DO','AbortOrder',GETDATE(),'Administrador',2)
		END
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//
	--//								OrderCode
	--//
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'OrderCode' AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		--/// OrderCode
		--/// En opciones se indicarán todas las órdenes maestras de HCDIS que se verán implicadas en este proceso de interoperabilidad. 
		--/// Descripcion en nombre de la orden
		--/// Value el código de la orden en HCDIS.
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'OrderCode',
			'En opciones se indicarán todas las órdenes maestras de HCDIS que se verán implicadas en estre proceso de interoperabilidad. En Descriopcion en nombre de la orden, en value el código de la orden en HCDIS.',
			0,'',0,0,
			1,1,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	IF NOT(@ATTRID = 0)
	BEGIN
		---// PRESCRIPCION
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='PRESCRIPCION'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'PRESCRIPCION','PRESCRIPCION NORMAL',GETDATE(),'Administrador',2)
		END
	END
END
