
UPDATE CustomerOrderRequest SET Placed=1
FROM CustomerOrderRequest COR
	JOIN AppointmentService AppS ON COR.ID=AppS.ElementID AND Apps.AppointmentElement=3
    JOIN CustomerAppointmentInformation CAI ON AppS.CustomerAppointmentInformationID=CAI.[ID]
WHERE (CAI.[Status] IN (1,2,6,7))

UPDATE CustomerOrderRequest SET Placed=1
FROM CustomerOrderRequest COR
	JOIN RoutineAct RA ON COR.ID=RA.CustomerOrderRequestID
    JOIN RealizationAppointmentService RAS ON RA.ID=RAS.ElementID AND RAS.AppointmentElement=2
    
UPDATE CustomerOrderRequest SET Placed=1
FROM CustomerOrderRequest COR
	JOIN ProcedureAct PA ON COR.ID=PA.CustomerOrderRequestID
    JOIN RealizationAppointmentService RAS ON PA.ID=RAS.ElementID AND RAS.AppointmentElement=1
    
UPDATE CustomerOrderRequest SET Placed=1
FROM CustomerOrderRequest COR
WHERE (COR.OrderControlCode IN (2))

UPDATE CustomerOrderRequest SET Placed=1
FROM CustomerOrderRequest COR
WHERE (COR.[Status] IN (16, 4, 128))
