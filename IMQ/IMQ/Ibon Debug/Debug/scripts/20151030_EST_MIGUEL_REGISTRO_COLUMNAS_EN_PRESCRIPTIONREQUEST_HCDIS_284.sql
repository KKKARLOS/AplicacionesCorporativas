--////////////////////////////////
--//
--// MODIFICAR TABLA DE PRESCRIPCIONES PARA AÑADIR 
--// LAS MODIFICACIONES PARA LOS SUEROS Y PRESCRIPCIONES 
--// COMPLEJAS
--//
--///////////////////////////////////
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PrescriptionRequest' and c.name = 'PharmacyTreatmentInstructions' and o.type ='U'))
BEGIN
	ALTER TABLE PrescriptionRequest ADD	[PharmacyTreatmentInstructions] [nvarchar](max) NULL
END
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PrescriptionRequest' and c.name = 'AdministrationInstructions' and o.type ='U'))
BEGIN
	ALTER TABLE PrescriptionRequest ADD	[AdministrationInstructions] [nvarchar](max) NULL
END
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PrescriptionRequest' and c.name = 'DeliveryToLocationID' and o.type ='U'))
BEGIN
	ALTER TABLE PrescriptionRequest ADD	[DeliveryToLocationID] [int] NOT NULL DEFAULT(0)
END
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PrescriptionRequest' and c.name = 'RequiresPharmacistTreatmentVerifier' and o.type ='U'))
BEGIN
	ALTER TABLE PrescriptionRequest ADD	[RequiresPharmacistTreatmentVerifier] [bit] NOT NULL DEFAULT(0)
END
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PrescriptionRequest' and c.name = 'NeedsHumanReview' and o.type ='U'))
BEGIN
	ALTER TABLE PrescriptionRequest ADD	[NeedsHumanReview] [bit] NOT NULL DEFAULT(0)
END
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PrescriptionRequest' and c.name = 'BodySiteConceptID' and o.type ='U'))
BEGIN
	ALTER TABLE PrescriptionRequest ADD	[BodySiteConceptID] [int] NOT NULL DEFAULT(0)
END
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PrescriptionRequest' and c.name = 'AdministrationDeviceID' and o.type ='U'))
BEGIN
	ALTER TABLE PrescriptionRequest ADD	[AdministrationDeviceID] [int] NOT NULL DEFAULT(0)
END
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PrescriptionRequest' and c.name = 'RequestedGiveStrength' and o.type ='U'))
BEGIN
	ALTER TABLE PrescriptionRequest ADD	[RequestedGiveStrength] [float] NULL DEFAULT(0)
END
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PrescriptionRequest' and c.name = 'RequestedGiveStrengthUnitsID' and o.type ='U'))
BEGIN
	ALTER TABLE PrescriptionRequest ADD	[RequestedGiveStrengthUnitsID] [int] NOT NULL DEFAULT(0)
END
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PrescriptionRequest' and c.name = 'PharmacyOrderType' and o.type ='U'))
BEGIN
	ALTER TABLE PrescriptionRequest ADD	[PharmacyOrderType] [smallint] NOT NULL DEFAULT(0)
END

--////////////////////////////////
--//
--// MODIFICAR TABLA DE ItemTreatmentOrderSequence PARA AÑADIR 
--// LAS MODIFICACIONES PARA LOS SUEROS Y PRESCRIPCIONES 
--// COMPLEJAS
--//
--///////////////////////////////////
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'ItemTreatmentOrderSequence' and c.name = 'PrescriptionRequestID' and o.type ='U'))
BEGIN
	ALTER TABLE ItemTreatmentOrderSequence ADD	[PrescriptionRequestID] [int] NOT NULL DEFAULT(0)
END
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'ItemTreatmentOrderSequence' and c.name = 'RequestedGiveStrengthVolume' and o.type ='U'))
BEGIN
	ALTER TABLE ItemTreatmentOrderSequence ADD	[RequestedGiveStrengthVolume] [float] NULL DEFAULT(0)
END
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'ItemTreatmentOrderSequence' and c.name = 'RequestedGiveStrengthVolumeUnitsID' and o.type ='U'))
BEGIN
	ALTER TABLE ItemTreatmentOrderSequence ADD	[RequestedGiveStrengthVolumeUnitsID] [int] NOT NULL DEFAULT(0)
END
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'ItemTreatmentOrderSequence' and c.name = 'GiveQtyToDispense' and o.type ='U'))
BEGIN
	ALTER TABLE ItemTreatmentOrderSequence ADD	[GiveQtyToDispense] [float] NULL DEFAULT(0)
END

