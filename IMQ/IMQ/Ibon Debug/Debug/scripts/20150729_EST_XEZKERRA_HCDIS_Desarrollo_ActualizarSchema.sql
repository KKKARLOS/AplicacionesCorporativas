/*
Script de implementación para HCDIS_Desarrollo

Una herramienta generó este código.
Los cambios realizados en este archivo podrían generar un comportamiento incorrecto y se perderán si
se vuelve a generar el código.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;

USE [HCDIS_Desarrollo];


GO
/*
Se está quitando la columna [dbo].[Invoice].[GuarantorInvoiceAgreePrintRuleID]; puede que se pierdan datos.
*/

IF EXISTS (select top 1 1 from [dbo].[Invoice])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
/*
Se está quitando la columna [dbo].[InvoiceConfigInvoiceAgreeRel].[InvoiceConfigAgreementID]; puede que se pierdan datos.

Debe agregarse la columna [dbo].[InvoiceConfigInvoiceAgreeRel].[GuarantorType] de la tabla [dbo].[InvoiceConfigInvoiceAgreeRel], pero esta columna no tiene un valor predeterminado y no admite valores NULL. Si la tabla contiene datos, el script ALTER no funcionará. Para evitar este problema, agregue un valor predeterminado a la columna, márquela de modo que permita valores NULL o habilite la generación de valores predeterminados inteligentes como opción de implementación.

Debe agregarse la columna [dbo].[InvoiceConfigInvoiceAgreeRel].[InvoiceAgreementID] de la tabla [dbo].[InvoiceConfigInvoiceAgreeRel], pero esta columna no tiene un valor predeterminado y no admite valores NULL. Si la tabla contiene datos, el script ALTER no funcionará. Para evitar este problema, agregue un valor predeterminado a la columna, márquela de modo que permita valores NULL o habilite la generación de valores predeterminados inteligentes como opción de implementación.
*/

IF EXISTS (select top 1 1 from [dbo].[InvoiceConfigInvoiceAgreeRel])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
/*
Se está quitando la tabla [dbo].[InvoiceAgreeRemittanceConfig]. La implementación se detendrá si la tabla contiene datos.
*/

IF EXISTS (select top 1 1 from [dbo].[InvoiceAgreeRemittanceConfig])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
/*
Se está quitando la tabla [dbo].[InvoiceConfigAgreement]. La implementación se detendrá si la tabla contiene datos.
*/

IF EXISTS (select top 1 1 from [dbo].[InvoiceConfigAgreement])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
/*
Se está quitando la tabla [dbo].[RemittanceConfigInvoiceAgreeRemittanceRel]. La implementación se detendrá si la tabla contiene datos.
*/

IF EXISTS (select top 1 1 from [dbo].[RemittanceConfigInvoiceAgreeRemittanceRel])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
PRINT N'Quitando DF_Invoice_CareCenterID...';


GO
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [DF_Invoice_CareCenterID];


GO
PRINT N'Quitando DF_Invoice_GuarantorID...';


GO
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [DF_Invoice_GuarantorID];


GO
PRINT N'Quitando DF_Invoice_GuarantorType...';


GO
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [DF_Invoice_GuarantorType];


GO
PRINT N'Quitando DF_Invoice_GuarantorInvoiceAgreePrintRuleID...';


GO
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [DF_Invoice_GuarantorInvoiceAgreePrintRuleID];


GO
PRINT N'Quitando DF_Invoice_RemittanceStatus...';


GO
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [DF_Invoice_RemittanceStatus];


GO
PRINT N'Quitando DF_ReasonChange_Status...';


GO
ALTER TABLE [dbo].[ReasonChange] DROP CONSTRAINT [DF_ReasonChange_Status];


GO
PRINT N'Quitando DF_InvoiceAgreeRemittanceConfigRel_RejectRemittanceIsPossible...';


GO
ALTER TABLE [dbo].[InvoiceAgreeRemittanceConfig] DROP CONSTRAINT [DF_InvoiceAgreeRemittanceConfigRel_RejectRemittanceIsPossible];


GO
PRINT N'Quitando DF_InvoiceAgreeRemittanceConfigRel_RenewRemittanceIsPossible...';


GO
ALTER TABLE [dbo].[InvoiceAgreeRemittanceConfig] DROP CONSTRAINT [DF_InvoiceAgreeRemittanceConfigRel_RenewRemittanceIsPossible];


