SET NOCOUNT ON;

----------------------------------------------------------------------------------------------
--	EpisodeTypeLeaveReasonRel
----------------------------------------------------------------------------------------------


DECLARE @episodeTypeLeaveReasonRelID int, @episodeTypeID int, @actualEpisodeTypeID int, @indi int
SET @indi=1
SET @actualEpisodeTypeID = 0


DECLARE order_cursor CURSOR FOR 
SELECT [ID], EpisodeTypeID
FROM EpisodeTypeLeaveReasonRel
ORDER BY EpisodeTypeID

OPEN order_cursor

FETCH NEXT FROM order_cursor 
INTO @episodeTypeLeaveReasonRelID, @episodeTypeID

WHILE @@FETCH_STATUS = 0
BEGIN
	IF @actualEpisodeTypeID!=@episodeTypeID
	BEGIN
		SET @actualEpisodeTypeID = @episodeTypeID
		SET @indi=1
	END
	UPDATE EpisodeTypeLeaveReasonRel SET [Order]=@indi WHERE [ID]=@episodeTypeLeaveReasonRelID
	SET @indi=@indi + 1
	
    FETCH NEXT FROM order_cursor 
    INTO @episodeTypeLeaveReasonRelID, @episodeTypeID
END 
CLOSE order_cursor;
DEALLOCATE order_cursor;
GO