--////////////////////////////////
--//
--// REGISTRO DE LOS NUEVOS ATRIBUTOS PARA AÑADIR 
--// LAS MODIFICACIONES PARA LOS SUEROS Y PRESCRIPCIONES 
--// COMPLEJAS
--//
--///////////////////////////////////
DECLARE @EACEID INT
DECLARE @ATTRID INT

SET @EACEID = ISNULL((SELECT ID FROM [EACElement] WHERE Name = 'PrescriptionRequestEntity'),0)
IF NOT(@EACEID IS NULL) AND @EACEID > 0
BEGIN
	--///////////////////////////////////////////////////////////////////////////////////
	--//								PharmacyTreatmentInstructions
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'PharmacyTreatmentInstructions' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'PharmacyTreatmentInstructions',
			'Instrucciones a Farmacia',
			3,'',0,0,
			1,0,0,0,1,1,200,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//								AdministrationInstructions
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'AdministrationInstructions' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'AdministrationInstructions',
			'Instrucciones a Enfermería',
			3,'',0,0,
			1,0,0,0,1,1,200,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//								DeliveryToLocation
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'DeliveryToLocation' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'DeliveryToLocation',
			'Lugar del tratamiento',
			6,'SII.HCD.BackOffice.Entities.LocationBaseEntity, SII.HCD.BackOffice.Entities',0,0,
			1,0,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//								AdministrationDevice
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'AdministrationDevice' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'AdministrationDevice',
			'Equipo del tratamiento',
			6,'SII.HCD.BackOffice.Entities.EquipmentBaseEntity, SII.HCD.BackOffice.Entities',0,0,
			1,0,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//								BodySiteConcept
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'BodySiteConcept' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'BodySiteConcept',
			'Lugar del cuerpo',
			6,'SII.HCD.BackOffice.Entities.BodySiteConceptEntity, SII.HCD.BackOffice.Entities',0,0,
			1,0,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//								RequestedGiveStrengthUnits
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'RequestedGiveStrengthUnits' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'RequestedGiveStrengthUnits',
			'Unidad de Fuerza del Goteo',
			6,'SII.HCD.BackOffice.Entities.PhysUnitEntity, SII.HCD.BackOffice.Entities',0,0,
			1,0,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//								RequestedGiveStrength
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'RequestedGiveStrength' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'RequestedGiveStrength',
			'Fuerza del Goteo',
			1,'',0,0,
			1,0,0,0,1,1,18,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//								RequiresPharmacistTreatmentVerifier
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'RequiresPharmacistTreatmentVerifier' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'RequiresPharmacistTreatmentVerifier',
			'Requiere verificación de Farmaica',
			5,'',0,0,
			1,0,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	--///////////////////////////////////////////////////////////////////////////////////
	--//								NeedsHumanReview
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'NeedsHumanReview' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'NeedsHumanReview',
			'Requiere verificación de Enfermería',
			5,'',0,0,
			1,0,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	
	--///////////////////////////////////////////////////////////////////////////////////
	--//								PharmacyOrderType
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'PharmacyOrderType' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'PharmacyOrderType',
			'Tipo de orden de farmacia',
			7,'SII.HCD.BackOffice.Entities.PharmacyOrderTypeEnum, SII.HCD.BackOffice.Entities',0,0,
			1,0,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
END



SET @EACEID = ISNULL((SELECT ID FROM [EACElement] WHERE Name = 'ItemTreatmentOrderSequenceEntity'),0)
IF NOT(@EACEID IS NULL) AND @EACEID > 0
BEGIN
	--///////////////////////////////////////////////////////////////////////////////////
	--//								PrescriptionRequestID
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'PrescriptionRequestID' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'PrescriptionRequestID',
			'Prescripcion',
			0,'',0,0,
			1,0,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	
	--///////////////////////////////////////////////////////////////////////////////////
	--//								RequestedGiveStrengthVolume
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'RequestedGiveStrengthVolume' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'RequestedGiveStrengthVolume',
			'Volumen del Componente',
			1,'',0,0,
			1,0,0,0,1,1,8,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	
		--///////////////////////////////////////////////////////////////////////////////////
	--//								RequestedGiveStrengthVolumeUnits
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'RequestedGiveStrengthVolumeUnitsID' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'RequestedGiveStrengthVolume',
			'Unidad de Volumen del Componente',
			6,'SII.HCD.BackOffice.Entities.PhysUnitEntity, SII.HCD.BackOffice.Entities',0,0,
			1,0,0,0,1,1,0,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
	
		--///////////////////////////////////////////////////////////////////////////////////
	--//								GiveQtyToDispense
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'GiveQtyToDispense' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'GiveQtyToDispense',
			'Unidades del componente a Dispensar',
			1,'',0,0,
			1,0,0,0,1,1,18,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

END