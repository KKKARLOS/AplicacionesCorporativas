UPDATE EACAttribute SET HasOptions=1 where EACElementID=435 and Name='OrderInService'

GO
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13637 AND Value='None')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13637, 'Único', 'None', GETDATE(), 'Administrador', 2)
	END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13637 AND Value='Entrance')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13637, 'Entrada', 'Entrance', GETDATE(), 'Administrador', 2)
	END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13637 AND Value='FirstService')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13637, 'Primeros', 'FirstService', GETDATE(), 'Administrador', 2)
	END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13637 AND Value='SecondService')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13637, 'Segundos', 'SecondService', GETDATE(), 'Administrador', 2)
	END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13637 AND Value='ThirdService')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13637, 'Terceros', 'ThirdService', GETDATE(), 'Administrador', 2)
	END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13637 AND Value='Dessert')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13637, 'Postre', 'Dessert', GETDATE(), 'Administrador', 2)
	END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13637 AND Value='Drinks')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13637, 'Bebidas', 'Drinks', GETDATE(), 'Administrador', 2)
	END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=13637 AND Value='Other')
	BEGIN
	INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
	VALUES (13637, 'Otros', 'Other', GETDATE(), 'Administrador', 2)
	END

select * from EACAttributeOption  where EACAttributeID=13637