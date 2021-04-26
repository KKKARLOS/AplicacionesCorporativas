USE [DBName]
GO
ALTER TABLE [dbo].[InsurerInvoiceAgreement] 
 ADD [InvoiceAgreementBaseID] INT CONSTRAINT [DF_InsurerInvoiceAgreement_InvoiceAgreementBaseID] 
 DEFAULT ((0)) NOT NULL;

ALTER TABLE [dbo].[InvoiceAgreement] 
 ADD [InvoiceAgreementBaseID] INT CONSTRAINT [DF_InvoiceAgreement_InvoiceAgreementBaseID] 
 DEFAULT ((0)) NOT NULL;
GO


DECLARE @myInvoiceAgreementBase TABLE([ID] INT IDENTITY, 
IABaseID INT, 
InvoiceAgreementID INT,  
[InvoiceConfigID] INT,
[Name]               NVARCHAR (100),
[Description]        NVARCHAR (200),
[AccountInformation] NVARCHAR (200),
[AccountNumber]      NVARCHAR (20),
[GoesDate]           DATETIME,
[ExpirationDate]     DATETIME,
[InvSendAddressID]   INT,
[InvAddressType]     SMALLINT,
[PaymentMethod]      SMALLINT,
[InvoiceMode]        SMALLINT,
[BillingDayID]       INT,
[RemittanceDayID]    INT,
[PaymentDueID]       INT,
[AncestorID]         INT,
[Status]             SMALLINT,
[LastUpdated]        DATETIME,
[ModifiedBy]         NVARCHAR (256))

DECLARE @myInsurerInvoiceAgreementBase TABLE([ID] INT IDENTITY, 
IABaseID INT, 
InsurerInvoiceAgreementID INT,
InsurerID INT,
[Name]               NVARCHAR (100),
[Description]        NVARCHAR (200),
[AccountInformation] NVARCHAR (200),
[AccountNumber]      NVARCHAR (20),
[GoesDate]           DATETIME,
[ExpirationDate]     DATETIME,
[InvSendAddressID]   INT,
[InvAddressType]     SMALLINT,
[PaymentMethod]      SMALLINT,
[InvoiceMode]        SMALLINT,
[BillingDayID]       INT,
[RemittanceDayID]    INT,
[PaymentDueID]       INT,
[AncestorID]         INT,
[Status]             SMALLINT,
[LastUpdated]        DATETIME,
[ModifiedBy]         NVARCHAR (256))

INSERT INTO @myInvoiceAgreementBase
(InvoiceAgreementID, [InvoiceConfigID], [Name],[Description],[AccountInformation],[AccountNumber],[GoesDate],
[ExpirationDate],[InvSendAddressID],[InvAddressType],[PaymentMethod],[InvoiceMode],[BillingDayID],[RemittanceDayID],
[PaymentDueID],[AncestorID],[Status],[LastUpdated],[ModifiedBy])
SELECT [ID], [InvoiceConfigID], Name,[Description],AccountInformation,AccountNumber,GoesDate,
ExpirationDate,InvSendAddressID,InvAddressType,PaymentMethod,InvoiceMode,
BillingDayID,RemittanceDayID,PaymentDueID,AncestorID,[Status],LastUpdated,ModifiedBy
FROM InvoiceAgreement

INSERT INTO @myInsurerInvoiceAgreementBase
(InsurerInvoiceAgreementID, InsurerID, [Name],[Description],[AccountInformation],[AccountNumber],[GoesDate],
[ExpirationDate],[InvSendAddressID],[InvAddressType],[PaymentMethod],[InvoiceMode],[BillingDayID],[RemittanceDayID],
[PaymentDueID],[AncestorID],[Status],[LastUpdated],[ModifiedBy])
SELECT [ID], InsurerID, Name,[Description],AccountInformation,AccountNumber,GoesDate,
ExpirationDate,InvSendAddressID,InvAddressType,PaymentMethod,InvoiceMode,
BillingDayID,RemittanceDayID,PaymentDueID,AncestorID,[Status],LastUpdated,ModifiedBy
FROM InsurerInvoiceAgreement

CREATE TABLE [InvoiceAgreementBase] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (100) NOT NULL,
    [Description]        NVARCHAR (200) NULL,
    [AccountInformation] NVARCHAR (200) NULL,
    [AccountNumber]      NVARCHAR (20)  NULL,
    [GoesDate]           DATETIME       NOT NULL,
    [ExpirationDate]     DATETIME       NULL,
    [InvSendAddressID]   INT            NULL,
    [InvAddressType]     SMALLINT       NOT NULL,
    [PaymentMethod]      SMALLINT       NOT NULL,
    [InvoiceMode]        SMALLINT       NOT NULL,
    [BillingDayID]       INT            NOT NULL,
    [RemittanceDayID]    INT            NULL,
    [PaymentDueID]       INT            NOT NULL,
    [LastUpdated]        DATETIME       NOT NULL,
    [ModifiedBy]         NVARCHAR (256) NOT NULL,
    [DBTimeStamp]        TIMESTAMP      NOT NULL
);
ALTER TABLE [InvoiceAgreementBase]
    ADD CONSTRAINT [PK_InvoiceAgreementBase] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

