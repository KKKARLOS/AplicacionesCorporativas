USE [HCDIS_OPT]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerPersonEntity]    Script Date: 30/05/2018 16:01:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ObtenerPersonEntity]
(
	@PersonID int
)
AS
BEGIN
				
	-- PersonTable (1)
	
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
		
	-- PersonTelephoneBaseTable (2)
	
	SELECT	[ID], 
			PersonID, 
			TelephoneID, 
			ModifiedBy, 
			LastUpdated, 
			CAST(DBTimeStamp as bigint) DBTimeStamp
			
	FROM	PersonTelephoneRel WITH(NOLOCK)
	WHERE	PersonID=@PersonID				
	
	-- TelephoneTable (3)
	
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
			
	-- AddressTable (4)
		
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

	-- AddressTable (5)
	
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
	
	-- PersonCategoryBaseTable (6)
	
	SELECT	[ID], 
			PersonID, 
			CategoryID, 
			ModifiedBy, 
			LastUpdated, 
			CAST(DBTimeStamp as bigint) DBTimeStamp
			
	FROM	PersonCatRel WITH(NOLOCK) 
	WHERE	PersonID=@PersonID		
	
	-- CategoryTable (7)
	
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
	
	-- SensitiveDataTable (8)										   							   									   								   						

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
	   
	   -- IdentifierTable (9)

		SELECT	[ID], 
				PersonID, 
				IdentifierTypeID, 
				IDNumber, 
				ModifiedBy, 
				LastUpdated, 
				CAST(DBTimeStamp as bigint) DBTimeStamp
		
		FROM	PersonIdentifierRel WITH(NOLOCK) 
		WHERE	PersonID=@PersonID
		
		-- IdentifierTypeTable (10)
		
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
END

--EXEC [ObtenerPersonEntity] 308893

