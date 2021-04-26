--///////////////////////////////////////////////////////////
--// SCRIPT PARA ELIMINAR EL FLAG DE MEDICO DE REFERENCIA DE LAS ORDENES ADT
--///////////////////////////////////////////////////////////
USE HCDIS
UPDATE [Order]
SET ToRequestOrderFlags = ToRequestOrderFlags - 4
WHERE NOT(ADTRequestAction = 0)