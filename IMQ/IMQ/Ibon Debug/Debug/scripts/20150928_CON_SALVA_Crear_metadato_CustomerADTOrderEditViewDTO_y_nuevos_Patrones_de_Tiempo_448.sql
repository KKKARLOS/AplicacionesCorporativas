DECLARE @EACEID INT
DECLARE @ATTRID INT

DECLARE @TPMañanasID INT
DECLARE @TPTardesID INT
DECLARE @TPLunesID INT
DECLARE @TPMartesID INT
DECLARE @TPMiercolesID INT
DECLARE @TPJuevesID INT
DECLARE @TPViernesID INT

---------------------------------------------------------------------------------------------
-- METADATO: CustomerADTOrderEditViewDTO
---------------------------------------------------------------------------------------------
IF NOT(EXISTS(SELECT ID FROM [EACElement] WHERE Name = 'CustomerADTOrderEditViewDTO'))
BEGIN
	INSERT INTO [EACElement]
			([ElementType],
			[Name],
			[ModuleName],
			[Description],
			[Status],
			[LastUpdated],
			[ModifiedBy])
	VALUES (2, 
			'CustomerADTOrderEditViewDTO',
			'',
			'La configuración de los metadatos DTO para la vista CustomerADTOrderEditView',
			2, 
			GETDATE(), 
			'Administrador')
	SET @EACEID = (SELECT @@IDENTITY)	
	IF NOT(@EACEID IS NULL) AND @EACEID > 0
	BEGIN
	---------------------------------------------------------------------------------------------
	-- ATRIBUTO
	---------------------------------------------------------------------------------------------
		INSERT INTO [EACAttribute]
		([EACElementID],
		[Name],
		[Description],
		[DefaultValue],
		[Type],[TypeName],[HasOptions],[Length],[Status],[LastUpdated],[ModifiedBy])        
		VALUES(@EACEID,
		'FrecuenciasFisioterapia',
		'Listado de Frecuencias de Fisioterapia utilizados en la vista de edición de solicitudes.',
		'FISIO',
		0,'',1,0,2,GETDATE(),'Administrador')
		SET @ATTRID = (SELECT @@IDENTITY)
		IF NOT(@ATTRID IS NULL) AND (@ATTRID > 0)
		BEGIN
			---------------------------------------------------------------------------------------------
			-- OPCIONES
			---------------------------------------------------------------------------------------------
			-- 1. Mañanas
			SET @TPMañanasID = (SELECT ID FROM TimePattern WHERE Meaning = 'RP  D/ X1 UT 10.00')
			IF (@TPMañanasID IS NULL) OR (@TPMañanasID <= 0)
				BEGIN
					INSERT INTO TimePattern
							(Meaning, [Description], PatternType, NumberOfTimes, Quantity,
							 TimeInterval, Unit, UnitInterval, TimeScheme, [DayOfWeek],
							 SelectedDays, Holidays, InUse, [Status], ModifiedBy, LastUpdated)
					VALUES	('RP  D/ X1 UT 10.00', 'Mañanas', 4, 1, 0,
							 1, 0, 5, 0, '',
							 '', 0, 0, 1, 'Administrador', GetDate()) 
					SET @TPMañanasID = (SELECT @@IDENTITY)
				END
			IF NOT(@TPMañanasID IS NULL) AND (@TPMañanasID > 0)
				BEGIN
					INSERT INTO UserTime
							([TimePatternID], [Time], ModifiedBy, LastUpdated)
					VALUES	(@TPMañanasID, '1753-01-01 10:00:00.000', 'Administrador', GetDate())
				
					INSERT INTO [EACAttributeOption]
					([EACAttributeID],
					[Value],
					[Description],
					[LastUpdated],[ModifiedBy],[Status])
					VALUES(@ATTRID,
					(SELECT Meaning FROM TimePattern WHERE ID=@TPMañanasID),
					(SELECT [Description] FROM TimePattern WHERE ID=@TPMañanasID),
					GETDATE(),'Administrador',2)
				END
			---------------------------------------------------------------------------------------------
			-- 2. Tardes
			SET @TPTardesID = (SELECT ID FROM TimePattern WHERE Meaning = 'RP  D/ X1 UT 15.00')
			IF (@TPTardesID IS NULL) OR (@TPTardesID <= 0)
				BEGIN
					INSERT INTO TimePattern
							(Meaning, [Description], PatternType, NumberOfTimes, Quantity,
							 TimeInterval, Unit, UnitInterval, TimeScheme, [DayOfWeek],
							 SelectedDays, Holidays, InUse, [Status], ModifiedBy, LastUpdated)
					VALUES	('RP  D/ X1 UT 15.00', 'Tardes', 4, 1, 0,
							 1, 0, 5, 0, '',
							 '', 0, 0, 1, 'Administrador', GetDate()) 
					SET @TPTardesID = (SELECT @@IDENTITY)
				END
			IF NOT(@TPTardesID IS NULL) AND (@TPTardesID > 0)
				BEGIN
					INSERT INTO UserTime
							([TimePatternID], [Time], ModifiedBy, LastUpdated)
					VALUES	(@TPTardesID, '1753-01-01 15:00:00.000', 'Administrador', GetDate())
				
					INSERT INTO [EACAttributeOption]
					([EACAttributeID],
					[Value],
					[Description],
					[LastUpdated],[ModifiedBy],[Status])
					VALUES(@ATTRID,
					(SELECT Meaning FROM TimePattern WHERE ID=@TPTardesID),
					(SELECT [Description] FROM TimePattern WHERE ID=@TPTardesID),
					GETDATE(),'Administrador',2)
				END
			---------------------------------------------------------------------------------------------
			-- 3. Los Lunes
			SET @TPLunesID = (SELECT ID FROM TimePattern WHERE Meaning = 'RP  W/ J1 X1 UT 00.00')
			IF (@TPLunesID IS NULL) OR (@TPLunesID <= 0)
				BEGIN
					INSERT INTO TimePattern
							(Meaning, [Description], PatternType, NumberOfTimes, Quantity,
							 TimeInterval, Unit, UnitInterval, TimeScheme, [DayOfWeek],
							 SelectedDays, Holidays, InUse, [Status], ModifiedBy, LastUpdated)
					VALUES	('RP  W/ J1 X1 UT 00.00', 'Los Lunes', 4, 1, 0,
							 1, 0, 5, 4, '',
							 '', 0, 0, 1, 'Administrador', GetDate()) 
					SET @TPLunesID = (SELECT @@IDENTITY)
				END
			IF NOT(@TPLunesID IS NULL) AND (@TPLunesID > 0)
				BEGIN
					INSERT INTO UserTime
							([TimePatternID], [Time], ModifiedBy, LastUpdated)
					VALUES	(@TPLunesID, '1753-01-01 00:00:00.000', 'Administrador', GetDate())
				
					INSERT INTO [EACAttributeOption]
					([EACAttributeID],
					[Value],
					[Description],
					[LastUpdated],[ModifiedBy],[Status])
					VALUES(@ATTRID,
					(SELECT Meaning FROM TimePattern WHERE ID=@TPLunesID),
					(SELECT [Description] FROM TimePattern WHERE ID=@TPLunesID),
					GETDATE(),'Administrador',2)
				END
			---------------------------------------------------------------------------------------------
			-- 4. Los Martes
			SET @TPMartesID = (SELECT ID FROM TimePattern WHERE Meaning = 'RP  W/ J2 X1 UT 00.00')
			IF (@TPMartesID IS NULL) OR (@TPMartesID <= 0)
				BEGIN
					INSERT INTO TimePattern
							(Meaning, [Description], PatternType, NumberOfTimes, Quantity,
							 TimeInterval, Unit, UnitInterval, TimeScheme, [DayOfWeek],
							 SelectedDays, Holidays, InUse, [Status], ModifiedBy, LastUpdated)
					VALUES	('RP  W/ J2 X1 UT 00.00', 'Los Martes', 4, 1, 0,
							 1, 0, 5, 4, '',
							 '', 0, 0, 1, 'Administrador', GetDate()) 
					SET @TPMartesID = (SELECT @@IDENTITY)
				END
			IF NOT(@TPMartesID IS NULL) AND (@TPMartesID > 0)
				BEGIN
					INSERT INTO UserTime
							([TimePatternID], [Time], ModifiedBy, LastUpdated)
					VALUES	(@TPMartesID, '1753-01-01 00:00:00.000', 'Administrador', GetDate())
				
					INSERT INTO [EACAttributeOption]
					([EACAttributeID],
					[Value],
					[Description],
					[LastUpdated],[ModifiedBy],[Status])
					VALUES(@ATTRID,
					(SELECT Meaning FROM TimePattern WHERE ID=@TPMartesID),
					(SELECT [Description] FROM TimePattern WHERE ID=@TPMartesID),
					GETDATE(),'Administrador',2)
				END
			---------------------------------------------------------------------------------------------
			-- 5. Los Miercoles
			SET @TPMiercolesID = (SELECT ID FROM TimePattern WHERE Meaning = 'RP  W/ J3 X1 UT 00.00')
			IF (@TPMiercolesID IS NULL) OR (@TPMiercolesID <= 0)
				BEGIN
					INSERT INTO TimePattern
							(Meaning, [Description], PatternType, NumberOfTimes, Quantity,
							 TimeInterval, Unit, UnitInterval, TimeScheme, [DayOfWeek],
							 SelectedDays, Holidays, InUse, [Status], ModifiedBy, LastUpdated)
					VALUES	('RP  W/ J3 X1 UT 00.00', 'Los Miércoles', 4, 1, 0,
							 1, 0, 5, 4, '',
							 '', 0, 0, 1, 'Administrador', GetDate()) 
					SET @TPMiercolesID = (SELECT @@IDENTITY)
				END
			IF NOT(@TPMiercolesID IS NULL) AND (@TPMiercolesID > 0)
				BEGIN
					INSERT INTO UserTime
							([TimePatternID], [Time], ModifiedBy, LastUpdated)
					VALUES	(@TPMiercolesID, '1753-01-01 00:00:00.000', 'Administrador', GetDate())
				
					INSERT INTO [EACAttributeOption]
					([EACAttributeID],
					[Value],
					[Description],
					[LastUpdated],[ModifiedBy],[Status])
					VALUES(@ATTRID,
					(SELECT Meaning FROM TimePattern WHERE ID=@TPMiercolesID),
					(SELECT [Description] FROM TimePattern WHERE ID=@TPMiercolesID),
					GETDATE(),'Administrador',2)
				END
			---------------------------------------------------------------------------------------------
			-- 6. Los Jueves
			SET @TPJuevesID = (SELECT ID FROM TimePattern WHERE Meaning = 'RP  W/ J4 X1 UT 00.00')
			IF (@TPJuevesID IS NULL) OR (@TPJuevesID <= 0)
				BEGIN
					INSERT INTO TimePattern
							(Meaning, [Description], PatternType, NumberOfTimes, Quantity,
							 TimeInterval, Unit, UnitInterval, TimeScheme, [DayOfWeek],
							 SelectedDays, Holidays, InUse, [Status], ModifiedBy, LastUpdated)
					VALUES	('RP  W/ J4 X1 UT 00.00', 'Los Jueves', 4, 1, 0,
							 1, 0, 5, 4, '',
							 '', 0, 0, 1, 'Administrador', GetDate()) 
					SET @TPJuevesID = (SELECT @@IDENTITY)
				END
			IF NOT(@TPJuevesID IS NULL) AND (@TPJuevesID > 0)
				BEGIN
					INSERT INTO UserTime
							([TimePatternID], [Time], ModifiedBy, LastUpdated)
					VALUES	(@TPJuevesID, '1753-01-01 00:00:00.000', 'Administrador', GetDate())
				
					INSERT INTO [EACAttributeOption]
					([EACAttributeID],
					[Value],
					[Description],
					[LastUpdated],[ModifiedBy],[Status])
					VALUES(@ATTRID,
					(SELECT Meaning FROM TimePattern WHERE ID=@TPJuevesID),
					(SELECT [Description] FROM TimePattern WHERE ID=@TPJuevesID),
					GETDATE(),'Administrador',2)
				END
			---------------------------------------------------------------------------------------------
			-- 7. Los Viernes
			SET @TPViernesID = (SELECT ID FROM TimePattern WHERE Meaning = 'RP  W/ J5 X1 UT 00.00')
			IF (@TPViernesID IS NULL) OR (@TPViernesID <= 0)
				BEGIN
					INSERT INTO TimePattern
							(Meaning, [Description], PatternType, NumberOfTimes, Quantity,
							 TimeInterval, Unit, UnitInterval, TimeScheme, [DayOfWeek],
							 SelectedDays, Holidays, InUse, [Status], ModifiedBy, LastUpdated)
					VALUES	('RP  W/ J5 X1 UT 00.00', 'Los Viernes', 4, 1, 0,
							 1, 0, 5, 4, '',
							 '', 0, 0, 1, 'Administrador', GetDate()) 
					SET @TPViernesID = (SELECT @@IDENTITY)
				END
			IF NOT(@TPViernesID IS NULL) AND (@TPViernesID > 0)
				BEGIN
					INSERT INTO UserTime
							([TimePatternID], [Time], ModifiedBy, LastUpdated)
					VALUES	(@TPViernesID, '1753-01-01 00:00:00.000', 'Administrador', GetDate())
				
					INSERT INTO [EACAttributeOption]
					([EACAttributeID],
					[Value],
					[Description],
					[LastUpdated],[ModifiedBy],[Status])
					VALUES(@ATTRID,
					(SELECT Meaning FROM TimePattern WHERE ID=@TPViernesID),
					(SELECT [Description] FROM TimePattern WHERE ID=@TPViernesID),
					GETDATE(),'Administrador',2)
				END
			---------------------------------------------------------------------------------------------
		END
	END
END
---------------------------------------------------------------------------------------------

		 
