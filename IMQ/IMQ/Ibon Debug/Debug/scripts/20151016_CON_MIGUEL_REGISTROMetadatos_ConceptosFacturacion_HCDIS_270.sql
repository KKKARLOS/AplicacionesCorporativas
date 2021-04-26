--//////////////////////////////////////////////////////////////////////////
--// SCRIPT DE CREACIÓN DEL CONTEXTO PARA EL DICCIONARIO DE CONCEPTOS DE FACTURACION 
--// ELEMENTO Y CONTEXTO TENDRAN COMO NOMBRE = "CONCEPTOS FACTURACION"
--//////////////////////////////////////////////////////////////////////////

--/////////////////////////////////////////////////////////////////////////
--//
--//								IMPORTANTE
--// HAY QUE DISPONER DE LOS CODIFICADORES DE IMQ PREVIAMENTE EN LA BD HCDISENCODERS 
--//
--/////////////////////////////////////////////////////////////////////////

USE [HCDIS]

--// se busca el metadato concreto si no existe devuelve error
--// se busca en enconder context asociado, si no existe se crea
--// se buscan los concept asociads por cada encoder correspondiente a un concepto de facturacion si no existe se crea
--// si el concepto no existe en la excel se crea uno con el encoder en nombre y descripcion

DECLARE @Now DATETIME
DECLARE @ModifiedBy NVARCHAR(256)
SET @Now = GETDATE()
SET @ModifiedBy = 'Administrador' 

DECLARE @EncoderNombre NVARCHAR(100)
 
DECLARE @Codigo NVARCHAR(200)
DECLARE @Nombre NVARCHAR(1024)
DECLARE @ElementID INT
DECLARE @EncoderContextID INT
DECLARE @EncoderFlag BIGINT 
DECLARE @EncoderID INT 

--// en los encoder flags   tabla encoder se pondran los diferentes tipos
--// aquellos que no estén en la excel tendrá encoder flag  = 0 

--//buscar o registrar metadato
SET @ElementID = ISNULL((SELECT TOP 1 ID FROM EACElement WHERE Name = 'CONCEPTOS FACTURACION'),0)
IF (@ElementID = 0)
BEGIN
	INSERT INTO [EACElement]
		([ElementType],[Name],[ModuleName],[Description],[Status],[LastUpdated],[ModifiedBy])
     VALUES
		(3,'CONCEPTOS FACTURACION','','Encoder CONCEPTOS FACTURACION',2,@Now,@ModifiedBy)
	SET @ElementID = (SELECT @@IDENTITY)
END
--//buscar o registrs encodercontext
SET @EncoderContextID = ISNULL((SELECT TOP 1 ID FROM EncoderContext WHERE EACElementID = @ElementID),0)
IF (@EncoderContextID = 0) AND NOT(@ElementID =0)
BEGIN
	INSERT INTO [EncoderContext]
		([Name],[Description],[ProviderName],[EACElementID],[CurrentVersion],[InUse],
		[Status],[LastUpdated],[ModifiedBy])
     VALUES 
		('CONCEPTOS FACTURACION','CONCEPTOS FACTURACION','EncoderProvider.ConceptFact',@ElementID,1.00,1,
		1, @Now, @ModifiedBy)
	SET @EncoderContextID = (SELECT @@IDENTITY)
END

IF NOT(@ElementID =0) AND NOT(@EncoderContextID = 0)
BEGIN
	DECLARE CURSOR_CF CURSOR FOR
	SELECT RTRIM(LTRIM(CODIGO)), RTRIM(LTRIM(DESCRIPCION)), RTRIM(LTRIM(TIPO))
	FROM [HCDISEncoders]..[ConceptosFacturacion]

	OPEN CURSOR_CF
	FETCH NEXT FROM CURSOR_CF
	INTO @Codigo, @Nombre, @EncoderNombre

	WHILE @@fetch_status = 0
	BEGIN
		SET @EncoderID = ISNULL((SELECT TOP 1 ID FROM Encoder WHERE EncoderContextID = @EncoderContextID AND Name = @EncoderNombre),0)	
		IF @EncoderID = 0
		BEGIN
			--//REGISTRO DEL ENCODER
			SET @EncoderFlag = ISNULL((SELECT TOP 1 MAX(EncoderFlag) FROM Encoder),0)
			IF @EncoderFlag = 0 
			BEGIN
				SET @EncoderFlag = 1
			END
			ELSE
			BEGIN
				SET @EncoderFlag = @EncoderFlag * 2
			END
			INSERT INTO [Encoder]
				([EncoderContextID],[EncoderFlag],[Name],[Description],[Filter],
				[Mode],[Version],[InUse],[Status],[LastUpdated],[ModifiedBy])
			VALUES
				(@EncoderContextID, @EncoderFlag, @EncoderNombre,@EncoderNombre,'',0,1,0,1,@Now, @ModifiedBy)
			SET @EncoderID = (SELECT @@IDENTITY)
		END
		ELSE
			SET @EncoderFlag = ISNULL((SELECT TOP 1 EncoderFlag FROM Encoder WHERE ID =@EncoderID),0)
		IF NOT(@EncoderID = 0)
		BEGIN
			IF NOT(EXISTS(SELECT ID FROM Concept WHERE Code = @Codigo))
			BEGIN
			--// REGISTRO DEL CONCEPT
				INSERT INTO [Concept]
					([EncoderContextID],[EncoderFlag],[Version],[Code],[Name],[Description],
					[Refinability],[IsCodified],[IsPrimitive],[Level],[AncestorID],[Status],
					[LastUpdated],[ModifiedBy])
				VALUES
					(@EncoderContextID, @EncoderFlag, '1', @Codigo, @Nombre, @Nombre,
					0,0,0,1,0,1,@Now, @ModifiedBy)
			END
		END
		--// SIGUIENTE	
		FETCH NEXT FROM CURSOR_CF
		INTO @Codigo, @Nombre, @EncoderNombre
	END
	CLOSE CURSOR_CF
	DEALLOCATE CURSOR_CF
END
ELSE
BEGIN
	Print 'ERROR EN LOS DATOS BASE PARA EL REGISTRO DE LA INFORMACIÓN. REVISE LOS DATOS MAESTROS REQUERIDOS'
END
