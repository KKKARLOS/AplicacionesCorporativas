DECLARE @EACEID INT
DECLARE @ATTRID INT
DECLARE @ATTROPTID INT

BEGIN TRANSACTION

SET @EACEID = ISNULL((SELECT ID FROM [EACElement] WHERE Name = 'AgreementEntity'),0)
IF NOT(@EACEID IS NULL) AND @EACEID > 0
BEGIN
	--///////////////////////////////////////////////////////////////////////////////////
	--//								AgreementEntity
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'AuthorizationTypeID' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'AuthorizationTypeID', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'InputMask' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'InputMask', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'IsChipCard' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'IsChipCard', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'StepToShow' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'StepToShow', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'RECEPTION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'RECEPTION',	'RECEPTION',	GETDATE(),	'Administrador',	2)
		END

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'ADMISSION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'ADMISSION',	'ADMISSION',	GETDATE(),	'Administrador',	2)
		END

	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ShowOnInvoice' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'ShowOnInvoice', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	
END

SET @EACEID = ISNULL((SELECT ID FROM [EACElement] WHERE Name = 'AssistanceAgreementEntity'),0)
IF NOT(@EACEID IS NULL) AND @EACEID > 0
BEGIN
	--///////////////////////////////////////////////////////////////////////////////////
	--//								AssistanceAgreementEntity
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'AuthorizationTypeID' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'AuthorizationTypeID', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'InputMask' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'InputMask', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'IsChipCard' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'IsChipCard', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'StepToShow' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'StepToShow', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'RECEPTION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'RECEPTION',	'RECEPTION',	GETDATE(),	'Administrador',	2)
		END

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'ADMISSION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'ADMISSION',	'ADMISSION',	GETDATE(),	'Administrador',	2)
		END

	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ShowOnInvoice' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'ShowOnInvoice', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	
END

SET @EACEID = ISNULL((SELECT ID FROM [EACElement] WHERE Name = 'InsurerAgreementEntity'),0)
IF NOT(@EACEID IS NULL) AND @EACEID > 0
BEGIN
	--///////////////////////////////////////////////////////////////////////////////////
	--//								InsurerAgreementEntity
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'AuthorizationTypeID' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'AuthorizationTypeID', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'InputMask' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'InputMask', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'IsChipCard' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'IsChipCard', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'StepToShow' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'StepToShow', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'RECEPTION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'RECEPTION',	'RECEPTION',	GETDATE(),	'Administrador',	2)
		END

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'ADMISSION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'ADMISSION',	'ADMISSION',	GETDATE(),	'Administrador',	2)
		END

	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ShowOnInvoice' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'ShowOnInvoice', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	
END

SET @EACEID = ISNULL((SELECT ID FROM [EACElement] WHERE Name = 'InsurerCoverAgreementEntity'),0)
IF NOT(@EACEID IS NULL) AND @EACEID > 0
BEGIN
	--///////////////////////////////////////////////////////////////////////////////////
	--//								InsurerCoverAgreementEntity
	--///////////////////////////////////////////////////////////////////////////////////
	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'AuthorizationTypeID' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'AuthorizationTypeID', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'InputMask' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'InputMask', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'IsChipCard' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'IsChipCard', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'StepToShow' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,N'StepToShow', N'Apunta a un registro de tipo de autorización (AuthorizationTypeEntity) definido para el garante al que pertenece la entidad.', 0, N'', 0, 0, 1, 0, 0, 0, 1, 1, 0, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'RECEPTION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'RECEPTION',	'RECEPTION',	GETDATE(),	'Administrador',	2)
		END

		SET @ATTROPTID = ISNULL((SELECT TOP 1 ID FROM EACAttributeOption WHERE Description = 'ADMISSION' 
							AND EACAttributeID = @ATTRID),0)

		IF (@ATTROPTID = 0)
		BEGIN
			insert into EACAttributeOption (EACAttributeID,	Description,	Value,	LastUpdated,	ModifiedBy,	Status)
			values (@ATTRID,	'ADMISSION',	'ADMISSION',	GETDATE(),	'Administrador',	2)
		END

	END

	SET @ATTRID = ISNULL((SELECT TOP 1 ID FROM EACAttribute WHERE Name = 'ShowOnInvoice' 
							AND EACElementID = @EACEID),0)
	IF (@ATTRID = 0)
	BEGIN
		INSERT INTO [EACAttribute]
		([EACElementID],[Name],
		[Description],
		[Type],[TypeName],[ComponentType],[DesignRequired],
		[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
		[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID, N'ShowOnInvoice', N'Indica la máscara con la que se deberá introducir la información y con la que deberá validarse el registro del documento único cuando se asocie una autorización a un elemento de cobertura.', 3, N'', 0, 0, 1, 0, 0, 0, 1, 1, 50, -1, N'', N'', N'', N'', N'', 2, GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
	END

	
END


COMMIT TRANSACTION