GO
PRINT N'Quitando DF_InvoiceConfigAgreementRel_RejectInvoiceIsPossible...';


GO
ALTER TABLE [dbo].[InvoiceConfigAgreement] DROP CONSTRAINT [DF_InvoiceConfigAgreementRel_RejectInvoiceIsPossible];


GO
PRINT N'Quitando FK_LocationAvalabilityReasonRel_ReasonChange...';


GO
ALTER TABLE [dbo].[LocationAvailabilityReasonRel] DROP CONSTRAINT [FK_LocationAvalabilityReasonRel_ReasonChange];


GO
PRINT N'Quitando FK_LocationAvalabilityReasonRel_LocationAvailability...';


GO
ALTER TABLE [dbo].[LocationAvailabilityReasonRel] DROP CONSTRAINT [FK_LocationAvalabilityReasonRel_LocationAvailability];


GO
PRINT N'Quitando [dbo].[InvoiceAgreeRemittanceConfig]...';


GO
DROP TABLE [dbo].[InvoiceAgreeRemittanceConfig];


GO
PRINT N'Quitando [dbo].[InvoiceConfigAgreement]...';


GO
DROP TABLE [dbo].[InvoiceConfigAgreement];


GO
PRINT N'Quitando [dbo].[RemittanceConfigInvoiceAgreeRemittanceRel]...';


GO
DROP TABLE [dbo].[RemittanceConfigInvoiceAgreeRemittanceRel];


GO
PRINT N'Modificando [dbo].[Act]...';


GO
ALTER TABLE [dbo].[Act] ALTER COLUMN [Description] NVARCHAR (2000) NULL;

ALTER TABLE [dbo].[Act] ALTER COLUMN [Name] NVARCHAR (1000) NOT NULL;


GO
PRINT N'Iniciando recompilación de la tabla [dbo].[CriteriaEpisodeReasonRel]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_CriteriaEpisodeReasonRel] (
    [ID]                      INT            IDENTITY (1, 1) NOT NULL,
    [EpisodeTypeID]           INT            NOT NULL,
    [EpisodeReasonTypeID]     INT            NOT NULL,
    [EpisodeReasonID]         INT            NOT NULL,
    [ProcessChartID]          INT            NOT NULL,
    [CriteriaReasonElement]   INT            NOT NULL,
    [CriteriaReason]          INT            NOT NULL,
    [CriteriaSetRequired]     BIT            NOT NULL,
    [CriteriaSetCopyReasons]  BIT            NOT NULL,
    [NotModifiable]           BIT            CONSTRAINT [DF_CriteriaEpisodeReasonRel_NotModifiable] DEFAULT ((0)) NOT NULL,
    [PreviousElementEntityID] INT            NOT NULL,
    [PreviousElementValue]    NVARCHAR (MAX) NULL,
    [ModifiedBy]              NVARCHAR (256) NULL,
    [LastUpdated]             DATETIME       NULL,
    [DBTimeStamp]             ROWVERSION     NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_CriteriaEpisodeReasonRel] PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[CriteriaEpisodeReasonRel])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CriteriaEpisodeReasonRel] ON;
        INSERT INTO [dbo].[tmp_ms_xx_CriteriaEpisodeReasonRel] ([ID], [EpisodeTypeID], [EpisodeReasonTypeID], [EpisodeReasonID], [ProcessChartID], [CriteriaReasonElement], [CriteriaReason], [CriteriaSetRequired], [CriteriaSetCopyReasons], [PreviousElementEntityID], [PreviousElementValue], [ModifiedBy], [LastUpdated])
        SELECT   [ID],
                 [EpisodeTypeID],
                 [EpisodeReasonTypeID],
                 [EpisodeReasonID],
                 [ProcessChartID],
                 [CriteriaReasonElement],
                 [CriteriaReason],
                 [CriteriaSetRequired],
                 [CriteriaSetCopyReasons],
                 [PreviousElementEntityID],
                 [PreviousElementValue],
                 [ModifiedBy],
                 [LastUpdated]
        FROM     [dbo].[CriteriaEpisodeReasonRel]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CriteriaEpisodeReasonRel] OFF;
    END

