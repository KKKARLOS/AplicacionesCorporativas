exec sp_executesql N'UPDATE EACElement 
SET    LastUpdated=GetDate(), ModifiedBy=@ModifiedBy
WHERE ([ID]=@ID)',
N'@ID int, @ModifiedBy nvarchar(8)',
@ID=688, @ModifiedBy=N'44976499'
go

exec sp_executesql N'UPDATE EACAttribute 
SET  DefaultValue=@DefaultValue, LastUpdated=GetDate(), ModifiedBy=@ModifiedBy
WHERE ([ID]=@ID)',
N'@ID int, @DefaultValue nvarchar(1), @ModifiedBy nvarchar(8)',
@ID=15691, @DefaultValue=N'5', @ModifiedBy=N'44976499'
go