 DECLARE @YaEsta int;
 DECLARE @ROL nvarchar(10);
 DECLARE cRol Cursor 
                 for select RoleName 
				from [HCDISSecurity].[dbo].[Roles] 
			where RoleName in ('DUEHOS','ADMHOSP','ADMHOSP+','OYS','ADMCCEE','ADMRX','DUESUP','TER','AUXENF');
 OPEN cRol;
 FETCH NEXT FROM cRol INTO @ROL;
 PRINT 'Asignación de permisos de bloqueo a los roles impactados';
 --PRINT '--------------------------------------------------------';
 WHILE @@FETCH_STATUS = 0
 BEGIN
    PRINT '--------------------------------------------------------';
    --PRINT 'Análisis del Rol:[' + @ROL + ']';
    SET @YaEsta = 0;
    SET @YaEsta = (select COUNT(*) FROM [HCDISSecurity].[dbo].[RoleFunctions] 
                  WHERE RoleID = (select Id from [HCDISSecurity].[dbo].[Roles] where RoleName = @ROL) 
				    AND [Function] = 'LocationBlock');
    IF @YaEsta = 0 
     BEGIN
       PRINT 'Asigno el permiso LocationBlock al Rol:[' + @ROL + ']';
       INSERT INTO [HCDISSecurity].[dbo].[RoleFunctions] ([RoleID], [Function])
	        VALUES
	   ((select Id from [HCDISSecurity].[dbo].[Roles] where RoleName = @ROL), 'LocationBlock');
     END;
	ELSE
	 BEGIN
	   PRINT 'El Rol:[' + @ROL + '] ya tenía el permiso asignado';
 	 END;

    FETCH NEXT FROM cRol INTO @ROL;
 END;
 CLOSE cRol;
 DEALLOCATE cRol;
 GO



