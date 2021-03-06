USE [HCDIS_OPT]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerHumanResourceEntity]    Script Date: 29/05/2018 13:17:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ObtenerHumanResourceEntity]
(
	@HumanResourceID int
)
AS
BEGIN
	DECLARE @PersonID int = 0--,
			--@AddressID int = 0,
			--@Address2ID int = 0
	set @PersonID = 282725
	 		
	--SELECT 	@PersonID = PersonID 
	--FROM	HumanResource WITH(NOLOCK) 
	--WHERE	[ID] = @HumanResourceID
	
	--IF (@@ROWCOUNT > 0 AND @PersonID>0)
	--BEGIN
		-- HumanResourceTable (1)
			
		SELECT	[ID], 
				PersonID, 
				FileNumber, 
				HasAvailability, 
				AdmitNotification, 
				IncludingEmail, 
				LastUpdated, ModifiedBy,
				CAST(DBTimeStamp AS BIGINT) AS DBTimeStamp
				
		FROM	HumanResource WITH(NOLOCK) 	
		WHERE	[ID] = @HumanResourceID
		
		-- HHRRProfileRelTable (2)
		
		SELECT	ID, 
				HumanResourceID, 
				ProfileID, 
				DefaultProfile, 
				[Status], 
				LastUpdated, 
				ModifiedBy, 
				CAST(DBTimeStamp as bigint) DBTimeStamp
				
		FROM	HHRRProfileRel WITH(NOLOCK) 
		WHERE	HumanResourceID=@HumanResourceID
		
		-- ProfileTable (3)
		
		SELECT	P.[ID], 
				P.[Code], 
				P.[Name], 
				P.[CategoryID], 
				C.[Name] CategoryName, 
				P.[Status], 
				P.LastUpdated, 
				P.ModifiedBy, 
				CAST(P.DBTimeStamp as bigint) DBTimeStamp
				
		FROM	[Profile] P WITH(NOLOCK) 
		
		JOIN	HHRRProfileRel PR WITH(NOLOCK) 
		ON		P.[ID]=PR.ProfileID
		
		LEFT JOIN [Category] C WITH(NOLOCK) 
		ON		P.CategoryID=C.[ID]	
		
		WHERE	PR.HumanResourceID=@HumanResourceID	
		
		-- ParticipateAsProfileRelTable (4)
		
		SELECT	PAPR.[ID], 
				PAPR.ParticipateAsID, 
				PAPR.ProfileID,
				PAPR.LastUpdated, 
				PAPR.ModifiedBy, 
				CAST(PAPR.DBTimeStamp as bigint) as DBTimeStamp
				
		FROM	ParticipateAsProfileRel PAPR WITH(NOLOCK) 
		
		JOIN	[Profile] P WITH(NOLOCK) 
		ON		PAPR.[ProfileID]=P.[ID]
		
		JOIN	HHRRProfileRel PR WITH(NOLOCK)  
		ON		P.[ID]=PR.ProfileID
				
		WHERE	PR.HumanResourceID=@HumanResourceID	
		
		--ParticipateAsTable (5)
		
		SELECT	DISTINCT 
				PA.[ID], 
				PA.[Code], 
				PA.[Description], 
				PA.InUse, 
				PA.ProviderName,
				PA.[Status], 
				PA.LastUpdated, 
				PA.ModifiedBy, CAST(PA.DBTimeStamp as bigint) as DBTimeStamp
		FROM	ParticipateAs PA WITH(NOLOCK) 
		
		JOIN	ParticipateAsProfileRel PAPR WITH(NOLOCK)  
		ON		PA.[ID]=PAPR.ParticipateAsID
		
		JOIN	[Profile] P WITH(NOLOCK) 
		ON		PAPR.[ProfileID]=P.[ID]
		
		JOIN	HHRRProfileRel PR WITH(NOLOCK) 
		ON		P.[ID]=PR.ProfileID
		
		WHERE	PR.HumanResourceID=@HumanResourceID
			
		-- ResourceDeviceRelTable (6)
				
		SELECT	[ID], 
				HumanResourceID, 
				DeviceID, 
				ActiveAt, 
				[Status], 
				LastUpdated, 
				ModifiedBy, 
				CAST(DBTimeStamp as bigint) DBTimeStamp
				
		FROM	ResourceDeviceRel WITH(NOLOCK) 
		WHERE	HumanResourceID=@HumanResourceID

		-- DeviceTable (7)
		
		SELECT	D.[ID], 
				D.[Code], 
				D.[Name], 
				D.[Description], 
				D.DeviceTypeID, 
				D.ParentLocationID, 
				D.PlaceAddressID,
				D.DeviceIP, 
				D.InstallationDate, 
				D.ActiveAtDate, 
				D.DeactiveFromDate, 
				D.AudioInput, 
				D.AudioOutput,
				D.HasAnAssignerActor, 
				D.MaxAudioSize, 
				D.MaxMessageSize, 
				D.MessageInput, 
				D.MessageOutput, 
				D.Model,
				D.Mobile, 
				D.Screen, 
				D.SerialNumber, 
				D.[Status], 
				D.LastUpdated, 
				D.ModifiedBy, 
				CAST(D.DBTimeStamp AS BIGINT) AS DBTimeStamp
				
		FROM	Device D WITH(NOLOCK) 
		
		JOIN	ResourceDeviceRel R WITH(NOLOCK) 
		ON		R.DeviceID = D.[ID]
		
		WHERE	R.[HumanResourceID]=@HumanResourceID		

		-- DeviceTypeTable (8)	 
		
		SELECT	DT.[ID], 
				DT.[Code], 
				DT.[Name], 
				DT.[AudioInputTypeFormat], 
				DT.[AudioOutputTypeFormat],
				DT.[Description], 
				DT.[MsgInputTypeFormat], 
				DT.[MsgOutputTypeFormat], 
				DT.[Localizable], 
				DT.[CanReceiveCall],
				CAST(DT.DBTimeStamp as bigint) DBTimeStamp 
		FROM	DeviceType DT WITH(NOLOCK) 
		
		JOIN	Device D WITH(NOLOCK) 
		ON		DT.[ID]=D.[DeviceTypeID]
		
		JOIN	ResourceDeviceRel RDR WITH(NOLOCK)  
		ON		D.[ID]=RDR.DeviceID
		
		WHERE RDR.HumanResourceID=@HumanResourceID		
		
		-- PersonAvailPatternTable (9)
		
		SELECT	PAP.[ID], 
				PAP.PersonID, 
				PAP.CareCenterID, 
				O.[Name] CareCenterName, 
				PAP.AvailPatternID, 
				PAP.[Status], 
				PAP.IsDefault,
				PAP.StartAt, 
				PAP.EndingIn, 
				PAP.LastUpdated, 
				PAP.ModifiedBy, 
				CAST(PAP.DBTimeStamp as bigint) DBTimeStamp
				
		FROM	PersonAvailPattern PAP WITH(NOLOCK)
		 
		JOIN	CareCenter CC WITH(NOLOCK) 
		ON		PAP.CareCenterID = CC.[ID] 
		
		JOIN	Organization O WITH(NOLOCK) 
		ON		CC.OrganizationID = O.[ID]
		
		WHERE	PersonID=@PersonID
		
		-- AvailPatternTable (10)
		
		SELECT	AP.[ID], 
				AP.Description, 
				AP.FillPattern, 
				AP.CalendarCellCapacity, 
				AP.OverbookingCapacity,
				AP.CalendarFractionation, AP.AvailabilityID, AP.StartAt, AP.EndingIn, AP.RepeatPatternEvery,
				AP.AdditionalInformation, AP.AvailPatternColor, AP.InUse, AP.Status, AP.LastUpdated, AP.ModifiedBy,
				CAST(AP.DBTimeStamp as bigint) DBTimeStamp
		FROM	AvailPattern AP WITH(NOLOCK) 
		 
		JOIN	PersonAvailPattern HRAP WITH(NOLOCK) 
		ON		AP.[ID]=HRAP.AvailPatternID
		
		WHERE	HRAP.PersonID=@PersonID	
		
		-- AvailabilityTable (11)
		
		SELECT	TP.[ID], 
				TP.Meaning, 
				TP.[Description], 
				TP.PatternType, 
				TP.[Status], 
				TP.ModifiedBy, 
				TP.LastUpdated,
				CAST(TP.DBTimeStamp AS BIGINT) AS DBTimeStamp
		FROM	TimePattern TP WITH(NOLOCK) 
		
		JOIN	AvailPattern AP WITH(NOLOCK) 
		ON		TP.[ID]=AP.AvailabilityID
		
		JOIN	PersonAvailPattern HHAP WITH(NOLOCK) 
		ON		HHAP.AvailPatternID=AP.[ID]
		
		WHERE	HHAP.PersonID=@PersonID	
			
		-- PersonCareCenterAccessTable (12)
		
		SELECT	PCCA.[ID], 
				PCCA.PersonID, 
				PCCA.CareCenterID, 
				PCCA.Workplace, 
				ORG.[Name] CareCenterName, 
				PCCA.StartAccessDate, 
				PCCA.EndAccessDate,
				ORG.SocialReason CareCenterSocialReason, 
				PCCA.LastUpdated, PCCA.ModifiedBy, 
				CAST(PCCA.DBTimeStamp as bigint) DBTimeStamp 
		FROM	PersonCareCenterAccess PCCA WITH(NOLOCK) 
		
		JOIN	Person P WITH(NOLOCK) 
		ON		PCCA.PersonID=P.[ID]
		
		JOIN	CareCenter CC WITH(NOLOCK) 
		ON		PCCA.CareCenterID=CC.[ID] 
		
		JOIN	Organization ORG WITH(NOLOCK) 
		ON		CC.OrganizationID=ORG.[ID]
		
		WHERE	PCCA.PersonID=@PersonID	
		
		-- PersonTable (13)
