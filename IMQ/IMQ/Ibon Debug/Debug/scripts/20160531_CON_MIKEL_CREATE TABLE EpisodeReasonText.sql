
USE [HCDIS];


GO



	CREATE TABLE [HCDIS].[dbo].[IB_IMQ_EpisodeReasonText]
	(

		ID int IDENTITY(1,1),
		EpisodeReasonTypeID int ,
		EpisodeReasonText nchar(1000),
		CustomerEpisodeID int,
		CustomerAdmissionID int,
		ProcesoDestinoName varchar(255),
		LastUpdated datetime,
		ModifiedBy nvarchar(256),
		PRIMARY KEY (ID)

	);

	  
	 INSERT INTO EpisodeReasonType (AssignedCode, [Name], [Description], Encoder, IsCodified, 
		ElementID, ElementReasonType, DefaultEpisodeReasonID, [Status], Version, LastUpdated, ModifiedBy) 
	VALUES ( 'DIAGINGHOSP', 'Diagnóstico al ingreso', 'Diagnóstico al ingreso', '', 1, 
		0, 1, 0, 1, '', GetDate(), '16052407') --El id generado debe ser 67
		


	INSERT INTO EpisodeTypeReasonRel (EpisodeTypeID, EpisodeReasonTypeID, [Order], [Required], AlwaysVisible, DefaultEpisodeReasonID, LastUpdated, ModifiedBy) 
	VALUES (2, 67, 6, 0, 1, 0, GetDate(), '16052407')


  