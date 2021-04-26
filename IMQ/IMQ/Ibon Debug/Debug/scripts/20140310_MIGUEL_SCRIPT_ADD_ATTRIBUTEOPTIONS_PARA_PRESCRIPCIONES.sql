use  [DATABASENAME]

--/////////////////////////////////////////////////
--//
--//ESTE SCRIPT SOLO SE DEBE EJECUTAR DESPUÉS DE ACTUALIZADO LOS METADATOS
--//
--/////////////////////////////////////////////////
DECLARE @PRESCRIPTIONOPTIONS TABLE ([ID]INT IDENTITY, ATRIBUTE NVARCHAR(256), OPTDES NVARCHAR(256), OPTVALUE NVARCHAR(1024))
INSERT INTO  @PRESCRIPTIONOPTIONS (ATRIBUTE,OPTDES,OPTVALUE)
VALUES ('VitalSignsObservations','BSA','EN13606_PF10')
INSERT INTO  @PRESCRIPTIONOPTIONS (ATRIBUTE,OPTDES,OPTVALUE)
VALUES ('VitalSignsObservations','Height','EN13606_PF2')
INSERT INTO  @PRESCRIPTIONOPTIONS (ATRIBUTE,OPTDES,OPTVALUE)
VALUES ('VitalSignsObservations','Weight','EN13606_PF1')


INSERT INTO  @PRESCRIPTIONOPTIONS (ATRIBUTE,OPTDES,OPTVALUE)
VALUES ('UsualMedicationTemplate','CLASSCODE','PCC_MED_0001')
INSERT INTO  @PRESCRIPTIONOPTIONS (ATRIBUTE,OPTDES,OPTVALUE)
VALUES ('UsualMedicationTemplate','STARTDATE','PCC_MED_0002')
INSERT INTO  @PRESCRIPTIONOPTIONS (ATRIBUTE,OPTDES,OPTVALUE)
VALUES ('UsualMedicationTemplate','STOPDATE','PCC_MED_0003')
INSERT INTO  @PRESCRIPTIONOPTIONS (ATRIBUTE,OPTDES,OPTVALUE)
VALUES ('UsualMedicationTemplate','DOSEQTY','PCC_MED_0004')
INSERT INTO  @PRESCRIPTIONOPTIONS (ATRIBUTE,OPTDES,OPTVALUE)
VALUES ('UsualMedicationTemplate','UNITQTY','PCC_MED_0005')
INSERT INTO  @PRESCRIPTIONOPTIONS (ATRIBUTE,OPTDES,OPTVALUE)
VALUES ('UsualMedicationTemplate','PHYSUNIT','PCC_MED_0006')
INSERT INTO  @PRESCRIPTIONOPTIONS (ATRIBUTE,OPTDES,OPTVALUE)
VALUES ('UsualMedicationTemplate','FREQUENCY','PCC_MED_0007')
INSERT INTO  @PRESCRIPTIONOPTIONS (ATRIBUTE,OPTDES,OPTVALUE)
VALUES ('UsualMedicationTemplate','TIMEPERDAY','PCC_MED_0008')
INSERT INTO  @PRESCRIPTIONOPTIONS (ATRIBUTE,OPTDES,OPTVALUE)
VALUES ('UsualMedicationTemplate','ROUTE','PCC_MED_0009')


DECLARE @ID INT
DECLARE @OLDID INT
DECLARE @ATTRIBUTEID INT
DECLARE @ATRIBUTE NVARCHAR(256)
DECLARE @OPTDES NVARCHAR(256)
DECLARE @OPTVALUE NVARCHAR(1024)
SET @OLDID=-1
SET @ID=0
IF (@OLDID < @ID)
BEGIN
	SET @OLDID = @ID
	SET @ID = ISNULL((SELECT TOP 1 [ID] FROM @PRESCRIPTIONOPTIONS WHERE ID > @OLDID ORDER BY ID),0)
	IF (@OLDID < @ID) AND NOT(@ID=0)
	BEGIN
		SELECT @ATRIBUTE = ATRIBUTE, @OPTDES=OPTDES, @OPTVALUE=OPTVALUE 
		FROM @PRESCRIPTIONOPTIONS WHERE ID=@ID
		SET @ATTRIBUTEID = ISNULL((SELECT TOP 1 [ID] FROM EACAttribute WHERE [NAME]=@ATRIBUTE),0)
		IF NOT(@ATTRIBUTEID =0) AND 
			NOT(EXISTS(SELECT ID FROM EACAttributeOption WHERE [DESCRIPTION] = @OPTDES))
		BEGIN
			INSERT INTO [EACAttributeOption]
			([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status])
			VALUES (@ATTRIBUTEID, @OPTDES, @OPTVALUE, GETDATE(), 'Administrador', 2)
		END
	END
END

--/////////////////////////////////////////////////
--//
--//REGISTOR DE OBSERVACIONES PARA LAS PRESCRIPCIONES
--//
--/////////////////////////////////////////////////

DECLARE @MYOBS TABLE([ID] INT IDENTITY, Code NVARCHAR(50), Name NVARCHAR(100), 
					KindOf INT, BasicType INT, BLen INT, DLen INT, Context NVARCHAR(100), 
					Encoder NVARCHAR(100), TemplateName NVARCHAR(100))

--/////////////////////////////////////////////////
--// Plantilla De Datos de Signos físicos
--/////////////////////////////////////////////////

--INSERT INTO @MYOBS (Code, Name, KindOf, BasicType, BLen, DLen, Context, Encoder, TemplateName) 
--VALUES('CODE','NOMBRE',128,0,0,0,'CIE9','DIAG-CIE9','Signos fisicos')

--/////////////////////////////////////////////////
--// Plantilla De Datos de Medicacion Habitual
--/////////////////////////////////////////////////

--INSERT INTO @MYOBS (Code, Name, KindOf, BasicType, BLen, DLen, Context, Encoder, TemplateName) 
--VALUES('CODE','NOMBRE',128,0,0,0,'CIE9','DIAG-CIE9','Medicacion Habitual')

--/////////////////////////////////////////////////
--// Plantilla De Datos de Alergias
--/////////////////////////////////////////////////

--INSERT INTO @MYOBS (Code, Name, KindOf, BasicType, BLen, DLen, Context, Encoder, TemplateName) 
--VALUES('CODE','NOMBRE',128,0,0,0,'CIE9','DIAG-CIE9','Alergias')

--/////////////////////////////////////////////////
--// Plantilla De Datos de Efectos Adversos
--/////////////////////////////////////////////////

--INSERT INTO @MYOBS (Code, Name, KindOf, BasicType, BLen, DLen, Context, Encoder, TemplateName) 
--VALUES('CODE','NOMBRE',128,0,0,0,'CIE9','DIAG-CIE9','Efectos Adversos')


