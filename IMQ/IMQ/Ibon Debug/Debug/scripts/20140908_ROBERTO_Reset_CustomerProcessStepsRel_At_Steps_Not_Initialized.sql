UPDATE CustomerProcessStepsRel
SET StepStatus = 0,
	StepDateTime = null,
	CloseDateTime = null
WHERE CurrentStepID = 0
