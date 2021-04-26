--//////////////////////////////////////////////////////////////////////////
--// SCRIPT DE CREACIÓN DE OBSERVACINES, PLANTILLA Y METADATOS PARA EL
--// INTERCAMBIO DE DATOS DE ANTECEDENTES MÉDICOS ENTRE INDIGO Y HCDIS
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
DECLARE @GenericCombo smallint
SET @GenericCombo = 2

DECLARE @TGenDT smallint
SET @TGenDT = 16
DECLARE @TGenST smallint
SET @TGenST = 8

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
DECLARE @Caracteres INT

DECLARE @Observaciones Table (Codigo NVARCHAR(50), Nombre NVARCHAR(100), Tipo SMALLINT, TGen SMALLINT, IndigoNombre NVARCHAR(100), Caracteres INT)
INSERT INTO @Observaciones (Codigo, Nombre, Tipo, TGen, IndigoNombre, Caracteres)
VALUES ('NOC_IDG601','Fecha Antecedente', @Generic, @TGenDT,'FECHA', 8)
INSERT INTO @Observaciones (Codigo, Nombre, Tipo, TGen, IndigoNombre,Caracteres)
VALUES ('NOC_IDG602','Tipo Antecedente', @GenericCombo, 0,'IDTIPO', 8)
INSERT INTO @Observaciones (Codigo, Nombre, Tipo, TGen, IndigoNombre, Caracteres)
VALUES ('NOC_IDG603','Antecedente', @Generic, @TGenST,'ANTECEDENTE', 200)



--//buscar metadato
SET @ElementID = ISNULL((SELECT TOP 1 ID FROM EACElement WHERE Name = 'IndigoInteropAttributes'),0)
SET @AttrID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'Antecedente' and EACElementID = @ElementID),0)
IF (@AttrID = 0)
BEGIN
	--//hay que registrar el atributo
	INSERT INTO [EACAttribute]
			([EACElementID],[Name],[Description],[Type],[TypeName],[ComponentType],[DesignRequired],
			[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
			[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],
			[LastUpdated],[ModifiedBy])
	VALUES(@ElementID, 'Antecedente',
	'En las opciones si indican los mapeos de observaciones. Descripción Nombre atributo Indigo, Value código de la observacion en HCDIS. En valor predeterminado nombre plantilla',
	3,'',0,0,0,1,0,0,1,1,0,-1,'','','','ANTECEDENTES (INDIGO)','',2,@Now, 'Administrador')
	SET @AttrID = (SELECT @@IDENTITY)
END
SET @ObsTypeID = ISNULL((SELECT TOP 1 ID FROM ObservationType WHERE AssignedCode = 'NOC'),0)
SET @TemplateID = ISNULL((SELECT TOP 1 ID FROM ObservationTemplate WHERE Name = 'ANTECEDENTES (INDIGO)'),0)
IF (@TemplateID =0)
BEGIN
	--//hay que crear la planitilla de datos las observaciones se registran posteriormente
	INSERT INTO [ObservationTemplate]
		([Name],[Description],[TemplateTitle],[ExportDocumentName],[TemplateEditionPresentation],
		[TemplateLayout],[TemplateLayoutItems],[TemplateViewResults],[TemplateViewResultItems],
		[TemplateCopy],[DocumentTemplateID],[RegistrationDateTime],[InUse],[InUseByCustomer],
		[Status],[ModifiedBy],[LastUpdated])
    VALUES('ANTECEDENTES (INDIGO)','ANTECEDENTES (INDIGO)','ANTECEDENTES','',
           4,2,0,0,0,0,0,@Now,0,0,3,'Administrador',@Now)
    SET @TemplateID = (SELECT @@IDENTITY)
END

DECLARE @InteresID INT
SET @InteresID = ISNULL((SELECT TOP 1 ID FROM SpecialCategory WHERE AssignedCode = 'INTERES'),0)


IF NOT(@AttrID =0) AND NOT(@ObsTypeID=0) AND NOT(@TemplateID = 0)
BEGIN
	DECLARE CURSOR_OBS CURSOR FOR
	SELECT Codigo, Nombre, Tipo, TGen, IndigoNombre, Caracteres
	FROM @Observaciones

	OPEN CURSOR_OBS
	FETCH NEXT FROM CURSOR_OBS
	INTO @Codigo, @Nombre, @Tipo, @TGen, @IndigoNombre,@Caracteres

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
				'', 0, @Tipo, @TGen, @Caracteres, 0, 
				0, 0, @Now, 0, 1, 0, 
				0,  0, @ObsSatus, @Now, 'Administrador')
			SET @ObsID = (SELECT @@IDENTITY)
			--// REGISTRO DE LAS OPCIONES
			IF NOT(@ObsID = 0) AND @Codigo = 'NOC_IDG602'
			BEGIN
				INSERT INTO [ObservationOption]
					([ObservationID],[Value],[Description],[IsDefault],[Status],[ModifiedBy],[LastUpdated])
				VALUES(@ObsID,'1','FAMILIAR',0,1,'Administrador', @Now)
				INSERT INTO [ObservationOption]
					([ObservationID],[Value],[Description],[IsDefault],[Status],[ModifiedBy],[LastUpdated])
				VALUES(@ObsID,'2','COMUN',0,1,'Administrador', @Now)
				INSERT INTO [ObservationOption]
					([ObservationID],[Value],[Description],[IsDefault],[Status],[ModifiedBy],[LastUpdated])
				VALUES(@ObsID,'3','ESPECIFICO',0,1,'Administrador', @Now)
			END
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
			IF NOT(@InteresID = 0) AND 
				NOT(EXISTS(SELECT ID FROM SpecialCategoryObsRel 
					WHERE ElementType = 1 AND ElementID = @ObsID AND SpecialCategoryID = @InteresID))
			BEGIN
				SET @Order = ISNULL((SELECT COUNT(ID) FROM SpecialCategoryObsRel 
				WHERE SpecialCategoryID = @InteresID),0) + 1 
				INSERT INTO [SpecialCategoryObsRel]
					([SpecialCategoryID],[Order],[ElementType],[ElementID],[HasCriterion],[HasNotification],
					[Status],[LastUpdated],[ModifiedBy])
				VALUES (@InteresID,@Order, 1 , @ObsID, 0, 0, 1, @Now,'Administrador')
			END
		END
		--// SIGUIENTE	
		FETCH NEXT FROM CURSOR_OBS
		INTO @Codigo, @Nombre, @Tipo, @TGen, @IndigoNombre, @Caracteres
	END
	CLOSE CURSOR_OBS
	DEALLOCATE CURSOR_OBS
END
ELSE
BEGIN
	Print 'ERROR EN LOS DATOS BASE PARA EL REGISTRO DE LA INFORMACIÓN. REVISE LOS DATOS MAESTROS REQUERIDOS'
END
