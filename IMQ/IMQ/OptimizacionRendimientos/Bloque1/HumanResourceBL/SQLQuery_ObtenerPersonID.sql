USE [HCDIS_OPT]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerHumanResourceEntity]    Script Date: 29/05/2018 13:17:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ObtenerPersonID]
(
	@HumanResourceID int
)
AS
BEGIN 		
	SELECT 	PersonID 
	FROM	HumanResource WITH(NOLOCK) 
	WHERE	[ID] = @HumanResourceID
END	

--exec ObtenerPersonID @HumanResourceID=1482
