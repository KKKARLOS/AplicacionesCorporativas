DECLARE @itemId INT;
DECLARE @administrationRouteID INT;
DECLARE vias_administracion CURSOR
FOR SELECT ID, AdministrationRouteID
FROM ITEM WHERE AdministrationRouteID IS NOT NULL AND AdministrationRouteID>0
FOR READ ONLY;
OPEN vias_administracion
FETCH NEXT FROM vias_administracion 
INTO @itemId, @administrationRouteID
WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO [IB_IMQ_ViasAdministracion] 
	([ItemId], [AdministrationRouteID],	[ViaPrincipal], [LastUpdated], [ModifiedBy])
	VALUES(@itemId, @administrationRouteID,1,GETDATE(),'CARGA_INICIAL')
	FETCH NEXT FROM vias_administracion 
	INTO @itemId, @administrationRouteID
END
CLOSE vias_administracion;
DEALLOCATE vias_administracion;
