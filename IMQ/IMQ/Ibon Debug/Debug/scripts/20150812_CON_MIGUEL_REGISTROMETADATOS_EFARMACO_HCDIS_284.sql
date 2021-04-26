use HCDIS

--REGISTRO DEL METADATO PARA INTERFACE EFARMACO-HCDIS
--PRIMERO EL ELEMENT
DECLARE @EACEID INT
DECLARE @ATTRID INT

IF NOT(EXISTS(SELECT ID FROM [EACElement] WHERE Name = 'CPOEInteropDTO'))
BEGIN
	INSERT INTO [EACElement]
	([ElementType],[Name],[ModuleName],[Description],[Status],[LastUpdated],[ModifiedBy])
	VALUES (2, 'CPOEInteropDTO','',
		'Para la configuración de valores de Interoperabilidad en procesos CPOE de Prescripción',
		2, GETDATE(), 'Administrador')
	SET @EACEID = (SELECT @@IDENTITY)	
	IF NOT(@EACEID IS NULL) AND @EACEID > 0
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
		IF NOT(@ATTRID IS NULL) AND @ATTRID > 0
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'SendMode','SynchronousACK',GETDATE(),'Administrador',2)
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'ReceiveMode','SynchronousACK',GETDATE(),'Administrador',2)
		END
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
		IF NOT(@ATTRID IS NULL) AND @ATTRID > 0
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'DecodeHL7OMP09MessageHandler','HL7_OMP09',GETDATE(),'Administrador',2)
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'DecodeHL7RDE11MessageHandler','HL7_RDE11',GETDATE(),'Administrador',2)
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'DecodeHL7ROR26MessageHandler','HL7_ROR26',GETDATE(),'Administrador',2)
		END
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
		IF NOT(@ATTRID IS NULL) AND @ATTRID > 0
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','eFarmaco',GETDATE(),'Administrador',2)
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','HCDIS',GETDATE(),'Administrador',2)
		END
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
		IF NOT(@ATTRID IS NULL) AND @ATTRID > 0
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmacoMessageHandler','eFarmaco',GETDATE(),'Administrador',2)
		END
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
		IF NOT(@ATTRID IS NULL) AND @ATTRID > 0
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','PrescriptionPlacer',GETDATE(),'Administrador',2)
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','PharmaceuticalAdviser',GETDATE(),'Administrador',2)
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','ADT',GETDATE(),'Administrador',2)
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','MedicationDispenser',GETDATE(),'Administrador',2)
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','MedicationAdministration',GETDATE(),'Administrador',2)
		END
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
		IF NOT(@ATTRID IS NULL) AND @ATTRID > 0
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','CVB',GETDATE(),'Administrador',2)
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','CVB',GETDATE(),'Administrador',2)
		END
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
		IF NOT(@ATTRID IS NULL) AND @ATTRID > 0
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HL7_OMP09','eFarmaco',GETDATE(),'Administrador',2)
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HL7_RDE11','eFarmaco',GETDATE(),'Administrador',2)
		END
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
		--IF NOT(@ATTRID IS NULL) AND @ATTRID > 0
		--BEGIN
		--	INSERT INTO [EACAttributeOption]
		--	([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
		--	VALUES(@ATTRID,'','',GETDATE(),'Administrador',2)
		--END
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
		IF NOT(@ATTRID IS NULL) AND @ATTRID > 0
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','NW',GETDATE(),'Administrador',2)
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','RP',GETDATE(),'Administrador',2)
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','XO',GETDATE(),'Administrador',2)
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','HD',GETDATE(),'Administrador',2)
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','DC',GETDATE(),'Administrador',2)
		END
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
		--IF NOT(@ATTRID IS NULL) AND @ATTRID > 0
		--BEGIN
		--	INSERT INTO [EACAttributeOption]
		--	([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
		--	VALUES(@ATTRID,'','',GETDATE(),'Administrador',2)
		--END
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
		--IF NOT(@ATTRID IS NULL) AND @ATTRID > 0
		--BEGIN
		--	INSERT INTO [EACAttributeOption]
		--	([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
		--	VALUES(@ATTRID,'','',GETDATE(),'Administrador',2)
		--END
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
		--IF NOT(@ATTRID IS NULL) AND @ATTRID > 0
		--BEGIN
		--	INSERT INTO [EACAttributeOption]
		--	([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
		--	VALUES(@ATTRID,'','',GETDATE(),'Administrador',2)
		--END
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
		--IF NOT(@ATTRID IS NULL) AND @ATTRID > 0
		--BEGIN
		--	INSERT INTO [EACAttributeOption]
		--	([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
		--	VALUES(@ATTRID,'','',GETDATE(),'Administrador',2)
		--END
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
		IF NOT(@ATTRID IS NULL) AND @ATTRID > 0
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'PRESCRIPCION','PRESCRIPCION NORMAL',GETDATE(),'Administrador',2)
		END
	END
END