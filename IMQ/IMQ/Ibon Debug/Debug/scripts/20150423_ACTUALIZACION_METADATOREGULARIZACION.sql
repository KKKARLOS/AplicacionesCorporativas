/*
This script was created by Visual Studio on 23/04/2015 at 18:30.
Run this script on [SI2NTMAD9.HCDISTesting.sa] to make it the same as [SI2NTMAD8.HCDV2.sa].
This script performs its actions in the following order:
1. Disable foreign-key constraints.
2. Perform DELETE commands. 
3. Perform UPDATE commands.
4. Perform INSERT commands.
5. Re-enable foreign-key constraints.
Please back up your target database before running this script.
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
/*Pointer used for text / image updates. This might not be needed, but is declared here just in case*/
DECLARE @pv binary(16)
BEGIN TRANSACTION
SET IDENTITY_INSERT [dbo].[EACElement] ON
INSERT INTO [dbo].[EACElement] ([ID], [ElementType], [Name], [ModuleName], [Description], [Status], [LastUpdated], [ModifiedBy]) VALUES (680, 2, N'UseRegularizeReasonDTO', N'', N'Entidad que indica la utilización de los motivos de regularización según las vistas que lo requieren', 2, '20150423 18:15:55.530', N'admhcdis')
SET IDENTITY_INSERT [dbo].[EACElement] OFF
SET IDENTITY_INSERT [dbo].[EACAttribute] ON
INSERT INTO [dbo].[EACAttribute] ([ID], [EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired], [Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index], [ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy]) 
				VALUES (15574, 680, N'ReasonsForInventoryManagementEditView', N'En las opciones se indicarán las razones que se utilizarán en la vista InventoryManagementEditView. En valor por defecto la razón que deberá aparecer en el combo de razones al abrir la vista.', 3, N'', 0, 0, 1, 1, 0, 0, 1, 1, 100, -1, N'', N'', N'', N'INVENTARIO', N'', 2, '20150423 18:15:36.327', N'admhcdis')
INSERT INTO [dbo].[EACAttribute] ([ID], [EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired], [Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index], [ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy]) 
				VALUES (15575, 680, N'ReasonsForRegularizeWizard', N'En las opciones se indicarán las razones que se utilizarán en la vista RegularizeWizard. En valor por defecto la razón que deberá aparecer en el combo de razones al abrir la vista.', 3, N'', 0, 0, 1, 1, 0, 0, 1, 1, 100, -1, N'', N'', N'', N'REGULARIZACIÓN', N'', 2, '20150423 18:15:36.343', N'admhcdis')
SET IDENTITY_INSERT [dbo].[EACAttribute] OFF


INSERT INTO [dbo].[EACAttributeOption] ([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status]) VALUES (15574, 'INVENTARIO', 'INVENTARIO', GETDATE(), 'Administrador', 2)
INSERT INTO [dbo].[EACAttributeOption] ([EACAttributeID],[Description],[Value],[LastUpdated],[ModifiedBy],[Status]) VALUES (15575, 'REGULARIZACIÓN', 'REGULARIZACIÓN', GETDATE(), 'Administrador', 2)

COMMIT TRANSACTION
