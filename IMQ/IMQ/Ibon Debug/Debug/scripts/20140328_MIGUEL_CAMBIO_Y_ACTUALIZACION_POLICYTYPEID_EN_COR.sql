
USE [DBNAME]

--////////////////////////////////////////
--//
--// SOLO SE PODRÁ EJECUTAR DESPUES DEL SCHEME COMPARE
--//
--////////////////////////////////////////

UPDATE CustomerOrderRequest SET PolicyTypeID = 
ISNULL(
	(SELECT TOP 1 F.[ID] FROM
		(SELECT CP.PolicyTypeID AS [ID] FROM CustomerReservation CR 
		 JOIN CustomerPolicy CP ON CR.CustomerPolicyID = CP.[ID]
		 WHERE CR.ADTOrderID = CustomerOrderRequest.ID
		 UNION 
		 SELECT CP.PolicyTypeID AS [ID] FROM CustomerReservation CR 
		 JOIN CustomerPolicy CP ON CR.CustomerPolicyID = CP.[ID]
		 JOIN CustomerReservationOrderRequestRel CROR ON CR.[ID] = CROR.CustomerReservationID
		 WHERE CROR.CustomerOrderRequestID = CustomerOrderRequest.ID
	) AS F),0)

