ALTER TABLE Item ADD Observations NVARCHAR(MAX) NULL

/*********************************************************/

PRINT N'Quitando [dbo].[CustomerRelatedCHNumber].[IX_CustomerRelatedCHNumber_CareCenterID]...';

GO
DROP INDEX [IX_CustomerRelatedCHNumber_CareCenterID]
    ON [dbo].[CustomerRelatedCHNumber];

GO
PRINT N'Quitando [dbo].[CustomerRelatedCHNumber].[IX_CustomerRelatedCHNumber_CustomerID_CareCenterID]...';
GO
DROP INDEX [IX_CustomerRelatedCHNumber_CustomerID_CareCenterID]
    ON [dbo].[CustomerRelatedCHNumber];
GO


PRINT N'Modificando [dbo].[UnidosisInfo]...';
GO
ALTER TABLE [dbo].[UnidosisInfo]
    ADD [AllowMultiDose] BIT DEFAULT ((0)) NOT NULL;

GO
PRINT N'Creando [dbo].[CustomerRelatedCHNumber].[II_CHNumber]...';
GO
CREATE NONCLUSTERED INDEX [II_CHNumber]
    ON [dbo].[CustomerRelatedCHNumber]([CHNumber] ASC);

GO
PRINT N'Actualización completada.';


/*********************************************************/

ALTER TABLE [dbo].[Invoice]
    ADD [DiscountValue]       MONEY          NULL,
        [DiscountType]        SMALLINT       NULL,
        [DiscountDescription] NVARCHAR (256) NULL;

/*********************************************************/

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

/*********************************************************/

--////////////////////////////////
--//
--// MODIFICAR TABLA DE FORMA FARMACEUTICA PARA AÑADIR IMAGEID
--// LAS MODIFICACIONES PARA PRESENTAR ICONO DE LA FORMA FARMACEUTICA 
--//
--///////////////////////////////////
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PharmaceuticalForm' and c.name = 'ImageID' and o.type ='U'))
BEGIN
	ALTER TABLE PharmaceuticalForm ADD [ImageID] [INT] NOT NULL Default(0)
END
--////////////////////////////////
--//
--// REGISTRO DE LOS NUEVOS ATRIBUTOS PARA AÑADIR 
--// LAS MODIFICACIONES DE FORMA FARMACEUTICA
--// LAS MODIFICACIONES PARA PRESENTAR ICONO DE LA FORMA FARMACEUTICA 
--//
--///////////////////////////////////
SET @EACEID = 0
SET @ATTRID = 0

SET @EACEID = ISNULL((SELECT ID FROM [EACElement] WHERE Name = 'PharmaceuticalFormEntity'),0)
IF NOT(@EACEID IS NULL) AND @EACEID > 0
BEGIN
	--///////////////////////////////////////////////////////////////////////////////////
	--//								ImageID
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ImageID' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'ImageID',
			'Imagen',
			0,'',0,0,
			1,0,0,0,1,1,8,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
END

/*********************************************************/

DECLARE @ID int
DECLARE @AttributeID int
DECLARE @EntityName nvarchar(256)
SET @EntityName='SupplierItemRelationshipEntity'
SET @ID = IsNull((SELECT [ID] FROM [EACElement] WHERE [Name]=@EntityName),0)
BEGIN
	INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
	[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
	[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
	VALUES (@ID, 'Quantity', 'Cantidad',1, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'false', 0, -1, '', '', '', '', '', 2, GetDate(), 'Administrador')
	SET @AttributeID=(SELECT @@IDENTITY)

	INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
	[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
	[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
	VALUES (@ID, 'ItemDescription', 'Descripción del artículo',3, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'false', 0, -1, '', '', '', '', '', 2, GetDate(), 'Administrador')
	SET @AttributeID=(SELECT @@IDENTITY)
END

/*********************************************************/

--REGISTRO DEL METADATO PARA INTERFACE EFARMACO-HCDIS
--PRIMERO EL ELEMENT
SET @EACEID = 0
SET @ATTRID = 0

SET @EACEID = ISNULL((SELECT ID FROM [EACElement] WHERE Name = 'CPOEInteropDTO'),0)
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
		--// eFarmaco  RE
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='eFarmaco' AND [Value] = 'RE'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'eFarmaco','RE',GETDATE(),'Administrador',2)
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
		--// HCDIS RE
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='HCDIS' AND [Value] = 'RE'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'HCDIS','RE',GETDATE(),'Administrador',2)
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
		
    	--// de RE OK
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='RE' AND [Value] = 'OK'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'RE','OK',GETDATE(),'Administrador',2)
		END
		--// de NW UA
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='RE' AND [Value] = 'UA'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'RE','UA',GETDATE(),'Administrador',2)
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
    	--// de RE UA
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @ATTRID 
			AND [Description] ='RE' AND [Value] = 'UA'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@ATTRID,'RE','UA',GETDATE(),'Administrador',2)
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

/*********************************************************/

SET @EACEID = 0
SET @ATTRID = 0
DECLARE @ATTROPTID INT

BEGIN TRANSACTION

SET @EACEID = ISNULL((SELECT ID FROM [EACElement] WHERE Name = 'AgreementEntity'),0)
IF NOT(@EACEID IS NULL) AND @EACEID > 0
BEGIN
	--///////////////////////////////////////////////////////////////////////////////////
	--//								AgreementEntity
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'AuthorizationTypeID' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'AuthorizationTypeID', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'InputMask' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'InputMask', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'IsChipCard' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'IsChipCard', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'StepToShow' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'StepToShow', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'RECEPTION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'RECEPTION',	'RECEPTION',	GETDATE(),	'Administrador',	2)
		END

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'ADMISSION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'ADMISSION',	'ADMISSION',	GETDATE(),	'Administrador',	2)
		END

	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ShowOnInvoice' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'ShowOnInvoice', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	
