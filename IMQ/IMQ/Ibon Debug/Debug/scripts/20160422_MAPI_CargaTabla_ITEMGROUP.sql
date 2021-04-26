delete from ItemGroup;
DECLARE @Code varchar(100);
DECLARE @Description varchar(400);
DECLARE Item_Group CURSOR
FOR SELECT GTEATCCOD,GTEATCDES  FROM [BOT].[dbo].[GTEATC] where GTEATCNIV=4 and  GTEATCTIP='H'
FOR READ ONLY;
OPEN Item_Group
FETCH NEXT FROM Item_Group 
INTO @Code, @Description
WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO ItemGroup 
	(Code,Description,Status,ModifiedBy,LastUpdated)
	VALUES(ltrim(rtrim(@Code)), ltrim(rtrim(@Description)),1,'CARGA_INICIAL',GETDATE())
	FETCH NEXT FROM Item_Group 
	INTO @Code, @Description
END
CLOSE Item_Group;
DEALLOCATE Item_Group;