
UPDATE RoutineActChargeRel SET LinkedEntityID=RCR.LinkedEntityID, LinkedTo=RCR.LinkedTo 
FROM RoutineActChargeRel RACR 
	JOIN RoutineAct RA ON RACR.RoutineActID=RA.[ID]
	JOIN RoutineChargeRel RCR ON (RACR.ServiceChargeID=RCR.ServiceChargeID) AND (RA.RoutineID=RCR.RoutineID)

GO

DECLARE @ElementRoutineID int
SET @ElementRoutineID=ISNULL((SELECT TOP 1 [ID] FROM EACElement WHERE Name='RoutineEntity'), 0)

IF (@ElementRoutineID > 0)
BEGIN
	UPDATE CustomerAccountCharge SET [Description]=R.Name, RealizeEntityID=R.[ID] 
	--SELECT R.NAme, R.ID, CAC.*
	FROM CustomerAccountCharge CAC JOIN Routine R ON R.Code=CAC.RealizeElementAttributeValue
	WHERE (RealizeElementID=@ElementRoutineID)
END
ELSE Print'No se encontro el Metadato de rutina'

