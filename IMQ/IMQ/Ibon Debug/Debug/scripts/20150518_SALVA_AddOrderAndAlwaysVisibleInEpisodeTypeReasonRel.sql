SET NOCOUNT ON;

----------------------------------------------------------------------------------------------
--	EpisodeTypeReasonRel
----------------------------------------------------------------------------------------------


DECLARE @episodeTypeReasonRelID int, @episodeTypeID int, @actualEpisodeTypeID int, @indi int
SET @indi=1
SET @actualEpisodeTypeID = 0


DECLARE order_cursor CURSOR FOR 
SELECT [ID], EpisodeTypeID
FROM EpisodeTypeReasonRel
ORDER BY EpisodeTypeID

OPEN order_cursor

FETCH NEXT FROM order_cursor 
INTO @episodeTypeReasonRelID, @episodeTypeID

WHILE @@FETCH_STATUS = 0
BEGIN
	IF @actualEpisodeTypeID!=@episodeTypeID
	BEGIN
		SET @actualEpisodeTypeID = @episodeTypeID
		SET @indi=1
	END
	UPDATE EpisodeTypeReasonRel SET [Order]=@indi WHERE [ID]=@episodeTypeReasonRelID
	SET @indi=@indi + 1
	
    FETCH NEXT FROM order_cursor 
    INTO @episodeTypeReasonRelID, @episodeTypeID
END 
CLOSE order_cursor;
DEALLOCATE order_cursor;
GO
