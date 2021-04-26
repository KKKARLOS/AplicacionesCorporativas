if exists(select * from sys.columns
            where Name = N'PharmaceuticalFormID' and Object_ID = Object_ID(N'Item'))    
begin
	declare @table_name nvarchar(256)
	declare @col_name nvarchar(256)
	declare @Command  nvarchar(1000)

	set @table_name = N'Item'
	set @col_name = N'PharmaceuticalFormID'

	select @Command = 'ALTER TABLE ' + @table_name + ' drop constraint ' + d.name
	 from sys.tables t   
	  join    sys.default_constraints d       
	   on d.parent_object_id = t.object_id  
	  join    sys.columns c      
	   on c.object_id = t.object_id      
		and c.column_id = d.parent_column_id
	 where t.name = @table_name
	  and c.name = @col_name
	execute (@Command)

    ALTER TABLE [Item]
    DROP COLUMN PharmaceuticalFormID
end
GO

if exists(select * from sys.columns
            where Name = N'AdministrationRouteID' and Object_ID = Object_ID(N'Item'))    
begin
	declare @table_name nvarchar(256)
	declare @col_name nvarchar(256)
	declare @Command  nvarchar(1000)

	set @table_name = N'Item'
	set @col_name = N'AdministrationRouteID'

	select @Command = 'ALTER TABLE ' + @table_name + ' drop constraint ' + d.name
	 from sys.tables t   
	  join    sys.default_constraints d       
	   on d.parent_object_id = t.object_id  
	  join    sys.columns c      
	   on c.object_id = t.object_id      
		and c.column_id = d.parent_column_id
	 where t.name = @table_name
	  and c.name = @col_name
	execute (@Command)

    ALTER TABLE [Item]
    DROP COLUMN AdministrationRouteID
end
GO

ALTER TABLE [Item]
ADD PharmaceuticalFormID int NOT NULL DEFAULT ((0))
GO

ALTER TABLE [Item]
ADD AdministrationRouteID int NOT NULL DEFAULT ((0))
GO

INSERT INTO PharmaceuticalForm (AssignedCode, Name, [Description], DispatchmentType, [Status], [ModifiedBy], [LastUpdated])
SELECT REPLICATE('0', 4 - LEN(row_number() OVER (ORDER BY T1.pharmaceuticalForm))) + CAST(row_number() OVER (ORDER BY T1.pharmaceuticalForm) AS VARCHAR(10)) AssignedCode, 
T1.pharmaceuticalForm [Name], T1.pharmaceuticalForm [Description], 1, 1, 'Administrador', GetDate() from
(SELECT DISTINCT pharmaceuticalForm FROM DrugInfo) T1
ORDER BY T1.pharmaceuticalForm
GO

UPDATE [Item] SET pharmaceuticalformID=IsNull((SELECT TOP 1 PF.[ID] FROM Pharmaceuticalform PF JOIN druginfo DI ON PF.[Name]=DI.PharmaceuticalForm WHERE DI.ItemID=Item.[ID]),0)
GO

ALTER TABLE DrugInfo
DROP COLUMN PharmaceuticalForm
GO

ALTER TABLE DrugInfo
DROP COLUMN AdministrationRoute
GO