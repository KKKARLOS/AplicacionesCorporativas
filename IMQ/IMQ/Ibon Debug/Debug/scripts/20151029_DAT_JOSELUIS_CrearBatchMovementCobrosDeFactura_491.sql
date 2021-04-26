IF (NOT EXISTS(SELECT [ID] FROM BatchMovementReason WHERE [Code]=100))
BEGIN
	INSERT INTO [dbo].[BatchMovementReason]
			   ([Code], [Reason], [BatchMovementReasonType], [Status], [LastUpdated], [ModifiedBy])
		 VALUES
			   ('100','Cobro de factura', 1, 1, GetDate(), 'Administrador')
END
ELSE
BEGIN
	print 'El motivo de movimiento ya existe.'
END
go

IF (NOT EXISTS(SELECT [ID] FROM BatchMovementReason WHERE [Code]=101))
BEGIN
	INSERT INTO [dbo].[BatchMovementReason]
			   ([Code], [Reason], [BatchMovementReasonType], [Status], [LastUpdated], [ModifiedBy])
		 VALUES
			   ('101','Anulación de cobro de factura', 2, 1, GetDate(), 'Administrador')
END
ELSE
BEGIN
	print 'El motivo de movimiento ya existe.'
END
go