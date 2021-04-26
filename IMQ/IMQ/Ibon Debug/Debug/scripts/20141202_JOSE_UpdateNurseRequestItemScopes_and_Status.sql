UPDATE NurseRequest SET SourceLocationID=
IsNull((SELECT DISTINCT TOP 1 RO.FromLocationID FROM ReplenishmentOrder RO
JOIN ReplenishmentOrderEntry ROE ON RO.[ID]=ROE.ReplenishmentOrderID
JOIN NurseRequestEntry NRE ON ROE.NurseRequestEntryID=NRE.[ID]
WHERE RO.OrderType IN (3,5,6,9) AND NRE.NurseRequestID=NurseRequest.[ID]),0)
WHERE NurseRequest.SourceLocationID=0

UPDATE NurseRequest SET ItemScope=8
WHERE NOT(EXISTS(SELECT DISTINCT RO.[ID] FROM ReplenishmentOrder RO
JOIN ReplenishmentOrderEntry ROE ON RO.[ID]=ROE.ReplenishmentOrderID
JOIN NurseRequestEntry NRE ON ROE.NurseRequestEntryID=NRE.[ID]
WHERE RO.OrderType IN (3,5,6,9) AND NRE.NurseRequestID=NurseRequest.[ID]))
AND (NurseRequest.ItemScope=0)

UPDATE NurseRequest SET ItemScope=16
WHERE (EXISTS(SELECT DISTINCT RO.[ID] FROM ReplenishmentOrder RO
JOIN ReplenishmentOrderEntry ROE ON RO.[ID]=ROE.ReplenishmentOrderID
JOIN NurseRequestEntry NRE ON ROE.NurseRequestEntryID=NRE.[ID]
WHERE RO.OrderType IN (3,5,6,9) AND NRE.NurseRequestID=NurseRequest.[ID]))
AND (NurseRequest.ItemScope=0)

UPDATE ReplenishmentOrder SET ItemScope=
CASE OrderType 
	WHEN 3 THEN 16
	WHEN 5 THEN 16
	WHEN 6 THEN 16
	WHEN 9 THEN 16
	WHEN 7 THEN 32
	WHEN 2 THEN 8
	WHEN 8 THEN 8
	WHEN 1 THEN 1
	WHEN 10 THEN 1
	ELSE 0
END
WHERE ItemScope=0

/* Nurse Request Entry */
UPDATE NurseRequestEntry SET [Status]=3 WHERE ([Status]=5) AND (RequestQuantity<>ServedQuantity)

UPDATE NurseRequestEntry SET [Status]=6 WHERE ([Status]=5) AND (ServedQuantity=0) 
AND EXISTS(SELECT [ID] FROM ReplenishmentOrderEntry WHERE NurseRequestEntryID=NurseRequestEntry.[ID])

/* Nurse Request */
/* Si ALGUNA linea esta pendiente, on request o parcialmente servida -> parcialmente servida */
UPDATE NurseRequest SET [Status]=3
WHERE EXISTS(SELECT [ID] FROM NurseRequestEntry WHERE ([Status] IN (3,5,6)) AND (NurseRequestID=NurseRequest.[ID]))

/* Si todas las lineas estan pendientes -> pendiente */
UPDATE NurseRequest SET [Status]=5
WHERE NOT(EXISTS(SELECT [ID] FROM NurseRequestEntry WHERE ([Status]<>5) AND (NurseRequestID=NurseRequest.[ID])))

/* Si todas las lineas estan on request -> on request */
UPDATE NurseRequest SET [Status]=6
WHERE NOT(EXISTS(SELECT [ID] FROM NurseRequestEntry WHERE ([Status]<>6) AND (NurseRequestID=NurseRequest.[ID])))

/* Si todas las lineas estan completadas -> completada */
UPDATE NurseRequest SET [Status]=7
WHERE NOT(EXISTS(SELECT [ID] FROM NurseRequestEntry WHERE ([Status]<>7) AND (NurseRequestID=NurseRequest.[ID])))

/* Si ALGUNA linea esta cerrada -> cerrada */
UPDATE NurseRequest SET [Status]=4
WHERE EXISTS(SELECT [ID] FROM NurseRequestEntry WHERE [Status]=4)