delete from Addin_MDS_AdmitReason
delete from Addin_MDS_AdmitReasonHCDRel

DBCC CHECKIDENT (Addin_MDS_AdmitReason, reseed, 0)
DBCC CHECKIDENT (Addin_MDS_AdmitReasonHCDRel, reseed, 0)

insert into Addin_MDS_AdmitReason (Code,Name,Status,LastUpdated,ModifiedBy) values ('1','No programado',1,getDate(),'ACT_CMBD')
insert into Addin_MDS_AdmitReason (Code,Name,Status,LastUpdated,ModifiedBy) values ('2','Programado',1,getDate(),'ACT_CMBD')
insert into Addin_MDS_AdmitReason (Code,Name,Status,LastUpdated,ModifiedBy) values ('9','Desconocido',1,getDate(),'ACT_CMBD')

INSERT INTO [dbo].[Addin_MDS_AdmitReasonHCDRel]
           ([MDSAdmitReasonID]
           ,[EpisodeReasonID]
           ,[EpisodeReasonName]
           ,[LastUpdated]
           ,[ModifiedBy])
     VALUES
           (2
           ,849
           ,'Programado'
           ,getDate()
           ,'ACT_CMBD')

INSERT INTO [dbo].[Addin_MDS_AdmitReasonHCDRel]
           ([MDSAdmitReasonID]
           ,[EpisodeReasonID]
           ,[EpisodeReasonName]
           ,[LastUpdated]
           ,[ModifiedBy])
     VALUES
           (1
           ,850
           ,'No Programado'
           ,getDate()
           ,'ACT_CMBD')

INSERT INTO [dbo].[Addin_MDS_AdmitReasonHCDRel]
           ([MDSAdmitReasonID]
           ,[EpisodeReasonID]
           ,[EpisodeReasonName]
           ,[LastUpdated]
           ,[ModifiedBy])
     VALUES
           (2
           ,851
           ,'Urgente'
           ,getDate()
           ,'ACT_CMBD')
