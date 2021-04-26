USE HCDIS
--/////////////////////////////////////////////
--// SCRIPT PARA LA CONFIGURACIÓN DE LOS MOTIVOS
--// EXCLUSIVOS DE CANCELACIÓN DE SOLICITUDES E INTERVENCIONES
--/////////////////////////////////////////////
DECLARE @LastUpdate DATETIME
DECLARE @ModifiedBy NVARCHAR(256)
SET @LastUpdate = GETDATE()
SET @ModifiedBy = 'Administrador'

DECLARE @OrderElementID INT
DECLARE @OrderCIESID INT
DECLARE @OrderENDOID INT

DECLARE @ProcedureElementID INT
DECLARE @ProcedureINTID INT
DECLARE @ProcedureENDOID INT

SET @OrderElementID = ISNULL((SELECT TOP 1 ID FROM EACElement WHERE Name = 'OrderEntity'),0)
SET @OrderCIESID = ISNULL((SELECT TOP 1 ID FROM [Order] WHERE AssignedCode = 'CIES'),0)
SET @OrderENDOID = ISNULL((SELECT TOP 1 ID FROM [Order] WHERE AssignedCode = 'ENDOS'),0)

SET @ProcedureElementID = ISNULL((SELECT TOP 1 ID FROM EACElement WHERE Name = 'ProcedureEntity'),0)
SET @ProcedureINTID = ISNULL((SELECT TOP 1 ID FROM [Procedure] WHERE AssignedCode = 'INTERVENCIONES'),0)
SET @ProcedureENDOID = ISNULL((SELECT TOP 1 ID FROM [Procedure] WHERE AssignedCode = 'ENDOSCOPIAS'),0)

DECLARE @Motivos TABLE ( Code NVARCHAR(50), Motivo NVARCHAR(200), ReasonType SMALLINT, ElementID INT, EntityID INT)
--//EL CODIGO SERA CALCULADO CON LA SIGUIENTE ESTRUCTURA
--// 3 LETRAS PARA LA ENTIDAD  + 3 LETRAS PARA EL TIP DE ACCION + 
--// 4 DIGITOS A PARTIR DE 1XXY PARA LAS INCIDENCIAS DE FORMA QUE 
--//	1XX1 PACIENTE
--//	1XX2 CIRUJANO
--//	1XX3 BQ
--//	1XX4 ADMISION
--//	1XX5 OTROS
--//  XX 2 Dígitos de valores de los diferentes EntityID
--// PARA CANCELACIÓN SOLICITUD		ORDCAN1XX1..5  --CANCEL
--// PARA ANULACIÓN SOLICITUD		ORDCAN1XX1..5  --ABORT
--// PARA BORRAR SOLICITUD			ORDUND1XX1..5  --UNDO
--// PARA CANCELACIÓN INTERVENCION	INTCAN1XX1..5  --CANCEL
--// PARA ANULAR INTERVENCION		INTANU1XX1..5  --ABORT
--// PARA ABORTADO INTERVENCION		INTABO1XX1..5  --REJECT
--// PARA ABORTADO INTERVENCION		INTUND1XX1..5  --UNDO

--//SOLICITUDES 
--//cancelar order para CIES
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1011',  'PACIENTE', 2, @OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1012',  'CIRUJANO', 2,@OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1013',  'BQ', 2,@OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1014',  'ADMISION', 2,@OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1015',  'OTROS', 2,@OrderElementID, @OrderCIESID)
--//cancelar order para ENDOS
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1021',  'PACIENTE', 2, @OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1022',  'CIRUJANO', 2,@OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1023',  'BQ', 2,@OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1024',  'ADMISION', 2,@OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1025',  'OTROS', 2,@OrderElementID, @OrderENDOID)
--//ANULAR order para CIES
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1011',  'PACIENTE', 1, @OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1012',  'CIRUJANO', 1,@OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1013',  'BQ', 1,@OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1014',  'ADMISION', 1,@OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1015',  'OTROS', 1,@OrderElementID, @OrderCIESID)
--//ANULAR order para ENDOS
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1021',  'PACIENTE', 1, @OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1022',  'CIRUJANO', 1,@OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1023',  'BQ', 1,@OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1024',  'ADMISION', 1,@OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDCAN1025',  'OTROS', 1,@OrderElementID, @OrderENDOID)

--UNDO order para CIES
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDUND1011',  'PACIENTE', 4, @OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDUND1012',  'CIRUJANO', 4,@OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDUND1013',  'BQ', 4,@OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDUND1014',  'ADMISION', 4,@OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDUND1015',  'OTROS', 4,@OrderElementID, @OrderCIESID)
--UNDO order para ENDOS
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDUND1021',  'PACIENTE', 4, @OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDUND1022',  'CIRUJANO', 4,@OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDUND1023',  'BQ', 4,@OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDUND1024',  'ADMISION', 4,@OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDUND1025',  'OTROS', 4,@OrderElementID, @OrderENDOID)


