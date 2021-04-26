--Con episodio (routine en ubicaion)
Update RealizationAppointmentService SEt CustomerProcessID=CP.[ID]
FROM RealizationAppointmentService RAS 
	JOIN LocationAvailability LA ON RAS.ResourceAvailID=LA.[ID] AND RAS.ResourceElement=1 AND LA.Status<>4
	JOIN RoutineAct RA ON RAS.AppointmentElement=2 AND RAS.ElementID=RA.[ID]
	JOIN CustomerEpisode CE ON RA.EpisodeID=CE.[ID] AND CE.Status<>8
	JOIN CustomerProcess CP ON CE.[ID]=CP.CustomerEpisodeID	
--Con episodio (procedure en ubicaion)	
Update RealizationAppointmentService SEt CustomerProcessID=CP.[ID]
FROM RealizationAppointmentService RAS 
	JOIN LocationAvailability LA ON RAS.ResourceAvailID=LA.[ID] AND RAS.ResourceElement=1 AND LA.Status<>4
	JOIN ProcedureAct RA ON RAS.AppointmentElement=1 AND RAS.ElementID=RA.[ID]
	JOIN CustomerEpisode CE ON RA.EpisodeID=CE.[ID] AND CE.Status<>8
	JOIN CustomerProcess CP ON CE.[ID]=CP.CustomerEpisodeID
--Con episodio (routine en equipo)	
Update RealizationAppointmentService SEt CustomerProcessID=CP.[ID]
FROM RealizationAppointmentService RAS 
	JOIN EquipmentAvailability LA ON RAS.ResourceAvailID=LA.[ID] AND RAS.ResourceElement=2 AND LA.Status<>4
	JOIN RoutineAct RA ON RAS.AppointmentElement=2 AND RAS.ElementID=RA.[ID]
	JOIN CustomerEpisode CE ON RA.EpisodeID=CE.[ID] AND CE.Status<>8
	JOIN CustomerProcess CP ON CE.[ID]=CP.CustomerEpisodeID
--Con episodio (procedure en equipo)	
Update RealizationAppointmentService SEt CustomerProcessID=CP.[ID]
FROM RealizationAppointmentService RAS 
	JOIN EquipmentAvailability LA ON RAS.ResourceAvailID=LA.[ID] AND RAS.ResourceElement=2 AND LA.Status<>4
	JOIN ProcedureAct RA ON RAS.AppointmentElement=1 AND RAS.ElementID=RA.[ID]
	JOIN CustomerEpisode CE ON RA.EpisodeID=CE.[ID] AND CE.Status<>8
	JOIN CustomerProcess CP ON CE.[ID]=CP.CustomerEpisodeID

--Sin episodio pero con reserva (routine en ubicacion)
Update RealizationAppointmentService SEt CustomerProcessID=CP.[ID]
FROM RealizationAppointmentService RAS 
	JOIN LocationAvailability LA ON RAS.ResourceAvailID=LA.[ID] AND RAS.ResourceElement=1 AND LA.Status<>4
	JOIN RoutineAct RA ON RAS.AppointmentElement=2 AND RAS.ElementID=RA.[ID]
	JOIN CustomerReservationOrderRequestRel CRORR ON RA.CustomerOrderRequestID=CRORR.CustomerOrderRequestID
	JOIN CustomerReservation CR ON CRORR.CustomerReservationID=CR.[ID]
	JOIN CustomerProcess CP ON CR.CustomerProcessID=CP.CustomerEpisodeID
--Sin episodio pero con reserva (procedure en ubicacion)
Update RealizationAppointmentService SEt CustomerProcessID=CP.[ID]
FROM RealizationAppointmentService RAS 
	JOIN LocationAvailability LA ON RAS.ResourceAvailID=LA.[ID] AND RAS.ResourceElement=1 AND LA.Status<>4
	JOIN ProcedureAct RA ON RAS.AppointmentElement=1 AND RAS.ElementID=RA.[ID]
	JOIN CustomerReservationOrderRequestRel CRORR ON RA.CustomerOrderRequestID=CRORR.CustomerOrderRequestID
	JOIN CustomerReservation CR ON CRORR.CustomerReservationID=CR.[ID]
	JOIN CustomerProcess CP ON CR.CustomerProcessID=CP.CustomerEpisodeID
--Sin episodio pero con reserva (routine en ubicacion)	
Update RealizationAppointmentService SEt CustomerProcessID=CP.[ID]
FROM RealizationAppointmentService RAS 
	JOIN EquipmentAvailability LA ON RAS.ResourceAvailID=LA.[ID] AND RAS.ResourceElement=2 AND LA.Status<>4
	JOIN RoutineAct RA ON RAS.AppointmentElement=2 AND RAS.ElementID=RA.[ID]
	JOIN CustomerReservationOrderRequestRel CRORR ON RA.CustomerOrderRequestID=CRORR.CustomerOrderRequestID
	JOIN CustomerReservation CR ON CRORR.CustomerReservationID=CR.[ID]
	JOIN CustomerProcess CP ON CR.CustomerProcessID=CP.CustomerEpisodeID
--Sin episodio pero con reserva (procedure en equipo)
Update RealizationAppointmentService SEt CustomerProcessID=CP.[ID]
FROM RealizationAppointmentService RAS 
	JOIN EquipmentAvailability LA ON RAS.ResourceAvailID=LA.[ID] AND RAS.ResourceElement=2 AND LA.Status<>4
	JOIN ProcedureAct RA ON RAS.AppointmentElement=1 AND RAS.ElementID=RA.[ID] AND RA.EpisodeID=0
	JOIN CustomerReservationOrderRequestRel CRORR ON RA.CustomerOrderRequestID=CRORR.CustomerOrderRequestID
	JOIN CustomerReservation CR ON CRORR.CustomerReservationID=CR.[ID]
	JOIN CustomerProcess CP ON CR.CustomerProcessID=CP.CustomerEpisodeID	

	
