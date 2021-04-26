SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
/*Pointer used for text / image updates. This might not be needed, but is declared here just in case*/
DECLARE @pv binary(16)
BEGIN TRANSACTION
UPDATE [dbo].[EACAttribute] SET [Type]=6, [ComponentType]=0, [HasOptions]=1, [CodeGenerator]=N'', [LastUpdated]='20150409 17:17:15.260', [ModifiedBy]=N'Administrador' WHERE [ID]=130
UPDATE [dbo].[EACAttribute] SET [ComponentType]=0, [HasOptions]=1, [CodeGenerator]=N'', [LastUpdated]='20150409 11:39:08.353', [ModifiedBy]=N'Administrador' WHERE [ID]=139
INSERT INTO [dbo].[EACAttributeOption] ( [EACAttributeID], [Description], [Value], [LastUpdated], [ModifiedBy], [Status]) VALUES (67, N'PhysicianEntity', N'PhysicianEntity', '20150409 09:03:57.717', N'Administrador', 2)
INSERT INTO [dbo].[EACAttributeOption] ( [EACAttributeID], [Description], [Value], [LastUpdated], [ModifiedBy], [Status]) VALUES (130, N'CareCenterEntity', N'CareCenterEntity', '20150409 10:45:16.543', N'Administrador', 2)
INSERT INTO [dbo].[EACAttributeOption] ( [EACAttributeID], [Description], [Value], [LastUpdated], [ModifiedBy], [Status]) VALUES (139, N'CIF', N'CIF', '20150409 10:52:45.987', N'Administrador', 2)
INSERT INTO [dbo].[EACAttributeOption] ( [EACAttributeID], [Description], [Value], [LastUpdated], [ModifiedBy], [Status]) VALUES (139, N'CIF EXTRANJERO', N'CIF EXTRANJERO', '20150409 11:39:08.360', N'Administrador', 2)
INSERT INTO [dbo].[EACAttributeOption] ( [EACAttributeID], [Description], [Value], [LastUpdated], [ModifiedBy], [Status]) VALUES (130, N'CustomerContactOrganizationEntity', N'CustomerContactOrganizationEntity', '20150409 17:13:50.070', N'admhcdis', 3)
INSERT INTO [dbo].[EACAttributeOption] ( [EACAttributeID], [Description], [Value], [LastUpdated], [ModifiedBy], [Status]) VALUES (130, N'InsurerEntity', N'InsurerEntity', '20150409 12:48:27.843', N'Administrador', 2)
INSERT INTO [dbo].[EACAttributeOption] ( [EACAttributeID], [Description], [Value], [LastUpdated], [ModifiedBy], [Status]) VALUES (130, N'BuyerEntity', N'BuyerEntity', '20150409 12:48:27.847', N'Administrador', 2)
INSERT INTO [dbo].[EACAttributeOption] ( [EACAttributeID], [Description], [Value], [LastUpdated], [ModifiedBy], [Status]) VALUES (130, N'SupplierEntity', N'SupplierEntity', '20150409 12:48:27.850', N'Administrador', 2)
INSERT INTO [dbo].[EACAttributeOption] ( [EACAttributeID], [Description], [Value], [LastUpdated], [ModifiedBy], [Status]) VALUES (67, N'CustomerContactPersonEntity', N'CustomerContactPersonEntity', '20150409 16:36:31.313', N'admhcdis', 2)
COMMIT TRANSACTION