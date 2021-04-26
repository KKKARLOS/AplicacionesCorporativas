DECLARE @Code varchar(100);
DECLARE @Description varchar(400);
DECLARE @CodeATC varchar(100);
DECLARE @CodCas varchar(100);
DECLARE PRINC_ACT CURSOR
FOR SELECT CODACT,DENOFI, CODTRE, CODCAS  FROM [BOT].[dbo].[PRINACT] where DENCOD IN (1,2,3,4,5,6,7,9,0)--He incluido el 7,0
FOR READ ONLY;
OPEN PRINC_ACT
FETCH NEXT FROM PRINC_ACT 
INTO @Code, @Description,@CodeATC,@CodCas
WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO IB_IMQ_PrincipiosActivos 
	(Code,CodCas,Name,Description,CodeATC,LastUpdated,ModifiedBy)
	VALUES(ltrim(rtrim(@Code)),ltrim(rtrim(@CodCas)), ltrim(rtrim(@Description)),ltrim(rtrim(@Description)), ltrim(rtrim(@CodeATC)),GETDATE(),'CARGA_INICIAL')
	FETCH NEXT FROM PRINC_ACT 
	INTO @Code, @Description,@CodeATC,@CodCas
END
CLOSE PRINC_ACT;
DEALLOCATE PRINC_ACT;

