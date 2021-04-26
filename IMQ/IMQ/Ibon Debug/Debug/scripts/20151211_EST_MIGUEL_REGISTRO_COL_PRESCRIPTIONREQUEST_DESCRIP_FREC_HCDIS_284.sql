--////////////////////////////////
--//
--// MODIFICAR TABLA DE PRESCRIPCIONES PARA AÑADIR 
--// LAS DESCRIPCIONES DE LAS FRECUENCIAS QUE VIENEN DE EFARMACO
--//
--///////////////////////////////////
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PrescriptionRequest' and c.name = 'FrequencyDescription' and o.type ='U'))
BEGIN
	ALTER TABLE PrescriptionRequest ADD	[FrequencyDescription] [nvarchar](max) NULL
END
