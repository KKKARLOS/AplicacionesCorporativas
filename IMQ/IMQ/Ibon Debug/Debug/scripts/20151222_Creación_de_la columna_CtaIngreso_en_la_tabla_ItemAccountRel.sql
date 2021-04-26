--/////////////////////////////////////////////////////////////////
-- Este script añade una nueva columna a la tabla ItemAccountRel.
-- Valida que la columna no exista antes de crearla.
-- Miguel Angel.
--/////////////////////////////////////////////////////////////////
USE HCDIS

IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'ItemAccountRel' and c.name = 'CtaIngreso' and o.type ='U'))
BEGIN
	Print 'Creando la columna CtaIngreso en tabla ItemAccountRel.'
	ALTER TABLE dbo.ItemAccountRel ADD CtaIngreso VARCHAR(50) NULL;
END
ELSE 
	Print 'La columna CtaIngreso ya existe en tabla ItemAccountRel.'
