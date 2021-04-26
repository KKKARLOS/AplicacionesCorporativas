--//////////////////////////////////////////////////////////////////////////
--// SCRIPT DE CREACIÓN DE OBSERVACINES, PLANTILLA Y METADATOS PARA EL
--// INTERCAMBIO DE DATOS DE EVOLUTIVOS MÉDICOS ENTRE INDIGO Y HCDIS
--//////////////////////////////////////////////////////////////////////////
USE [HCDIS]


--// se busca un tipo concreto de observacion si no existe devuelve error
--// se busca el metadato concreto de interoperabilidad indigo si no existe devuelve error
--// se busca el atributo del metadato concreto de interoperabilidad indigo si no existe devuelve error
--// se busca una plantilla concreta si no existe devuelve error
--// se buscan las observaciones si no existen se crean
--// si existe se validan las observaciones relacionadas y se asignan de no estar
--// se busca el atributo del metadato concreto y se añaden las observaciones si no existen en las opciones
DECLARE @ObsTypeID INT
DECLARE @ObsID INT
DECLARE @TemplateID INT
DECLARE @ElementID INT
DECLARE @AttrID INT
DECLARE @AttrOptID INT

DECLARE @Generic smallint
SET @Generic = 1
DECLARE @TGenBool smallint
SET @TGenBool = 128
DECLARE @TGenDT smallint
SET @TGenDT = 16
DECLARE @TGenRT smallint
SET @TGenRT = 256

DECLARE @Now DATETIME
SET @Now = GETDATE()
DECLARE @ObsSatus SMALLINT
SET @ObsSatus = 3
 
DECLARE @Codigo NVARCHAR(50)
DECLARE @Nombre NVARCHAR(100)
DECLARE @Tipo SMALLINT
DECLARE @TGen SMALLINT
DECLARE @IndigoNombre NVARCHAR(100)
DECLARE @Order INT

DECLARE @Observaciones Table (Codigo NVARCHAR(50), Nombre NVARCHAR(100), Tipo SMALLINT, TGen SMALLINT, IndigoNombre NVARCHAR(100))
INSERT INTO @Observaciones (Codigo, Nombre, Tipo, TGen, IndigoNombre)
VALUES ('NOC_IDG400','Evolutivo', @Generic, @TGenRT,'ENTRADA')
INSERT INTO @Observaciones (Codigo, Nombre, Tipo, TGen, IndigoNombre)
VALUES ('NOC_IDG401','Resumen', @Generic, @TGenBool,'RESUMEN')
INSERT INTO @Observaciones (Codigo, Nombre, Tipo, TGen, IndigoNombre)
VALUES ('NOC_IDG402','No Valido', @Generic, @TGenBool,'NOVALIDO')
INSERT INTO @Observaciones (Codigo, Nombre, Tipo, TGen, IndigoNombre)
VALUES ('NOC_IDG403','Confidencial', @Generic, @TGenBool,'CONFIDENCIAL')
INSERT INTO @Observaciones (Codigo, Nombre, Tipo, TGen, IndigoNombre)
VALUES ('NOC_IDG404','Fecha Evolutivo', @Generic, @TGenDT,'FECHA')

--//buscar metadato
SET @ElementID = ISNULL((SELECT TOP 1 ID FROM EACElement WHERE Name = 'IndigoInteropAttributes'),0)
SET @AttrID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'Evolutivo' and EACElementID = @ElementID),0)
SET @ObsTypeID = ISNULL((SELECT TOP 1 ID FROM ObservationType WHERE AssignedCode = 'NOC'),0)
SET @TemplateID = ISNULL((SELECT TOP 1 ID FROM ObservationTemplate WHERE Name = 'EVOLUTIVO MEDICO (INDIGO)'),0)

IF NOT(@AttrID =0) AND NOT(@ObsTypeID=0) AND NOT(@TemplateID = 0)
BEGIN
	DECLARE CURSOR_OBS CURSOR FOR
	SELECT Codigo, Nombre, Tipo, TGen, IndigoNombre
	FROM @Observaciones

	OPEN CURSOR_OBS
	FETCH NEXT FROM CURSOR_OBS
	INTO @Codigo, @Nombre, @Tipo, @TGen, @IndigoNombre

	WHILE @@fetch_status = 0
	BEGIN
		SET @ObsID = ISNULL((SELECT TOP 1 ID FROM Observation WHERE [AssignedCode] = @Codigo),0)	
		IF @ObsID = 0
		BEGIN
		--//REGISTRO DE LA OBSERVACION
			INSERT INTO [Observation]
				([ObservationTypeID],[AssignedCode],[Name],[Description],[Encoder],[IsCodified],
				 [DocumentName],[PhysicalUnitID],[KindOf],[BasicType],[BasicTypeLength],[BasicTypeDecimalLength],
				 [GraphicColor],[ValidateCriteria],[RegistrationDateTime],[AncestorID],[InUse],[InUseByCustomer],
				 [IsCalculated],[ValidateOnDemand],[Status],[LastUpdated],[ModifiedBy])
			VALUES
				(@ObsTypeID, @Codigo, @Nombre, @Nombre, '', 0,
				'', 0, @Tipo, @TGen, 8, 0, 
				0, 0, @Now, 0, 1, 0, 
				0,  0, @ObsSatus, @Now, 'Administrador')
			SET @ObsID = (SELECT @@IDENTITY)
		END
		IF NOT(@ObsID = 0)
		BEGIN
			IF NOT(EXISTS(SELECT ID FROM ObservationTemplateRel WHERE ObservationTemplateID = @TemplateID 
					AND ObservationID = @ObsID))
			BEGIN
				SET @Order = ISNULL((SELECT COUNT(ID) FROM ObservationTemplateRel WHERE ObservationTemplateID = @TemplateID),0) + 1 
				INSERT INTO [ObservationTemplateRel]
					([ObservationTemplateID],[ElementType],[ObservationBlockID],[ObservationID],[ElementTitle],
					[ElementTitlePosition],[VisibleLabel],[Order],[Required],[Status],[ModifiedBy],[LastUpdated])
				VALUES
					(@TemplateID, 2, 0, @ObsID, @Nombre, 0, 1, @Order, 0, 1, 'Administrador', @Now)
			END
			IF NOT(EXISTS(SELECT ID FROM EACAttributeOption WHERE EACAttributeID = @AttrID 
					AND [Value] = @Codigo))
			BEGIN
				INSERT INTO [EACAttributeOption]
					([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			     VALUES 
					(@AttrID,@IndigoNombre,@Codigo,@Now,'Administrador',2)
			END
		END
		--// SIGUIENTE	
		FETCH NEXT FROM CURSOR_OBS
		INTO @Codigo, @Nombre, @Tipo, @TGen, @IndigoNombre
	END
	CLOSE CURSOR_OBS
	DEALLOCATE CURSOR_OBS
END
ELSE
BEGIN
	Print 'ERROR EN LOS DATOS BASE PARA EL REGISTRO DE LA INFORMACIÓN. REVISE LOS DATOS MAESTROS REQUERIDOS'
END
