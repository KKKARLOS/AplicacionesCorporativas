DECLARE @ID int
DECLARE @AttributeID int
DECLARE @EntityName nvarchar(256)
SET @EntityName='ReceptionAdditionalInfoEntity'
SET @ID = IsNull((SELECT [ID] FROM [EACElement] WHERE [Name]=@EntityName),0)
IF (@ID > 0)
BEGIN
	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@ID AND [Name]='LeaveVisit'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@ID, 'LeaveVisit', 'Fecha/Hora salida de consulta',4, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'true', 0, -1, '', '', '', '', '', 2, GetDate(), 'Administrador')
	END
	ELSE
	BEGIN
		print 'El atributo LeaveVisit ya existe.'
	END

	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@ID AND [Name]='EnterVisit'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@ID, 'EnterVisit', 'Fecha/Hora entrada en consulta',4, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'true', 0, -1, '', '', '', '', '', 2, GetDate(), 'Administrador')
	END
	ELSE
	BEGIN
		print 'El atributo EnterVisit ya existe.'
	END

	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@ID AND [Name]='WaitingNumber'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@ID, 'WaitingNumber', '',3, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'true', 50, -1, '', '', '', '', '', 2, GetDate(), 'Administrador')
	END
	ELSE
	BEGIN
		print 'El atributo WaitingNumber ya existe.'
	END

	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@ID AND [Name]='CallingDeviceID'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@ID, 'CallingDeviceID', '',0, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'true', 0, -1, '', '', '', '', '', 2, GetDate(), 'Administrador')
	END
	ELSE
	BEGIN
		print 'El atributo CallingDeviceID ya existe.'
	END
END
ELSE
BEGIN
	print 'La entidad ReceptionAdditionalInfoEntity no existe'
END