DROP TABLE [dbo].[CriteriaEpisodeReasonRel];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_CriteriaEpisodeReasonRel]', N'CriteriaEpisodeReasonRel';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_CriteriaEpisodeReasonRel]', N'PK_CriteriaEpisodeReasonRel', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Iniciando recompilación de la tabla [dbo].[InsurerInvoiceAgreement]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_InsurerInvoiceAgreement] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [InsurerID]          INT            NOT NULL,
    [Name]               NVARCHAR (100) NOT NULL,
    [Description]        NVARCHAR (200) NULL,
    [AccountInformation] NVARCHAR (200) NULL,
    [AccountNumber]      NVARCHAR (24)  NULL,
    [GoesDate]           DATETIME       NOT NULL,
    [ExpirationDate]     DATETIME       NULL,
    [InvSendAddressID]   INT            NULL,
    [InvAddressType]     SMALLINT       NOT NULL,
    [PaymentMethod]      SMALLINT       NOT NULL,
    [InvoiceMode]        SMALLINT       NOT NULL,
    [BillingDayID]       INT            NOT NULL,
    [RemittanceDayID]    INT            NULL,
    [PaymentDueID]       INT            NOT NULL,
    [AncestorID]         INT            NOT NULL,
    [BillGroupingMode]   SMALLINT       CONSTRAINT [DF_InsurerInvoiceAgreement_BillGroupingMode] DEFAULT ((0)) NOT NULL,
    [Status]             SMALLINT       NOT NULL,
    [LastUpdated]        DATETIME       NOT NULL,
    [ModifiedBy]         NVARCHAR (256) NOT NULL,
    [DBTimeStamp]        ROWVERSION     NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_InsurerInvoiceAgreement] PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[InsurerInvoiceAgreement])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_InsurerInvoiceAgreement] ON;
        INSERT INTO [dbo].[tmp_ms_xx_InsurerInvoiceAgreement] ([ID], [InsurerID], [Name], [Description], [AccountInformation], [AccountNumber], [GoesDate], [ExpirationDate], [InvSendAddressID], [InvAddressType], [PaymentMethod], [InvoiceMode], [BillingDayID], [RemittanceDayID], [PaymentDueID], [AncestorID], [Status], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [InsurerID],
                 [Name],
                 [Description],
                 [AccountInformation],
                 [AccountNumber],
                 [GoesDate],
                 [ExpirationDate],
                 [InvSendAddressID],
                 [InvAddressType],
                 [PaymentMethod],
                 [InvoiceMode],
                 [BillingDayID],
                 [RemittanceDayID],
                 [PaymentDueID],
                 [AncestorID],
                 [Status],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[InsurerInvoiceAgreement]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_InsurerInvoiceAgreement] OFF;
    END

DROP TABLE [dbo].[InsurerInvoiceAgreement];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_InsurerInvoiceAgreement]', N'InsurerInvoiceAgreement';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_InsurerInvoiceAgreement]', N'PK_InsurerInvoiceAgreement', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Iniciando recompilación de la tabla [dbo].[Invoice]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Invoice] (
    [ID]                          INT            IDENTITY (1, 1) NOT NULL,
    [CareCenterID]                INT            CONSTRAINT [DF_Invoice_CareCenterID_Temp] DEFAULT ((0)) NOT NULL,
    [GuarantorID]                 INT            CONSTRAINT [DF_Invoice_GuarantorID_Temp] DEFAULT ((0)) NOT NULL,
    [GuarantorType]               SMALLINT       CONSTRAINT [DF_Invoice_GuarantorType_Temp] DEFAULT ((0)) NOT NULL,
    [InvoiceType]                 SMALLINT       NOT NULL,
    [InvoiceNumber]               NVARCHAR (50)  NOT NULL,
    [InvoiceAddressID]            INT            NOT NULL,
    [Comments]                    NVARCHAR (MAX) NULL,
    [InvoiceDateTime]             DATETIME       NOT NULL,
    [RegistrationDateTime]        DATETIME       NOT NULL,
    [TotalQty]                    MONEY          NOT NULL,
    [TaxQty]                      MONEY          NOT NULL,
    [TotalPaid]                   MONEY          NULL,
    [GuarantorInvoiceAgreementID] INT            CONSTRAINT [DF_Invoice_GuarantorInvoiceAgreementID_Temp] DEFAULT ((0)) NOT NULL,
    [InvoicePrintRuleID]          INT            NOT NULL,
    [PrintedDateTime]             DATETIME       NULL,
    [PrintedBy]                   NVARCHAR (256) NOT NULL,
    [InvoiceStatus]               SMALLINT       NOT NULL,
    [PaymentStatus]               SMALLINT       NOT NULL,
    [RemittanceStatus]            SMALLINT       CONSTRAINT [DF_Invoice_RemittanceStatus_Temp] DEFAULT ((0)) NOT NULL,
    [LastUpdated]                 DATETIME       NOT NULL,
    [ModifiedBy]                  NVARCHAR (256) NOT NULL,
    [DBTimeStamp]                 ROWVERSION     NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Invoice] PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Invoice])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Invoice] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Invoice] ([ID], [CareCenterID], [GuarantorID], [GuarantorType], [InvoiceType], [InvoiceNumber], [InvoiceAddressID], [Comments], [InvoiceDateTime], [RegistrationDateTime], [TotalQty], [TaxQty], [InvoicePrintRuleID], [PrintedDateTime], [PrintedBy], [InvoiceStatus], [PaymentStatus], [RemittanceStatus], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [CareCenterID],
                 [GuarantorID],
                 [GuarantorType],
                 [InvoiceType],
                 [InvoiceNumber],
                 [InvoiceAddressID],
                 [Comments],
                 [InvoiceDateTime],
                 [RegistrationDateTime],
                 [TotalQty],
                 [TaxQty],
                 [InvoicePrintRuleID],
                 [PrintedDateTime],
                 [PrintedBy],
                 [InvoiceStatus],
                 [PaymentStatus],
                 [RemittanceStatus],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[Invoice]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Invoice] OFF;
    END

