--////////////////////////////////
--//
--// MODIFICAR TABLA DE FORMA FARMACEUTICA PARA AÑADIR IMAGEID
--// LAS MODIFICACIONES PARA PRESENTAR ICONO DE LA FORMA FARMACEUTICA 
--//
--///////////////////////////////////
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PharmaceuticalForm' and c.name = 'ImageID' and o.type ='U'))
BEGIN
	ALTER TABLE PharmaceuticalForm ADD [ImageID] [INT] NOT NULL Default(0)
END
--////////////////////////////////
--//
--// REGISTRO DE LOS NUEVOS ATRIBUTOS PARA AÑADIR 
--// LAS MODIFICACIONES DE FORMA FARMACEUTICA
--// LAS MODIFICACIONES PARA PRESENTAR ICONO DE LA FORMA FARMACEUTICA 
--//
--///////////////////////////////////
DECLARE @EACEID INT
DECLARE @ATTRID INT

SET @EACEID = ISNULL((SELECT ID FROM [EACElement] WHERE Name = 'PharmaceuticalFormEntity'),0)
IF NOT(@EACEID IS NULL) AND @EACEID > 0
BEGIN
	--///////////////////////////////////////////////////////////////////////////////////
	--//								ImageID
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ImageID' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'ImageID',
			'Imagen',
			0,'',0,0,
			1,0,0,0,1,1,8,-1,
			'','','','','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
END








--////////////////////////////////
--//
--// MODIFICAR TABLA DE UNIDOSISINFO PARA AÑADIR 
--// si permite o no multidosis 
--//
--///////////////////////////////////
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'UnidosisInfo' and c.name = 'AllowMultiDose' and o.type ='U'))
BEGIN
	ALTER TABLE UnidosisInfo ADD [AllowMultiDose] [BIT] NOT NULL Default(0)
END
--////////////////////////////////
--//
--// REGISTRO DE LOS NUEVOS ATRIBUTOS PARA AÑADIR 
--// A UNIDOSISINFO si permite o no multidosis 
--//
--///////////////////////////////////
SET @EACEID = ISNULL((SELECT ID FROM [EACElement] WHERE Name = 'UnidosisEntity'),0)
IF NOT(@EACEID IS NULL) AND @EACEID > 0
BEGIN
	--///////////////////////////////////////////////////////////////////////////////////
	--//								ImageID
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'AllowMultiDose' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,'AllowMultiDose',
			'Permite Multidosis',
			5,'',0,0,
			1,0,0,0,1,1,8,-1,
			'','','','False','',2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END
END
