--////////////////////////////////
--//
--// MODIFICAR TABLA DE ItemTreatmentOrderSequence PARA AÑADIR 
--// EL MEANING DEL INTERVALO DE CADA PERFUSION RequestedGivePerTimeUnitMeaning 
--// QUE VIENE DE EFARMACO Y QUE SE ASOCIA A LA BASE.
--//
--///////////////////////////////////
IF NOT(EXISTS(
	SELECT c.name FROM sysobjects o JOIN syscolumns c ON o.id = c.id
	WHERE o.name = 'ItemTreatmentOrderSequence' and c.name = 'RequestedGivePerTimeUnitMeaning' and o.type ='U'))
BEGIN
	ALTER TABLE ItemTreatmentOrderSequence ADD	[RequestedGivePerTimeUnitMeaning] [nvarchar](max) NULL
END