END

SET @EACEID = ISNULL((SELECT ID FROM [EACElement] WHERE Name = 'AssistanceAgreementEntity'),0)
IF NOT(@EACEID IS NULL) AND @EACEID > 0
BEGIN
	--///////////////////////////////////////////////////////////////////////////////////
	--//								AssistanceAgreementEntity
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'AuthorizationTypeID' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'AuthorizationTypeID', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'InputMask' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'InputMask', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'IsChipCard' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'IsChipCard', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'StepToShow' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'StepToShow', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'RECEPTION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'RECEPTION',	'RECEPTION',	GETDATE(),	'Administrador',	2)
		END

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'ADMISSION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'ADMISSION',	'ADMISSION',	GETDATE(),	'Administrador',	2)
		END

	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ShowOnInvoice' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'ShowOnInvoice', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	
END

SET @EACEID = ISNULL((SELECT ID FROM [EACElement] WHERE Name = 'InsurerAgreementEntity'),0)
IF NOT(@EACEID IS NULL) AND @EACEID > 0
BEGIN
	--///////////////////////////////////////////////////////////////////////////////////
	--//								InsurerAgreementEntity
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'AuthorizationTypeID' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'AuthorizationTypeID', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'InputMask' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'InputMask', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'IsChipCard' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'IsChipCard', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'StepToShow' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'StepToShow', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'RECEPTION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'RECEPTION',	'RECEPTION',	GETDATE(),	'Administrador',	2)
		END

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'ADMISSION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'ADMISSION',	'ADMISSION',	GETDATE(),	'Administrador',	2)
		END

	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ShowOnInvoice' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'ShowOnInvoice', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	
END

SET @EACEID = ISNULL((SELECT ID FROM [EACElement] WHERE Name = 'InsurerCoverAgreementEntity'),0)
IF NOT(@EACEID IS NULL) AND @EACEID > 0
BEGIN
	--///////////////////////////////////////////////////////////////////////////////////
	--//								InsurerCoverAgreementEntity
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'AuthorizationTypeID' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'AuthorizationTypeID', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'InputMask' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'InputMask', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'IsChipCard' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'IsChipCard', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'StepToShow' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'StepToShow', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'RECEPTION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'RECEPTION',	'RECEPTION',	GETDATE(),	'Administrador',	2)
		END

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'ADMISSION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'ADMISSION',	'ADMISSION',	GETDATE(),	'Administrador',	2)
		END

	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ShowOnInvoice' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'ShowOnInvoice', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	
END


COMMIT TRANSACTION

/*********************************************************/

SET @ID = 0
SET @AttributeID = 0
SET @EntityName='BatchMovementConfigDTO'
SET @ID = IsNull((SELECT [ID] FROM [EACElement] WHERE [Name]=@EntityName),0)
IF (@ID = 0)
BEGIN
	INSERT INTO [EACElement] ([ElementType], [Name], [ModuleName], [Description], [Status], [LastUpdated], [ModifiedBy])
	VALUES (2, @EntityName, '', 'Parámetros de configuración de movimientos de caja', 2, GetDate(), 'Administrador')
	SET @ID=(SELECT @@IDENTITY)

	INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
	[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
	[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
	VALUES (@ID, 'BatchMovementReasonAtInvoiceVoidPayment', 'Especifica el código de motivo de pago por defecto que se va a usar al realizar una anulación de cobro de factura',3, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'true', 0, -1, '', '', '', '101', '', 2, GetDate(), 'Administrador')
	SET @AttributeID=(SELECT @@IDENTITY)

	INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
	[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
	[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
	VALUES (@ID, 'BatchMovementReasonAtInvoicePayment', 'Especifica el código de motivo de pago por defecto que se va a usar al realizar un cobro de factura',3, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'true', 0, -1, '', '', '', '100', '', 2, GetDate(), 'Administrador')
	SET @AttributeID=(SELECT @@IDENTITY)
END
ELSE
BEGIN
	print 'La entidad ya existe'
END
go

UPDATE EACAttribute SET DesignRequired=1, [Required]=1 WHERE [Name]='BatchMovementReasonID' 
AND EACElementID IN (SELECT [ID] FROM EACElement WHERE [Name]='BatchMovementEntity')
go

/*********************************************************/

DECLARE @EACEID int
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
	
	---------------------------------------------------------------------------------------------
	---- FIN ATRIBUTO OtrasNotas
	---------------------------------------------------------------------------------------------
END


/*********************************************************/

IF (NOT EXISTS(SELECT [ID] FROM BatchMovementReason WHERE [Code]=100))
BEGIN
	INSERT INTO [dbo].[BatchMovementReason]
			   ([Code], [Reason], [BatchMovementReasonType], [Status], [LastUpdated], [ModifiedBy])
		 VALUES
			   ('100','Cobro de factura', 1, 1, GetDate(), 'Administrador')
END
ELSE
BEGIN
	print 'El motivo de movimiento ya existe.'
END
go

IF (NOT EXISTS(SELECT [ID] FROM BatchMovementReason WHERE [Code]=101))
BEGIN
	INSERT INTO [dbo].[BatchMovementReason]
			   ([Code], [Reason], [BatchMovementReasonType], [Status], [LastUpdated], [ModifiedBy])
		 VALUES
			   ('101','Anulación de cobro de factura', 2, 1, GetDate(), 'Administrador')
END
ELSE
BEGIN
	print 'El motivo de movimiento ya existe.'
END
go


/*********************************************************/