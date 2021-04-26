USE [HCDISSecurity]
IF (NOT EXISTS(SELECT [Jerarquia] FROM [RoleFunctionsJerarquia] WHERE [RolFuncion] = 'CustomerOrderRequestListModifyFilters'))
BEGIN
	INSERT INTO [RoleFunctionsJerarquia] 
		([Jerarquia],[RolFuncion],[RolDescripcion]) 
		VALUES ('Módulos/HCDIS Seguridad/Procesos Asistenciales/Información Común para Procesos Asistenciales/','CustomerOrderRequestListModifyFilters','Permitir la modificación de los filtros en la Lista de Solicitudes')
END
ELSE
BEGIN
	print 'La función de permisos "CustomerOrderRequestListModifyFilters" ya existe en la tabla [RoleFunctionsJerarquia].'
END
go