--////////////////////////////////////////////////////////////////////////
--//
--// IMORTANTE: ESTE SCRIPT ES PARA LA BD DE SECURITY
--// INDICAR AL ROLE ADMCCEE DERECHOS DE ANULAR ANALISIS DE COBERTURAS
--// CON ELLO SE PODRA REALIZAR LA ACCION DE ANULAR SALIDA DE VISITA
--//
--////////////////////////////////////////////////////////////////////////
USE HCDISSecurity

DECLARE @RoleID INT
DECLARE @FunctionName nvarchar(256)

SET @RoleID = ISNULL((SELECT TOP 1 [ID] FROM Roles WHERE RoleName = 'ADMCCEE'),0)
IF NOT(@RoleID = 0)
BEGIN
	IF NOT(EXISTS(SELECT ID FROM RoleFunctions WHERE RoleID = @RoleID AND [Function] = 'CustomerUndoDeliveryNote'))
	BEGIN
		INSERT INTO [RoleFunctions] ([RoleID],[Function])
		VALUES (@RoleID, 'CustomerUndoDeliveryNote')
	END
	ELSE
		Print 'La funcion  , ya está asociada al role ADMCCEE'
END
ELSE
	Print 'El Role ADMCCEE no está registrado en la BD'
