/****** Object:  StoredProcedure [dbo].[GetCustomerCareProcessListNurseStation]    Script Date: 23/06/2017 10:34:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCustomerCareProcessListNurseStation]
	@StartDateTime datetime, 
	@EndDateTime datetime, 
	@Status int, 
	@ActiveStatus int, 
	@ClosedStatus int,
	@MaxRecords int,
	@TVPTableLOC TVPInteger readonly,
	@TVPTableIdentifiers TVPInteger readonly
	
AS

	SELECT DISTINCT TOP(@MaxRecords) CAP.[ID], C.[ID] AS CustomerID, C.PersonID, 
   (CASE WHEN CR.[ID] IS NULL THEN C.CHNumber ELSE CR.CHNumber END) CHNumber, 
   P.FirstName, P.LastName, P.LastName2, P.ImageID, PSD.Sex, PSD.BirthDate, 
   PSD.DeathDateTime, PSD.DeathReason, C.CustomerNameConfidentiality, C.CustomerIdentifierConfidentiality, C.PoorlyIdentified, 
   ISNULL((SELECT TOP 1 PIR.IDNumber FROM PersonIdentifierRel PIR WITH(NOLOCK) 
			JOIN @TVPTableIdentifiers TVPId ON PIR.IdentifierTypeID=TVPId.[ID]
			WHERE (PIR.PersonID=P.[ID])), '') AS IDNumber, 
ISNULL((SELECT TOP 1 IT.[Name] FROM PersonIdentifierRel PIR WITH(NOLOCK) 
		JOIN IdentifierType IT WITH(NOLOCK) ON PIR.IdentifierTypeID=IT.[ID] 
		JOIN @TVPTableIdentifiers TVPId ON PIR.IdentifierTypeID=TVPId.[ID]
		WHERE (PIR.PersonID=P.[ID])), '') AS DefaultIdentifier, 
 
   CE.[ID] AS EpisodeID, CP.[ID] CustomerProcessID, CE.EpisodeNumber, CE.EpisodeTypeID, ET.[Description] EpisodeTypeName, ET.[EpisodeCase], CE.StartDateTime, CE.EndDateTime, CE.[Status], CE.ProcessChartID, PC.Name AS ProcessChartName, ORG.[Name] AdmissionInsurerName,
   CA.CurrentLocationID,   
   (CASE WHEN CTE.TransferAction = 5 AND CTE.TemporalTransferStatus = 0 THEN L.[Name] + ' (' + (SELECT TOP 1 [Name] FROM [Location] WHERE [ID] = CTE.[TargetLocationID]) + ')'
              WHEN CTE2.TransferAction = 5 AND CTE2.TemporalTransferStatus = 1 THEN L.[Name]
              ELSE (SELECT TOP 1 [Name] FROM [Location] WHERE [ID] = CA.[CurrentLocationID]) END) CurrentLocationName, 
   L.LocationTypeID CurrentLocationTypeID, 
   ISNULL(L.LocationClassID,0) CurrentLocationClassID, LC.Description AS CurrentLocationClassName, ISNULL(EA.EquipmentID,0) CurrentEquipmentID, 
    PH.[ID] PhysID, ISNULL(PHP.FirstName,'') PhysFirstName, ISNULL(PHP.LastName,'') PhysLastName, ISNULL(PHP.LastName2,'') PhysLastName2, 
   (CASE WHEN EXISTS(SELECT CEPR.[ID] FROM CustomerEpisodeReasonRel CEPR WITH(NOLOCK) WHERE CEPR.CustomerEpisodeID = CE.[ID] AND NOT(CEPR.EpisodeReasonTypeID=0) AND NOT(CEPR.EpisodeReasonID=0))  
   THEN (SELECT TOP 1 EPR.FullySpecifiedName 
           FROM CustomerEpisodeReasonRel CEPR WITH(NOLOCK) JOIN EpisodeReasonType EPRT WITH(NOLOCK) ON CEPR.EpisodeReasonTypeID = EPRT.[ID]
           JOIN EpisodeReason EPR WITH(NOLOCK) ON CEPR.EpisodeReasonID = EPR.[ID] WHERE CEPR.CustomerEpisodeID = CE.[ID]) ELSE '' END) EpisodeReasonName, 
   ISNULL(ASSD.Name, '') LastAssistanceDegreeName,
   ISNULL(ASSD.DegreeColor, 0) LastAssistanceDegreeColor,
   CPA.InitDate FirstPreAssessment,
   CPA.InitDate LastPreAssessment,
   CPA.ConfirmedDate ConfirmLastPreAssessment,
   '' AssistanceServiceUnitName,
   0 HasActivity, 0 HasPrescriptions, 0 HasOrders,
    (CASE WHEN EXISTS(SELECT RA.[ID] 
    	FROM RoutineAct RA WITH(NOLOCK)
    	JOIN Routine R WITH(NOLOCK) ON RA.RoutineID = R.[ID]  
    	JOIN RoutineTypeEditBlock RTEB WITH(NOLOCK) ON RTEB.RoutineTypeID=R.RoutineTypeID  
    	WHERE RA.EpisodeID = CE.[ID] AND RA.[Status]=1 
    	AND RTEB.RoutineEditBlock = 12 AND NOT(RTEB.RoutineEditBlock IN (1,3,7,8,10,11,15,16))) 
    	THEN 1 
    	WHEN EXISTS(SELECT RA.[ID]  
    	FROM RoutineAct RA WITH(NOLOCK)
    	JOIN Routine R WITH(NOLOCK) ON RA.RoutineID = R.[ID]  
    	JOIN RoutineTypeEditBlock RTEB WITH(NOLOCK) ON RTEB.RoutineTypeID=R.RoutineTypeID  
    	WHERE RA.EpisodeID = CE.[ID] AND RA.[Status] IN (8,64) AND NOT(RA.[Status]=1) 
    	AND RTEB.RoutineEditBlock = 12 AND NOT(RTEB.RoutineEditBlock IN (1,3,7,8,10,11,15,16))) 
    	THEN 64 
    	WHEN EXISTS(SELECT RA.[ID]  
    	FROM RoutineAct RA WITH(NOLOCK) 
    	JOIN Routine R WITH(NOLOCK) ON RA.RoutineID = R.[ID]  
    	JOIN RoutineTypeEditBlock RTEB WITH(NOLOCK) ON RTEB.RoutineTypeID=R.RoutineTypeID  
    	WHERE RA.EpisodeID = CE.[ID] AND RA.[Status] IN (2,16) AND NOT(RA.[Status] IN (1,8,64)) 
    	AND RTEB.RoutineEditBlock = 12 AND NOT(RTEB.RoutineEditBlock IN (1,3,7,8,10,11,15,16))) 
    	THEN 16 ELSE 0 END) ClinicalNotesStatus,  
    (CASE WHEN EXISTS(SELECT RA.[ID]  
    	FROM RoutineAct RA WITH(NOLOCK)
    	JOIN Routine R WITH(NOLOCK) ON RA.RoutineID = R.[ID]  
    	JOIN RoutineTypeEditBlock RTEB WITH(NOLOCK) ON RTEB.RoutineTypeID=R.RoutineTypeID  
    	WHERE RA.EpisodeID = CE.[ID] AND RA.[Status]=1 
    	AND (RTEB.RoutineEditBlock IN (1,3,7,8,10,11,15,16))) 
    	THEN 1 
    	WHEN EXISTS(SELECT RA.[ID]  
    	FROM RoutineAct RA WITH(NOLOCK) 
    	JOIN Routine R WITH(NOLOCK) ON RA.RoutineID = R.[ID]  
    	JOIN RoutineTypeEditBlock RTEB WITH(NOLOCK) ON RTEB.RoutineTypeID=R.RoutineTypeID  
    	WHERE RA.EpisodeID = CE.[ID] AND RA.[Status] IN (8,64) AND NOT(RA.[Status]=1) 
    	AND (RTEB.RoutineEditBlock IN (1,3,7,8,10,11,15,16))) 
    	THEN 64 
    	WHEN EXISTS(SELECT RA.[ID]  
    	FROM RoutineAct RA WITH(NOLOCK) 
    	JOIN Routine R WITH(NOLOCK) ON RA.RoutineID = R.[ID]  
    	JOIN RoutineTypeEditBlock RTEB WITH(NOLOCK) ON RTEB.RoutineTypeID=R.RoutineTypeID  
    	WHERE RA.EpisodeID = CE.[ID] AND RA.[Status] IN (2,16) AND NOT(RA.[Status] IN (1,8,64)) 
    	AND (RTEB.RoutineEditBlock IN (1,3,7,8,10,11,15,16))) 
    	THEN 16 ELSE 0 END) ServiceActsStatus,  
    (CASE  
    WHEN EXISTS(SELECT * FROM CustomerOrderRequest COR WITH(NOLOCK) 
    	JOIN PrescriptionRequest PR WITH(NOLOCK) ON COR.[ID]=PR.CustomerOrderRequestID   
        JOIN OrderRequestSchPlanning ORSP WITH(NOLOCK) ON ORSP.CustomerOrderRequestID=COR.[ID]
    	WHERE COR.CustomerEpisodeID=CE.ID  
    	AND ((ORSP.FrequencyOfApplicationID > 0) OR (ORSP.FrequencyOfApplicationMeaning <> ''))
    	AND PR.StartDateTime <= GETDATE() AND NOT(PR.[Status] = 128)) 
    THEN 16 ELSE 0 END) PrescriptionSupplyStatus, 
    (CASE  
    WHEN EXISTS(SELECT * FROM CustomerOrderRequest COR WITH(NOLOCK) 
    	JOIN OrderRequestSchPlanning ORSP WITH(NOLOCK) ON COR.[ID]=ORSP.CustomerOrderRequestID   
    	WHERE COR.CustomerEpisodeID=CE.ID  
    	AND ORSP.OrderEffectiveAt <= GETDATE() 
    	AND NOT(EXISTS(SELECT PR.[ID] FROM PrescriptionRequest PR WITH(NOLOCK) WHERE PR.CustomerOrderRequestID=COR.[ID] AND NOT(PR.[Status] = 128))) )
    THEN 16 ELSE 0 END) OrderRequestsStatus, 
   L.ParentLocationID,
   ISNULL((SELECT TOP 1 PL.[Name] FROM Location PL WITH(NOLOCK) WHERE PL.ID = L.ParentLocationID),'') ParentLocationName,
   '' DiagCIE, '' DiagDescription
FROM CustomerEpisode CE WITH(NOLOCK)
    JOIN Customer C WITH(NOLOCK) ON CE.CustomerID=C.[ID] 
    JOIN Person P WITH(NOLOCK) ON C.PersonID=P.[ID] 
    JOIN SensitiveData PSD WITH(NOLOCK) ON P.[ID]=PSD.PersonID 
    JOIN CustomerAssistancePlan CAP WITH(NOLOCK) ON CAP.EpisodeID=CE.[ID] 
    JOIN EpisodeType ET WITH(NOLOCK) ON CE.EpisodeTypeID=ET.[ID] 
    JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] 
    JOIN CustomerProcess CP WITH(NOLOCK) ON CP.CustomerEpisodeID = CE.[ID] 
    JOIN ProcessChart PC WITH(NOLOCK) ON CE.ProcessChartID=PC.[ID] 
    LEFT JOIN Location L WITH(NOLOCK) ON CA.CurrentLocationID=L.[ID] 
    LEFT JOIN LocationClass LC WITH(NOLOCK) ON L.LocationClassID=LC.[ID] 
    LEFT JOIN EquipmentAvailability EA WITH(NOLOCK) ON CE.CurrentEquipmentAvailID=EA.[ID] 
    LEFT JOIN Physician PH WITH(NOLOCK) ON CE.PhysicianID = PH.[ID] LEFT JOIN Person PHP WITH(NOLOCK) ON PH.PersonID = PHP.[ID] 
    LEFT JOIN CustomerRelatedCHNumber CR WITH(NOLOCK) ON CR.CustomerID=CE.CustomerID AND CR.CareCenterID=CP.CareCenterID
    LEFT JOIN CustomerProcessStepsRel CPSR WITH(NOLOCK) ON CPSR.CustomerProcessID=CP.ID AND CPSR.Step=1024 
    LEFT JOIN CustomerPreAssessment CPA WITH(NOLOCK) ON CPA.CustomerID=C.[ID] AND CPA.[ID]=CPSR.CurrentStepID
    LEFT JOIN AssistanceDegree ASSD WITH(NOLOCK) ON CPA.AssistanceDegreeRecommendedID=ASSD.[ID] 
	LEFT JOIN Insurer INS WITH(NOLOCK) ON CE.AdmissionInsurerID=INS.[ID]
    LEFT JOIN Organization ORG WITH(NOLOCK) ON INS.OrganizationID=ORG.[ID]
    LEFT JOIN CustomerTransferEntry CTE WITH(NOLOCK) ON CP.CustomerEpisodeID = CTE.[SourceEpisodeID] AND CTE.[TransferAction] = 5 AND CTE.[TemporalTransferStatus] = 0 AND CTE.[Status] = 1 
    LEFT JOIN CustomerTransferEntry CTE2 WITH(NOLOCK) ON CP.CustomerEpisodeID = CTE2.[SourceEpisodeID] AND CTE2.[TransferAction] = 5 AND CTE2.[TemporalTransferStatus] = 1 AND CTE2.[Status] = 1 
	JOIN @TVPTableLOC TVP ON CA.CurrentLocationID=TVP.[ID]

WHERE ((SELECT TOP(1) CAssP.[ID] FROM CustomerAssistancePlan CAssP WITH(NOLOCK) WHERE CAssP.EpisodeID=CE.[ID] ORDER BY CAssP.[ID] DESC) = CAP.[ID])
AND (CE.Status != @Status) 
AND ((CE.Status = @ActiveStatus) OR ((CE.EndDateTime>=@StartDateTime) 
	AND (CE.Status=@ClosedStatus))) 
AND (CE.StartDateTime<=@EndDateTime)