/****** Object:  StoredProcedure [dbo].[GetNotParentCustomerOrderRequestsNurseStation]    Script Date: 23/06/2017 10:34:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetNotParentCustomerOrderRequestsNurseStation]
	@StartDateTime datetime, 
	@EndDateTime datetime, 
	@OrderControlCode int, 
	@ActionStatusSuperceded int, 

	@TVPTableLOC TVPInteger readonly
	
AS

	SELECT DISTINCT COR.[ID], COR.CustomerID, COR.CustomerEpisodeID EpisodeID, COR.OrderControlCode, OT.OrderCase AS OrderClassificationType, OT.AssignedCode AS OrderTypeCode, 
    O.[Name] AS OrderName, COR.OrderNumber, COR.PlaceOrderNumber, COR.Priority, COR.RequestDateTime RequestedDateTime, COR.RequestEffectiveAtDateTime, 
    RPhy.[ID] RequestedPhysicianID, ISNULL(RPHP.FirstName,'') PhysFirstName, ISNULL(RPHP.LastName,'') PhysLastName, ISNULL(RPHP.LastName2,'') PhysLastName2, 
    COR.RelevantClinicalInfo, COR.RequestExplanation, COR.ResponseFlag ResponseFlags,
    CAST((CASE WHEN EXISTS((SELECT CORE.ID FROM CustomerOrderRealization CORE WITH(NOLOCK) WHERE CORE.CustomerOrderRequestID=COR.ID AND CORE.ResultsChecked = 1)) THEN 1 ELSE 0 END) as BIT) ResultsChecked,
    CAST((CASE WHEN EXISTS((SELECT IR.ID FROM InterpretationReport IR WITH(NOLOCK) WHERE IR.AppElement = 3 AND IR.ElementID = COR.ID)) THEN 1 ELSE 0 END) as BIT) InterpretationReportAvailable,
    CCORG.[Name] RequestedCareCenterName, ASS.[ID] RequestedAssistanceServiceID, ASS.[Name] RequestedAssistanceServiceName, 
    MS.[Name] RequestedMedicalSpecialtyName, LOC.[Name] RequestedLocationName, 
    COR.RequestedInsurerID, ORG.[Name] RequestedInsurerName, COR.PolicyTypeID, PT.Name PolicyTypeName, ADTORP.OrderEffectiveAt EstimatedAdmissionDT, 
    ORSP.OrderEffectiveAt, ORSP.EstimatedFinalizeAt,  ORSP.FrequencyOfApplicationID, ORSP.ID OrderRequestSchPlanningID, 
    ISNULL(TP.[Description],'') AS FrequencyOfApplication, ORSP.FrequencyOfApplicationMeaning as Meaning, ORSP.EstimatedDurationID, 
    PR.UnitaryQty UnitaryQuantity, PR.UnitaryDose, I.GenericName AS RequestAction, 0 AS ItemTreatmentRouteID, 
    (CASE WHEN NOT(I.[ID] IS NULL) THEN 2 WHEN NOT(ORSP.[ID] IS NULL) THEN 1 ELSE 0 END) AdditionalInfo, COR.[Status], COR.ModifiedBy, CAST(COR.DBTimeStamp as BIGINT) AS DBTimeStamp 
FROM CustomerOrderRequest COR WITH(NOLOCK) 
    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]
    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID=OT.[ID]
    JOIN CustomerEpisode CE WITH(NOLOCK) ON COR.CustomerEpisodeID=CE.[ID] 
    JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] 
	JOIN @TVPTableLOC TVP ON CA.CurrentLocationID=TVP.[ID]
    LEFT JOIN PrescriptionRequest PR WITH(NOLOCK) ON COR.[ID]=PR.CustomerOrderRequestID AND NOT(PR.[Status]=@ActionStatusSuperceded)
    LEFT JOIN Person RPHP WITH(NOLOCK) ON COR.RequestedPersonID= RPHP.[ID] 
    LEFT JOIN Physician RPhy WITH(NOLOCK) ON RPhy.PersonID = RPHP.[ID]
    LEFT JOIN AssistanceService ASS WITH(NOLOCK) ON COR.AssistanceServiceID=ASS.ID 
    LEFT JOIN Location LOC WITH(NOLOCK) ON COR.RequestedLocationID=LOC.[ID] 
    LEFT JOIN Insurer IR WITH(NOLOCK) ON COR.RequestedInsurerID=IR.[ID] 
    LEFT JOIN Organization ORG WITH(NOLOCK) ON IR.OrganizationID = ORG.[ID] 
    LEFT JOIN PolicyType PT WITH(NOLOCK) ON COR.PolicyTypeID=PT.[ID] 
    LEFT JOIN CareCenter CC WITH(NOLOCK) ON COR.RequestedCareCenterID=CC.[ID] 
    LEFT JOIN Organization CCORG WITH(NOLOCK) ON CC.OrganizationID = CCORG.[ID] 
    LEFT JOIN CustomerOrderRequest ADTCOR WITH(NOLOCK) ON COR.ParentCustomerOrderRequestID=ADTCOR.[ID]
    LEFT JOIN OrderRequestSchPlanning ADTORP WITH(NOLOCK) ON ADTCOR.[ID]=ADTORP.CustomerOrderRequestID
    LEFT JOIN OrderRequestADTInfo ADTInfo WITH(NOLOCK) ON ADTCOR.[ID]=ADTInfo.CustomerOrderRequestID
    LEFT JOIN OrderRequestSchPlanning ORSP WITH(NOLOCK) ON COR.[ID]=ORSP.CustomerOrderRequestID 
    LEFT JOIN Item I WITH(NOLOCK) ON PR.ItemID=I.[ID] 
    LEFT JOIN OrderRequestCustomerProcedureRel ORCPR WITH(NOLOCK) ON ORSP.[ID]=ORCPR.OrderRequestSchPlanningID 
    LEFT JOIN OrderRequestCustomerRoutineRel ORCRR WITH(NOLOCK) ON ORSP.[ID]=ORCRR.OrderRequestSchPlanningID 
    LEFT JOIN MedicalSpecialty MS WITH(NOLOCK) ON MS.ID=COR.MedicalSpecialtyID
    LEFT JOIN TimePattern TP WITH(NOLOCK) ON ORSP.FrequencyOfApplicationID=TP.[ID] 
WHERE NOT(COR.OrderControlCode=@OrderControlCode) 
 AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) AND (CE.StartDateTime<=@EndDateTime) 
UNION
SELECT DISTINCT COR.[ID], COR.CustomerID, COR.CustomerEpisodeID EpisodeID, COR.OrderControlCode, OT.OrderCase AS OrderClassificationType, OT.AssignedCode AS OrderTypeCode, 
    O.[Name] AS OrderName, COR.OrderNumber, COR.PlaceOrderNumber, COR.Priority, COR.RequestDateTime RequestedDateTime, COR.RequestEffectiveAtDateTime, 
    RPhy.[ID] RequestedPhysicianID, ISNULL(RPHP.FirstName,'') PhysFirstName, ISNULL(RPHP.LastName,'') PhysLastName, ISNULL(RPHP.LastName2,'') PhysLastName2, 
    COR.RelevantClinicalInfo, COR.RequestExplanation, COR.ResponseFlag ResponseFlags,
    CAST((CASE WHEN EXISTS((SELECT CORE.ID FROM CustomerOrderRealization CORE WITH(NOLOCK) WHERE CORE.CustomerOrderRequestID=COR.ID AND CORE.ResultsChecked = 1)) THEN 1 ELSE 0 END) as BIT) ResultsChecked,
    CAST((CASE WHEN EXISTS((SELECT IR.ID FROM InterpretationReport IR WITH(NOLOCK) WHERE IR.AppElement = 3 AND IR.ElementID = COR.ID)) THEN 1 ELSE 0 END) as BIT) InterpretationReportAvailable,
    CCORG.[Name] RequestedCareCenterName, ASS.[ID] RequestedAssistanceServiceID, ASS.[Name] RequestedAssistanceServiceName, 
    MS.[Name] RequestedMedicalSpecialtyName, LOC.[Name] RequestedLocationName, 
    COR.RequestedInsurerID, ORG.[Name] RequestedInsurerName, COR.PolicyTypeID, PT.Name PolicyTypeName, ADTORP.OrderEffectiveAt EstimatedAdmissionDT, 
    ORSP.OrderEffectiveAt, ORSP.EstimatedFinalizeAt,  ORSP.FrequencyOfApplicationID, ORSP.ID OrderRequestSchPlanningID, 
    ISNULL(TP.[Description],'') AS FrequencyOfApplication, ORSP.FrequencyOfApplicationMeaning as Meaning, ORSP.EstimatedDurationID, 
    PR.UnitaryQty UnitaryQuantity, PR.UnitaryDose, I.GenericName AS RequestAction, 0 AS ItemTreatmentRouteID, 
    (CASE WHEN NOT(I.[ID] IS NULL) THEN 2 WHEN NOT(ORSP.[ID] IS NULL) THEN 1 ELSE 0 END) AdditionalInfo, COR.[Status], COR.ModifiedBy, CAST(COR.DBTimeStamp as BIGINT) AS DBTimeStamp 
FROM CustomerOrderRequest COR WITH(NOLOCK)
    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]
    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID=OT.[ID]
    JOIN OrderRequestSchPlanning ORSP WITH(NOLOCK) ON COR.[ID]=ORSP.CustomerOrderRequestID
    JOIN CustomerRoutine CR WITH(NOLOCK) ON COR.[ID]=CR.CustomerOrderRequestID
    JOIN CustomerEpisode CE WITH(NOLOCK) ON CR.EpisodeID=CE.[ID]
    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID
	JOIN @TVPTableLOC TVP ON CA.CurrentLocationID=TVP.[ID]
    LEFT JOIN PrescriptionRequest PR WITH(NOLOCK) ON COR.[ID]=PR.CustomerOrderRequestID AND NOT(PR.[Status]=@ActionStatusSuperceded)
    LEFT JOIN Person RPHP WITH(NOLOCK) ON COR.RequestedPersonID= RPHP.[ID]
    LEFT JOIN Physician RPhy WITH(NOLOCK)  ON RPhy.PersonID = RPHP.[ID]
    LEFT JOIN AssistanceService ASS WITH(NOLOCK) ON COR.AssistanceServiceID=ASS.ID
    LEFT JOIN Location LOC WITH(NOLOCK) ON COR.RequestedLocationID=LOC.[ID]
    LEFT JOIN Insurer IR WITH(NOLOCK) ON COR.RequestedInsurerID=IR.[ID]
    LEFT JOIN Organization ORG WITH(NOLOCK) ON IR.OrganizationID = ORG.[ID]
    LEFT JOIN PolicyType PT WITH(NOLOCK) ON COR.PolicyTypeID=PT.[ID]
    LEFT JOIN CareCenter CC WITH(NOLOCK) ON COR.RequestedCareCenterID=CC.[ID]
    LEFT JOIN Organization CCORG WITH(NOLOCK) ON CC.OrganizationID = CCORG.[ID]
    LEFT JOIN Item I WITH(NOLOCK) ON PR.ItemID=I.[ID]
    LEFT JOIN OrderRequestCustomerProcedureRel ORCPR WITH(NOLOCK) ON ORSP.[ID]=ORCPR.OrderRequestSchPlanningID
    LEFT JOIN OrderRequestCustomerRoutineRel ORCRR WITH(NOLOCK) ON ORSP.[ID]=ORCRR.OrderRequestSchPlanningID
    LEFT JOIN MedicalSpecialty MS WITH(NOLOCK) ON MS.ID=COR.MedicalSpecialtyID
    LEFT JOIN TimePattern TP WITH(NOLOCK) ON ORSP.FrequencyOfApplicationID=TP.[ID]
    LEFT JOIN CustomerOrderRequest ADTCOR WITH(NOLOCK) ON COR.ParentCustomerOrderRequestID=ADTCOR.[ID]
    LEFT JOIN OrderRequestSchPlanning ADTORP WITH(NOLOCK) ON ADTCOR.[ID]=ADTORP.CustomerOrderRequestID
    LEFT JOIN OrderRequestADTInfo ADTInfo WITH(NOLOCK) ON ADTCOR.[ID]=ADTInfo.CustomerOrderRequestID
WHERE NOT(COR.OrderControlCode=@OrderControlCode) 
 AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) AND (CE.StartDateTime<=@EndDateTime) 
UNION
SELECT DISTINCT COR.[ID], COR.CustomerID, COR.CustomerEpisodeID EpisodeID, COR.OrderControlCode, OT.OrderCase AS OrderClassificationType, OT.AssignedCode AS OrderTypeCode, 
    O.[Name] AS OrderName, COR.OrderNumber, COR.PlaceOrderNumber, COR.Priority, COR.RequestDateTime RequestedDateTime, COR.RequestEffectiveAtDateTime, 
    RPhy.[ID] RequestedPhysicianID, ISNULL(RPHP.FirstName,'') PhysFirstName, ISNULL(RPHP.LastName,'') PhysLastName, ISNULL(RPHP.LastName2,'') PhysLastName2, 
    COR.RelevantClinicalInfo, COR.RequestExplanation, COR.ResponseFlag ResponseFlags,
    CAST((CASE WHEN EXISTS((SELECT CORE.ID FROM CustomerOrderRealization CORE WITH(NOLOCK) WHERE CORE.CustomerOrderRequestID=COR.ID AND CORE.ResultsChecked = 1)) THEN 1 ELSE 0 END) as BIT) ResultsChecked,
    CAST((CASE WHEN EXISTS((SELECT IR.ID FROM InterpretationReport IR WITH(NOLOCK) WHERE IR.AppElement = 3 AND IR.ElementID = COR.ID)) THEN 1 ELSE 0 END) as BIT) InterpretationReportAvailable,
    CCORG.[Name] RequestedCareCenterName, ASS.[ID] RequestedAssistanceServiceID, ASS.[Name] RequestedAssistanceServiceName, 
    MS.[Name] RequestedMedicalSpecialtyName, LOC.[Name] RequestedLocationName, 
    COR.RequestedInsurerID, ORG.[Name] RequestedInsurerName, COR.PolicyTypeID, PT.Name PolicyTypeName, ADTORP.OrderEffectiveAt EstimatedAdmissionDT, 
    ORSP.OrderEffectiveAt, ORSP.EstimatedFinalizeAt,  ORSP.FrequencyOfApplicationID, ORSP.ID OrderRequestSchPlanningID, 
    ISNULL(TP.[Description],'') AS FrequencyOfApplication, ORSP.FrequencyOfApplicationMeaning as Meaning, ORSP.EstimatedDurationID, 
    PR.UnitaryQty UnitaryQuantity, PR.UnitaryDose, I.GenericName AS RequestAction, 0 AS ItemTreatmentRouteID, 
    (CASE WHEN NOT(I.[ID] IS NULL) THEN 2 WHEN NOT(ORSP.[ID] IS NULL) THEN 1 ELSE 0 END) AdditionalInfo, COR.[Status], COR.ModifiedBy, CAST(COR.DBTimeStamp as BIGINT) AS DBTimeStamp 
FROM CustomerOrderRequest COR WITH(NOLOCK)
    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]
    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID=OT.[ID]
    JOIN OrderRequestSchPlanning ORSP WITH(NOLOCK) ON COR.[ID]=ORSP.CustomerOrderRequestID
    JOIN CustomerProcedure CP WITH(NOLOCK) ON COR.[ID]=CP.CustomerOrderRequestID
    JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID]
    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID
	JOIN @TVPTableLOC TVP ON CA.CurrentLocationID=TVP.[ID]
    LEFT JOIN PrescriptionRequest PR WITH(NOLOCK) ON COR.[ID]=PR.CustomerOrderRequestID AND NOT(PR.[Status]=@ActionStatusSuperceded)
    LEFT JOIN Person RPHP WITH(NOLOCK) ON COR.RequestedPersonID= RPHP.[ID]
    LEFT JOIN Physician RPhy WITH(NOLOCK)  ON RPhy.PersonID = RPHP.[ID]
    LEFT JOIN AssistanceService ASS WITH(NOLOCK) ON COR.AssistanceServiceID=ASS.ID
    LEFT JOIN Location LOC WITH(NOLOCK) ON COR.RequestedLocationID=LOC.[ID]
    LEFT JOIN Insurer IR WITH(NOLOCK) ON COR.RequestedInsurerID=IR.[ID]
    LEFT JOIN Organization ORG WITH(NOLOCK) ON IR.OrganizationID = ORG.[ID]
    LEFT JOIN PolicyType PT WITH(NOLOCK) ON COR.PolicyTypeID=PT.[ID]
    LEFT JOIN CareCenter CC WITH(NOLOCK) ON COR.RequestedCareCenterID=CC.[ID]
    LEFT JOIN Organization CCORG WITH(NOLOCK) ON CC.OrganizationID = CCORG.[ID]
    LEFT JOIN Item I WITH(NOLOCK) ON PR.ItemID=I.[ID]
    LEFT JOIN OrderRequestCustomerProcedureRel ORCPR WITH(NOLOCK) ON ORSP.[ID]=ORCPR.OrderRequestSchPlanningID
    LEFT JOIN OrderRequestCustomerRoutineRel ORCRR WITH(NOLOCK) ON ORSP.[ID]=ORCRR.OrderRequestSchPlanningID
    LEFT JOIN MedicalSpecialty MS WITH(NOLOCK) ON MS.ID=COR.MedicalSpecialtyID
    LEFT JOIN TimePattern TP WITH(NOLOCK) ON ORSP.FrequencyOfApplicationID=TP.[ID]
    LEFT JOIN CustomerOrderRequest ADTCOR WITH(NOLOCK) ON COR.ParentCustomerOrderRequestID=ADTCOR.[ID]
    LEFT JOIN OrderRequestSchPlanning ADTORP WITH(NOLOCK) ON ADTCOR.[ID]=ADTORP.CustomerOrderRequestID
    LEFT JOIN OrderRequestADTInfo ADTInfo WITH(NOLOCK) ON ADTCOR.[ID]=ADTInfo.CustomerOrderRequestID
WHERE NOT(COR.OrderControlCode=@OrderControlCode) 
 AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) AND (CE.StartDateTime<=@EndDateTime) 
UNION
SELECT DISTINCT COR.[ID], COR.CustomerID, COR.CustomerEpisodeID EpisodeID, COR.OrderControlCode, OT.OrderCase AS OrderClassificationType, OT.AssignedCode AS OrderTypeCode, 
    O.[Name] AS OrderName, COR.OrderNumber, COR.PlaceOrderNumber, COR.Priority, COR.RequestDateTime RequestedDateTime, COR.RequestEffectiveAtDateTime, 
    RPhy.[ID] RequestedPhysicianID, ISNULL(RPHP.FirstName,'') PhysFirstName, ISNULL(RPHP.LastName,'') PhysLastName, ISNULL(RPHP.LastName2,'') PhysLastName2, 
    COR.RelevantClinicalInfo, COR.RequestExplanation, COR.ResponseFlag ResponseFlags,
    CAST((CASE WHEN EXISTS((SELECT CORE.ID FROM CustomerOrderRealization CORE WITH(NOLOCK) WHERE CORE.CustomerOrderRequestID=COR.ID AND CORE.ResultsChecked = 1)) THEN 1 ELSE 0 END) as BIT) ResultsChecked,
    CAST((CASE WHEN EXISTS((SELECT IR.ID FROM InterpretationReport IR WITH(NOLOCK) WHERE IR.AppElement = 3 AND IR.ElementID = COR.ID)) THEN 1 ELSE 0 END) as BIT) InterpretationReportAvailable,
    CCORG.[Name] RequestedCareCenterName, ASS.[ID] RequestedAssistanceServiceID, ASS.[Name] RequestedAssistanceServiceName, 
    MS.[Name] RequestedMedicalSpecialtyName, LOC.[Name] RequestedLocationName, 
    COR.RequestedInsurerID, ORG.[Name] RequestedInsurerName, COR.PolicyTypeID, PT.Name PolicyTypeName, ADTORP.OrderEffectiveAt EstimatedAdmissionDT, 
    ORSP.OrderEffectiveAt, ORSP.EstimatedFinalizeAt,  ORSP.FrequencyOfApplicationID, ORSP.ID OrderRequestSchPlanningID, 
    ISNULL(TP.[Description],'') AS FrequencyOfApplication, ORSP.FrequencyOfApplicationMeaning as Meaning, ORSP.EstimatedDurationID, 
    PR.UnitaryQty UnitaryQuantity, PR.UnitaryDose, I.GenericName AS RequestAction, 0 AS ItemTreatmentRouteID, 
    (CASE WHEN NOT(I.[ID] IS NULL) THEN 2 WHEN NOT(ORSP.[ID] IS NULL) THEN 1 ELSE 0 END) AdditionalInfo, COR.[Status], COR.ModifiedBy, CAST(COR.DBTimeStamp as BIGINT) AS DBTimeStamp 
FROM CustomerOrderRequest COR WITH(NOLOCK)
    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]
    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID=OT.[ID]
    JOIN OrderRequestSchPlanning ORSP WITH(NOLOCK) ON COR.[ID]=ORSP.CustomerOrderRequestID
    JOIN RoutineAct RA WITH(NOLOCK) ON COR.[ID]=RA.CustomerOrderRequestID
    JOIN CustomerEpisode CE WITH(NOLOCK) ON RA.EpisodeID=CE.[ID]
    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID
	JOIN @TVPTableLOC TVP ON CA.CurrentLocationID=TVP.[ID]
    LEFT JOIN PrescriptionRequest PR WITH(NOLOCK) ON COR.[ID]=PR.CustomerOrderRequestID AND NOT(PR.[Status]=@ActionStatusSuperceded)
    LEFT JOIN Person RPHP WITH(NOLOCK) ON COR.RequestedPersonID= RPHP.[ID]
    LEFT JOIN Physician RPhy WITH(NOLOCK)  ON RPhy.PersonID = RPHP.[ID]
    LEFT JOIN AssistanceService ASS WITH(NOLOCK) ON COR.AssistanceServiceID=ASS.ID
    LEFT JOIN Location LOC WITH(NOLOCK) ON COR.RequestedLocationID=LOC.[ID]
    LEFT JOIN Insurer IR WITH(NOLOCK) ON COR.RequestedInsurerID=IR.[ID]
    LEFT JOIN Organization ORG WITH(NOLOCK) ON IR.OrganizationID = ORG.[ID]
    LEFT JOIN PolicyType PT WITH(NOLOCK) ON COR.PolicyTypeID=PT.[ID]
    LEFT JOIN CareCenter CC WITH(NOLOCK) ON COR.RequestedCareCenterID=CC.[ID]
    LEFT JOIN Organization CCORG WITH(NOLOCK) ON CC.OrganizationID = CCORG.[ID]
    LEFT JOIN Item I WITH(NOLOCK) ON PR.ItemID=I.[ID]
    LEFT JOIN OrderRequestCustomerProcedureRel ORCPR WITH(NOLOCK) ON ORSP.[ID]=ORCPR.OrderRequestSchPlanningID
    LEFT JOIN OrderRequestCustomerRoutineRel ORCRR WITH(NOLOCK) ON ORSP.[ID]=ORCRR.OrderRequestSchPlanningID
    LEFT JOIN MedicalSpecialty MS WITH(NOLOCK) ON MS.ID=COR.MedicalSpecialtyID
    LEFT JOIN TimePattern TP WITH(NOLOCK) ON ORSP.FrequencyOfApplicationID=TP.[ID]
    LEFT JOIN CustomerOrderRequest ADTCOR WITH(NOLOCK) ON COR.ParentCustomerOrderRequestID=ADTCOR.[ID]
    LEFT JOIN OrderRequestSchPlanning ADTORP WITH(NOLOCK) ON ADTCOR.[ID]=ADTORP.CustomerOrderRequestID
    LEFT JOIN OrderRequestADTInfo ADTInfo WITH(NOLOCK) ON ADTCOR.[ID]=ADTInfo.CustomerOrderRequestID
WHERE NOT(COR.OrderControlCode=@OrderControlCode) 
 AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) AND (CE.StartDateTime<=@EndDateTime) 
UNION
SELECT DISTINCT COR.[ID], COR.CustomerID, COR.CustomerEpisodeID EpisodeID, COR.OrderControlCode, OT.OrderCase AS OrderClassificationType, OT.AssignedCode AS OrderTypeCode, 
    O.[Name] AS OrderName, COR.OrderNumber, COR.PlaceOrderNumber, COR.Priority, COR.RequestDateTime RequestedDateTime, COR.RequestEffectiveAtDateTime, 
    RPhy.[ID] RequestedPhysicianID, ISNULL(RPHP.FirstName,'') PhysFirstName, ISNULL(RPHP.LastName,'') PhysLastName, ISNULL(RPHP.LastName2,'') PhysLastName2, 
    COR.RelevantClinicalInfo, COR.RequestExplanation, COR.ResponseFlag ResponseFlags,
    CAST((CASE WHEN EXISTS((SELECT CORE.ID FROM CustomerOrderRealization CORE WITH(NOLOCK) WHERE CORE.CustomerOrderRequestID=COR.ID AND CORE.ResultsChecked = 1)) THEN 1 ELSE 0 END) as BIT) ResultsChecked,
    CAST((CASE WHEN EXISTS((SELECT IR.ID FROM InterpretationReport IR WITH(NOLOCK) WHERE IR.AppElement = 3 AND IR.ElementID = COR.ID)) THEN 1 ELSE 0 END) as BIT) InterpretationReportAvailable,
    CCORG.[Name] RequestedCareCenterName, ASS.[ID] RequestedAssistanceServiceID, ASS.[Name] RequestedAssistanceServiceName, 
    MS.[Name] RequestedMedicalSpecialtyName, LOC.[Name] RequestedLocationName, 
    COR.RequestedInsurerID, ORG.[Name] RequestedInsurerName, COR.PolicyTypeID, PT.Name PolicyTypeName, ADTORP.OrderEffectiveAt EstimatedAdmissionDT, 
    ORSP.OrderEffectiveAt, ORSP.EstimatedFinalizeAt,  ORSP.FrequencyOfApplicationID, ORSP.ID OrderRequestSchPlanningID, 
    ISNULL(TP.[Description],'') AS FrequencyOfApplication, ORSP.FrequencyOfApplicationMeaning as Meaning, ORSP.EstimatedDurationID, 
    PR.UnitaryQty UnitaryQuantity, PR.UnitaryDose, I.GenericName AS RequestAction, 0 AS ItemTreatmentRouteID, 
    (CASE WHEN NOT(I.[ID] IS NULL) THEN 2 WHEN NOT(ORSP.[ID] IS NULL) THEN 1 ELSE 0 END) AdditionalInfo, COR.[Status], COR.ModifiedBy, CAST(COR.DBTimeStamp as BIGINT) AS DBTimeStamp 
FROM CustomerOrderRequest COR WITH(NOLOCK)
    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]
    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID=OT.[ID]
    JOIN OrderRequestSchPlanning ORSP WITH(NOLOCK) ON COR.[ID]=ORSP.CustomerOrderRequestID
    JOIN ProcedureAct PA WITH(NOLOCK) ON COR.[ID]=PA.CustomerOrderRequestID
    JOIN CustomerEpisode CE WITH(NOLOCK) ON PA.EpisodeID=CE.[ID]
    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID
	JOIN @TVPTableLOC TVP ON CA.CurrentLocationID=TVP.[ID]
    LEFT JOIN PrescriptionRequest PR WITH(NOLOCK) ON COR.[ID]=PR.CustomerOrderRequestID AND NOT(PR.[Status]=@ActionStatusSuperceded)
    LEFT JOIN Person RPHP WITH(NOLOCK) ON COR.RequestedPersonID= RPHP.[ID]
    LEFT JOIN Physician RPhy WITH(NOLOCK)  ON RPhy.PersonID = RPHP.[ID]
    LEFT JOIN AssistanceService ASS WITH(NOLOCK) ON COR.AssistanceServiceID=ASS.ID
    LEFT JOIN Location LOC WITH(NOLOCK) ON COR.RequestedLocationID=LOC.[ID]
    LEFT JOIN Insurer IR WITH(NOLOCK) ON COR.RequestedInsurerID=IR.[ID]
    LEFT JOIN Organization ORG WITH(NOLOCK) ON IR.OrganizationID = ORG.[ID]
    LEFT JOIN PolicyType PT WITH(NOLOCK) ON COR.PolicyTypeID=PT.[ID]
    LEFT JOIN CareCenter CC WITH(NOLOCK) ON COR.RequestedCareCenterID=CC.[ID]
    LEFT JOIN Organization CCORG WITH(NOLOCK) ON CC.OrganizationID = CCORG.[ID]
    LEFT JOIN Item I WITH(NOLOCK) ON PR.ItemID=I.[ID]
    LEFT JOIN OrderRequestCustomerProcedureRel ORCPR WITH(NOLOCK) ON ORSP.[ID]=ORCPR.OrderRequestSchPlanningID
    LEFT JOIN OrderRequestCustomerRoutineRel ORCRR WITH(NOLOCK) ON ORSP.[ID]=ORCRR.OrderRequestSchPlanningID
    LEFT JOIN MedicalSpecialty MS WITH(NOLOCK) ON MS.ID=COR.MedicalSpecialtyID
    LEFT JOIN TimePattern TP WITH(NOLOCK) ON ORSP.FrequencyOfApplicationID=TP.[ID]
    LEFT JOIN CustomerOrderRequest ADTCOR WITH(NOLOCK) ON COR.ParentCustomerOrderRequestID=ADTCOR.[ID]
    LEFT JOIN OrderRequestSchPlanning ADTORP WITH(NOLOCK) ON ADTCOR.[ID]=ADTORP.CustomerOrderRequestID
    LEFT JOIN OrderRequestADTInfo ADTInfo WITH(NOLOCK) ON ADTCOR.[ID]=ADTInfo.CustomerOrderRequestID
	

WHERE NOT(COR.OrderControlCode=@OrderControlCode) 
 AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) 
 AND (CE.StartDateTime<=@EndDateTime)