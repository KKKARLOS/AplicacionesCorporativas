--////////////////////////////////////////////////////////////////////////
--//
--// IMORTANTE: ESTE SCRIPT ES PARA LA BD DE SECURITY
--// INDICAR AL ROLE OYS DERECHOS DE REALIZACION Y REPORTES EN RX
--// CON ELLO EL USUARIO CON ROL OYS PODRÁ EJECUTAR LA REALIZACIÓN Y CREAR 
--// REPORTS AUNQUE SE ESTÉ EN INTEROPERABILIDAD CON AGFA
--//
--////////////////////////////////////////////////////////////////////////
USE HCDISSecurity

DECLARE @RoleID INT
DECLARE @FunctionName nvarchar(256)

SET @RoleID = ISNULL((SELECT TOP 1 [ID] FROM Roles WHERE RoleName = 'OYS'),0)
IF NOT(@RoleID = 0)
BEGIN
	IF NOT(EXISTS(SELECT ID FROM RoleFunctions WHERE RoleID = @RoleID AND [Function] = 'AllowAgfaRealization'))
	BEGIN
		INSERT INTO [RoleFunctions] ([RoleID],[Function])
		VALUES (@RoleID, 'AllowAgfaRealization')
	END
	ELSE
		Print 'La funcion  AllowAgfaRealization, ya está asociada al role OYS'
		
	IF NOT(EXISTS(SELECT ID FROM RoleFunctions WHERE RoleID = @RoleID AND [Function] = 'AllowAgfaReports'))
	BEGIN
		INSERT INTO [RoleFunctions] ([RoleID],[Function])
		VALUES (@RoleID, 'AllowAgfaReports')
	END
	ELSE
		Print 'La funcion  AllowAgfaReports, ya está asociada al role OYS'
END
ELSE
	Print 'El Role OYS no está registrado en la BD'
