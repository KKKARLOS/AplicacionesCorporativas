update RoutineAct 
set RoutineAct.AssistancePlanID = CR.AssistancePlanID
from RoutineAct join CustomerRoutine CR ON RoutineAct.CustomerRoutineID=CR.ID

update ProcedureAct
set ProcedureAct.AssistancePlanID = CP.AssistancePlanID
from ProcedureAct join CustomerProcedure CP ON ProcedureAct.CustomerProcedureID=CP.ID

update CustomerRoutine 
set CustomerRoutine.CustomerOrderRequestID = ORSP.CustomerOrderRequestID
from CustomerRoutine
JOIN OrderRequestCustomerRoutineRel ORCPR ON ORCPR.CustomerRoutineID=CustomerRoutine.[ID]
JOIN OrderRequestSchPlanning ORSP ON ORCPR.OrderRequestSchPlanningID=ORSP.[ID] 

update CustomerProcedure 
set CustomerProcedure.CustomerOrderRequestID = ORSP.CustomerOrderRequestID
from CustomerProcedure
JOIN OrderRequestCustomerProcedureRel ORCPR ON ORCPR.CustomerProcedureID=CustomerProcedure.[ID]
JOIN OrderRequestSchPlanning ORSP ON ORCPR.OrderRequestSchPlanningID=ORSP.[ID] 