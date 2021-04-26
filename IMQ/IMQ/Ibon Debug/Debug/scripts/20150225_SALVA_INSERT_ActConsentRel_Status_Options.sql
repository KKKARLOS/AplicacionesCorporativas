IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=15535 AND Value='Active')
      BEGIN
      INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
      VALUES (15535, 'Active', 'Active', GETDATE(), 'Administrador', 2)
      END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=15535 AND Value='Bypassed')
      BEGIN
      INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
      VALUES (15535, 'Bypassed', 'Bypassed', GETDATE(), 'Administrador', 3)
      END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=15535 AND Value='Limited')
      BEGIN
      INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
      VALUES (15535, 'Limited', 'Limited', GETDATE(), 'Administrador', 3)
      END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=15535 AND Value='Pending')
      BEGIN
      INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
      VALUES (15535, 'Pending', 'Pending', GETDATE(), 'Administrador', 2)
      END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=15535 AND Value='Refused')
      BEGIN
      INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
      VALUES (15535, 'Refused', 'Refused', GETDATE(), 'Administrador', 2)
      END
IF NOT EXISTS(select * from EACAttributeOption where EACAttributeID=15535 AND Value='Rescinded')
      BEGIN
      INSERT INTO EACAttributeOption (EACAttributeID, [Description], Value, LastUpdated, ModifiedBy, [Status])
      VALUES (15535, 'Rescinded', 'Rescinded', GETDATE(), 'Administrador', 3)
      END
