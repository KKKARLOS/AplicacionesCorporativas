IF (NOT EXISTS(SELECT [ID] FROM [ObservationTemplateRel] WHERE [ObservationTemplateID] = 24 and [ObservationID] = 193))
BEGIN
	INSERT INTO [ObservationTemplateRel] 
		([ObservationTemplateID], [ElementType], [ObservationBlockID], [ObservationID], [ElementTitle], [ElementTitlePosition], [VisibleLabel], [Order], [Required], [Status], [ModifiedBy], [LastUpdated])
		VALUES (24, 2, 0, 193, 'No Valido', 0, 1, 1, 0, 1, 'Administrador', GetDate())
END
ELSE
BEGIN
	print 'La observación "No Valido" ya existe.'
END
go


UPDATE [ObservationTemplateRel] SET [Order] = 4 WHERE [ObservationTemplateID] = 19 and [ObservationID] = 105

UPDATE [ObservationTemplateRel] SET [Order] = 1 WHERE [ObservationTemplateID] = 19 and [ObservationID] = 192

UPDATE [ObservationTemplateRel] SET [Order] = 2 WHERE [ObservationTemplateID] = 19 and [ObservationID] = 193

UPDATE [ObservationTemplateRel] SET [Order] = 3 WHERE [ObservationTemplateID] = 19 and [ObservationID] = 194

UPDATE [ObservationTemplateRel] SET [Order] = 2 WHERE [ObservationTemplateID] = 24 and [ObservationID] = 196