DROP TABLE [dbo].[Invoice];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Invoice]', N'Invoice';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Invoice]', N'PK_Invoice', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Iniciando recompilación de la tabla [dbo].[InvoiceConfigInvoiceAgreeRel]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_InvoiceConfigInvoiceAgreeRel] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [InvoiceConfigID]    INT            NOT NULL,
    [GuarantorType]      SMALLINT       NOT NULL,
    [InvoiceAgreementID] INT            NOT NULL,
    [LastUpdated]        DATETIME       NOT NULL,
    [ModifiedBy]         NVARCHAR (256) NOT NULL,
    [DBTimeStamp]        ROWVERSION     NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_InvoiceConfigInvoiceAgreeRel] PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[InvoiceConfigInvoiceAgreeRel])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_InvoiceConfigInvoiceAgreeRel] ON;
        INSERT INTO [dbo].[tmp_ms_xx_InvoiceConfigInvoiceAgreeRel] ([ID], [InvoiceConfigID], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [InvoiceConfigID],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[InvoiceConfigInvoiceAgreeRel]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_InvoiceConfigInvoiceAgreeRel] OFF;
    END

DROP TABLE [dbo].[InvoiceConfigInvoiceAgreeRel];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_InvoiceConfigInvoiceAgreeRel]', N'InvoiceConfigInvoiceAgreeRel';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_InvoiceConfigInvoiceAgreeRel]', N'PK_InvoiceConfigInvoiceAgreeRel', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Iniciando recompilación de la tabla [dbo].[ReasonChange]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_ReasonChange] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [AssignedCode]     NVARCHAR (50)  NULL,
    [Reason]           NVARCHAR (200) NULL,
    [ElementID]        INT            CONSTRAINT [DF_ReasonChange_ElementID] DEFAULT ((0)) NOT NULL,
    [EntityID]         INT            CONSTRAINT [DF_ReasonChange_EntityID] DEFAULT ((0)) NOT NULL,
    [IsDefault]        BIT            CONSTRAINT [DF_ReasonChange_IsDefault] DEFAULT ((0)) NOT NULL,
    [ReasonChangeType] SMALLINT       NOT NULL,
    [Status]           SMALLINT       CONSTRAINT [DF_ReasonChange_Status] DEFAULT ((1)) NOT NULL,
    [LastUpdated]      DATETIME       NULL,
    [ModifiedBy]       NVARCHAR (256) NULL,
    [DBTimeStamp]      ROWVERSION     NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_ReasonChange] PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[ReasonChange])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ReasonChange] ON;
        INSERT INTO [dbo].[tmp_ms_xx_ReasonChange] ([ID], [ReasonChangeType], [AssignedCode], [Reason], [Status], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [ReasonChangeType],
                 [AssignedCode],
                 [Reason],
                 [Status],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[ReasonChange]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ReasonChange] OFF;
    END