DECLARE @myID INT
DECLARE @oldID INT
DECLARE @IAB INT
DECLARE @InvoiceAgreementID INT
DECLARE @InsurerInvoiceAgreementID INT
SET @myID=0
SET @oldID=-1
WHILE (@myID>@oldID)
BEGIN
	SET @oldID=@myID
	SET @myID=ISNULL((SELECT TOP 1 [ID] FROM @myInvoiceAgreementBase WHERE [ID]>@oldID),0)
	IF (@myID>@oldID)
	BEGIN
		INSERT INTO InvoiceAgreementBase ([Name],[Description],[AccountInformation],[AccountNumber],[GoesDate],
		[ExpirationDate],[InvSendAddressID],[InvAddressType],[PaymentMethod],[InvoiceMode],[BillingDayID],
		[RemittanceDayID],[PaymentDueID],[LastUpdated],[ModifiedBy])
		SELECT TOP 1 IIA.Name,IIA.[Description],IIA.AccountInformation,IIA.AccountNumber,IIA.GoesDate,
		IIA.ExpirationDate,IIA.InvSendAddressID,IIA.InvAddressType,IIA.PaymentMethod,IIA.InvoiceMode,
		IIA.BillingDayID,IIA.RemittanceDayID,IIA.PaymentDueID,IIA.LastUpdated,IIA.ModifiedBy
		FROM @myInvoiceAgreementBase IIA WHERE IIA.[ID]=@myID
		SET @IAB = (SELECT @@IDENTITY)
		--UPDATE @myInvoiceAgreementBase SET IABaseID=@IAB WHERE [ID]=@myID
		SET @InvoiceAgreementID=ISNULL((SELECT TOP 1 InvoiceAgreementID FROM @myInvoiceAgreementBase WHERE [ID]=@myID),0)
		UPDATE InvoiceAgreement SET [InvoiceAgreementBaseID]=@IAB WHERE [ID]=@InvoiceAgreementID
	END
END
SET @myID=0
SET @oldID=-1
WHILE (@myID>@oldID)
BEGIN
	SET @oldID=@myID
	SET @myID=ISNULL((SELECT TOP 1 [ID] FROM @myInsurerInvoiceAgreementBase WHERE [ID]>@oldID),0)
	IF (@myID>@oldID)
	BEGIN
		INSERT INTO InvoiceAgreementBase ([Name],[Description],[AccountInformation],[AccountNumber],[GoesDate],
		[ExpirationDate],[InvSendAddressID],[InvAddressType],[PaymentMethod],[InvoiceMode],[BillingDayID],
		[RemittanceDayID],[PaymentDueID],[LastUpdated],[ModifiedBy])
		SELECT TOP 1 IIA.Name,IIA.[Description],IIA.AccountInformation,IIA.AccountNumber,IIA.GoesDate,
		IIA.ExpirationDate,IIA.InvSendAddressID,IIA.InvAddressType,IIA.PaymentMethod,IIA.InvoiceMode,
		IIA.BillingDayID,IIA.RemittanceDayID,IIA.PaymentDueID,IIA.LastUpdated,IIA.ModifiedBy
		FROM @myInsurerInvoiceAgreementBase IIA WHERE IIA.[ID]=@myID
		SET @IAB = (SELECT @@IDENTITY)
		--UPDATE @myInsurerInvoiceAgreementBase SET IABaseID=@IAB WHERE [ID]=@myID
		SET @InsurerInvoiceAgreementID=ISNULL((SELECT TOP 1 InsurerInvoiceAgreementID FROM @myInsurerInvoiceAgreementBase WHERE [ID]=@myID),0)
		UPDATE InsurerInvoiceAgreement SET [InvoiceAgreementBaseID]=@IAB WHERE [ID]=@InsurerInvoiceAgreementID
	END
END

ALTER TABLE [InsurerInvoiceAgreement] WITH NOCHECK
    ADD CONSTRAINT [FK_InsurerInvoiceAgreement_InvoiceAgreementBase] FOREIGN KEY ([InvoiceAgreementBaseID]) REFERENCES [InvoiceAgreementBase] ([ID]) ON DELETE CASCADE ON UPDATE NO ACTION;

ALTER TABLE [InvoiceAgreement] WITH NOCHECK
    ADD CONSTRAINT [FK_InvoiceAgreement_InvoiceAgreementBase] FOREIGN KEY ([InvoiceAgreementBaseID]) REFERENCES [InvoiceAgreementBase] ([ID]) ON DELETE CASCADE ON UPDATE NO ACTION;

ALTER TABLE [InsurerInvoiceAgreement] WITH CHECK CHECK CONSTRAINT [FK_InsurerInvoiceAgreement_InvoiceAgreementBase];

ALTER TABLE [InvoiceAgreement] WITH CHECK CHECK CONSTRAINT [FK_InvoiceAgreement_InvoiceAgreementBase];

