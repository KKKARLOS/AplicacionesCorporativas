UPDATE EACAttribute SET HasOptions=1 where EACElementID=435 and Name='DailyFoodService'

GO

IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13638 AND Value='Breakfast')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13638, 'Desayuno', 'Breakfast', GETDATE(), 'Administrador', 2)
	END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13638 AND Value='MorningSnack')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13638, 'Media Mañana', 'MorningSnack', GETDATE(), 'Administrador', 2)
	END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13638 AND Value='Lunch')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13638, 'Comida', 'Lunch', GETDATE(), 'Administrador', 2)
	END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13638 AND Value='AfternoonSnack')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13638, 'Merienda', 'AfternoonSnack', GETDATE(), 'Administrador', 2)
	END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13638 AND Value='Dinner')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13638, 'Cena', 'Dinner', GETDATE(), 'Administrador', 2)
	END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13638 AND Value='EveningSnack')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13638, 'Recena', 'EveningSnack', GETDATE(), 'Administrador', 2)
	END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13638 AND Value='OtherMeal')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13638, 'Otros', 'OtherMeal', GETDATE(), 'Administrador', 2)
	END

select * from EACAttributeOption  where EACAttributeID=13638