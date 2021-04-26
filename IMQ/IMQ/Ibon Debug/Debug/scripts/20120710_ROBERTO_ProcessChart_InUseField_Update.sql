use [HCDxxxx]
go

UPDATE ProcessChart
SET InUse = 
	(CASE WHEN EXISTS(
		SELECT [ID]
		FROM CustomerProcess CP 
		WHERE PC.[ID]=CP.ProcessChartID) THEN 1 ELSE 0 END)
FROM ProcessChart PC


