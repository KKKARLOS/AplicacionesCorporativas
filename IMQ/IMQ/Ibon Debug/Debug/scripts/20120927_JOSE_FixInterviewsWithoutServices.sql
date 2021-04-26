BEGIN TRAN
DECLARE @TempTable AS TABLE (actid int, id int)
INSERT INTO @TempTable
SELECT cia.[ID], cia.[CustomerInterviewID] FROM customerinterviewact cia
LEFT JOIN interviewservice isw on cia.[Id]=isw.CustomerInterviewActID
where isw.Id is null

delete from CustomerInterviewAct where [ID] in (SELECT actID FROM @TempTable) 

DECLARE @TempTable2 as Table (id int)
INSERT INTO @TempTable2
SELECT DISTINCT T.[ID] FROM @TempTable T
JOIN CustomerInterview ci ON T.[ID]=ci.[ID]
LEFT JOIN CustomerInterviewAct cia ON ci.[Id]=cia.CustomerInterviewID
WHERE cia.[Id] is null

delete from customerinterview where [ID] in
(SELECT [ID] FROM @TempTable2)

update CustomerProcessStepsRel set CurrentStepID=0, StepStatus=0, StepDateTime=null
where [ID] in (select [ID] from CustomerProcessStepsRel where step=512 and currentstepid in (SELECT [ID] FROM @TempTable2))
COMMIT TRAN