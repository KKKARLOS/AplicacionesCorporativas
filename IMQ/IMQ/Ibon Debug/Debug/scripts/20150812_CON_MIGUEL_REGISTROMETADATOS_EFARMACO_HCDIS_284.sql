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
		'Para la configuraci�n de valores de Interoperabilidad en procesos CPOE de Prescripci�n',
		2, GETDATE(), 'Administrador')
	SET @EACEID = (SELECT @@IDENTITY)	
	IF NOT(@EACEID IS NULL) AND @EACEID > 0
	BEGIN
		---REGISTRO DE LOS ATRIBUTOS
		--/// ActiveMode
		--/// Modo activo para cada tipo de mensaje. De conformidad todos los mensajes tipo QRY son siempre s�ncronos. 
		--/// Description: ReceiveMode o SendMode. 
		--/// Value: Synchronous, Asynchronous, SynchronousACK
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'ActiveMode',
			'Modo activo para cada tipo de mensaje. De conformidad todos los mensajes tipo QRY son siempre s�ncronos. Description: ReceiveMode o SendMode. Value: Synchronous, SynchronousACK o Asynchronous',
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
		--/// En las opciones se indicar�n los manejadores de cada mensaje. 
		--/// Descripcion el nombre exacto del manejador en HCDIS
		--/// Value el c�digo HCDIS de ese mensaje.
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'CPOEMessageHandler',
			'En las opciones se indicar�n los manejadores de cada mensaje. Descripcion el nombre exacto del manejador en HCDIS, en Value el c�digo HCDIS de ese mensaje.',
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
		--/// Descripcion: Identificaci�n del actor en el sistema. 
		--/// Value: Ruta del servicio
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'ExpectedAppActors',
			'En opciones Nombres de los actores de interoperabilidad y la ruta de sus servicios. Descripcion: Identificaci�n del actor en el sistema. Value: Ruta del servicio',
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
		--/// En las opciones se indicar�n los manejadores de cada appActor. 
		--/// Descripcion: Identificaci�n del actor en el sistema. 
		--/// Value: El nombre exacto del manejador en HCDIS
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'AppActorMessageHandler',
			'En las opciones se indicar�n los manejadores de cada appActor. Descripcion: Identificaci�n del actor en el sistema. Value: El nombre exacto del manejador en HCDIS',
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
		--/// Descripcion: Identificaci�n del actor en el sistema. 
		--/// Value: Rol del actor. ADT, MPI, PRESCRIBER (PRESCRIPTOR), DISPENSER (DISPENSADOR), SUPPLIER (ADMINISTRADOR), REPOSITORY (REPOSITORIO)
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'AppActorRol',
			'En opciones rol de los actores de interoperabilidad. Descripcion: Identificaci�n del actor en el sistema. Value: Rol del actor. ADT, MPI, PrescriptionPlacer, PharmaceuticalAdviser, MedicationDispenser, MedicationAdministration.',
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
		--/// Descripcion: Identificaci�n del actor en el sistema. 
		--/// Value: Identificaci�n del Centro desde el que se env�a/recibe el mensaje para dicho actor.
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'ExpectedFacilityActors',
			'En opciones Centros de cada actor. Descripcion: Identificaci�n del actor en el sistema. Value: Identificaci�n del Centro desde el que se env�a/recibe el mensaje para dicho actor.',
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
		--/// Descripci�n codigo del mensaje en HCDIS
		--/// Value identificaci�n del actor, la que est� en ExpectedAppActors.
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'MessageReceiveExpected',
			'En opciones se indican los mensajes por cada actor que se esperan recibir. En descripci�n codigo del mensaje en HCDIS, en value identificaci�n del actor, la que est� en ExpectedAppActors.',
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
		--/// En opciones se indican los mensajes que se esperan envian y a qu� actores. 
		--/// Descripci�n codigo del mensaje en HCDIS
		--/// Value identificaci�n del actor, la que est� en ExpectedAppActors.
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'MessageSendExpected',
			'En opciones se indican los mensajes que se esperan envian y a qu� actores. En descripci�n codigo del mensaje en HCDIS, en value identificaci�n del actor, la que est� en ExpectedAppActors.',
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
		--/// Description el c�digo exacto del actor tomado de ExpectedAppActors.
		--/// Value el c�digo de HCDIS para la acci�n
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'ReceiveControlCode',
			'En las opciones se identifican las diferentes acciones esperadas para el ORC.1. En Value el c�digo de HCDIS para la acci�n y en description el c�digo exacto del actor tomado de ExpectedAppActors.',
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
		--/// En las opciones se identifican las diferentes acciones que se enviar�n para el ORC.1. 
		--/// Description el c�digo exacto del actor tomado de ExpectedAppActors.
		--/// Value el c�digo de HCDIS para la acci�n 
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'SendControlCode',
			'En las opciones se identifican las diferentes acciones que se enviar�n en el ORC.1. En Value el c�digo de HCDIS para la acci�n y en description el c�digo exacto del actor tomado de ExpectedAppActors.',
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
		--/// Description el c�digo exacto del evento.
		--/// Value el c�digo de HCDIS para el ORC.1
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'ReceiveEventCode',
			'En las opciones se identifican las diferentes acciones esperadas para el ORC.1. En Value el c�digo de HCDIS para la acci�n y en description el c�digo exacto del actor tomado de ExpectedAppActors.',
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
		--/// En las opciones se identifican las diferentes eventos que se enviar�n en el ORC.16.1 combinado con el ORC.1. 
		--/// Description el c�digo exacto del evento.
		--/// Value el c�digo de HCDIS para el ORC.1
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'SendEventCode',
			'En las opciones se identifican las diferentes eventos salientes para el ORC.16.1 combinado con el ORC.1. En Value el c�digo de HCDIS para el ORC.1 tomado de SendControlCode y en description el c�digo del evento.',
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
		--/// En las opciones se indicar�n los diferentes motivos configurados en HCDIS para cada una de las acciones que provoquen la interrupci�n del circuito normal. 
		--/// Description c�digo de la accion en HCDIS tomado de los Control Code del ORC.1.
		--/// Value c�digo de la raz�n en HCDIS. 
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'MessageReason',
			'En las opciones se indicar�n los diferentes motivos configurados en HCDIS para cada una de las acciones que provoquen la interrupci�n del circuito normal. En Value c�digo de la raz�n en HCDIS. En description c�digo de la accion en HCDIS.',
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
		--/// En opciones se indicar�n todas las �rdenes maestras de HCDIS que se ver�n implicadas en este proceso de interoperabilidad. 
		--/// Descripcion en nombre de la orden
		--/// Value el c�digo de la orden en HCDIS.
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'OrderCode',
			'En opciones se indicar�n todas las �rdenes maestras de HCDIS que se ver�n implicadas en estre proceso de interoperabilidad. En Descriopcion en nombre de la orden, en value el c�digo de la orden en HCDIS.',
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