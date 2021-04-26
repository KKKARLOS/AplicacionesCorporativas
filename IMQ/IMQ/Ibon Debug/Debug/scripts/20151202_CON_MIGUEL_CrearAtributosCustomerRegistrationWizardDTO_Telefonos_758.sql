--////////////////////////////////
--//
--// objetivo poder identificar qué telefonos se muestran en el Registro de pacientes
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
			'En las opciones se indican qué tipo telefonos serán incluidos.',
			1, '', 0, 'false', 
			'false', 'true', 'false', 'false','true', 'true', 0, -1, 
			'', '', '', '', '', 2, GetDate(), 'Administrador')
		SET @AttributeID=(SELECT @@IDENTITY)

	END
	IF NOT(@AttributeID IS NULL) AND @AttributeID > 0
	BEGIN
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @AttributeID 
			AND [Description] ='Teléfono1'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@AttributeID,'Teléfono1','Teléfono1',GETDATE(),'Administrador',2)
		END
		IF NOT(EXISTS(SELECT ID FROM [EACAttributeOption] WHERE [EACAttributeID] = @AttributeID 
			AND [Description] ='Teléfono2'))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES(@AttributeID,'Teléfono2','Teléfono2',GETDATE(),'Administrador',2)
		END
	END
END
ELSE
BEGIN
	print 'La entidad CustomerRegistrationWizardDTO no existe'
END