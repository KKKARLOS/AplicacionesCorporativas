--////////////////////////////////
--//
--// objetivo poder identificar qué unidades de enfermería corresponden a la UCI
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
			'Indica en las opciones quienes serán las ubicaciones unidades de enfermería de UCI.',
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