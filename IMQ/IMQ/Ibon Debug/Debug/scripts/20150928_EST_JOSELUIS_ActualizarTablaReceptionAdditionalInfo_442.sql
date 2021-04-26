IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ReceptionAdditionalInfo_CallingDeviceID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ReceptionAdditionalInfo] DROP CONSTRAINT [DF_ReceptionAdditionalInfo_CallingDeviceID]
END
go

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ReceptionAdditionalInfo_Status]') AND type = 'D')
BEGIN
ALTER TABLE dbo.ReceptionAdditionalInfo
	DROP CONSTRAINT DF_ReceptionAdditionalInfo_Status
END
go

CREATE TABLE dbo.Tmp_ReceptionAdditionalInfo
	(
	ID int NOT NULL IDENTITY (1, 1),
	CustomerReceptionID int NOT NULL,
	ArrivalDateTime datetime NOT NULL,
	AttendDateTime datetime NULL,
	CollectionResults datetime NULL,
	ResultsPrinted bit NOT NULL,
	AuthorizatedToCollectionID int NOT NULL,
	CompanionID int NOT NULL,
	ProformaSignedByID int NOT NULL,
	ProformaPrinted bit NOT NULL,
	ConsentSignedByID int NOT NULL,
	MedicalEpisodeID int NOT NULL,
	CustomerMedEpisodeActID int NOT NULL,
	Explanation nvarchar(MAX) NULL,
	EnterVisit datetime NULL,
	LeaveVisit datetime NULL,
	WaitingNumber nvarchar(50) NULL,
	CallingDeviceID int NOT NULL,
	Status smallint NOT NULL,
	LastUpdated datetime NOT NULL,
	ModifiedBy nvarchar(256) NOT NULL,
	DBTimeStamp timestamp NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
go

ALTER TABLE dbo.Tmp_ReceptionAdditionalInfo SET (LOCK_ESCALATION = TABLE)

go

ALTER TABLE dbo.Tmp_ReceptionAdditionalInfo ADD CONSTRAINT
	DF_ReceptionAdditionalInfo_CallingDeviceID DEFAULT 0 FOR CallingDeviceID

go
ALTER TABLE dbo.Tmp_ReceptionAdditionalInfo ADD CONSTRAINT
	DF_ReceptionAdditionalInfo_Status DEFAULT ((1)) FOR Status

go
SET IDENTITY_INSERT dbo.Tmp_ReceptionAdditionalInfo ON

go
IF EXISTS(SELECT * FROM dbo.ReceptionAdditionalInfo)
	 EXEC('INSERT INTO dbo.Tmp_ReceptionAdditionalInfo (ID, CustomerReceptionID, ArrivalDateTime, AttendDateTime, CollectionResults, ResultsPrinted, AuthorizatedToCollectionID, CompanionID, ProformaSignedByID, ProformaPrinted, ConsentSignedByID, MedicalEpisodeID, CustomerMedEpisodeActID, Explanation, Status, 
LastUpdated, ModifiedBy)
		SELECT ID, CustomerReceptionID, ArrivalDateTime, AttendDateTime, CollectionResults, ResultsPrinted, AuthorizatedToCollectionID, CompanionID, ProformaSignedByID, ProformaPrinted, ConsentSignedByID, MedicalEpisodeID, CustomerMedEpisodeActID, Explanation, Status, LastUpdated, ModifiedBy FROM dbo.ReceptionAdditionalInfo WITH (HOLDLOCK TABLOCKX)')

go
SET IDENTITY_INSERT dbo.Tmp_ReceptionAdditionalInfo OFF

go
DROP TABLE dbo.ReceptionAdditionalInfo

go
EXECUTE sp_rename N'dbo.Tmp_ReceptionAdditionalInfo', N'ReceptionAdditionalInfo', 'OBJECT' 

go
ALTER TABLE dbo.ReceptionAdditionalInfo ADD CONSTRAINT
	PK_ReceptionAdditionalInfo PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


go