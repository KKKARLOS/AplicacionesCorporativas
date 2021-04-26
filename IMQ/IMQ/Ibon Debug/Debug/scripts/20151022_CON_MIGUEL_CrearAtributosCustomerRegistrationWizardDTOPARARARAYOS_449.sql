--////////////////////////////////
--//
--// objetivo poder identificar cuando estamos en un proeso ce Radiologia
--//
--///////////////////////////////

DECLARE @ID int
DECLARE @AttributeID int
DECLARE @EntityName nvarchar(256)
SET @EntityName='CustomerReceptionWizardDTO'
SET @ID = IsNull((SELECT [ID] FROM [EACElement] WHERE [Name]=@EntityName),0)
IF (@ID > 0)
BEGIN
	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@ID 
	AND [Name]='RxProcess'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@ID, 'RxProcess', 
			'En valor por defecto indica el nombre del proceso de Radiología.',
			1, '', 0, 'false', 'true', 'false', 'false', 'false', 
			'true', 'true', 0, -1, '', '', '', 'RADIOLOGIA', '', 2, GetDate(), 'Administrador')
		SET @AttributeID=(SELECT @@IDENTITY)

	END
	ELSE
	BEGIN
		print 'El atributo RxProcess ya existe.'
	END


END
ELSE
BEGIN
	print 'La entidad CustomerRegistrationWizardDTO no existe'
END