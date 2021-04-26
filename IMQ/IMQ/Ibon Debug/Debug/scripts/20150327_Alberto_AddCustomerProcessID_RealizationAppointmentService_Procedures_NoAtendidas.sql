DECLARE @ReceptionAdditionalInfoID as int
DECLARE @ActID as int
DECLARE @CustomerProcessID as int

DECLARE Name_Cursor cursor FAST_FORWARD for
select RAI.[ID] ReceptionInfoAppointmentRelID, PA.[ID] ActID, CR.CustomerProcessID
FROM CustomerReception CR 
	JOIN ReceptionAdditionalInfo RAI ON RAI.CustomerReceptionID=CR.[ID]
	JOIN ReceptionInfoAppointmentRel RIAR ON RIAR.ReceptionAdditionalInfoID=RAI.[ID]
	JOIN AppointmentService AppS ON RIAR.AppointmentServiceID=AppS.[ID] AND Apps.CustomerAppointmentInformationID=0 AND StepType=0
	JOIN CustomerOrderRequest COR ON Apps.AppointmentElement=3 AND COR.[ID]=Apps.ElementID
	JOIN ProcedureAct PA ON PA.CustomerOrderRequestID=COR.[ID]
WHERE CR.[Status]=8 AND RAI.[Status]=8 AND Apps.[Status]=4
	ORDER BY RIAR.LastUpdated

OPEN Name_Cursor
FETCH NEXT FROM Name_Cursor INTO @ReceptionAdditionalInfoID, @ActID, @CustomerProcessID
WHILE @@FETCH_STATUS = 0
BEGIN
	DECLARE @RASID int
	SET @RASID = ISNULL((select TOP 1 RAS.[ID] RealizationAppointmentService
						FROM ReceptionAdditionalInfo RAI 
							JOIN ReceptionInfoAppointmentRel RIAR ON RIAR.ReceptionAdditionalInfoID=RAI.[ID]
							JOIN AppointmentService AppS ON RIAR.AppointmentServiceID=AppS.[ID] AND Apps.CustomerAppointmentInformationID=0 AND StepType=0
							JOIN CustomerOrderRequest COR ON Apps.AppointmentElement=3 AND COR.[ID]=Apps.ElementID
							JOIN ProcedureAct PA ON PA.CustomerOrderRequestID=COR.[ID]
							JOIN RealizationAppointmentService RAS ON PA.[ID]=RAS.ElementID AND RAS.AppointmentElement=1 AND (RAS.CustomerProcessID=0)
							JOIN (SELECT 1 ResourceElement, LA.[ID] ResourceAvailID, LA.LastUpdated FROM LocationAvailability LA 
								WHERE (LA.Status=4)
								UNION 
								SELECT 2 ResourceElement, EA.[ID] ResourceAvailID, EA.LastUpdated FROM EquipmentAvailability EA 
								WHERE (EA.Status=4)) Avail ON Avail.ResourceElement=RAS.ResourceElement AND Avail.ResourceAvailID=RAS.ResourceAvailID
						WHERE (RAi.[ID]=@ReceptionAdditionalInfoID) AND (PA.[ID]=@ActID)
						ORDER BY Avail.LastUpdated), 0)
						
	IF (@RASID > 0)
	BEGIN
		UPDATE RealizationAppointmentService SET CustomerProcessID=@CustomerProcessID
		WHERE [ID]=@RASID
		print @ReceptionAdditionalInfoID
		print @ActID
		print @RASID
		print @CustomerProcessID
		print '---------------------------------------------------------------'
	END
      
    FETCH NEXT FROM Name_Cursor INTO @ReceptionAdditionalInfoID, @ActID, @CustomerProcessID
END
CLOSE Name_Cursor
DEALLOCATE Name_Cursor