--REJECT oRDER para CIES
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDABO1011',  'PACIENTE', 5,@OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDABO1012',  'CIRUJANO', 5,@OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDABO1013',  'BQ', 5,@OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDABO1014',  'ADMISION', 5,@OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDABO1015',  'OTROS', 5,@OrderElementID, @OrderCIESID)
--REJECT oRDER para ENDOS
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDABO1021',  'PACIENTE', 5,@OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDABO1022',  'CIRUJANO', 5,@OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDABO1023',  'BQ', 5,@OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDABO1024',  'ADMISION', 5,@OrderElementID, @OrderENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('ORDABO1025',  'OTROS', 5,@OrderElementID, @OrderENDOID)




--//INTERVENCIONES
--//cancelar PROCEDURES para CIES
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1011',  'PACIENTE', 2,@OrderElementID, @OrderCIESID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1012',  'CIRUJANO', 2,@ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1013',  'BQ', 2,@ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1014',  'ADMISION', 2,@ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1015',  'OTROS', 2,@ProcedureElementID, @ProcedureINTID)
--cancelar PROCEDURES para ENDOS
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1021',  'PACIENTE', 2, @ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1022',  'CIRUJANO', 2,@ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1023',  'BQ', 2,@ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1024',  'ADMISION', 2,@ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1025',  'OTROS', 2,@ProcedureElementID, @ProcedureENDOID)
--ANULAR PROCEDURES para CIES
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1011',  'PACIENTE', 1, @ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1012',  'CIRUJANO', 1,@ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1013',  'BQ', 1,@ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1014',  'ADMISION', 1,@ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1015',  'OTROS', 1,@ProcedureElementID, @ProcedureINTID)
--ANULAR PROCEDURES para ENDOS
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1021',  'PACIENTE', 1, @ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1022',  'CIRUJANO', 1,@ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1023',  'BQ', 1,@ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1024',  'ADMISION', 1,@ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTCAN1025',  'OTROS', 1,@ProcedureElementID, @ProcedureENDOID)


--REJECT PROCEDURES para CIES
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTABO1011',  'PACIENTE', 5, @ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTABO1012',  'CIRUJANO', 5,@ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTABO1013',  'BQ', 5,@ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTABO1014',  'ADMISION', 5,@ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTABO1015',  'OTROS', 5,@ProcedureElementID, @ProcedureINTID)
--REJECT PROCEDURES para ENDOS
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTABO1021',  'PACIENTE', 5, @ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTABO1022',  'CIRUJANO', 5,@ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTABO1023',  'BQ', 5,@ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTABO1024',  'ADMISION', 5,@ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTABO1025',  'OTROS', 5,@ProcedureElementID, @ProcedureENDOID)


--UNDO PROCEDURES para CIES
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTUND1011',  'PACIENTE', 4, @ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTUND1012',  'CIRUJANO', 4,@ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTUND1013',  'BQ', 4,@ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTUND1014',  'ADMISION', 4,@ProcedureElementID, @ProcedureINTID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTUND1015',  'OTROS', 4,@ProcedureElementID, @ProcedureINTID)
--UNDO PROCEDURES para ENDOS
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTUND1021',  'PACIENTE', 4, @ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTUND1022',  'CIRUJANO', 4,@ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTUND1023',  'BQ', 4,@ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTUND1024',  'ADMISION', 4,@ProcedureElementID, @ProcedureENDOID)
INSERT INTO @Motivos (Code, Motivo, ReasonType, ElementID, EntityID)
VALUES ('INTUND1025',  'OTROS', 4,@ProcedureElementID, @ProcedureENDOID)

--// PRIMERO DISCOTINUO LAS QUE EXISTEN
UPDATE [ReasonChange] SET [Status] = 2, [LastUpdated] = @LastUpdate, [ModifiedBy] = @ModifiedBy
FROM [ReasonChange] 
WHERE ReasonChangeType in (1,2,4,5)
AND ElementID IN (@OrderElementID, @ProcedureElementID)
AND NOT(EXISTS(SELECT ID FROM [ReasonChange] RC WHERE RC.AssignedCode = [ReasonChange].AssignedCode))

INSERT INTO [ReasonChange]
([AssignedCode],[Reason],[ElementID],[EntityID],[IsDefault],
	[ReasonChangeType],[Status],[LastUpdated],[ModifiedBy])
SELECT M.Code, M.Motivo, M.ElementID, M.EntityID, 0, M.ReasonType, 1, @LastUpdate, @ModifiedBy
FROM @Motivos M
WHERE NOT(EXISTS(SELECT ID FROM [ReasonChange] RC WHERE RC.AssignedCode = M.Code))


