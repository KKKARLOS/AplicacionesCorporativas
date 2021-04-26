UPDATE AP
SET		[InUse] = 'True',
		ModifiedBy = 'Administrador',
		LastUpdated = GetDate()
FROM AvailPattern AP
JOIN EquipmentAvailPatternRel T1 ON T1.AvailPatternID=AP.[ID]

UPDATE AP
SET		[InUse] = 'True',
		ModifiedBy = 'Administrador',
		LastUpdated = GetDate()
FROM AvailPattern AP
JOIN PersonAvailPattern T1 ON T1.AvailPatternID=AP.[ID]

UPDATE AP
SET		[InUse] = 'True',
		ModifiedBy = 'Administrador',
		LastUpdated = GetDate()
FROM AvailPattern AP
JOIN LocationAvailPatternRel T1 ON T1.AvailPatternID=AP.[ID]

UPDATE AP
SET		[InUse] = 'True',
		ModifiedBy = 'Administrador',
		LastUpdated = GetDate()
FROM AvailPattern AP
JOIN CitationConfig T1 ON T1.DefaultAvailPatternID=AP.[ID]

UPDATE AP
SET		[InUse] = 'True',
		ModifiedBy = 'Administrador',
		LastUpdated = GetDate()
FROM AvailPattern AP
JOIN WaitingListConfig T1 ON T1.DefaultAvailPatternID=AP.[ID]