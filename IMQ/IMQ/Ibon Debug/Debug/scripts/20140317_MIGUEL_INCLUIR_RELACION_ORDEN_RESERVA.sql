use [DBName]

INSERT INTO CustomerReservationOrderRequestRel (CustomerReservationID, CustomerOrderRequestID,
	ModifiedBy, LastUpdated)
SELECT DISTINCT CR.[ID], COR.[ID], CR.ModifiedBy, CR.LastUpdated
FROM CustomerReservation CR
JOIN CustomerOrderRequest COR ON CR.ADTOrderID = COR.ParentCustomerOrderRequestID
WHERE CR.ADTOrderID > 0
AND NOT(EXISTS(SELECT CREL.[ID] FROM CustomerReservationOrderRequestRel CREL
WHERE CREL.CustomerReservationID = CR.[ID] AND CREL.CustomerOrderRequestID = COR.[ID]))
