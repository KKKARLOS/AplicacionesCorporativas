DECLARE @ID int
DECLARE @AttributeID int
DECLARE @EntityName nvarchar(256)
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