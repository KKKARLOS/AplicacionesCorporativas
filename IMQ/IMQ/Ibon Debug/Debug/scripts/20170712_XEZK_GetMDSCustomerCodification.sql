
/****** Object:  StoredProcedure [dbo].[GetMDSCustomerCodification]    Script Date: 10/07/2017 12:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetMDSCustomerCodification]
	@TransferReason nvarchar(8),
	@AssistanceService int,
	@StartDateTime datetime,
	@EndDateTime datetime,

	@Step int,
	@Status int,
	@CodificationStatus int,

	@TVPProcessChartIDs TVPInteger readonly,
	@TVPLocations		TVPInteger readonly,
	@TVPCareCenterIDs	TVPInteger readonly

	
AS
BEGIN

	--BasicProcessStepsEnum
	DECLARE @None int = 0,
			@Contact int = 1,               
			@Visit int = 2,                 
			@WaitingList int = 4,           
			@Budget int = 8,                
			@Contract int = 16,             
			@Guarantee int = 32,            
			@Reservation int = 64,          
			@Citation int = 128,            
			@Reception int = 256,           
			@Interview int = 512,           
			@PreAssessment int = 1024,     	
			@Admission int = 2048,          
			@TemporalLeave int = 4096,      
			@Transfer int = 8192,           
			@Leave int = 16384,             
			@Realization int = 32768,       
			@Reports int = 65536,           
			@DeliveryResults int = 131072,  
			@Charges int = 262144,          
			@CoverAnalysis int = 524288,    
			@DeliveryNote int = 1048576,    
			@Invoice int = 2097152,         
			@Remittance int = 4194304,      
			@Codification int = 8388608

	--StatusEnum
	DECLARE --@None int = 0,
			@Active int = 1,
			@Cancelled int = 2,
			@Confirmed int = 3,
			@Closed int = 4,
			@Pending int = 5,
			@Superceded int = 6,
			@Completed int = 7,
			@Held int = 8,
			@RealizadaEn int = 9

	--MixedCodificationStatusEnum
	DECLARE @CodificationStatusNone int = 0,
			@CodificationStatusPending int = 1,
			@CodificationStatusConfirmed int = 2


    DECLARE @sSql nvarchar(max)
    DECLARE @sWhereClause nvarchar(4000)
    DECLARE @ParmDefinition nvarchar(4000)
    DECLARE @NewLine nvarchar(4000)
    SET @NewLine = CHAR(13) + CHAR(10)

    SET @sWhereClause = '' 


	SET @sSql = 'SELECT DISTINCT' + @NewLine
	+ 'CP.[ID] CustomerProcessID,CP.[ProcessChartID], CP.[CareCenterID], CODCPSR.CurrentStepID CustomerCodificationID,' + @NewLine
	+ 'CP.[CustomerEpisodeID], LEVCPSR.CurrentStepID CustomerLeaveID,CP.[CustomerID], CP.[PersonID], OCC.[Name] CareCenterName,' + @NewLine
	+ 'PC.[Name] ProcessChartName,ET.[Description] EpisodeTypeName,ISNULL(CRCH.CHNumber,C.CHNumber) CHNumber,' + @NewLine
	+ 'SD.Sex, P.FirstName,P.LastName,P.LastName2,SD.BirthDate,SD.DeathDateTime,' + @NewLine
	+ 'CE.EpisodeNumber CustomerEpisodeNumber,CE.StartDateTime CustomerEpisodeDate,' + @NewLine
	+ 'ISNULL((SELECT TOP 1 ER.FullySpecifiedName' + @NewLine
 	+ '	FROM CustomerEpisodeReasonRel CERR WITH(NOLOCK)' + @NewLine
 	+ '	JOIN EpisodeReason ER WITH(NOLOCK) ON CERR.EpisodeReasonID = ER.[ID]' + @NewLine
 	+ '	WHERE CERR.CustomerEpisodeID=CE.[ID]),'''') EpisodeReason,' + @NewLine
	+ 'ISNULL(ASS.[Name],'''') AssistanceService,' + @NewLine
	+ 'ISNULL(CE.Origin,'''') Origin,' + @NewLine
	+ 'CE.EndDateTime CustomerEpisodeEndDate,' + @NewLine
	+ 'ISNULL((SELECT TOP 1 ER.FullySpecifiedName' + @NewLine
 	+ '	FROM CustomerEpisodeLeaveReasonRel CELRR WITH(NOLOCK)' + @NewLine
 	+ '	JOIN EpisodeReason ER WITH(NOLOCK) ON CELRR.EpisodeReasonID = ER.[ID]' + @NewLine
 	+ '	JOIN EpisodeReasonType ERT WITH(NOLOCK) ON ER.EpisodeReasonTypeID = ERT.[ID]' + @NewLine
 	+ '	WHERE CELRR.CustomerEpisodeID=CE.[ID] AND NOT(ERT.[AssignedCode]=@TransferReason)),'''') EpisodeLeaveReason,' + @NewLine
	+ 'ISNULL((SELECT TOP 1 ASS.[Name] FROM CustomerEpisodeServiceRel CESR1 WITH(NOLOCK)' + @NewLine
 	+ '		JOIN AssistanceService ASS WITH(NOLOCK) ON  CESR1.AssistanceServiceID = ASS.ID' + @NewLine
 	+ '		WHERE CE.ID = CESR1.CustomerEpisodeID AND CESR1.Step =@Leave),'''') DischargeAssistanceService,' + @NewLine
	+ 'ISNULL((SELECT TOP 1 ER.FullySpecifiedName' + @NewLine
 	+ '	FROM CustomerEpisodeLeaveReasonRel CELRR WITH(NOLOCK)' + @NewLine
 	+ '	JOIN EpisodeReason ER WITH(NOLOCK) ON CELRR.EpisodeReasonID = ER.[ID]' + @NewLine
 	+ '	JOIN EpisodeReasonType ERT WITH(NOLOCK) ON ER.EpisodeReasonTypeID = ERT.[ID]' + @NewLine
 	+ '	WHERE CELRR.CustomerEpisodeID=CE.[ID] AND ERT.[AssignedCode]=@TransferReason),'''') DischargeFacilityName,' + @NewLine
	+ 'ISNULL(CCOD.[Status],0) CustomerCodificationStatus,' + @NewLine
	+ 'ISNULL((SELECT TOP 1 D.AssignedCode+'' - ''+D.FullySpecifiedName' + @NewLine
 	
	SET @sSql = @sSql 
	+ '	   FROM CodedDiagnosis CD WITH(NOLOCK)' + @NewLine
 	+ '	   JOIN DiagnosisCIE10 D WITH(NOLOCK) ON CD.DiagnosisID = D.[ID]' + @NewLine
 	+ '	   WHERE CD.CustomerCodificationID = CCOD.ID AND CD.PrimaryDiagnosis = 1),'''') PrimaryDiagnosis,' + @NewLine
	+ 'CAST((CASE WHEN EXISTS(SELECT CD.[ID]' + @NewLine
 	+ '	   FROM CodedDiagnosis CD WITH(NOLOCK)' + @NewLine
 	+ '	   WHERE CD.CustomerCodificationID = CCOD.ID AND NOT(CD.PrimaryDiagnosis = 1))' + @NewLine
 	+ '	   THEN 1 ELSE 0 END) AS BIT) HasOtherDiagnosis,' + @NewLine
	+ 'CAST((CASE WHEN EXISTS(SELECT CD.[ID]' + @NewLine
 	+ '	   FROM CodedDiagnosis CD WITH(NOLOCK)' + @NewLine
 	+ '	   WHERE CD.CustomerCodificationID = CCOD.ID AND NOT(CD.PresentOnAdmission = 2))' + @NewLine
 	+ '	   THEN 1 ELSE 0 END) AS BIT) IncludePreviousEpisodes,' + @NewLine
	+ 'CAST((CASE WHEN EXISTS(SELECT CM.[ID]' + @NewLine
 	+ '	   FROM CodedMorphology CM WITH(NOLOCK)' + @NewLine
 	+ '	   WHERE CM.CustomerCodificationID = CCOD.ID)' + @NewLine
 	+ '	   THEN 1 ELSE 0 END) AS BIT) HasMorphologies,' + @NewLine
	+ 'CAST((CASE WHEN EXISTS(SELECT CPR.[ID]' + @NewLine
 	+ '	   FROM CodedProcedure CPR WITH(NOLOCK)' + @NewLine
 	+ '	   JOIN EpisodeProcedureCIE10 EPR  WITH(NOLOCK) ON CPR.EpisodeProcedureID = EPR.[ID]' + @NewLine
 	+ '	   WHERE CPR.CustomerCodificationID = CCOD.ID AND EPR.ProcedureClass=1)' + @NewLine
 	+ '	   THEN 1 ELSE 0 END) AS BIT) HasSurgicalProcedures,' + @NewLine
	+ 'CAST((CASE WHEN EXISTS(SELECT CPR.[ID]' + @NewLine
 	+ '	   FROM CodedProcedure CPR WITH(NOLOCK)' + @NewLine
 	+ '	   JOIN EpisodeProcedureCIE10 EPR  WITH(NOLOCK) ON CPR.EpisodeProcedureID = EPR.[ID]' + @NewLine
 	+ '	   WHERE CPR.CustomerCodificationID = CCOD.ID AND EPR.ProcedureClass=2)' + @NewLine
 	+ '	   THEN 1 ELSE 0 END) AS BIT) HasObstetricProcedures,' + @NewLine
	+ 'CAST((CASE WHEN EXISTS(SELECT CPR.[ID]' + @NewLine
 	+ '	   FROM CodedProcedure CPR WITH(NOLOCK)' + @NewLine
 	+ '	   JOIN EpisodeProcedureCIE10 EPR  WITH(NOLOCK) ON CPR.EpisodeProcedureID = EPR.[ID]' + @NewLine
 	+ '	   WHERE CPR.CustomerCodificationID = CCOD.ID AND EPR.ProcedureClass> 2)' + @NewLine
 	+ '	   THEN 1 ELSE 0 END) AS BIT) HasOtherProcedures,' + @NewLine
	
	
	+ 'ISNULL((SELECT TOP 1 MDC.AssignedCode+'' - ''+MDC.FullySpecifiedName' + @NewLine
 	+ '	   FROM CodedMDC CMDC WITH(NOLOCK)' + @NewLine
 	+ '	   JOIN MDC WITH(NOLOCK) ON CMDC.MDCID = MDC.[ID]' + @NewLine
 	+ '	   WHERE CMDC.CustomerCodificationID = CCOD.ID AND CMDC.PrimaryMDC = 1),'''') PrimaryMDC,' + @NewLine
	+ 'CAST((CASE WHEN EXISTS(SELECT CMDC.[ID]' + @NewLine
 	+ '	   FROM CodedMDC CMDC WITH(NOLOCK)' + @NewLine
 	+ '	   WHERE CMDC.CustomerCodificationID = CCOD.ID AND CMDC.PrimaryMDC <> 1)' + @NewLine
 	+ '	   THEN 1 ELSE 0 END) AS BIT) HasOtherMDCs,' + @NewLine
	+ 'ISNULL((SELECT TOP 1 DRG.AssignedCode+'' - ''+DRG.FullySpecifiedName' + @NewLine
 	+ '	   FROM CodedDRG CDRG WITH(NOLOCK)' + @NewLine
 	+ '	   JOIN DRG WITH(NOLOCK) ON CDRG.DRGID = DRG.[ID]' + @NewLine
 	+ '	   WHERE CDRG.CustomerCodificationID = CCOD.ID AND CDRG.PrimaryDRG = 1),'''') PrimaryDRG,' + @NewLine
	+ 'CAST((CASE WHEN EXISTS(SELECT CDRG.[ID]' + @NewLine
 	+ '	   FROM CodedDRG CDRG WITH(NOLOCK)' + @NewLine
 	+ '	   WHERE CDRG.CustomerCodificationID = CCOD.ID AND CDRG.PrimaryDRG <> 1)' + @NewLine
 	+ '	   THEN 1 ELSE 0 END) AS BIT) HasOtherDRGs,' + @NewLine
	

	SET @sSql = @sSql 
	+ 'ISNULL(CCOD.RelatedWeight,0) PrimaryDRGWeight,' + @NewLine
	+ 'ISNULL(CCOD.RelatedCost,0) RelatedCost,' + @NewLine
	+ 'ISNULL(CCOD.Explanation,'''') Explanation,' + @NewLine
	+ 'CAST((CASE WHEN CCOD.[Status] = 3 THEN 1 ELSE 0 END) AS BIT) CodifiedConfirm,' + @NewLine
	+ 'CAST((CASE WHEN LEVCPSR.CurrentStepID > 0 THEN 1 ELSE 0 END) AS BIT) Leave,' + @NewLine
	+ 'CAST(ISNULL(CCOD.Exported,0) AS BIT) Exported,' + @NewLine
	+ 'CCOD.ExportedDateTime, CCOD.RegistrationDateTime, CCOD.ConfirmedDate ConfirmedDateTime' + @NewLine
	+ 'FROM CustomerProcess CP WITH(NOLOCK)' + @NewLine
	+ 'JOIN CustomerProcessStepsRel CPSR WITH(NOLOCK) ON CP.[ID] = CPSR.CustomerProcessID' + @NewLine
	+ 'JOIN ProcessChart PC WITH(NOLOCK) ON CP.ProcessChartID = PC.[ID]' + @NewLine
	+ 'JOIN EpisodeType ET WITH(NOLOCK) ON PC.EpisodeConfigID=ET.[ID]' + @NewLine
	+ 'JOIN CareCenter CC WITH(NOLOCK) ON CP.CareCenterID = CC.[ID]' + @NewLine
	+ 'JOIN Organization OCC WITH(NOLOCK) ON CC.OrganizationID = OCC.[ID]' + @NewLine
	+ 'JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.CustomerEpisodeID = CE.ID' + @NewLine
	+ 'JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID = CA.ID' + @NewLine
	+ 'JOIN CustomerProcessStepsRel CODCPSR WITH(NOLOCK) ON CP.[ID] = CODCPSR.CustomerProcessID' + @NewLine
	+ 'JOIN CustomerProcessStepsRel LEVCPSR WITH(NOLOCK) ON CP.[ID] = LEVCPSR.CustomerProcessID' + @NewLine
	+ 'JOIN Customer C WITH(NOLOCK) ON CP.CustomerID = C.[ID]' + @NewLine
	+ 'JOIN Person P WITH(NOLOCK) ON CP.PersonID = P.[ID]' + @NewLine
	+ 'JOIN SensitiveData SD WITH(NOLOCK) ON P.[ID] = SD.PersonID' + @NewLine
	+ 'LEFT JOIN CustomerRelatedCHNumber CRCH WITH(NOLOCK) ON CP.CustomerID = CRCH.CustomerID AND CP.CareCenterID = CRCH.CareCenterID' + @NewLine
	+ 'LEFT JOIN CustomerCodification CCOD WITH(NOLOCK) ON CODCPSR.CurrentStepID = CCOD.ID' + @NewLine 
	+ 'LEFT JOIN CustomerEpisodeServiceRel CESR WITH(NOLOCK) ON CE.ID = CESR.CustomerEpisodeID AND CESR.Step IN (@Reception,@Admission)' + @NewLine
	+ 'LEFT JOIN AssistanceService ASS WITH(NOLOCK) ON CESR.AssistanceServiceID = ASS.ID' + @NewLine

	IF EXISTS (SELECT 1 FROM @TVPProcessChartIDs)
	BEGIN
		SET @sSql = @sSql + 'JOIN @TVPProcessChartIDs TVPPC ON PC.ID = TVPPC.[ID]' + @NewLine
	END

	IF EXISTS (SELECT 1 FROM @TVPLocations)
	BEGIN
		SET @sSql = @sSql + 'JOIN @TVPLocations TVPLOC ON CA.CurrentLocationID = TVPLOC.[ID]' + @NewLine
	END

	IF EXISTS (SELECT 1 FROM @TVPCareCenterIDs)
	BEGIN
		SET @sSql = @sSql + 'JOIN @TVPCareCenterIDs TVPCC ON CP.CareCenterID = TVPCC.[ID]' + @NewLine
	END

	SET @sSql = @sSql + '	WHERE CPSR.Step IN (@Reception,@Admission) AND CODCPSR.Step = @Codification AND LEVCPSR.Step = @Leave'


    if (@Step = @Admission OR @Step = @Reception)
    begin
        if (@Status <> @None)
			SET @sWhereClause = @sWhereClause + @NewLine + '   AND CPSR.StepStatus = @Status'
    end

    --en este caso no incluyo el customercodification
    if (@Step = @Leave)
    begin
        --primero analizo si tengo que poner mas joins
        SET @sWhereClause = @sWhereClause + @NewLine + '   AND LEVCPSR.CurrentStepID > @None'

        if (@Status <> @None)
            SET @sWhereClause = @sWhereClause + @NewLine + ' AND LEVCPSR.StepStatus = @Status'
    end


    --filtrar o no por el customercodificacion dependerá del atributo codificationStatus
    if (@CodificationStatus = @CodificationStatusPending) 
	begin
		SET @sWhereClause = @sWhereClause + @NewLine + ' AND CODCPSR.CurrentStepID > @None'
		SET @sWhereClause = @sWhereClause + @NewLine + ' AND CODCPSR.StepStatus in (@Active, @Pending)'
	end
	else if (@CodificationStatus = @CodificationStatusConfirmed) 
	begin
		SET @sWhereClause = @sWhereClause + @NewLine + ' AND CODCPSR.CurrentStepID > @None'
		SET @sWhereClause = @sWhereClause + @NewLine + ' AND CODCPSR.StepStatus = @Confirmed'
	end
	else
	begin
		SET @sWhereClause = @sWhereClause + @NewLine + ' AND CODCPSR.CurrentStepID <= @None'
	end
    
    if (@AssistanceService > 0)
    begin
        SET @sWhereClause = @sWhereClause + @NewLine + ' AND CESR.AssistanceServiceID = @AssistanceService'
    end

    if (@StartDateTime IS NOT NULL)
    begin
        if (@Step = @None)
        begin
            if (@CodificationStatus <> @CodificationStatusNone)
            begin
                if (@CodificationStatus = @CodificationStatusPending)
                begin
                    SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CODCPSR.StepDateTime IS NULL OR CODCPSR.StepDateTime >= @StartDateTime)'
				end
                else if (@CodificationStatus = @CodificationStatusConfirmed)
				begin    
					SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CCOD.ConfirmedDate IS NULL OR CCOD.ConfirmedDate >= @StartDateTime)'
				end
            end
            else
            begin
                SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CP.CloseDateTime IS NULL OR CP.CloseDateTime >= @StartDateTime)'
            end
        end
        else
        begin
            if (@Step = @Admission OR @Step = @Reception)
            begin
                SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CE.StartDateTime IS NULL OR CE.StartDateTime >= @StartDateTime)'
            end
            else
            begin
                if (@Step = @Codification)
                begin
					if (@CodificationStatus = @CodificationStatusPending)
					begin
						SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CODCPSR.StepDateTime IS NULL OR CODCPSR.StepDateTime >= @StartDateTime)'
					end
					else if (@CodificationStatus = @CodificationStatusConfirmed)
					begin    
						SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CCOD.ConfirmedDate IS NULL OR CCOD.ConfirmedDate >= @StartDateTime)'
					end
					else
					begin
						SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CODCPSR.StepDateTime IS NULL OR CODCPSR.StepDateTime >= @StartDateTime)'
					end
                end
                else
                begin
                    SET @sWhereClause = @sWhereClause + @NewLine + ' AND (LEVCPSR.StepDateTime IS NULL OR LEVCPSR.StepDateTime >= @StartDateTime)'
                end
            end
        end
    end
    if (@EndDateTime IS NOT NULL)
    begin
        if (@Step = @None)
        begin
            if (@CodificationStatus <> @CodificationStatusNone)
            begin
				if (@CodificationStatus = @CodificationStatusPending)
				begin
					SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CODCPSR.StepDateTime IS NULL OR CODCPSR.StepDateTime <= @EndDateTime)'
				end
				else if (@CodificationStatus = @CodificationStatusConfirmed)
				begin    
					SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CCOD.ConfirmedDate IS NULL OR CCOD.ConfirmedDate <= @EndDateTime)'
				end
				else
				begin
					SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CODCPSR.StepDateTime IS NULL OR CODCPSR.StepDateTime <= @EndDateTime)'
				end
            end
            else
            begin
                SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CP.RegistrationDateTime <= @EndDateTime)'
            end
        end
        else
        begin
            if (@Step = @Admission OR @Step = @Reception)
            begin
                SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CE.StartDateTime IS NULL OR CE.StartDateTime <= @EndDateTime)'
            end
            else
            begin
                if (@Step = @Codification)
                begin
					if (@CodificationStatus = @CodificationStatusPending)
					begin
						SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CODCPSR.StepDateTime IS NULL OR CODCPSR.StepDateTime <= @EndDateTime)'
					end
					else if (@CodificationStatus = @CodificationStatusConfirmed)
					begin    
						SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CCOD.ConfirmedDate IS NULL OR CCOD.ConfirmedDate <= @EndDateTime)'
					end
					else
					begin
						SET @sWhereClause = @sWhereClause + @NewLine + ' AND (CODCPSR.StepDateTime IS NULL OR CODCPSR.StepDateTime <= @EndDateTime)'
					end                    
                end
                else
                begin
                    SET @sWhereClause = @sWhereClause + @NewLine + ' AND (LEVCPSR.StepDateTime IS NULL OR LEVCPSR.StepDateTime <= @EndDateTime)'
                end
            end
        end
    end

	SET @sWhereClause = @sWhereClause + @NewLine + ' ORDER BY CP.[ID]'

	SET @ParmDefinition = '@TransferReason nvarchar(8),' + @NewLine
                        + '       @AssistanceService int,' + @NewLine
                        + '       @StartDateTime datetime,' + @NewLine
                        + '       @EndDateTime datetime,' + @NewLine
                        + '       @Step int,' + @NewLine
                        + '       @Status int,' + @NewLine
						+ '       @CodificationStatus int,' + @NewLine
						+ '       @TVPProcessChartIDs TVPInteger READONLY,' + @NewLine
						+ '       @TVPLocations		TVPInteger READONLY,' + @NewLine
						+ '       @TVPCareCenterIDs	TVPInteger READONLY,' + @NewLine

						+ '       @None int,' + @NewLine
						+ '       @Reception int,' + @NewLine
						+ '       @Admission int,' + @NewLine
						+ '       @Leave int,' + @NewLine
						+ '       @Codification int,' + @NewLine
						+ '       @Active int,' + @NewLine
						+ '       @Confirmed int,' + @NewLine
						+ '       @Pending int,' + @NewLine

						+ '       @CodificationStatusNone int,' + @NewLine
						+ '       @CodificationStatusPending int,' + @NewLine
						+ '       @CodificationStatusConfirmed int' + @NewLine

	SET @sSql = @sSql + @sWhereClause

	--SELECT @sSql

	
	EXEC sp_executesql @sSql, @ParmDefinition,
                       @TransferReason					= @TransferReason,
                       @AssistanceService				= @AssistanceService,
                       @StartDateTime 					= @StartDateTime,
                       @EndDateTime 					= @EndDateTime,
                       @Step 							= @Step,
                       @Status							= @Status,
					   @CodificationStatus 				= @CodificationStatus,
					   @TVPProcessChartIDs 				= @TVPProcessChartIDs,
					   @TVPLocations					= @TVPLocations,
					   @TVPCareCenterIDs				= @TVPCareCenterIDs,
					   									
					   @None 							= @None,
					   @Reception 						= @Reception,
					   @Admission 						= @Admission,
					   @Leave 							= @Leave,
					   @Codification 					= @Codification,
					   @Active 							= @Active,
					   @Confirmed 						= @Confirmed,
					   @Pending 						= @Pending,
					   									 
					   @CodificationStatusNone 			= @CodificationStatusNone,
					   @CodificationStatusPending 		= @CodificationStatusPending,
					   @CodificationStatusConfirmed		= @CodificationStatusConfirmed
	
END