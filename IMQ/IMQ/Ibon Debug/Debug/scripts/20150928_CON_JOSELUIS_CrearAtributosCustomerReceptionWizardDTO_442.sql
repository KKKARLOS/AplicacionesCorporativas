DECLARE @ID int
DECLARE @AttributeID int
DECLARE @EntityName nvarchar(256)
SET @EntityName='CustomerReceptionWizardDTO'
SET @ID = IsNull((SELECT [ID] FROM [EACElement] WHERE [Name]=@EntityName),0)
IF (@ID > 0)
BEGIN
	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@ID AND [Name]='EnterVisit'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@ID, 'EnterVisit', 'Entrada en la consulta',0, '', 0, 'false', 'true', 'true', 'false', 'false', 'true', 'true', 0, -1, '', '', '', '', '', 2, GetDate(), 'Administrador')
		SET @AttributeID=(SELECT @@IDENTITY)

		INSERT INTO [EACAttributeOption] ([EACAttributeID], [Description], [Value], [LastUpdated], [ModifiedBy], [Status])
		VALUES (@AttributeID, 'Entrada en la consulta', 'REHABILITACION', GetDate(), 'Administrador', 2)
	END
	ELSE
	BEGIN
		print 'El atributo EnterVisit ya existe.'
	END

	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@ID AND [Name]='LeaveVisit'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@ID, 'LeaveVisit', 'Salida de la consulta',3, '', 0, 'false', 'true', 'true', 'false', 'false', 'true', 'true', 0, -1, '', '', '', '', '', 2, GetDate(), 'Administrador')
		SET @AttributeID=(SELECT @@IDENTITY)

		INSERT INTO [EACAttributeOption] ([EACAttributeID], [Description], [Value], [LastUpdated], [ModifiedBy], [Status])
		VALUES (@AttributeID, 'Salida de la consulta', 'REHABILITACION', GetDate(), 'Administrador', 2)
	END
	ELSE
	BEGIN
		print 'El atributo LeaveVisit ya existe.'
	END
	
	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@ID AND [Name]='RehabRequest'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@ID, 'RehabRequest', 'Cita Rehabilitación',3, '', 0, 'false', 'true', 'true', 'false', 'false', 'true', 'true', 0, -1, '', '', '', '', '', 2, GetDate(), 'Administrador')
		SET @AttributeID=(SELECT @@IDENTITY)

		INSERT INTO [EACAttributeOption] ([EACAttributeID], [Description], [Value], [LastUpdated], [ModifiedBy], [Status])
		VALUES (@AttributeID, 'Cita Rehabilitación', 'FISIOTERAPIA', GetDate(), 'Administrador', 2)
	END
	ELSE
	BEGIN
		print 'El atributo RehabRequest ya existe.'
	END

	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@ID AND [Name]='RequestingAssistanceServiceID'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@ID, 'RequestingAssistanceServiceID', 'Servicio asistencial solicitante',3, '', 0, 'false', 'true', 'false', 'false', 'false', 'false', 'false', 0, -1, '', 'paRequestingService,ucbeRequestingService,ulaRequestingService', '', '', '', 2, GetDate(), 'Administrador')
	END
	ELSE
	BEGIN
		print 'El atributo RequestingAssistanceServiceID ya existe.'
	END
END
ELSE
BEGIN
	print 'La entidad CustomerReceptionWizardDTO no existe'
END
