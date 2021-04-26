DECLARE @locationID int
DECLARE @careCenterID int

DECLARE location_cursor CURSOR FOR 
SELECT [ID] FROM Location

OPEN location_cursor
FETCH NEXT FROM location_cursor 
INTO @locationID

WHILE @@FETCH_STATUS = 0
BEGIN
	SET @careCenterID = 0

	;WITH RecursiveLocations([ID], ParentLocationID, [Level]) AS (
	SELECT L.[ID], L.ParentLocationID, 0 AS [Level]
	FROM Location L
	WHERE L.[ID]=@locationID
	UNION ALL
	SELECT L.[ID], L.ParentLocationID, [Level] + 1 AS [Level]
	FROM Location L
	INNER JOIN RecursiveLocations RL ON L.[ID] = RL.[ParentLocationID])
	
	--SELECT TOP 1 @locationID CurrentLocationID, (SELECT [Name] FROM Location WHERE [ID]=@locationID), L.[ID] LocationID, ORG.[ID] OrganizationID, CC.[ID] CareCenterID
	SELECT TOP 1 @careCenterID = CC.[ID]
	FROM RecursiveLocations RL
	JOIN Location L ON RL.[ID] = L.[ID] 
	JOIN Organization ORG ON L.OrganizationsID=ORG.[ID]
	JOIN CareCenter CC ON CC.OrganizationID=ORG.[ID]
	WHERE ORG.[Status]=1 and RL.[Level] <> 0
	ORDER BY [Level] ASC
	
	UPDATE Location SET CareCenterID = @careCenterID WHERE [ID]=@locationID
	--Print @careCenterID

    FETCH NEXT FROM location_cursor 
    INTO @locationID
END 
CLOSE location_cursor
DEALLOCATE location_cursor