/*		
		SELECT	[ID], 
				FirstName, 
				LastName, 
				LastName2, 
				AsUser, 
				EmailAddress, 
				ImageID, 
				RegistrationDate, 
				AddressID, 
				Address2ID,
				DuplicateGroupID, 
				RecordMerged, 
				HasMergedRegisters, 
				LastUpdated, 
				Status, 
				ModifiedBy, 
				CAST(DBTimeStamp as bigint) DBTimeStamp
		
		FROM	Person WITH(NOLOCK) 
		WHERE	[ID]=@PersonID	
			
		-- PersonTelephoneTable (14)
		
		SELECT	[ID], 
				PersonID, 
				TelephoneID, 
				ModifiedBy, 
				LastUpdated, 
				CAST(DBTimeStamp as bigint) DBTimeStamp
				
		FROM	PersonTelephoneRel WITH(NOLOCK)
		WHERE	PersonID=@PersonID				
		
		-- TelephoneTable (15)
		
		SELECT	T.[ID], 
				T.Telephone, 
				T.Comments, 
				T.TelephoneType, 
				T.EmergencyContactPhone, 
				T.ModifiedBy, 
				T.LastUpdated, 
				CAST(T.DBTimeStamp as bigint) DBTimeStamp
		FROM	Telephone T WITH(NOLOCK)
		
		JOIN	PersonTelephoneRel P WITH(NOLOCK) 
		ON		T.[ID]=P.TelephoneID 
		
		WHERE	P.PersonID=@PersonID		
/*
		SELECT 	@AddressID = AddressID,
				@Address2ID = Address2ID 
				
		FROM	Person WITH(NOLOCK) 
		WHERE	[ID]=@PersonID	
		
		IF (@AddressID>0)
		BEGIN	
*/				
			-- AddressTable (16)
			
			SELECT	Address.[ID], 
					Address1, 
					Address2, 
					AddressType, 
					City, 
					Country, 
					Province, 
					State, 
					ZipCode, 
					Address.LastUpdated, 
					Address.ModifiedBy, 
					CAST(Address.DBTimeStamp as bigint) DBTimeStamp
					
			FROM	Person WITH(NOLOCK) 
			
			JOIN	Address WITH(NOLOCK)
			ON		Person.AddressID = Address.ID
										
			WHERE	Person.ID=@PersonID
