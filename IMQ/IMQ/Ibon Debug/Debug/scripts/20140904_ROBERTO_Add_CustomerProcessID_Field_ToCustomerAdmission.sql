UPDATE CustomerAdmission
SET CustomerProcessID = ISNULL((select CustomerProcessID from CustomerProcessStepsRel where [Step]=2048 and CurrentStepID=CustomerAdmission.ID), 0)


UPDATE CustomerReports
SET CustomerProcessID = ISNULL((select CustomerProcessID from CustomerProcessStepsRel where [Step]=65536 and CurrentStepID=CustomerReports.ID), 0)


UPDATE CustomerInterview
SET CustomerProcessID = ISNULL((select CustomerProcessID from CustomerProcessStepsRel where [Step]=512 and CurrentStepID=CustomerInterview.ID), 0)