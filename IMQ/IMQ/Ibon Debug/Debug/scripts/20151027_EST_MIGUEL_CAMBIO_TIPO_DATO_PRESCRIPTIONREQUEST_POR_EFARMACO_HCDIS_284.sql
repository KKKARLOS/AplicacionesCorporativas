--////////////////////////////////////////////////////////////////////////////
--/// cambio en el tipo de datos de la columna InitialDoseUnits 
--/// se pasa a float porque desde eFarmaco pueden venir dosis fraccionadas
--///////////////////////////////////////////////////////////////////////////
IF (EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'PrescriptionRequest' and c.name = 'InitialDoseUnits' and o.type ='U'))
BEGIN
	ALTER TABLE [dbo].[PrescriptionRequest] 
	DROP CONSTRAINT [DF_PrescriptionRequest_InitialDoseUnits]

	ALTER TABLE PrescriptionRequest ALTER COLUMN InitialDoseUnits FLOAT NOT NULL

	ALTER TABLE [dbo].[PrescriptionRequest] 
	ADD CONSTRAINT [DF_PrescriptionRequest_InitialDoseUnits] 
	DEFAULT 0 FOR InitialDoseUnits

	Print 'Cambio del tipo de datos de la columna InitialDoseUnits en tabla PrescriptionRequest'
	
END
ELSE
	Print 'LA columna InitialDoseUnits en tabla PrescriptionRequest NO EXISTE'
