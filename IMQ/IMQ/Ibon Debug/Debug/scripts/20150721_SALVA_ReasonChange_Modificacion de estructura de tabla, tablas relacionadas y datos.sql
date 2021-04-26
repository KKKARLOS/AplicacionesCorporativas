/* CREACIÓN DE LA NUEVA ESTRUCTURA DE LA TABLA [ReasonChange]*/
IF OBJECT_ID('FK_LocationAvalabilityReasonRel_LocationAvailability') IS NOT NULL 
	EXEC sp_rename 'FK_LocationAvalabilityReasonRel_LocationAvailability', 'FK_LocationAvailabilityReasonRel_LocationAvailability'
IF OBJECT_ID('FK_LocationAvalabilityReasonRel_ReasonChange') IS NOT NULL 
	EXEC sp_rename 'FK_LocationAvalabilityReasonRel_ReasonChange', 'FK_LocationAvailabilityReasonRel_ReasonChange'

--Borramos FOREIGN KEYS 
ALTER TABLE [LocationAvailabilityReasonRel]
DROP CONSTRAINT [FK_LocationAvailabilityReasonRel_ReasonChange]
GO

CREATE TABLE [ReasonChangeCopy]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AssignedCode] [nvarchar](50) NULL,
	[Reason] [nvarchar](200) NULL,
	[ElementID] [int] NOT NULL,
	[EntityID] [int] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[ReasonChangeType] [smallint] NOT NULL,
	[Status] [smallint] NOT NULL,
	[LastUpdated] [datetime] NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
	CONSTRAINT [PK_ReasonChangeCopy] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
	WITH 
	(
		PAD_INDEX  = OFF, 
		STATISTICS_NORECOMPUTE  = OFF, 
		IGNORE_DUP_KEY = OFF, 
		ALLOW_ROW_LOCKS  = ON, 
		ALLOW_PAGE_LOCKS  = ON
	) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [ReasonChangeCopy] 
ADD  CONSTRAINT [DF_ReasonChangeCopy_ElementID]  
DEFAULT ((0)) FOR [ElementID]
GO

ALTER TABLE [ReasonChangeCopy] 
ADD  CONSTRAINT [DF_ReasonChangeCopy_EntityID]  
DEFAULT ((0)) FOR [EntityID]
GO

ALTER TABLE [ReasonChangeCopy] 
ADD  CONSTRAINT [DF_ReasonChangeCopy_IsDefault]  
DEFAULT ((0)) FOR [IsDefault]
GO

ALTER TABLE [ReasonChangeCopy] 
ADD  CONSTRAINT [DF_ReasonChangeCopy_Status]  
DEFAULT ((1)) FOR [Status]
GO



/**************************************************************************************************************************/

/* INSERCIÓN DE LOS VALORES DE LA TABLA ORIGINAL A LA NUEVA*/
SET IDENTITY_INSERT [ReasonChangeCopy] ON
INSERT INTO [ReasonChangeCopy] 
	([ID], 
		[AssignedCode], 
		[Reason], 
		[ElementID], 
		[EntityID], 
		[IsDefault],
		[ReasonChangeType], 
		[Status], 
		[LastUpdated], 
		[ModifiedBy])
SELECT [ID], 
		[AssignedCode], 
		[Reason], 
		CASE  
		WHEN [ReasonChangeType] IN (1,2,3,29,31,33)
			THEN (select top 1 ID from EACElement where Name='RoutineTypeEntity')
		WHEN [ReasonChangeType] IN (4,5,6,30,32,34,100)
			THEN (select top 1 ID from EACElement where Name='ProcedureClassificationEntity') 
		WHEN [ReasonChangeType] IN (16,17,18,35,36,37,103)
			THEN (select top 1 ID from EACElement where Name='OrderTypeEntity')
		WHEN [ReasonChangeType] IN (12,13)
			THEN (select top 1 ID from EACElement where Name='EpisodeTypeEntity')
		WHEN [ReasonChangeType] IN (10)
			THEN (select top 1 ID from EACElement where Name='CustomerEpisodeAuthorizationEntity')
		WHEN [ReasonChangeType] IN (40,41)
			THEN (select top 1 ID from EACElement where Name='CustomerCitationEntity')
		WHEN [ReasonChangeType] IN (44)
			THEN (select top 1 ID from EACElement where Name='CustomerReservationEntity')
		WHEN [ReasonChangeType] IN (43)
			THEN (select top 1 ID from EACElement where Name='CustomerTransferEntity')
		WHEN [ReasonChangeType] IN (42)
			THEN (select top 1 ID from EACElement where Name='CustomerLeaveEntity')
		WHEN [ReasonChangeType] IN (23,24)
			THEN (select top 1 ID from EACElement where Name='CustomerReceptionEntity')
		WHEN [ReasonChangeType] IN (25,26)
			THEN (select top 1 ID from EACElement where Name='CustomerReportsEntity')
		WHEN [ReasonChangeType] IN (14)
			THEN (select top 1 ID from EACElement where Name='NotificationActEntity')
		WHEN [ReasonChangeType] IN (15)
			THEN (select top 1 ID from EACElement where Name='ReceiveNotificationActionEntity')	
		WHEN [ReasonChangeType] IN (19,20,104)
			THEN (select top 1 ID from EACElement where Name='MedicalEpisodeEntity')
		WHEN [ReasonChangeType] IN (21,22)
			THEN (select top 1 ID from EACElement where Name='ProcessChartEntity')
		WHEN [ReasonChangeType] IN (28)
			THEN (select top 1 ID from EACElement where Name='MedEpisodeProcessChartEntity')
		WHEN [ReasonChangeType] IN (90)
			THEN (select top 1 ID from EACElement where Name='LocationEntity')
		ELSE 0 
		END as [ElementID],
		0 as [EntityID],
		0 as [IsDefault],
		[ReasonChangeType],
		[Status],
		[LastUpdated], 
		[ModifiedBy]
