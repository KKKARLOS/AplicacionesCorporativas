DECLARE @ID int
DECLARE @AttributeID int
DECLARE @EntityName nvarchar(256)
SET @EntityName='ChargesToInvoiceListViewDTO'
SET @ID = IsNull((SELECT [ID] FROM [EACElement] WHERE [Name]=@EntityName),0)
IF (@ID = 0)
BEGIN
	INSERT INTO [EACElement] ([ElementType], [Name], [ModuleName], [Description], [Status], [LastUpdated], [ModifiedBy])
	VALUES (2, @EntityName, '', 'La configuraci�n de los metadatos de DTO para la vista ChargesToInvoiceListView', 2, GetDate(), 'Administrador')
	SET @ID=(SELECT @@IDENTITY)

	INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
	[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
	[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
	VALUES (@ID, 'FiltroFechaAltaHasta', 'Indica si el filtro de fecha hasta de alta filtra por hoy o por ayer
True = Hoy
False = ayer ',5, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'true', 0, -1, '', '', '', 'true', '', 2, GetDate(), 'Administrador')
	SET @AttributeID=(SELECT @@IDENTITY)

	INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
	[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
	[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
	VALUES (@ID, 'FiltroFechaAltaDesde', 'Nos dice desde que fecha de alta nos va a mostrar el censo de facturaci�n por cargos',4, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'true', 0, -1, '', '', '', '14/11/2015', '', 2, GetDate(), 'Administrador')
	SET @AttributeID=(SELECT @@IDENTITY)

	INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
	[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
	[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
	VALUES (@ID, 'AutoRefresh', 'Indica cada cuantos minutos se autorefresca el censo de facturacion por cargo',0, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'true', 0, -1, '', '', '', '15', '', 2, GetDate(), 'Administrador')
	SET @AttributeID=(SELECT @@IDENTITY)
END
ELSE
BEGIN
	print 'La entidad ya existe'
END

GO 

/***********************************************************************************/

--////////////////////////////////
--//
--// objetivo poder identificar qu� telefonos se muestran en el Registro de pacientes
--//
--///////////////////////////////

DECLARE @ID int
DECLARE @AttributeID int
DECLARE @EntityName nvarchar(256)
SET @EntityName='CustomerRegistrationWizardDTO'
SET @ID = IsNull((SELECT [ID] FROM [EACElement] WHERE [Name]=@EntityName),0)
IF (@ID > 0)
BEGIN
	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@ID 
	AND [Name]='IncludeTelephoneTypes'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], 
		[Description], 
		[Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@ID, 'IncludeTelephoneTypes', 
			'En las opciones se indican qu� tipo telefonos ser�n incluidos.',
			1, '', 0, 'false', 
			'false', 'true', 'false', 'false','true', 'true', 0, -1, 
			'', '', '', '', '', 2, GetDate(), 'Administrador')
		SET @AttributeID=(SELECT @@IDENTITY)

	END
	IF NOT(@AttributeID IS NULL) AND @AttributeID > 0
	BEGIN
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @AttributeID 
			AND [Description] ='Tel�fono1'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@AttributeID,'Tel�fono1','Tel�fono1',GETDATE(),'Administrador',2)
		END
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @AttributeID 
			AND [Description] ='Tel�fono2'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@AttributeID,'Tel�fono2','Tel�fono2',GETDATE(),'Administrador',2)
		END
	END
END
ELSE
BEGIN
	print 'La entidad CustomerRegistrationWizardDTO no existe'
END

GO

/****************************************************************************************/

--////////////////////////////////
--//
--// objetivo poder identificar qu� unidades de enfermer�a corresponden a la UCI
--//
--///////////////////////////////

DECLARE @ID int
DECLARE @AttributeID int
DECLARE @EntityName nvarchar(256)
SET @EntityName='CommonInteropAttributes'
SET @ID = ISNULL((SELECT [ID] FROM [EACElement] WHERE [Name]=@EntityName),0)
IF (@ID > 0)
BEGIN
	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@ID 
	AND [Name]='ICUPointOfCare'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], 
		[Description], 
		[Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@ID, 'ICUPointOfCare', 
			'Indica en las opciones quienes ser�n las ubicaciones unidades de enfermer�a de UCI.',
			1, '', 0, 'false', 
			'false', 'true', 'false', 'false','true', 'true', 0, -1, 
			'', '', '', '', '', 2, GetDate(), 'Administrador')
		SET @AttributeID=(SELECT @@IDENTITY)
	END
	IF NOT(@AttributeID IS NULL) AND @AttributeID > 0
	BEGIN
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @AttributeID 
			AND [Description] ='UCI'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@AttributeID,'UCI','UCI',GETDATE(),'Administrador',2)
		END
	END
END
ELSE
BEGIN
	print 'La entidad CommonInteropAttributes no existe'
END

GO

/**********************************************************************************/

--////////////////////////////////
--//
--// MODIFICAR TABLA DE PRESCRIPCIONES PARA A�ADIR 
--// LAS DESCRIPCIONES DE LAS FRECUENCIAS QUE VIENEN DE EFARMACO
--//
--///////////////////////////////////
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PrescriptionRequest' and c.name = 'FrequencyDescription' and o.type ='U'))
BEGIN
	ALTER TABLE PrescriptionRequest ADD	[FrequencyDescription] [nvarchar](max) NULL
END


/**********************************************************************************/

DECLARE @ID int
DECLARE @AttributeID int
DECLARE @EntityName nvarchar(256)
SET @EntityName='ActivityControlWizardDTO'
SET @ID = IsNull((SELECT [ID] FROM [EACElement] WHERE [Name]=@EntityName),0)
IF (@ID > 0)
BEGIN
	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@ID 
	AND [Name]='ProcessChartName_FallidaNotAvailable'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], 
		[Description], 
		[Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@ID, 'ProcessChartName_FallidaNotAvailable', 
			'Nombre del proceso para el que no se habilita bot�n de marcar realizaci�n como Fallida.',
			3, '', 0, 'false', 
			'false', 'false', 'false', 'false','true', 'true', 0, -1, 
			'', '', '', 'FISIOTERAPIA', '', 2, GetDate(), 'Administrador')
		SET @AttributeID=(SELECT @@IDENTITY)

	END
END
ELSE
BEGIN
	print 'La entidad ActivityControlWizardDTO no existe'
END

GO


/**************************************************************************************/

--////////////////////////////////
--//
--// objetivo es registrar los metadatos para la vista de registro de tomas
--//
--///////////////////////////////

DECLARE @EACEID int
DECLARE @AttributeID int
DECLARE @EntityName nvarchar(256)
SET @EntityName='NurseStationPrescriptionWizardDTO'
SET @EACEID = ISNULL((SELECT [ID] FROM [EACElement] WHERE [Name]=@EntityName),0)
IF (@EACEID = 0)  
BEGIN
	--//SI NO EXISTE SE REGISTRA EL METADATO
	INSERT INTO [EACElement]
	([ElementType],[Name],[ModuleName],[Description],[Status],[LastUpdated],[ModifiedBy])
	VALUES (2, 'NurseStationPrescriptionWizardDTO','',
		'Para la configuraci�n de valores de la Hoja de tomas de Prescripciones',
		2, GETDATE(), 'Administrador')
	SET @EACEID = (SELECT @@IDENTITY)	
END
IF (@EACEID > 0)
BEGIN
	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@EACEID 
			AND [Name]='DateInterval'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], 
		[Description], 
		[Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@EACEID, 'DateInterval', 
			'Indica los d�as que permite moverse hacia el futuro. Por defecto 1',
			0, '', 0, 'false', 
			'false', 'false', 'false', 'false','false', 'false', 0, -1, 
			'', '', '', '1', '', 2, GetDate(), 'Administrador')
		SET @AttributeID=(SELECT @@IDENTITY)
	END
	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@EACEID 
			AND [Name]='HourInterval'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], 
		[Description], 
		[Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@EACEID, 'HourInterval', 
			'Indica el intervalo de horas que permite editar las tomas. Por defecto 2. En opciones por unidad de enfermer�a Descrtipcion Name, Value horas.',
			0, '', 0, 'false', 
			'false', 'true', 'false', 'false','false', 'false', 0, -1, 
			'', '', '', '2', '', 2, GetDate(), 'Administrador')
		SET @AttributeID=(SELECT @@IDENTITY)
	END
	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@EACEID 
			AND [Name]='StartShiftHour'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], 
		[Description], 
		[Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@EACEID, 'StartShiftHour', 
			'Indica la horas (en 0..23) de inicio de presentaci�n de las tomas. Por defecto 0. En opciones por unidad de enfermer�a Descrtipcion Name, Value horas.',
			0, '', 0, 'false', 
			'false', 'true', 'false', 'false','false', 'false', 0, -1, 
			'', '', '', '0', '', 2, GetDate(), 'Administrador')
		SET @AttributeID=(SELECT @@IDENTITY)
	END
	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@EACEID 
			AND [Name]='BackHourInterval'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], 
		[Description], 
		[Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@EACEID, 'BackHourInterval', 
			'Indica el intervalo de horas hacia atr�s que permite editar las tomas. Por defecto 2. En opciones por unidad de enfermer�a Descrtipcion Name, Value horas.',
			0, '', 0, 'false', 
			'false', 'true', 'false', 'false','false', 'false', 0, -1, 
			'', '', '', '2', '', 2, GetDate(), 'Administrador')
		SET @AttributeID=(SELECT @@IDENTITY)
	END
