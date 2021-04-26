--Ejecutar después del schema compare
UPDATE CustomerLeave 
SET CustomerProcessID = (select CustomerProcessID from CustomerProcessStepsRel where [Step]=16384 and CurrentStepID=CustomerLeave.ID)

UPDATE CustomerTransfer 
SET CustomerProcessID = (select CustomerProcessID from CustomerProcessStepsRel where [Step]=8192 and CurrentStepID=CustomerTransfer.ID)

UPDATE CustomerReservation 
SET CustomerProcessID = (select CustomerProcessID from CustomerProcessStepsRel where [Step]=64 and CurrentStepID=CustomerReservation.ID),
	AssistanceServiceID = ISNULL((select AssistanceServiceID from CustomerOrderRequest where CustomerOrderRequest.ID=CustomerReservation.ADTOrderID),0)



