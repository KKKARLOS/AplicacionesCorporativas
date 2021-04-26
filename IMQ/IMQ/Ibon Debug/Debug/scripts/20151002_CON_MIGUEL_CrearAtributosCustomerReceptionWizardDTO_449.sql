--////////////////////////////////////////////////////////////
--//
--// EL OBJETO DE ESTE ATRIBUTO ES PODER RELACIONAR LOS MÉDICOS
--// DE REHABILITACION CON LAS UBICACIONES DE LAS CONSULTAS
--// HASTA QUE SE ELABOREN LOS DESARROLLOS CORRESPONDIENTES EN HCDIS
--//
--////////////////////////////////////////////////////////////
DECLARE @ID int
DECLARE @AttributeID int
DECLARE @EntityName nvarchar(256)
SET @EntityName='CustomerReceptionWizardDTO'
SET @ID = IsNull((SELECT [ID] FROM [EACElement] WHERE [Name]=@EntityName),0)
IF (@ID > 0)
BEGIN
	SET @AttributeID=IsNull((SELECT [ID] FROM [EACAttribute] WHERE [EACElementID]=@ID AND [Name]='RehabRelMedicoConsulta'),0)
	IF @AttributeID = 0
	BEGIN
		INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], 
		[Type], [TypeName], [ComponentType], [DesignRequired],
		[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
		[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
		VALUES (@ID, 'RehabRelMedicoConsulta', 
		'Relacion Medico-Consulta en Rehabiliotacion. Valor por defecto nombre proceso. En opciones Valor Ubicacion, Descripcion Identificador medico. Se toma el HHRRFileNumber que es único.',
		0, '', 0, 'false', 
		'true', 'true', 'false', 'false', 'true', 'true', 0, -1, '', 
		'', '', 'REHABILITACION', '', 2, GetDate(), 'Administrador')
		SET @AttributeID=(SELECT @@IDENTITY)

		INSERT INTO [EACAttributeOption] ([EACAttributeID], [Description], [Value], [LastUpdated], [ModifiedBy], [Status])
		VALUES (@AttributeID, '267202', 'Socias Amezua, Luis', GetDate(), 'Administrador', 2)
		INSERT INTO [EACAttributeOption] ([EACAttributeID], [Description], [Value], [LastUpdated], [ModifiedBy], [Status])
		VALUES (@AttributeID, '267231', 'Urruticoechea Aguirre, Jose Mª', GetDate(), 'Administrador', 2)
		
	END
	ELSE
	BEGIN
		print 'El atributo RehabRelMedicoConsulta ya existe.'
	END

END
ELSE
BEGIN
	print 'La entidad CustomerReceptionWizardDTO no existe'
END