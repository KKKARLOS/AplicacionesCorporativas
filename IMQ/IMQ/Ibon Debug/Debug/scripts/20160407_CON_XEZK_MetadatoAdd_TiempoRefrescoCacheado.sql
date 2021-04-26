
INSERT INTO [dbo].[EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired], [Design],
			[HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index], [ValidationMask], [DisplayMask],
			[InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
     VALUES
           (701, 'AgreementByEpisodesRefreshTime'
           ,'Tiempo en minutos para el refresco de los datos de autorizaciones.'
           ,0
           ,''
           ,0
           ,0
           ,1
           ,0
           ,0
           ,0
           ,1
           ,1
           ,0
           ,-1
           ,''
           ,''
           ,''
           ,'1440'
           ,''
           ,2
           ,getdate()
           ,'44976499')
GO

