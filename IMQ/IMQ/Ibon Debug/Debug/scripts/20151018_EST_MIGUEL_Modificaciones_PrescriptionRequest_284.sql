USE [HCDIS]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PrescriptionRequest_CustomerOrderRequestID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_CustomerOrderRequestID]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PrescriptionRequest_CustomerProcedureID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_CustomerProcedureID]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PrescriptionRequest_RequestedPersonID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_RequestedPersonID]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PrescriptionRequest_CurrentLocationID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_CurrentLocationID]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PrescriptionRequest_PharmacistValidateStatus]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_PharmacistValidateStatus]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PrescriptionRequest_UnitaryQty]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_UnitaryQty]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PrescriptionRequest_AdministrationRouteID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_AdministrationRouteID]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PrescriptionRequest_AdministrationMethodID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_AdministrationMethodID]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PrescriptionRequest_PhysicianValidateStatus]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_PhysicianValidateStatus]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PrescriptionRequest_PharmacistValidateStatus_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_PharmacistValidateStatus_1]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PrescriptionRequest_AllowSubstitute]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_AllowSubstitute]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PrescriptionRequest_SupplySupervised]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_SupplySupervised]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PrescriptionRequest_Dispatchment]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_Dispatchment]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PrescriptionRequest_PredecessorID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PrescriptionRequest] DROP CONSTRAINT [DF_PrescriptionRequest_PredecessorID]
END

GO

