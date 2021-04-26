use [DBNAME]

--////////////////////////////////////////////////////////////
--//
--// ACTUALIZA LA FECHA DE SOLICITUD DE EJECUCION DEL MEDICO. 
--// SOLO SE DEBE EJECUTAR EL SCRIPT DESPUÉS DEL SCHEME COMPARE
--//
--////////////////////////////////////////////////////////////

UPDATE CustomerOrderRequest set RequestEffectiveAtDateTime = 
(SELECT TOP 1 OSR.OrderEffectiveAt FROM OrderRequestSchPlanning OSR 
	WHERE OSR.CustomerOrderRequestID = CustomerOrderRequest.ID)
	