--TEMPLATE PARAMETERS
DECLARE @OTElementType INT
SET @OTElementType=4
DECLARE @OTSourceType INT
SET @OTSourceType=16
--BLOCK PARAMETERS
DECLARE @OBElementType INT
SET @OBElementType=2
--OBSERVATION PARAMETERS
DECLARE @OElementType INT
SET @OElementType=1

UPDATE ObservationTemplate SET InUse='False'
UPDATE ObservationTemplate SET InUse='True'
WHERE InUse='False' AND ([ID] IN (SELECT DISTINCT ElementID FROM RoutineObservationRel WHERE ElementType=@OTElementType
								UNION
								SELECT DISTINCT ElementID FROM ProcedureObservationRel WHERE ElementType=@OTElementType
								UNION
								SELECT DISTINCT ElementID FROM OrderObservationRel WHERE ElementType=@OTElementType
								UNION
								SELECT DISTINCT ElementID FROM SpecialCategoryObsRel WHERE ElementType=@OTElementType
								UNION
								SELECT DISTINCT ElementID FROM ProtocolObservationRel WHERE ElementType=@OTElementType
								UNION
								SELECT DISTINCT ReferenceID FROM StepPreprint WHERE SourceType=@OTSourceType --StepPreprints
								UNION
								SELECT DISTINCT ReferenceID FROM PollDocumentRel WHERE SourceType=@OTSourceType --PollDocuments
								UNION
								SELECT DISTINCT ObservationTemplateID FROM ReportActsConfig --Reports
								UNION
								SELECT DISTINCT ObservationTemplateID FROM InterviewObsTemplateRel --Interviews
								UNION
								SELECT DISTINCT ObservationTemplateID FROM PreAssessmentObsTemplateRel --PreAssessments
								UNION
								SELECT DISTINCT ObservationTemplateID FROM ContactConfig --Contact
								UNION
								SELECT DISTINCT ObservationTemplateID FROM VisitConfig --Visit
								UNION
								SELECT DISTINCT ObservationTemplateID FROM MedEpisodeObsTemplateRel --MedicalEpisodeTypes
								UNION
								SELECT DISTINCT ObservationTemplateID FROM CustomerTemplate --CustomerTemplate
								)
							)

UPDATE ObservationBlock SET InUse='False'
UPDATE ObservationBlock SET InUse='True'
WHERE InUse='False' AND ([ID] IN (SELECT DISTINCT ObservationBlockID FROM ObservationTemplateRel WHERE (ObservationBlockID > 0)
								UNION
								SELECT DISTINCT ElementID FROM RoutineObservationRel WHERE ElementType=@OBElementType
								UNION
								SELECT DISTINCT ElementID FROM ProcedureObservationRel WHERE ElementType=@OBElementType
								UNION
								SELECT DISTINCT ElementID FROM OrderObservationRel WHERE ElementType=@OBElementType
								UNION
								SELECT DISTINCT ElementID FROM SpecialCategoryObsRel WHERE ElementType=@OBElementType
								UNION
								SELECT DISTINCT ElementID FROM ProtocolObservationRel WHERE ElementType=@OBElementType
								)
						)

UPDATE Observation SET InUse='False'		
UPDATE Observation SET InUse='True'
WHERE InUse='False' AND ([ID] IN (SELECT DISTINCT ObservationID FROM ObservationTemplateRel WHERE (ObservationID > 0)
								UNION
								SELECT DISTINCT ObservationID FROM ObservationBlockRel WHERE (ObservationID > 0)
								UNION
								SELECT DISTINCT ElementID FROM RoutineObservationRel WHERE ElementType=@OElementType
								UNION
								SELECT DISTINCT ElementID FROM ProcedureObservationRel WHERE ElementType=@OElementType
								UNION
								SELECT DISTINCT ElementID FROM OrderObservationRel WHERE ElementType=@OElementType
								UNION
								SELECT DISTINCT ElementID FROM SpecialCategoryObsRel WHERE ElementType=@OElementType
								UNION
								SELECT DISTINCT ElementID FROM ProtocolObservationRel WHERE ElementType=@OElementType
								)
						)	
						
UPDATE ObservationTemplate SET InUseByCustomer='False'
UPDATE ObservationTemplate SET InUseByCustomer='True'
WHERE InUseByCustomer='False' AND ([ID] IN (SELECT DISTINCT ObservationTemplateID FROM CustomerTemplate))

UPDATE ObservationBlock SET InUseByCustomer='False'
UPDATE ObservationBlock SET InUseByCustomer='True'
WHERE InUseByCustomer='False' AND ([ID] IN (SELECT DISTINCT ObservationBlockID FROM CustomerBlock))

UPDATE Observation SET InUseByCustomer='False'
UPDATE Observation SET InUseByCustomer='True'
WHERE InUseByCustomer='False' AND ([ID] IN (SELECT DISTINCT ObservationID FROM CustomerObservation))