END


GO

/**********************************************************************************************************/
--////////////////////////////////
--//
--// MODIFICAR TABLA DE ItemTreatmentOrderSequence PARA A�ADIR 
--// EL MEANING DEL INTERVALO DE CADA PERFUSION RequestedGivePerTimeUnitMeaning 
--// QUE VIENE DE EFARMACO Y QUE SE ASOCIA A LA BASE.
--//
--///////////////////////////////////

IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'ItemTreatmentOrderSequence' and c.name = 'RequestedGivePerTimeUnitMeaning' and o.type ='U'))
BEGIN
	ALTER TABLE ItemTreatmentOrderSequence ADD	[RequestedGivePerTimeUnitMeaning] [nvarchar](max) NULL
END


/**********************************************************************************************************/


--/////////////////////////////////////////////////////////////////
-- Este script a�ade una nueva columna a la tabla ItemAccountRel.
-- Valida que la columna no exista antes de crearla.
-- Miguel Angel.
--/////////////////////////////////////////////////////////////////

IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'ItemAccountRel' and c.name = 'CtaIngreso' and o.type ='U'))
BEGIN
	Print 'Creando la columna CtaIngreso en tabla ItemAccountRel.'
	ALTER TABLE dbo.ItemAccountRel ADD CtaIngreso VARCHAR(50) NULL;
END
ELSE 
	Print 'La columna CtaIngreso ya existe en tabla ItemAccountRel.'