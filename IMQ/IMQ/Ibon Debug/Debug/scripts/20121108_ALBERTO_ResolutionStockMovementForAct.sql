DECLARE @MovementType int
SET @MovementType = 6
DECLARE @RoutineActEntityName nvarchar(50)
SET @RoutineActEntityName = 'RoutineActEntity'
DECLARE @ProcedureActEntityName nvarchar(50)
SET @ProcedureActEntityName = 'ProcedureActEntity' 

--Stock Movement RoutienAct
UPDATE StockMovement SET FromInfo=@RoutineActEntityName
WHERE ((FromInfo = 'Routine') OR (FromInfo = 'Rutina')) AND (MovementType=@MovementType) 

--Stock Movement ProcedureAct
UPDATE StockMovement SET FromInfo=@ProcedureActEntityName
WHERE ((FromInfo = 'Procedure') OR (FromInfo = 'Procedimiento')) AND (MovementType=@MovementType)
