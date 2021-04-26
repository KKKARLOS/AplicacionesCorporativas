

INSERT INTO StepPreprint (ProcessChartID, StepConfig, StepConfigID, SourceType, Reference, 
	ReportType, Label, [FileName], Copies, LastUpdated, ModifiedBy)
SELECT DISTINCT RC.ProcessChartID, 65536 AS StepConfig, RC.ID StepConfigID, RAC.SourceType, RAC.Reference, 
	0 ReportType, '' Label, '' [FileName], 0 Copies, RC.LastUpdated, RC.ModifiedBy
FROM ReportActsConfig RAC
JOIN ReportConfig RC ON RAC.ReportConfigID = RC.[ID]
