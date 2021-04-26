
--PROCESO ORIGEN          PROCESO DESTINO         Procedencia del  ingreso
--10 HOSPITAL DE DIA       2 INGRESOS                       Hospital de día (con su código de observación correspondiente)
--18 AMBULATORIO          2 INGRESOS                       Ambulante (con su código de observación correspondiente y que ahora mismo no existe)



CREATE TABLE [HCDIS_LOCAL].[dbo].[TrasladoProcesos]
(

	ID int IDENTITY(1,1),
	IDProcesoOrigen int ,
	ProcesoOrigenName varchar(255),
	IDProcesoDestino int,
	ProcesoDestinoName varchar(255),
	ProcedenciadelIngreso varchar(255),
	PRIMARY KEY (ID)

);

  insert into [HCDIS_LOCAL].[dbo].[TrasladoProcesos]
  ([IDProcesoOrigen],[ProcesoOrigenName] ,[IDProcesoDestino],[ProcesoDestinoName],[ProcedenciadelIngreso])
  VALUES(10,'HOSPITAL DE DIA', 2, 'INGRESOS', 'Hospital de día')
  
    insert into [HCDIS_LOCAL].[dbo].[TrasladoProcesos]
  ([IDProcesoOrigen],[ProcesoOrigenName] ,[IDProcesoDestino],[ProcesoDestinoName],[ProcedenciadelIngreso])
  VALUES(18,'AMBULATORIO', 2, 'INGRESOS', 'Ambulante')
  
  
CREATE TABLE [HCDIS_LOCAL].[dbo].[TrasladoProcesosLog]
(

	ID int IDENTITY(1,1),
	IDProcesoOrigen int ,
	IDProcesoDestino int,
	CustomerProcessId int,
	CustomerEpisodeId int,
	IDServicioOrigen int,
	IDServicioDestino int,
	IDMedicoOrigen int,
	IDMedicoDestino int,
	LastUpdated DATETIME,
	ModifiedBy nvarchar(256),
	PRIMARY KEY (ID)

);


  