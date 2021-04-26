
update ReceptionAdditionalInfo SET [Status]=CR.[Status]
FROM ReceptionAdditionalInfo RAI 
	JOIN CustomerReception CR ON RAI.CustomerReceptionID=CR.[ID]
WHERE RAi.[Status]<>CR.[Status] AND CR.[Status] IN (1,8)

update RoutineAct SET AssistancePlanID=CAP.[ID]
FROM RoutineAct RA 
	JOIN CustomerEpisode CE ON RA.EpisodeID=CE.[ID]
	JOIN (SELECT * FROM CustomerAssistancePlan) CAP ON CE.[ID]=CAP.EpisodeID
			 
update ProcedureAct SET AssistancePlanID=CAP.[ID]
FROM ProcedureAct PA 
	JOIN CustomerEpisode CE ON PA.EpisodeID=CE.[ID]
	JOIN (SELECT * FROM CustomerAssistancePlan) CAP ON CE.[ID]=CAP.EpisodeID
	