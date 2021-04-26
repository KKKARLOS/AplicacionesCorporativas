DECLARE @ProcCIRUGIASID INT
SET @ProcCIRUGIASID=(SELECT TOP 1 ID FROM [Procedure] WHERE Name LIKE 'CIRUGÍAS')

DECLARE @ProcENDOSCOPIASID INT
SET @ProcENDOSCOPIASID=(SELECT TOP 1 ID FROM [Procedure] WHERE Name LIKE 'ENDOSCOPIAS')

IF (@ProcCIRUGIASID > 0)
BEGIN
	DELETE FROM ProcedureHumanResourceRel WHERE ProcedureID=(@ProcCIRUGIASID)

	--Auxiliar
	INSERT INTO ProcedureHumanResourceRel (ProcedureID, ProfileID, RequiredActors, RequiredTime, [Status], LastUpdated, ModifiedBy)
	VALUES (@ProcCIRUGIASID, 1, 1, 100, 1, GETDATE(), 'Administrador')
	--DUE Circulante
	INSERT INTO ProcedureHumanResourceRel (ProcedureID, ProfileID, RequiredActors, RequiredTime, [Status], LastUpdated, ModifiedBy)
	VALUES (@ProcCIRUGIASID, 15, 1, 100, 1, GETDATE(), 'Administrador')
	--Cirujano
	INSERT INTO ProcedureHumanResourceRel (ProcedureID, ProfileID, ResourceIsRealizationRequired, RequiredActors, RequiredTime, [Status], LastUpdated, ModifiedBy)
	VALUES (@ProcCIRUGIASID, 10, 1, 1, 100, 1, GETDATE(), 'Administrador')
	--Matrona
	INSERT INTO ProcedureHumanResourceRel (ProcedureID, ProfileID, RequiredActors, RequiredTime, [Status], LastUpdated, ModifiedBy)
	VALUES (@ProcCIRUGIASID, 4, 1, 100, 1, GETDATE(), 'Administrador')

END

IF (@ProcENDOSCOPIASID > 0)
BEGIN
	DELETE FROM ProcedureHumanResourceRel WHERE ProcedureID=(@ProcENDOSCOPIASID)

	--Auxiliar
	INSERT INTO ProcedureHumanResourceRel (ProcedureID, ProfileID, RequiredActors, RequiredTime, [Status], LastUpdated, ModifiedBy)
	VALUES (@ProcENDOSCOPIASID, 1, 1, 100, 1, GETDATE(), 'Administrador')
	--DUE Circulante
	INSERT INTO ProcedureHumanResourceRel (ProcedureID, ProfileID, RequiredActors, RequiredTime, [Status], LastUpdated, ModifiedBy)
	VALUES (@ProcENDOSCOPIASID, 15, 1, 100, 1, GETDATE(), 'Administrador')
	--Cirujano
	INSERT INTO ProcedureHumanResourceRel (ProcedureID, ProfileID, ResourceIsRealizationRequired, RequiredActors, RequiredTime, [Status], LastUpdated, ModifiedBy)
	VALUES (@ProcENDOSCOPIASID, 10, 1, 1, 100, 1, GETDATE(), 'Administrador')
	--Matrona
	INSERT INTO ProcedureHumanResourceRel (ProcedureID, ProfileID, RequiredActors, RequiredTime, [Status], LastUpdated, ModifiedBy)
	VALUES (@ProcENDOSCOPIASID, 4, 1, 100, 1, GETDATE(), 'Administrador')
END