/****** Object:  Table [dbo].[PrescriptionRequest]    Script Date: 10/18/2015 13:14:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tmp_ms_xx_PrescriptionRequest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerOrderRequestID] [int] NULL,
	[CustomerProcedureID] [int] NULL,
	[RequestedPersonID] [int] NULL,
	[CurrentLocationID] [int] NOT NULL,
	[LocationID] [int] NOT NULL,
	[ItemID] [int] NOT NULL,
	[IncludeInitialDose] [bit] NOT NULL,
	[InitialDoseUnits] [int] NOT NULL CONSTRAINT [DF_PrescriptionRequest_InitialDoseUnits] DEFAULT(0),
	[InformativePrescription] [bit] NOT NULL CONSTRAINT [DF_PrescriptionRequest_InformativePrescription] DEFAULT(0),
	[UnitaryQty] [float] NOT NULL,
	[UnitaryDose] [float] NULL,
	[DayDose] [float] NULL,
	[TotalDose] [float] NULL,
	[AdministrationRouteID] [int] NOT NULL,
	[AdministrationMethodID] [int] NOT NULL,
	[PhysicianValidateStatus] [smallint] NOT NULL,
	[PharmacistValidateStatus] [smallint] NOT NULL,
	[AllowSubstitute] [bit] NOT NULL,
	[SupplySupervised] [bit] NOT NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[Dispatchment] [smallint] NOT NULL,
	[LastDispatchDateTime] [datetime] NULL,
	[EstimatedDurationLastDispatch] [datetime] NULL,
	[PredecessorID] [int] NULL,
	[MeaningBeforeSuperceded] [nvarchar](max) NULL,
	[Status] [smallint] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
);

ALTER TABLE [dbo].[tmp_ms_xx_PrescriptionRequest]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_PrescriptionRequest] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[PrescriptionRequest])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_PrescriptionRequest] ON;
        INSERT INTO [dbo].[tmp_ms_xx_PrescriptionRequest] 
        ([ID],[CustomerOrderRequestID],[CustomerProcedureID],[RequestedPersonID],[CurrentLocationID],
        [LocationID],[ItemID],[IncludeInitialDose],[UnitaryQty],[UnitaryDose],[DayDose],[TotalDose],
        [AdministrationRouteID],[AdministrationMethodID],[PhysicianValidateStatus],
        [PharmacistValidateStatus],[AllowSubstitute],[SupplySupervised],[StartDateTime],[EndDateTime],
        [Dispatchment],[LastDispatchDateTime],[EstimatedDurationLastDispatch],[PredecessorID],
        [MeaningBeforeSuperceded],[Status],[LastUpdated],[ModifiedBy])
        SELECT  [ID],[CustomerOrderRequestID],[CustomerProcedureID],[RequestedPersonID],[CurrentLocationID],
        [LocationID],[ItemID],[IncludeInitialDose],[UnitaryQty],[UnitaryDose],[DayDose],[TotalDose],
        [AdministrationRouteID],[AdministrationMethodID],[PhysicianValidateStatus],
        [PharmacistValidateStatus],[AllowSubstitute],[SupplySupervised],[StartDateTime],[EndDateTime],
        [Dispatchment],[LastDispatchDateTime],[EstimatedDurationLastDispatch],[PredecessorID],
        [MeaningBeforeSuperceded],[Status],[LastUpdated],[ModifiedBy]
        FROM     [dbo].[PrescriptionRequest]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_PrescriptionRequest] OFF;
    END

DROP TABLE [dbo].[PrescriptionRequest];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_PrescriptionRequest]', N'PrescriptionRequest';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_PrescriptionRequest]', N'PK_PrescriptionRequest', N'OBJECT';

GO

ALTER TABLE [dbo].[PrescriptionRequest] ADD  CONSTRAINT [DF_PrescriptionRequest_CustomerOrderRequestID]  DEFAULT ((0)) FOR [CustomerOrderRequestID]
GO

ALTER TABLE [dbo].[PrescriptionRequest] ADD  CONSTRAINT [DF_PrescriptionRequest_CustomerProcedureID]  DEFAULT ((0)) FOR [CustomerProcedureID]
GO

ALTER TABLE [dbo].[PrescriptionRequest] ADD  CONSTRAINT [DF_PrescriptionRequest_RequestedPersonID]  DEFAULT ((0)) FOR [RequestedPersonID]
GO

ALTER TABLE [dbo].[PrescriptionRequest] ADD  CONSTRAINT [DF_PrescriptionRequest_CurrentLocationID]  DEFAULT ((0)) FOR [CurrentLocationID]
GO

ALTER TABLE [dbo].[PrescriptionRequest] ADD  CONSTRAINT [DF_PrescriptionRequest_PharmacistValidateStatus]  DEFAULT ((0)) FOR [IncludeInitialDose]
GO

ALTER TABLE [dbo].[PrescriptionRequest] ADD  CONSTRAINT [DF_PrescriptionRequest_UnitaryQty]  DEFAULT ((1)) FOR [UnitaryQty]
GO

ALTER TABLE [dbo].[PrescriptionRequest] ADD  CONSTRAINT [DF_PrescriptionRequest_AdministrationRouteID]  DEFAULT ((0)) FOR [AdministrationRouteID]
GO

ALTER TABLE [dbo].[PrescriptionRequest] ADD  CONSTRAINT [DF_PrescriptionRequest_AdministrationMethodID]  DEFAULT ((0)) FOR [AdministrationMethodID]
GO

ALTER TABLE [dbo].[PrescriptionRequest] ADD  CONSTRAINT [DF_PrescriptionRequest_PhysicianValidateStatus]  DEFAULT ((0)) FOR [PhysicianValidateStatus]
GO

ALTER TABLE [dbo].[PrescriptionRequest] ADD  CONSTRAINT [DF_PrescriptionRequest_PharmacistValidateStatus_1]  DEFAULT ((0)) FOR [PharmacistValidateStatus]
GO

ALTER TABLE [dbo].[PrescriptionRequest] ADD  CONSTRAINT [DF_PrescriptionRequest_AllowSubstitute]  DEFAULT ((1)) FOR [AllowSubstitute]
GO

ALTER TABLE [dbo].[PrescriptionRequest] ADD  CONSTRAINT [DF_PrescriptionRequest_SupplySupervised]  DEFAULT ((0)) FOR [SupplySupervised]
GO

ALTER TABLE [dbo].[PrescriptionRequest] ADD  CONSTRAINT [DF_PrescriptionRequest_Dispatchment]  DEFAULT ((0)) FOR [Dispatchment]
GO

ALTER TABLE [dbo].[PrescriptionRequest] ADD  CONSTRAINT [DF_PrescriptionRequest_PredecessorID]  DEFAULT ((0)) FOR [PredecessorID]
GO


