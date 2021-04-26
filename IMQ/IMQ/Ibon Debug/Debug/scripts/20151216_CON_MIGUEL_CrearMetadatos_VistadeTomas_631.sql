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
		'Para la configuración de valores de la Hoja de tomas de Prescripciones',
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
			'Indica los días que permite moverse hacia el futuro. Por defecto 1',
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
			'Indica el intervalo de horas que permite editar las tomas. Por defecto 2. En opciones por unidad de enfermería Descrtipcion Name, Value horas.',
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
			'Indica la horas (en 0..23) de inicio de presentación de las tomas. Por defecto 0. En opciones por unidad de enfermería Descrtipcion Name, Value horas.',
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
			'Indica el intervalo de horas hacia atrás que permite editar las tomas. Por defecto 2. En opciones por unidad de enfermería Descrtipcion Name, Value horas.',
			0, '', 0, 'false', 
			'false', 'true', 'false', 'false','false', 'false', 0, -1, 
			'', '', '', '2', '', 2, GetDate(), 'Administrador')
		SET @AttributeID=(SELECT @@IDENTITY)
	END
END
