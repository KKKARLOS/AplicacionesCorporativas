USE HCDIS
--/////////////////////////////////////////////////////////////////////////
--//SCRIPT DE CONFIGURACIÓN PARA ASOCIAR EL PASO DE FIANZA AL PROCESO DE FISIOTERAPIA
--////////////////////////////////////////////////////////////////////////


DECLARE @ModifiedBy NVARCHAR(256)
DECLARE @LastUpdated DATETIME
DECLARE @FisioProcessChartID INT
DECLARE @InputBatchReasonID INT
DECLARE @OutputBatchReasonID INT
DECLARE @GuaranteeConfigID INT
SET @ModifiedBy = 'Administrador'
SET @LastUpdated = GETDATE()
SET @FisioProcessChartID = ISNULL((SELECT ID FROM ProcessChart WHERE Name = 'FISIOTERAPIA'),0)
SET @InputBatchReasonID = ISNULL((SELECT ID FROM BatchMovementReason WHERE Reason = 'Anticipo'),0)
SET @OutputBatchReasonID = ISNULL((SELECT ID FROM BatchMovementReason WHERE Reason = 'Devolución'),0)
IF (@InputBatchReasonID =0)
BEGIN
	INSERT INTO [BatchMovementReason]
		([Code],[Reason],[BatchMovementReasonType],[Status],[LastUpdated],[ModifiedBy])
    VALUES ('06','Anticipo',1,1,@LastUpdated,@ModifiedBy)
	SET @InputBatchReasonID = (SELECT @@IDENTITY)
END
IF (@OutputBatchReasonID =0)
BEGIN
	INSERT INTO [BatchMovementReason]
		([Code],[Reason],[BatchMovementReasonType],[Status],[LastUpdated],[ModifiedBy])
    VALUES ('07','Devolución',2,1,@LastUpdated,@ModifiedBy)
	SET @OutputBatchReasonID = (SELECT @@IDENTITY)
END

IF NOT(@FisioProcessChartID=0)
AND NOT(EXISTS(SELECT ID FROM [BasicStepsInProcess] 
		WHERE [ProcessChartID] = @FisioProcessChartID AND [ProcessStep]=32))
BEGIN
	INSERT INTO [GuaranteeConfig]
		([ProcessChartID],[GuaranteeQuantity],[AdmitMultipleDeposit],[GuaranteeRequiredInvoice],
		[PaymentCalculation],[InputBatchReasonID],[OutputBatchReasonID],[SourceType],
		[GuaranteeReceipt],[LastUpdated],[ModifiedBy])
    VALUES
		(@FisioProcessChartID,0,1,1,3,@InputBatchReasonID,@OutputBatchReasonID,0,
		'ReceiptFormat.rdlc',@LastUpdated,@ModifiedBy)
	SET @GuaranteeConfigID = (SELECT @@IDENTITY)
	UPDATE [BasicStepsInProcess] SET [Position] = [Position]+1
	WHERE [Position] > 1 AND [ProcessChartID] = @FisioProcessChartID
	
	INSERT INTO [BasicStepsInProcess]
		([ProcessChartID],[ProcessStep],[Position],[StepRequired],
		[StepVisibleInProcessList],[StepAllowExecution],[LastUpdated],[ModifiedBy])
	VALUES
		(@FisioProcessChartID,32,2,0,1,1,@LastUpdated,@ModifiedBy)
END
ELSE
BEGIN
	PRINT 'El proceso de "Fisioterapia" no existe o no está registrado con ese nombre'
END