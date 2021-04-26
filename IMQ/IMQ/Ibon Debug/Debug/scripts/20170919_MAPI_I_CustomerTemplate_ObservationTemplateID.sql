USE [HCDIS_Preproduccion]
GO
DROP INDEX [dbo].[CustomerTemplate].I_CustomerTemplate_ObservationTemplateID
/****** Object:  Index [I_CustomerTemplate_ObservationTemplateID]    Script Date: 09/19/2017 11:09:30 ******/
CREATE NONCLUSTERED INDEX [I_CustomerTemplate_ObservationTemplateID] ON [dbo].[CustomerTemplate] 
(
	[ObservationTemplateID] ASC,
	[Status] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


