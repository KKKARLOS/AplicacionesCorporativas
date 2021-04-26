USE [DBNAME] 

UPDATE EACAttribute SET [Required] = 0
FROM EACAttribute AT
	JOIN EACElement E ON AT.EACElementID=E.[ID]
WHERE (E.Name = 'RoutineActEntity') AND (AT.Name = 'LocationID')

UPDATE EACAttribute SET [Required] = 0
FROM EACAttribute AT
	JOIN EACElement E ON AT.EACElementID=E.[ID]
WHERE (E.Name = 'ProcedureActEntity') AND (AT.Name = 'LocationID')
