UPDATE CustomerReception SET CustomerProcessID=CPSR.CustomerProcessID
FROM CustomerProcessStepsRel CPSR
	JOIN CustomerReception CR ON CPSR.Step=256 AND CPSR.CurrentStepID=CR.[ID]
	