DROP TABLE [dbo].[ReasonChange];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_ReasonChange]', N'ReasonChange';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_ReasonChange]', N'PK_ReasonChange', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Iniciando recompilación de la tabla [dbo].[RemittanceContent]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_RemittanceContent] (
    [ID]                            INT            IDENTITY (1, 1) NOT NULL,
    [RemittanceType]                SMALLINT       NOT NULL,
    [RemittanceExplanation]         NVARCHAR (MAX) NULL,
    [FinancialEntityIdentification] NVARCHAR (50)  NULL,
    [GuarantorID]                   INT            NOT NULL,
    [GuarantorType]                 SMALLINT       NOT NULL,
    [BankID]                        INT            NOT NULL,
    [AccountNumber]                 NVARCHAR (50)  NOT NULL,
    [RemittanceContentNumber]       NVARCHAR (50)  NOT NULL,
    [RemittanceContentName]         NVARCHAR (50)  NULL,
    [RemittanceDateTime]            DATETIME       NOT NULL,
    [RemittanceTotalQty]            MONEY          NOT NULL,
    [PaidTotalQty]                  MONEY          NOT NULL,
    [ReturnedTotalQty]              MONEY          NOT NULL,
    [SendDateTime]                  DATETIME       NULL,
    [SendExplanation]               NVARCHAR (MAX) NULL,
    [InvoicesFromDate]              DATETIME       NOT NULL,
    [InvoicesToDate]                DATETIME       NOT NULL,
    [AncestorID]                    INT            NOT NULL,
    [Status]                        SMALLINT       NOT NULL,
    [LastUpdated]                   DATETIME       NOT NULL,
    [ModifiedBy]                    NVARCHAR (256) NOT NULL,
    [DBTimeStamp]                   ROWVERSION     NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_RemittanceContent] PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[RemittanceContent])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_RemittanceContent] ON;
        INSERT INTO [dbo].[tmp_ms_xx_RemittanceContent] ([ID], [RemittanceType], [RemittanceExplanation], [GuarantorID], [GuarantorType], [BankID], [AccountNumber], [RemittanceContentNumber], [RemittanceContentName], [RemittanceDateTime], [RemittanceTotalQty], [PaidTotalQty], [ReturnedTotalQty], [SendDateTime], [SendExplanation], [InvoicesFromDate], [InvoicesToDate], [AncestorID], [Status], [LastUpdated], [ModifiedBy])
        SELECT   [ID],
                 [RemittanceType],
                 [RemittanceExplanation],
                 [GuarantorID],
                 [GuarantorType],
                 [BankID],
                 [AccountNumber],
                 [RemittanceContentNumber],
                 [RemittanceContentName],
                 [RemittanceDateTime],
                 [RemittanceTotalQty],
                 [PaidTotalQty],
                 [ReturnedTotalQty],
                 [SendDateTime],
                 [SendExplanation],
                 [InvoicesFromDate],
                 [InvoicesToDate],
                 [AncestorID],
                 [Status],
                 [LastUpdated],
                 [ModifiedBy]
        FROM     [dbo].[RemittanceContent]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_RemittanceContent] OFF;
    END

DROP TABLE [dbo].[RemittanceContent];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_RemittanceContent]', N'RemittanceContent';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_RemittanceContent]', N'PK_RemittanceContent', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creando [dbo].[CustomerReservationCancelReasonRel]...';


