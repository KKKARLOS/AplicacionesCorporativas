--//////////////////////////////////////////////////////////////////////////////////
--//el objetivo de este script es añadir nuevas columnas en la tabla de Invoice
--// Valida que la columna no exista antes de añadirla
--//////////////////////////////////////////////////////////////////////////////////
USE HCDIS

IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'Invoice' and c.name = 'EstadoJDEFactura' and o.type ='U'))
BEGIN
	Print 'registro de columna EstadoJDEFactura en tabla Invoice'
	ALTER TABLE Invoice ADD EstadoJDEFactura bit NOT NULL DEFAULT(0)
END
ELSE 
	Print 'La columna EstadoJDEFactura ya existe en tabla Invoice'

IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'Invoice' and c.name = 'FechaJDEFactura' and o.type ='U'))
BEGIN
	Print 'registro de columna FechaJDEFactura en tabla Invoice'
	ALTER TABLE Invoice ADD FechaJDEFactura DateTime NULL
END
ELSE 
	Print 'La columna FechaJDEFactura ya existe en tabla Invoice'

IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'Invoice' and c.name = 'EstadoJDECobro' and o.type ='U'))
BEGIN
	Print 'registro de columna EstadoJDECobro en tabla Invoice'
	ALTER TABLE Invoice ADD EstadoJDECobro bit NOT NULL DEFAULT(0)
END
ELSE 
	Print 'La columna EstadoJDECobro ya existe en tabla Invoice'

IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'Invoice' and c.name = 'FechaJDECobro' and o.type ='U'))
BEGIN
	Print 'registro de columna FechaJDECobro en tabla Invoice'
	ALTER TABLE Invoice ADD FechaJDECobro DateTime NULL
END
ELSE 
	Print 'La columna FechaJDECobro ya existe en tabla Invoice'


