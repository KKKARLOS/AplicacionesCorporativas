declare @p8 dbo.TVPInteger
insert into @p8 values(1)
insert into @p8 values(2)
insert into @p8 values(10)
insert into @p8 values(18)
insert into @p8 values(21)

declare @p9 dbo.TVPInteger

declare @p10 dbo.TVPInteger
insert into @p10 values(9)

exec GetMDSCustomerCodification @Step=16384,@Status=4,@AssistanceService=0,@CodificationStatus=0,@TransferReason='TRASLADO',@StartDateTime='20160125',@EndDateTime='20180525',@TVPProcessChartIDs=@p8,@TVPLocations=@p9,@TVPCareCenterIDs=@p10



select top 100 * from Street