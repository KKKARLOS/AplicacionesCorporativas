DECLARE @HeldStatus int
SET @HeldStatus = 8

UPDATE ReceptionAdditionalInfo SET [Status]=@HeldStatus
WHERE CustomerReceptionID IN 
(
SELECT CR.[ID] FROM CustomerReception CR 
JOIN ProcessChart PC ON CR.ProcessChartID = PC.[ID]
JOIN CitationConfig CC ON CC.ProcessChartID = PC.[ID]
WHERE (CR.[Status]=@HeldStatus) AND (CC.AllowMultipleAppointment=0)
)
AND ([Status]!=@HeldStatus)
