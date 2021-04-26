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