/*		END
		IF (@Address2ID>0)
		BEGIN	
*/		
		-- AddressTable (17)
		
			SELECT	Address.[ID], 
					Address1, 
					Address2, 
					AddressType, 
					City, 
					Country, 
					Province, 
					State, 
					ZipCode, 
					Address.LastUpdated, 
					Address.ModifiedBy, 
					CAST(Address.DBTimeStamp as bigint) DBTimeStamp
					
			FROM	Person WITH(NOLOCK) 
				
			JOIN	Address WITH(NOLOCK)  
			ON		Person.Address2ID = Address.ID
								
			WHERE	Person.ID=@PersonID	
--		END
		
		-- PersonCategoryTable (18)
		
		SELECT	[ID], 
				PersonID, 
				CategoryID, 
				ModifiedBy, 
				LastUpdated, 
				CAST(DBTimeStamp as bigint) DBTimeStamp
				
		FROM	PersonCatRel WITH(NOLOCK) 
		WHERE	PersonID=@PersonID		
		
		-- CategoryTable (19)
		
		SELECT	C.[ID], 
				C.CategoryKey, 
				C.[Name], 
				C.[Type], 
				C.ModifiedBy, 
				C.LastUpdated, 
				CAST(C.DBTimeStamp as bigint) DBTimeStamp
		
		FROM	Category C WITH(NOLOCK) 
		
		JOIN	PersonCatRel P WITH(NOLOCK) 		
		ON		C.[ID]=P.CategoryID 
		
		WHERE	P.PersonID=@PersonID	
		
		-- SensitiveDataTable (20)										   							   									   								   						

		SELECT	SD.[ID], 
				SD.PersonID, 
				SD.BirthDate, 
				SD.Sex, 
				SD.ReligiousPreference, 
				SD.[Language], 
				SD.EducationLevel, 
				SD.MaritalStatus, 
				SD.BirthPlace, 
				SD.Citizenship, 
				SD.CitizenshipComments, 
				SD.DeathDateTime, 
				SD.DeathReason,
				SD.LastUpdated, 
				SD.ModifiedBy, 
				CAST(SD.DBTimeStamp as bigint) DBTimeStamp 
				
		   FROM SensitiveData SD WITH(NOLOCK) 
		   WHERE (SD.PersonID=@PersonID)
		   
		   -- IdentifierTable (21)
			SELECT	[ID], 
					PersonID, 
					IdentifierTypeID, 
					IDNumber, 
					ModifiedBy, 
					LastUpdated, 
					CAST(DBTimeStamp as bigint) DBTimeStamp
			
			FROM	PersonIdentifierRel WITH(NOLOCK) 
			WHERE	PersonID=@PersonID
			
			-- IdentifierTypeTable (22)
			
			SELECT	I.[ID], 
					I.[Name], 
					I.[Description], 
					I.[Type], 
					I.[Status], 
					I.[RequiredValidation], 
					I.[ValidationClass], 
					I.[ValidationMask], 
					I.[ModifiedBy], 
					I.[LastUpdated], 
					CAST(I.[DBTimeStamp] as bigint) DBTimeStamp
					
			FROM	IdentifierType I WITH(NOLOCK) 
			
			JOIN	PersonIdentifierRel P WITH(NOLOCK) 
			ON		I.[ID]=P.IdentifierTypeID 
			
			WHERE	P.PersonID=@PersonID	
*/									   		   		
	--END				   
END

--EXEC [ObtenerHumanResourceEntity] 1286 ;2050

--exec ObtenerHumanResourceEntity @HumanResourceID=1482
--exec ObtenerPersonEntity @PERSONID=286589