GO
CREATE TABLE [dbo].[CustomerReservationCancelReasonRel] (
    [ID]                    INT            IDENTITY (1, 1) NOT NULL,
    [CustomerReservationID] INT            NOT NULL,
    [ReasonChangeID]        INT            NULL,
    [Explanation]           NVARCHAR (MAX) NULL,
    [LastUpdated]           DATETIME       NOT NULL,
    [ModifiedBy]            NVARCHAR (256) NULL,
    [DBTimeStamp]           ROWVERSION     NOT NULL,
    CONSTRAINT [PK_CustomerReservationCancelReasonRel] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creando [dbo].[IB_IMQ_OfertasProveedores]...';


GO
CREATE TABLE [dbo].[IB_IMQ_OfertasProveedores] (
    [ID]                  INT             IDENTITY (1, 1) NOT NULL,
    [Descripcion]         NVARCHAR (256)  NULL,
    [Descuento]           DECIMAL (7, 4)  NULL,
    [TipoOferta]          NVARCHAR (50)   NULL,
    [UnidadesPedido]      INT             NULL,
    [UnidadesRegalo]      INT             NULL,
    [CantidadMinima]      INT             NULL,
    [ImporteMinimo]       DECIMAL (15, 4) NULL,
    [FechaInicioVigencia] DATETIME        NULL,
    [FechaFinVigencia]    DATETIME        NULL,
    [OfertaPrincipal]     BIT             NULL,
    [ItemID]              INT             NULL,
    [SupplierID]          INT             NULL,
    [LastUpdated]         DATETIME        NULL,
    [ModifiedBy]          NVARCHAR (256)  NULL,
    [DBTimeStamp]         ROWVERSION      NULL,
    CONSTRAINT [PK_IB.IMQ.OfertasProveedores] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creando [dbo].[IB_IMQ_OfertasProveedores].[IX_IB_IMQ_OfertasProveedores_SupplierID]...';


GO
CREATE NONCLUSTERED INDEX [IX_IB_IMQ_OfertasProveedores_SupplierID]
    ON [dbo].[IB_IMQ_OfertasProveedores]([SupplierID] ASC)
    INCLUDE([FechaInicioVigencia], [FechaFinVigencia]);


GO
PRINT N'Creando [dbo].[IB_IMQ_OfertasProveedoresAplicadas]...';


GO
CREATE TABLE [dbo].[IB_IMQ_OfertasProveedoresAplicadas] (
    [ID]                         INT             IDENTITY (1, 1) NOT NULL,
    [Descuento]                  DECIMAL (7, 4)  NULL,
    [TipoOferta]                 NVARCHAR (50)   NULL,
    [UnidadesPedido]             INT             NULL,
    [UnidadesRegalo]             INT             NULL,
    [CantidadMinima]             INT             NULL,
    [ImporteMinimo]              DECIMAL (15, 4) NULL,
    [FechaInicioVigencia]        DATETIME        NULL,
    [FechaFinVigencia]           DATETIME        NULL,
    [OfertaPrincipal]            BIT             NULL,
    [ItemID]                     INT             NULL,
    [SupplierID]                 INT             NULL,
    [SupplyReplenishmentEntryID] INT             NULL,
    [FechaCalculo]               DATETIME        NULL,
    [LastUpdated]                DATETIME        NULL,
    [ModifiedBy]                 NVARCHAR (256)  NULL,
    [DBTimeStamp]                ROWVERSION      NULL,
    [OfertaID]                   INT             NULL,
    CONSTRAINT [PK_IB_IMQ_OfertasProveedoresAplicadas] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creando [dbo].[IB_IMQ_OfertasProveedoresAplicadas].[IX_IB_IMQ_OfertasProveedoresAplicadas_SupplyReplenishmentEntryID]...';


GO
CREATE NONCLUSTERED INDEX [IX_IB_IMQ_OfertasProveedoresAplicadas_SupplyReplenishmentEntryID]
    ON [dbo].[IB_IMQ_OfertasProveedoresAplicadas]([SupplyReplenishmentEntryID] ASC);


GO
PRINT N'Creando [dbo].[RemittanceConfigInvoiceAgreeRel]...';


GO
CREATE TABLE [dbo].[RemittanceConfigInvoiceAgreeRel] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [RemittanceConfigID] INT            NOT NULL,
    [GuarantorType]      SMALLINT       NOT NULL,
    [InvoiceAgreementID] INT            NOT NULL,
    [LastUpdated]        DATETIME       NOT NULL,
    [ModifiedBy]         NVARCHAR (256) NOT NULL,
    [DBTimeStamp]        ROWVERSION     NOT NULL,
    CONSTRAINT [PK_RemittanceConfigInvoiceAgreeRel] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creando [dbo].[CustomerOrderRealization].[IX_CustomerOrderRealization_CustomerOrderRequestID]...';


GO
CREATE NONCLUSTERED INDEX [IX_CustomerOrderRealization_CustomerOrderRequestID]
    ON [dbo].[CustomerOrderRealization]([CustomerOrderRequestID] ASC);


GO
PRINT N'Creando [dbo].[CustomerPreAssessment].[IX_CustomerPreAssessment_CustomerProcessID]...';


GO
CREATE NONCLUSTERED INDEX [IX_CustomerPreAssessment_CustomerProcessID]
    ON [dbo].[CustomerPreAssessment]([CustomerProcessID] ASC);


GO
PRINT N'Creando [dbo].[CustomerPreAssessmentReasonRel].[IX_CustomerPreAssessmentReasonRel_CustomerPreAssessmentID]...';


GO
CREATE NONCLUSTERED INDEX [IX_CustomerPreAssessmentReasonRel_CustomerPreAssessmentID]
    ON [dbo].[CustomerPreAssessmentReasonRel]([CustomerPreAssessmentID] ASC);


GO
PRINT N'Creando [dbo].[CustomerProcedureTime].[IX_CustomerProcedureTime_CustomerProcedureID]...';


GO
CREATE NONCLUSTERED INDEX [IX_CustomerProcedureTime_CustomerProcedureID]
    ON [dbo].[CustomerProcedureTime]([CustomerProcedureID] ASC);


GO
PRINT N'Creando [dbo].[CustomerProcessStepsRel].[IX_CPSR_Step_CurrentStepID]...';


GO
CREATE NONCLUSTERED INDEX [IX_CPSR_Step_CurrentStepID]
    ON [dbo].[CustomerProcessStepsRel]([Step] ASC, [CurrentStepID] ASC)
    INCLUDE([CustomerProcessID]);


GO
PRINT N'Creando [dbo].[CustomerRoutineTime].[IX_CustomerRoutineTime_CustomerRoutineID]...';


GO
CREATE NONCLUSTERED INDEX [IX_CustomerRoutineTime_CustomerRoutineID]
    ON [dbo].[CustomerRoutineTime]([CustomerRoutineID] ASC);


GO
PRINT N'Creando [dbo].[IB_IMQ_OrderRealizationServiceWGRel].[IX_IB_IMQ_OrderRealizationServiceWGRel_ProcedureID]...';


GO
CREATE NONCLUSTERED INDEX [IX_IB_IMQ_OrderRealizationServiceWGRel_ProcedureID]
    ON [dbo].[IB_IMQ_OrderRealizationServiceWGRel]([ProcedureID] ASC);


GO
PRINT N'Creando [dbo].[IB_IMQ_OrderRequestServiceWGRel].[IX_IB_IMQ_OrderRequestServiceWGRel_CustomerOrderRequestID]...';


GO
CREATE NONCLUSTERED INDEX [IX_IB_IMQ_OrderRequestServiceWGRel_CustomerOrderRequestID]
    ON [dbo].[IB_IMQ_OrderRequestServiceWGRel]([CustomerOrderRequestID] ASC);


GO
PRINT N'Creando [dbo].[OrderRequestCustomerProcedureRel].[IX_OrderRequestCustomerProcedureRel_CustomerProcedureID]...';


GO
CREATE NONCLUSTERED INDEX [IX_OrderRequestCustomerProcedureRel_CustomerProcedureID]
    ON [dbo].[OrderRequestCustomerProcedureRel]([CustomerProcedureID] ASC)
    INCLUDE([OrderRequestSchPlanningID]);


GO
PRINT N'Creando [dbo].[OrderRequestCustomerRoutineRel].[IX_OrderRequestCustomerRoutineRel_CustomerRoutineID]...';


GO
CREATE NONCLUSTERED INDEX [IX_OrderRequestCustomerRoutineRel_CustomerRoutineID]
    ON [dbo].[OrderRequestCustomerRoutineRel]([CustomerRoutineID] ASC)
    INCLUDE([OrderRequestSchPlanningID]);


GO
PRINT N'Creando [dbo].[OrderRequestProcedureRoutineRel].[IX_OrderRequestProcedureRoutineRel_OrderRequestProcedureRelID]...';


GO
CREATE NONCLUSTERED INDEX [IX_OrderRequestProcedureRoutineRel_OrderRequestProcedureRelID]
    ON [dbo].[OrderRequestProcedureRoutineRel]([OrderRequestProcedureRelID] ASC);


GO
PRINT N'Creando [dbo].[PrescriptionRequest].[IX_PrescriptionRequest_CustomerOrderRequestID]...';


GO
CREATE NONCLUSTERED INDEX [IX_PrescriptionRequest_CustomerOrderRequestID]
    ON [dbo].[PrescriptionRequest]([CustomerOrderRequestID] ASC);


GO
PRINT N'Creando [dbo].[ProcedureAct].[IX_ProcedureAct_EpisodeID]...';


GO
CREATE NONCLUSTERED INDEX [IX_ProcedureAct_EpisodeID]
    ON [dbo].[ProcedureAct]([EpisodeID] ASC);


GO
PRINT N'Creando [dbo].[ProcedureActRoutineActRel].[IX_ProcedureActRoutineActRel_RoutineActID]...';


GO
CREATE NONCLUSTERED INDEX [IX_ProcedureActRoutineActRel_RoutineActID]
    ON [dbo].[ProcedureActRoutineActRel]([RoutineActID] ASC)
    INCLUDE([ID], [ProcedureActID]);


GO
PRINT N'Creando [dbo].[RealizationAppointmentService].[IX_RealizationAppointmentService_AppElement_ElementID]...';


GO
CREATE NONCLUSTERED INDEX [IX_RealizationAppointmentService_AppElement_ElementID]
    ON [dbo].[RealizationAppointmentService]([AppointmentElement] ASC, [ElementID] ASC)
    INCLUDE([ID]);


GO
PRINT N'Creando [dbo].[ReservedProcedureAct].[IX_ReservedProcedureAct_CustomerReservationID]...';


GO
CREATE NONCLUSTERED INDEX [IX_ReservedProcedureAct_CustomerReservationID]
    ON [dbo].[ReservedProcedureAct]([CustomerReservationID] ASC);


GO
PRINT N'Creando [dbo].[ReservedProcedureAct].[IX_ReservedProcedureAct_Element_ElementID]...';


GO
CREATE NONCLUSTERED INDEX [IX_ReservedProcedureAct_Element_ElementID]
    ON [dbo].[ReservedProcedureAct]([Element] ASC, [ElementID] ASC);


GO
PRINT N'Creando [dbo].[ReservedRoutineAct].[IX_ReservedRoutineAct_CustomerReservationID]...';


GO
CREATE NONCLUSTERED INDEX [IX_ReservedRoutineAct_CustomerReservationID]
    ON [dbo].[ReservedRoutineAct]([CustomerReservationID] ASC);


GO
PRINT N'Creando [dbo].[ReservedRoutineAct].[IX_ReservedRoutineAct_Element_ElementID]...';


GO
CREATE NONCLUSTERED INDEX [IX_ReservedRoutineAct_Element_ElementID]
    ON [dbo].[ReservedRoutineAct]([Element] ASC, [ElementID] ASC);


GO
PRINT N'Creando DF_Person_ImageID...';


GO
ALTER TABLE [dbo].[Person]
    ADD CONSTRAINT [DF_Person_ImageID] DEFAULT ((0)) FOR [ImageID];


GO
PRINT N'Creando FK_LocationAvailabilityReasonRel_LocationAvailability...';


GO
ALTER TABLE [dbo].[LocationAvailabilityReasonRel] WITH NOCHECK
    ADD CONSTRAINT [FK_LocationAvailabilityReasonRel_LocationAvailability] FOREIGN KEY ([LocationAvailabilityID]) REFERENCES [dbo].[LocationAvailability] ([ID]);


GO
PRINT N'Creando FK_LocationAvailabilityReasonRel_ReasonChange...';


GO
ALTER TABLE [dbo].[LocationAvailabilityReasonRel] WITH NOCHECK
    ADD CONSTRAINT [FK_LocationAvailabilityReasonRel_ReasonChange] FOREIGN KEY ([ReasonChangeID]) REFERENCES [dbo].[ReasonChange] ([ID]);


GO
PRINT N'Comprobando los datos existentes con las restricciones recién creadas';


GO
USE [HCDIS_Desarrollo];


GO
ALTER TABLE [dbo].[LocationAvailabilityReasonRel] WITH CHECK CHECK CONSTRAINT [FK_LocationAvailabilityReasonRel_LocationAvailability];

ALTER TABLE [dbo].[LocationAvailabilityReasonRel] WITH CHECK CHECK CONSTRAINT [FK_LocationAvailabilityReasonRel_ReasonChange];


GO
PRINT N'Actualización completada.';


GO
