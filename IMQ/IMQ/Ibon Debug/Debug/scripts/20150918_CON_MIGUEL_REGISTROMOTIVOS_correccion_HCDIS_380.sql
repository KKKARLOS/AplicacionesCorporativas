USE HCDIS
DECLARE @ProcedureElementID INT
DECLARE @ProcedureINTID INT
SET @ProcedureElementID = ISNULL((SELECT TOP 1 ID FROM EACElement WHERE Name = 'ProcedureEntity'),0)
SET @ProcedureINTID = ISNULL((SELECT TOP 1 ID FROM [Procedure] WHERE AssignedCode = 'INTERVENCIONES'),0)
UPDATE [ReasonChange] SET [ElementID]= @ProcedureElementID, [EntityID] =@ProcedureINTID
WHERE [AssignedCode]='INTCAN1011' AND [Reason]= 'PACIENTE'

UPDATE [ReasonChange] SET [AssignedCode] = REPLACE([AssignedCode],'CAN','ANU')
WHERE [AssignedCode] like 'INTCAN%' AND [ReasonChangeType] =1

UPDATE [ReasonChange] SET [AssignedCode] = REPLACE([AssignedCode],'CAN','ANU')
WHERE [AssignedCode] like 'ORDCAN%' AND [ReasonChangeType] =1

