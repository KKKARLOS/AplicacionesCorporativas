--///////////////////////////////////////////////////////////
--// SCRIPT PARA ELIMINAR DE LOS PASOS DE ADMISI�N DE LOS PROCESOS CON PREINGRESO LA ORDEN ADT
--///////////////////////////////////////////////////////////
USE HCDIS
UPDATE AdmissionConfig 
SET AdmissionOrderID = 0
WHERE AdmissionConfig.ProcessChartID 
IN (SELECT DISTINCT PC.ID FROM ProcessChart PC
	JOIN BasicStepsInProcess BSP ON PC.ID = BSP.ProcessChartID
	WHERE BSP.ProcessStep = 64) /* RESERVATION */