FROM [ReasonChange]
WHERE ReasonChangeType NOT IN (3,6,7,8,9)--Estos no se copiarán, ya que no están enlazados a ninguna tabla.
SET IDENTITY_INSERT [ReasonChangeCopy] OFF
GO

--Borramos tabla
DROP TABLE [ReasonChange]

GO

EXEC sp_rename 'ReasonChangeCopy', 'ReasonChange';
EXEC sp_rename 'ReasonChange.PK_ReasonChangeCopy', 'PK_ReasonChange';
EXEC sp_rename 'DF_ReasonChangeCopy_ElementID', 'DF_ReasonChange_ElementID';
EXEC sp_rename 'DF_ReasonChangeCopy_EntityID', 'DF_ReasonChange_EntityID';
EXEC sp_rename 'DF_ReasonChangeCopy_IsDefault', 'DF_ReasonChange_IsDefault';
EXEC sp_rename 'DF_ReasonChangeCopy_Status', 'DF_ReasonChange_Status';

GO

ALTER TABLE [LocationAvailabilityReasonRel] WITH CHECK
ADD CONSTRAINT [FK_LocationAvailabilityReasonRel_ReasonChange]
FOREIGN KEY ([ReasonChangeID])
REFERENCES [ReasonChange]([ID])

ALTER TABLE [LocationAvailabilityReasonRel] CHECK CONSTRAINT [FK_LocationAvailabilityReasonRel_ReasonChange]
GO

--UPDATES en tabla ReasonChange

INSERT INTO ReasonChange
([AssignedCode], 
	[Reason], 
	[ElementID], 
	[EntityID], 
	[IsDefault],
	[ReasonChangeType], 
	[Status], 
	[LastUpdated], 
	[ModifiedBy])
SELECT TOP 1 [AssignedCode]+'P' as [AssignedCode], 
	[Reason], 
	(select top 1 ID from EACElement where Name='ProcedureClassificationEntity') as [ElementID],
	0 as [EntityID],
	0 as [IsDefault],
	[ReasonChangeType],
	[Status],
	GetDate(), 
	'Salva'
FROM [ReasonChange]
WHERE ReasonChangeType=101

UPDATE ProcedureActReasonRel 
SET ReasonChangeID=(SELECT TOP 1 ID FROM ReasonChange WHERE ReasonChangeType=101 AND ElementID>0)
WHERE ReasonChangeID=(SELECT TOP 1 ID FROM ReasonChange WHERE ReasonChangeType=101 AND ElementID=0)

UPDATE ReasonChange
SET ElementID=(SELECT TOP 1 ID FROM EACElement WHERE Name='RoutineTypeEntity')
WHERE ReasonChangeType=101 AND ElementID=0

INSERT INTO ReasonChange
([AssignedCode], 
	[Reason], 
	[ElementID], 
	[EntityID], 
	[IsDefault],
	[ReasonChangeType], 
	[Status], 
	[LastUpdated], 
	[ModifiedBy])
SELECT TOP 1 [AssignedCode]+'P' as [AssignedCode], 
	[Reason], 
	(select top 1 ID from EACElement where Name='ProcedureClassificationEntity') as [ElementID],
	0 as [EntityID],
	0 as [IsDefault],
	[ReasonChangeType],
	[Status],
	GetDate(), 
	'Salva'
FROM ReasonChange
WHERE ReasonChangeType=102

UPDATE CustomerProcedureReasonRel 
SET ReasonChangeID=(SELECT TOP 1 ID FROM ReasonChange WHERE ReasonChangeType=102 and ElementID>0)
WHERE ReasonChangeID=(SELECT TOP 1 ID FROM ReasonChange WHERE ReasonChangeType=102 and ElementID=0)

UPDATE ReasonChange
SET ElementID=(SELECT TOP 1 ID FROM EACElement WHERE Name='RoutineTypeEntity')
WHERE ReasonChangeType=102 AND ElementID=0


UPDATE ReasonChange SET ReasonChangeType=1 WHERE ReasonChangeType IN (1,29,4,30,16,12,42,43,23,19,21)
UPDATE ReasonChange SET ReasonChangeType=2 WHERE ReasonChangeType IN (2,31,5,32,100,17,101,13,40,44,24,14,15,20,22)
UPDATE ReasonChange SET ReasonChangeType=3 WHERE ReasonChangeType IN (3,6,18,10,90)
UPDATE ReasonChange SET ReasonChangeType=4 WHERE ReasonChangeType IN (33,34,35)
UPDATE ReasonChange SET ReasonChangeType=5 WHERE ReasonChangeType IN (37,25)
UPDATE ReasonChange SET ReasonChangeType=6 WHERE ReasonChangeType IN (36,103,102)
UPDATE ReasonChange SET ReasonChangeType=7 WHERE ReasonChangeType IN (104,28)
UPDATE ReasonChange SET ReasonChangeType=8 WHERE ReasonChangeType IN (26)
UPDATE ReasonChange SET ReasonChangeType=9 WHERE ReasonChangeType IN (41)

UPDATE EACElement SET [Description]='Informes' WHERE Name='CustomerReportsEntity'

GO

