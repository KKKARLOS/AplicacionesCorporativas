--///////////////////////////////////////////////////////////////////////
--//
--// ACTUALIZACION Y MODIFICACION DE LAS RAZONES DE CAMBIO AL NUEVO TIPO
--//
--//////////////////////////////////////////////////////////////////////
UPDATE ReasonChange 
SET 
ReasonChangeType = (CASE WHEN ReasonChangeType IN (1,29,4,30,16,12,42,43,23,19,21) THEN 1
						 WHEN ReasonChangeType IN (2,31,5,32,100,17,101,13,40,44,24,14,15,20,22) THEN 2
						 WHEN ReasonChangeType IN (3,6,18,10,90) THEN 3
						 WHEN ReasonChangeType IN (33,34,35) THEN 4
						 WHEN ReasonChangeType IN (37,25) THEN 5
						 WHEN ReasonChangeType IN (36,103,102) THEN 6
						 WHEN ReasonChangeType IN (104,28) THEN 7
						 WHEN ReasonChangeType = 26 THEN 8
						 WHEN ReasonChangeType = 41 THEN 9 
						 ELSE 0 END),
ElementID= (CASE 
		WHEN [ReasonChangeType] IN (1,2,3,29,31,33, 101,102)
			THEN (select top 1 ID from EACElement where Name='RoutineTypeEntity')
		WHEN [ReasonChangeType] IN (4,5,6,30,32,34,100)
			THEN (select top 1 ID from EACElement where Name='ProcedureClassificationEntity') 
		WHEN [ReasonChangeType] IN (16,17,18,35,36,37,103)
			THEN (select top 1 ID from EACElement where Name='OrderTypeEntity')
		WHEN [ReasonChangeType] IN (12,13)
			THEN (select top 1 ID from EACElement where Name='EpisodeTypeEntity')
		WHEN [ReasonChangeType] IN (10)
			THEN (select top 1 ID from EACElement where Name='CustomerEpisodeAuthorizationEntity')
		WHEN [ReasonChangeType] IN (40,41)
			THEN (select top 1 ID from EACElement where Name='CustomerCitationEntity')
		WHEN [ReasonChangeType] IN (44)
			THEN (select top 1 ID from EACElement where Name='CustomerReservationEntity')
		WHEN [ReasonChangeType] IN (43)
			THEN (select top 1 ID from EACElement where Name='CustomerTransferEntity')
		WHEN [ReasonChangeType] IN (42)
			THEN (select top 1 ID from EACElement where Name='CustomerLeaveEntity')
		WHEN [ReasonChangeType] IN (23,24)
			THEN (select top 1 ID from EACElement where Name='CustomerReceptionEntity')
		WHEN [ReasonChangeType] IN (25,26)
			THEN (select top 1 ID from EACElement where Name='CustomerReportsEntity')
		WHEN [ReasonChangeType] IN (14)
			THEN (select top 1 ID from EACElement where Name='NotificationActEntity')
		WHEN [ReasonChangeType] IN (15)
			THEN (select top 1 ID from EACElement where Name='ReceiveNotificationActionEntity')	
		WHEN [ReasonChangeType] IN (19,20,104)
			THEN (select top 1 ID from EACElement where Name='MedicalEpisodeEntity')
		WHEN [ReasonChangeType] IN (21,22)
			THEN (select top 1 ID from EACElement where Name='ProcessChartEntity')
		WHEN [ReasonChangeType] IN (28)
			THEN (select top 1 ID from EACElement where Name='MedEpisodeProcessChartEntity')
		WHEN [ReasonChangeType] IN (90)
			THEN (select top 1 ID from EACElement where Name='LocationEntity')
    	ELSE 0 END)
FROM ReasonChange
WHERE ElementID = 0

--////////////////////////////////////////////////////////////////////////////
--// ANTES DE BORRAR COMPROBAR QUE SÓLO SON 2 QUE SE HAN QUEDADO DESASIGNADOS
--DELETE ReasonChange WHERE ElementID = 0

