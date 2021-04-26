USE HCDIS
--/////////////////////////////////////////////////////////////////////////
--//SCRIPT DE CONFIGURACIÓN PARA EL RESGISTRO DE LAS MODIFICACIONES EN EL PROCESO DE REHABILITACION
--//PRIMERO CREA LAS CONFIGURACIONES NECESARIAS PARA EL PROCESO
--//SEGUNDO CREA EL PROCESO
--//TERCERO LIMPIA TODA LA ACTIVIDAD DEL PROCESO DE REHABILITACION ANTERIOR
--////////////////////////////////////////////////////////////////////////


--//MEDICOS DE REHABILITACION PARA CLINICA VIRGEN BLANCA
--SELECT P.*, PH.* FROM Physician PH
--JOIN Person P ON PH.PersonID = P.ID
--WHERE (P.FirstName = 'LUIS' AND P.LastName = 'SOCIAS' )
--OR (P.FirstName = 'JOSE MARIA' AND P.LastName = 'URRUTICOECHEA')

DECLARE @LocationRehabID INT --//REHAB
DECLARE @Location1ID INT --//CONSULTA SOCIAS
DECLARE @Location2ID INT --//CONSULTA URRUTICOECHEA
DECLARE @LocationTypeID INT
DECLARE @LocationClassID INT --// CONSULTA REHAB
DECLARE @PatternLocation1ID INT --//SOCIAS
DECLARE @PatternLocation2ID INT --//URRUTICOECHEA

DECLARE @ServiceTypeID INT --//primera visita
DECLARE @ServiceID INT --//primera visita
DECLARE @ServiceCharge1ID INT --//primera visita
DECLARE @ServiceCharge2ID INT --//visita sucesiva

DECLARE @RoutineTypeID INT --//tipo de rutina Consulta Rehabilitacion
DECLARE @Routine1ID INT --//primera visita
DECLARE @Routine2ID INT --//visita sucesiva
DECLARE @Duration15ID INT
DECLARE @Duration10ID INT

DECLARE @RehabOrderTypeID INT 
DECLARE @RehabOrderID INT 
DECLARE @ToRequestOrderFlags BIGINT
DECLARE @ToRequestRequiredOrderFlags BIGINT

DECLARE @ClinicaVBID INT
DECLARE @ModifiedBy NVARCHAR(256)
DECLARE @LastUpdated DATETIME
DECLARE @Error INT
DECLARE @ErrorST NVARCHAR(MAX)

SET @ClinicaVBID = 9
SET @ModifiedBy = 'Administrador'
SET @LastUpdated = GETDATE()
SET @Error = 0
SET @ErrorST = ''

--///////////////////////////////////////////////
--// INICIO UBICACIONS REHABILITACION
--///////////////////////////////////////////////
--// REGISTRO UBICACIONES
SET @LocationRehabID = ISNULL((SELECT TOP 1 ID FROM Location WHERE Name = 'REHAB'),0)
SET @LocationTypeID =  ISNULL((SELECT TOP 1 ID FROM LocationType WHERE Name = 'SALA'),0)
SET @PatternLocation1ID =  28 --//  lunes de 16:30 a 20:00
SET @PatternLocation2ID =  29 --//  miercoles de 16:00 a 19:20
--// registro las ubicaciones
IF NOT(@LocationRehabID = 0) AND NOT(@LocationTypeID =0)
BEGIN
	SET @LocationClassID = ISNULL((SELECT TOP 1 ID FROM LocationClass WHERE Code = 'CONSULTA REHAB'),0) 
	IF (@LocationClassID = 0)	
	BEGIN
		INSERT INTO [LocationClass]
			([Code],[Description],[LocationTypeID],[LastUpdated],[ModifiedBy])
	    VALUES
			('CONSULTA REHAB','CONSULTA REHAB',@LocationTypeID,@LastUpdated,@ModifiedBy)
		SET @LocationClassID = (SELECT @@IDENTITY)
	END

	SET @Location1ID = ISNULL((SELECT TOP 1 ID FROM Location WHERE Name = 'CONSULTA SOCIAS'),0) 
	IF (@Location1ID = 0)
	BEGIN
		INSERT INTO [Location]
			([CareCenterID],[Name],[Description],[ReferenceLoc],[ParentLocationID],
			[LocationTypeID],[LocationClassID],[PlaceAddressID],[OrganizationsID],
			[Mobile],[AdditionalPlanningRequires],[StartDateTime],[EndDateTime],
			[Status],[LastUpdated],[ModifiedBy])
		VALUES(@ClinicaVBID,'CONSULTA SOCIAS','CONSULTA SOCIAS','',@LocationRehabID,
			@LocationTypeID,@LocationClassID,0,0,0,1,'01/09/2015', NULL, 1, @LastUpdated, @ModifiedBy)
		SET @Location1ID = (SELECT @@IDENTITY)
		
		INSERT INTO [HHRRLocationRel]
           ([HumanResourceID],[LocationID],[AuthorizedStatus],[LastUpdated],[ModifiedBy])
        SELECT DISTINCT HR.ID,  @Location1ID, 2, @LastUpdated, @ModifiedBy
        FROM HumanResource HR
        JOIN PersonSecurityRel PSR ON HR.PersonID = PSR.PersonID
        WHERE PSR.UserName IN ('ADMCCEE','OYS','FISION')
		
	END
	SET @Location2ID = ISNULL((SELECT TOP 1 ID FROM Location WHERE Name = 'CONSULTA URRUTICOECHEA'),0) 
	IF (@Location2ID = 0)
	BEGIN
		INSERT INTO [Location]
			([CareCenterID],[Name],[Description],[ReferenceLoc],[ParentLocationID],
			[LocationTypeID],[LocationClassID],[PlaceAddressID],[OrganizationsID],
			[Mobile],[AdditionalPlanningRequires],[StartDateTime],[EndDateTime],
			[Status],[LastUpdated],[ModifiedBy])
		VALUES(@ClinicaVBID,'CONSULTA URRUTICOECHEA','CONSULTA URRUTICOECHEA','',@LocationRehabID,
			@LocationTypeID,@LocationClassID,0,0,0,1,'01/09/2015', NULL, 1, @LastUpdated, @ModifiedBy)
		SET @Location2ID = (SELECT @@IDENTITY)
		
		INSERT INTO [HHRRLocationRel]
           ([HumanResourceID],[LocationID],[AuthorizedStatus],[LastUpdated],[ModifiedBy])
        SELECT DISTINCT HR.ID,  @Location2ID, 2, @LastUpdated, @ModifiedBy
        FROM HumanResource HR
        JOIN PersonSecurityRel PSR ON HR.PersonID = PSR.PersonID
        WHERE PSR.UserName IN ('ADMCCEE','OYS','FISION')

	END
END
IF (@Location1ID = 0) OR (@Location2ID =0)
BEGIN
	SET @Error = -1
	SET @ErrorST = @ErrorST + ' ERROR EN UBICACIONES'
END
--//registro los availpatterns
IF NOT(@Location1ID = 0) AND NOT(@Location2ID =0) AND (@Error = 0)
BEGIN
	IF NOT(EXISTS(SELECT ID FROM LocationAvailPatternRel 
		WHERE LocationID = @Location1ID AND AvailPatternID = @PatternLocation1ID))
	BEGIN
		INSERT INTO [LocationAvailPatternRel]
			([LocationID],[AvailPatternID],[Status],[StartAt],[EndingIn],[LastUpdated],[ModifiedBy])
		VALUES		
			(@Location1ID,@PatternLocation1ID,1,'01/09/2015',null,@LastUpdated, @ModifiedBy)
	END			
	IF NOT(EXISTS(SELECT ID FROM LocationAvailPatternRel 
		WHERE LocationID = @Location2ID AND AvailPatternID = @PatternLocation2ID))
	BEGIN
		INSERT INTO [LocationAvailPatternRel]
			([LocationID],[AvailPatternID],[Status],[StartAt],[EndingIn],[LastUpdated],[ModifiedBy])
		VALUES		
			(@Location2ID,@PatternLocation2ID,1,'01/09/2015',null,@LastUpdated, @ModifiedBy)
	END			
END
--///////////////////////////////////////////////
--// FIN UBICACIONS REHABILITACION
--///////////////////////////////////////////////

--///////////////////////////////////////////////
--// INICIO CARGOS REHABILITACION
--///////////////////////////////////////////////
--// registro los cargos
SET @ServiceTypeID = ISNULL((SELECT TOP 1 ID FROM ServiceType WHERE Code = 'REHA'),0)
IF (@ServiceTypeID = 0)
BEGIN
	INSERT INTO [ServiceType]
		([Code],[Description],[AccountNumber],[AncestorID],[Status],[LastUpdated],[ModifiedBy])
    VALUES
		('REHA','REHABILITACION','70500009',0,1,@LastUpdated,@ModifiedBy)
	SET @ServiceTypeID = (SELECT @@IDENTITY)
END
IF (@ServiceTypeID = 0)
BEGIN
	SET @Error = -1
	SET @ErrorST = @ErrorST + ' ERROR EN TIPO SERVICIO'
END

SET @ServiceID = ISNULL((SELECT TOP 1 ID FROM Service WHERE Code = 'REHA'),0)
IF (@ServiceID = 0)
BEGIN
	INSERT INTO [Service]
		([Code],[Description],[AccountNumber],[TaxTypeID],[ServiceTypeID],[AncestorID],
		[Status],[LastUpdated],[ModifiedBy])
     VALUES
		('REHA','REHABILITACION','70500009',1,@ServiceTypeID,0,1,@LastUpdated,@ModifiedBy)
	SET @ServiceID = (SELECT @@IDENTITY)
END
IF (@ServiceID = 0)
BEGIN
	SET @Error = -1
	SET @ErrorST = @ErrorST + ' ERROR EN SERVICIO'
END

--//primera visita
IF (@Error = 0)
BEGIN
	SET @ServiceCharge1ID = ISNULL((SELECT TOP 1 ID FROM ServiceCharge WHERE Code = '9000096992'),0) 
	IF (@ServiceCharge1ID = 0)
	BEGIN
		INSERT INTO [ServiceCharge]
			([Code],[Name],[Description],[ServiceID],[Units],[PhysUnitID],[CostUnitPrice],
			[CostAmountQty],[RefUnitPrice],[RefAmountQty],[TaxTypeID],[WhenToCharge],
			[ElementID],[AncestorID],[Status],[LastUpdated],[ModifiedBy])
		 VALUES
			('9000096992','PRIMERA CONSULTA REHABILITACION','PRIMERA CONSULTA REHABILITACION',
			@ServiceID,1,7,0,0,0,0,1,8,0,0,1,@LastUpdated, @ModifiedBy)
		SET @ServiceCharge1ID = (SELECT @@IDENTITY)
	END
	 --//visita sucesiva
	SET @ServiceCharge2ID = ISNULL((SELECT TOP 1 ID FROM ServiceCharge WHERE Code = '9000096993'),0) 
	IF (@ServiceCharge2ID = 0)
	BEGIN
		INSERT INTO [ServiceCharge]
			([Code],[Name],[Description],[ServiceID],[Units],[PhysUnitID],[CostUnitPrice],
			[CostAmountQty],[RefUnitPrice],[RefAmountQty],[TaxTypeID],[WhenToCharge],
			[ElementID],[AncestorID],[Status],[LastUpdated],[ModifiedBy])
		 VALUES
			('9000096993','CONSULTA SUCESIVA REHABILITACION','CONSULTA SUCESIVA REHABILITACION',
			@ServiceID,1,7,0,0,0,0,1,8,0,0,1,@LastUpdated, @ModifiedBy)
		SET @ServiceCharge2ID = (SELECT @@IDENTITY)
	END
	IF (@ServiceCharge1ID = 0) OR (@ServiceCharge2ID =0)
	BEGIN
		SET @Error = -1
		SET @ErrorST = @ErrorST + ' ERROR EN CARGOS'
	END
END
--///////////////////////////////////////////////
--// FIN CARGOS REHABILITACION
--///////////////////////////////////////////////

--///////////////////////////////////////////////
--// INICIO RUTINAS DE VISITAS
--///////////////////////////////////////////////
SET @RoutineTypeID = ISNULL((SELECT TOP 1 ID FROM RoutineType WHERE Code = 'CONSULTA REHABILITACION' ),0)
IF @RoutineTypeID = 0 
BEGIN
	SET @Error = -1
	SET @ErrorST = @ErrorST + ' ERROR EN TIPO RUTINA'

END
SET @Duration15ID = ISNULL((SELECT TOP 1 ID FROM TimePattern WHERE Meaning = 'DT MI15' ),0)
IF @Duration15ID = 0 
BEGIN
	SET @Error = -1
	SET @ErrorST = @ErrorST + ' ERROR EN DURACION 15 MIN'
END
SET @Duration10ID = ISNULL((SELECT TOP 1 ID FROM TimePattern WHERE Meaning = 'DT MI10' ),0)
IF @Duration10ID = 0 
BEGIN
	SET @Error = -1
	SET @ErrorST = @ErrorST + ' ERROR EN DURACION 10 MIN'
END
IF (@Error = 0)
BEGIN
	--//primera visita
	SET @Routine1ID = ISNULL((SELECT TOP 1 ID FROM Routine WHERE Code = '9000096992' ),0)
	IF (@Routine1ID = 0)
	BEGIN
		INSERT INTO [Routine]
			([Code],[Name],[Description],[RoutineTypeID],[IsCodified],[Encoder],[RoutineClassificationID],
			[RoutineDocument],[PreviousPreparation],[ConfirmedBy],[CreatedBy],[RegistrationDateTime],
			[ConfirmedDateTime],[AvailPattern],[TimingControl],[StepsControl],[AdditionalPlanningRequires],
			[EstimatedDurationID],[FrequencyOfApplicationID],[ReferenceCost],[ReferencePrice],[TaxTypeID],
			[Status],[ModifiedBy],[LastUpdated])
		VALUES
			('9000096992','PRIMERA VISITA','PRIMERA VISITA REHABILITACION',@RoutineTypeID,0,'',0,
			'','',@ModifiedBy,@ModifiedBy,@LastUpdated,
			@LastUpdated,0,0,0,1,@Duration15ID,0,0,0,1, 3, @ModifiedBy, @LastUpdated)
		SET @Routine1ID = (SELECT @@IDENTITY)
		IF NOT(@Routine1ID = 0)
		BEGIN
			INSERT INTO [RoutineChargeRel]
				([RoutineID],[ServiceChargeID],[LinkedEntityID],[LinkedTo],[LastUpdated],[ModifiedBy])
			VALUES
				(@Routine1ID,@ServiceCharge1ID,0,1,@LastUpdated,@ModifiedBy)
				
			INSERT INTO [RoutineLocationRel]
				([RoutineID],[LocationID],[LastUpdated],[ModifiedBy])
			VALUES	
				(@Routine1ID,@LocationRehabID,@LastUpdated,@ModifiedBy)
			INSERT INTO [RoutineLocationRel]
				([RoutineID],[LocationID],[LastUpdated],[ModifiedBy])
			VALUES	
				(@Routine1ID,@Location1ID,@LastUpdated,@ModifiedBy)
			INSERT INTO [RoutineLocationRel]
				([RoutineID],[LocationID],[LastUpdated],[ModifiedBy])
			VALUES	
				(@Routine1ID,@Location2ID,@LastUpdated,@ModifiedBy)
		END
	END
	 --//visita sucesiva
	SET @Routine2ID = ISNULL((SELECT TOP 1 ID FROM Routine WHERE Code = '9000096993' ),0)
	IF (@Routine2ID = 0)
	BEGIN
		INSERT INTO [Routine]
			([Code],[Name],[Description],[RoutineTypeID],[IsCodified],[Encoder],[RoutineClassificationID],
			[RoutineDocument],[PreviousPreparation],[ConfirmedBy],[CreatedBy],[RegistrationDateTime],
			[ConfirmedDateTime],[AvailPattern],[TimingControl],[StepsControl],[AdditionalPlanningRequires],
			[EstimatedDurationID],[FrequencyOfApplicationID],[ReferenceCost],[ReferencePrice],[TaxTypeID],
			[Status],[ModifiedBy],[LastUpdated])
		VALUES
			('9000096993','VISITA SUCESIVA','VISITA SUCESIVA REHABILITACION',@RoutineTypeID,0,'',0,
			'','',@ModifiedBy,@ModifiedBy,@LastUpdated,
			@LastUpdated,0,0,0,1,@Duration10ID,0,0,0,1, 3, @ModifiedBy, @LastUpdated)
		SET @Routine2ID = (SELECT @@IDENTITY)
		IF NOT(@Routine2ID = 0)
		BEGIN
			--// CARGO RUTINA
			INSERT INTO [RoutineChargeRel]
				([RoutineID],[ServiceChargeID],[LinkedEntityID],[LinkedTo],[LastUpdated],[ModifiedBy])
			VALUES
				(@Routine2ID,@ServiceCharge1ID,0,1,@LastUpdated,@ModifiedBy)
			--// LOCATIONS	
			INSERT INTO [RoutineLocationRel]
				([RoutineID],[LocationID],[LastUpdated],[ModifiedBy])
			VALUES	
				(@Routine2ID,@LocationRehabID,@LastUpdated,@ModifiedBy)
			INSERT INTO [RoutineLocationRel]
				([RoutineID],[LocationID],[LastUpdated],[ModifiedBy])
			VALUES	
				(@Routine2ID,@Location1ID,@LastUpdated,@ModifiedBy)
			INSERT INTO [RoutineLocationRel]
				([RoutineID],[LocationID],[LastUpdated],[ModifiedBy])
			VALUES	
				(@Routine2ID,@Location2ID,@LastUpdated,@ModifiedBy)
		END
	END
END
IF (@Routine1ID = 0) OR (@Routine2ID =0)
BEGIN
	SET @Error = -1
	SET @ErrorST = @ErrorST + ' ERROR EN RUTINAS'

END
--///////////////////////////////////////////////
--// FIN RUTINAS DE VISITAS
--///////////////////////////////////////////////

--///////////////////////////////////////////////
--// INICIO SOLICITUD DE VISITA
--///////////////////////////////////////////////
IF (@Error = 0)
BEGIN
	SET @RehabOrderTypeID = ISNULL((SELECT TOP 1 ID FROM OrderType WHERE AssignedCode = 'CONSULTA REHABILITACION'),0)  
	IF (@RehabOrderTypeID = 0)
	BEGIN
		INSERT INTO [OrderType]
			([IsCodified],[Encoder],[AssignedCode],[Name],[Description],[OrderCase],[OrderClassificationID],
			[RegistrationDateTime],[ImageID],[AncestorID],[Status],[ModifiedBy],[LastUpdated])
		 VALUES
			(0,'','CONSULTA REHABILITACION','CONSULTA REHABILITACION','CONSULTA REHABILITACION',2,0,
			@LastUpdated,0,0,1,@ModifiedBy,@LastUpdated)
		SET @RehabOrderTypeID = (SELECT @@IDENTITY)
	END
	ELSE
	BEGIN
		UPDATE [OrderType] SET [OrderCase] = 2, [Status]=1, [ModifiedBy] =@ModifiedBy, [LastUpdated] =@LastUpdated
		WHERE ID = @RehabOrderTypeID
	END
	IF (@RehabOrderTypeID = 0)
	BEGIN
		SET @Error = -1
		SET @ErrorST = @ErrorST + ' ERROR EN TIPO SOLICITUD'
	END
	SET @RehabOrderID =   ISNULL((SELECT TOP 1 ID FROM [Order] WHERE AssignedCode = 'CREHAB'),0)  
	SET @ToRequestOrderFlags  = 2 + 64 + 128  --// Servicio solicita + Asegurdora + Poliza
	SET @ToRequestRequiredOrderFlags =  2 + 64 --// Servicio solicita + Asegurdora
	IF (@RehabOrderID = 0) AND (@Error = 0)
	BEGIN
		INSERT INTO [Order]
			([OrderTypeID],[IsCodified],[Encoder],[AssignedCode],[Name],[Description],[RegistrationDateTime],
			[ToRequestOrderFlags],[ToRequestRequiredOrderFlags],[AllowPlanning],[ADTRequestAction],[AncestorID],
			[AvailPattern],[InUse],[Status],[ModifiedBy],[LastUpdated])
	     VALUES
			(@RehabOrderTypeID,0,'','CREHAB','CONSULTA REHABILITACION','CONSULTA REHABILITACION',@LastUpdated,
			@ToRequestOrderFlags,@ToRequestRequiredOrderFlags,1,0,0,
			0,0,1,@ModifiedBy,@LastUpdated)
		SET @RehabOrderID = (SELECT @@IDENTITY)
	END
	ELSE
	BEGIN
		UPDATE [Order] SET ToRequestOrderFlags =@ToRequestOrderFlags, 
			ToRequestRequiredOrderFlags = @ToRequestRequiredOrderFlags,
			[Status]=1, [ModifiedBy] =@ModifiedBy, [LastUpdated] =@LastUpdated
		WHERE ID = @RehabOrderID
	END
	IF NOT(EXISTS(SELECT ID FROM [OrderRoutineRel] WHERE [OrderID]=@RehabOrderID AND [RoutineID]=@Routine1ID)) AND (@Error = 0)
	BEGIN
		INSERT INTO [OrderRoutineRel]
			([OrderID],[RoutineTypeID],[RoutineID],[RelationType],[Order],[Priority],
			[HasCriterion],[Status],[LastUpdated],[ModifiedBy])
		VALUES
			(@RehabOrderID,@RoutineTypeID,@Routine1ID,1,1,0,0,1,@LastUpdated,@ModifiedBy)
	END
	IF NOT(EXISTS(SELECT ID FROM [OrderRoutineRel] WHERE [OrderID]=@RehabOrderID AND [RoutineID]=@Routine2ID)) AND (@Error = 0)
	BEGIN
		INSERT INTO [OrderRoutineRel]
			([OrderID],[RoutineTypeID],[RoutineID],[RelationType],[Order],[Priority],
			[HasCriterion],[Status],[LastUpdated],[ModifiedBy])
		VALUES
			(@RehabOrderID,@RoutineTypeID,@Routine2ID,1,2,0,0,1,@LastUpdated,@ModifiedBy)
	END
	
END
IF (@RehabOrderID = 0)
BEGIN
	SET @Error = -1
	SET @ErrorST = @ErrorST + ' ERROR EN SOLICITUD'
END
--///////////////////////////////////////////////
--// FIN SOLICITUD DE VISITA
--///////////////////////////////////////////////


--///////////////////////////////////////////////
--// INICIO BORRADO PROCESO REHABILITACION ANTERIOR
--///////////////////////////////////////////////
IF (@Error =0)
BEGIN
	DECLARE @OldRehabPCID INT
	SET @OldRehabPCID = ISNULL((SELECT TOP 1 PC.ID FROM ProcessChart PC
			 JOIN CitationConfig CC ON PC.ID = CC.ProcessChartID
			 WHERE PC.Name = 'REHABILITACION' AND CC.CitationType = 3),0)
	IF NOT(@OldRehabPCID =0)
	BEGIN
		--//BORRANDO ACTIVIDAD DEL PROCESO
		--IF EXISTS((SELECT ID FROM CustomerProcess WHERE ProcessChartID =@OldRehabPCID))
		--BEGIN
		--	--// ESTE PROCESO TIENE EPISODIOS MÉDICOS Y ACTOS MEDICOS
			
		--END 
		--// BORRADO DEL PROCESO
		
		
		
		--///////////////////////////////////////////////////////////////
		--//
		--// NOTA: POR AHORA SOLO CAMBIAREMOS EL NOMBRE DEL PROCESO Y LO DESHABILITAMOS
		--//
		--//////////////////////////////////////////////////////////////
		UPDATE ProcessChart SET Name = 'REHABILITACION ANTIGUO', [Status] = 2 /* CANCELLED */
		WHERE ID = @OldRehabPCID 
	END
END
--///////////////////////////////////////////////
--// FIN BORRADO PROCESO REHABILITACION ANTERIOR
--///////////////////////////////////////////////



--///////////////////////////////////////////////
--// INICIO PROCESO REHABILITACION
--///////////////////////////////////////////////
DECLARE @RehabAssitPCID INT
DECLARE @RehabEpisodeTypeID INT
DECLARE @RehabAssServID INT
SET @RehabAssitPCID = ISNULL((SELECT TOP 1 ID FROM AssistanceProcessChart WHERE Name = 'PACONSULTAS'),0)
SET @RehabEpisodeTypeID = ISNULL((SELECT TOP 1 ID FROM EpisodeType WHERE Code = '05'),0)
SET @RehabAssServID = ISNULL((SELECT TOP 1 ID FROM AssistanceService WHERE AssignedCode = '1130'),0)
IF @RehabAssitPCID = 0 OR @RehabEpisodeTypeID = 0 OR @RehabAssServID = 0
BEGIN
	SET @Error = -1
	SET @ErrorST = @ErrorST + ' ERROR EN CREACION PROCESO'
END
IF (@Error = 0)
BEGIN
	--// PROCESO
	DECLARE @RehabStepID INT
	DECLARE @RehabPCID INT
	DECLARE @ProcessChartAssistProcessRelID INT
	
	
	
	SET @RehabPCID = ISNULL((SELECT TOP 1 PC.ID FROM ProcessChart PC
			 JOIN CitationConfig CC ON PC.ID = CC.ProcessChartID
			 WHERE PC.Name = 'REHABILITACION' AND CC.CitationType = 4),0)
	IF (@RehabPCID =0)
	BEGIN
		--// PRIMERO EL PROCESO
		INSERT INTO [ProcessChart]
			([Name],[Description],[DataFlowID],[CustomerAsGuarantor],[CustomerGuarantorType],[CustomerType],
			[EpisodeConfigID],[RequiredPreviousEpisode],[EpisodeNumberMask],[EpisodeMandatoryConfig],
			[AdmitMultipleInstance],[ExcludeSteps],[InUse],[Status],[LastUpdated],[ModifiedBy])
		VALUES
			('REHABILITACION','REHABILITACION',1,1,2,0,
			@RehabEpisodeTypeID,0,'EpisodioCCEE',2,0,0,0,1,@LastUpdated,@ModifiedBy)
		SET @RehabPCID = (SELECT @@IDENTITY)
		IF NOT(@RehabPCID =0)
		BEGIN
			--//EL CENTRO
			INSERT INTO [ProcessChartCareCenterRel]
				([ProcessChartID],[CareCenterID],[IsDefault],[Status],[LastUpdated],[ModifiedBy])
		     VALUES
				(@RehabPCID,@ClinicaVBID,0,1,@LastUpdated,@ModifiedBy)
			--//PROCESO ASISTENCIAL
			INSERT INTO [ProcessChartAssistProcessRel]
				([ProcessChartID],[CareCenterID],[AssistanceProcessChartID],[IsDefault],
				[StartDateTime],[EndDateTime],[Status],[LastUpdated],[ModifiedBy])
			VALUES				
				(@RehabPCID,@ClinicaVBID,@RehabAssitPCID,1,@LastUpdated,NULL,1,@LastUpdated,@ModifiedBy)
			SET @ProcessChartAssistProcessRelID = (SELECT @@IDENTITY)
			--//SERVICIOS
			INSERT INTO [AssistanceProcessChartAssistanceServiceRel]
				([ProcessChartAssistProcessRelID],[AssistanceServiceID],[LastUpdated],[ModifiedBy])
			VALUES
				(@ProcessChartAssistProcessRelID,@RehabAssServID,@LastUpdated,@ModifiedBy)
			--//UBICACIONES
			INSERT INTO [ProcessChartHierarchyRel]
				([ProcessChartID],[CareCenterID],[LocationID],[LocationTypeID],[LocationClassID],
				[StartDateTime],[EndDateTime],[IsDefault],[DefaultPlacementChargeID],
				[PlacementChargeIsMandatory],[MinimumTimeIntervalID],[FractionationUnit],
				[PlacementChargeFractionation],[ResourceChargeAsPlacementCharge],[CleaningStatusAtDischarge],
				[CleaningServiceType],[CleaningServiceID],[CleaningDischargeNotificationID],
				[Status],[LastUpdated],[ModifiedBy])
			VALUES
				(@RehabPCID,@ClinicaVBID,@LocationRehabID,66,0,
				@LastUpdated,null,0,0,0,0,0,0,0,0,0,0,0,1,@LastUpdated,@ModifiedBy)
			--//GARANTE ASEGURADORA
			INSERT INTO [ProcessChartGuarTypeRel]
				([ProcessChartID],[CareCenterID],[GuarantorType],[Required],[OnlyOne],[InvoiceAgreeRequired],
				[AsDefaultGuarantor],[AdmitSrvOutsideAgree],[CoverOrder],[Status],[LastUpdated],[ModifiedBy])
			VALUES				
				(@RehabPCID,@ClinicaVBID,1,0,0,0,1,0,1,1,@LastUpdated,@ModifiedBy)
			--//GARANTE PRIVADO
			INSERT INTO [ProcessChartGuarTypeRel]
				([ProcessChartID],[CareCenterID],[GuarantorType],[Required],[OnlyOne],[InvoiceAgreeRequired],
				[AsDefaultGuarantor],[AdmitSrvOutsideAgree],[CoverOrder],[Status],[LastUpdated],[ModifiedBy])
			VALUES				
				(@RehabPCID,@ClinicaVBID,2,0,0,0,0,0,2,1,@LastUpdated,@ModifiedBy)
			--// PASOS DE PROCESO
			--// CITACION
			INSERT INTO [BasicStepsInProcess]
				([ProcessChartID],[ProcessStep],[Position],[StepRequired],[StepVisibleInProcessList],
				[StepAllowExecution],[LastUpdated],[ModifiedBy])
			VALUES
				(@RehabPCID,128,0,0,1,0,@LastUpdated,@ModifiedBy)
			--// RECEPCION
			INSERT INTO [BasicStepsInProcess]
				([ProcessChartID],[ProcessStep],[Position],[StepRequired],[StepVisibleInProcessList],
				[StepAllowExecution],[LastUpdated],[ModifiedBy])
			VALUES
				(@RehabPCID,256,1,1,1,0,@LastUpdated,@ModifiedBy)
			--// REALIZACION
			INSERT INTO [BasicStepsInProcess]
				([ProcessChartID],[ProcessStep],[Position],[StepRequired],[StepVisibleInProcessList],
				[StepAllowExecution],[LastUpdated],[ModifiedBy])
			VALUES
				(@RehabPCID,32768,2,0,0,1,@LastUpdated,@ModifiedBy)
			--// ALTA
			INSERT INTO [BasicStepsInProcess]
				([ProcessChartID],[ProcessStep],[Position],[StepRequired],[StepVisibleInProcessList],
				[StepAllowExecution],[LastUpdated],[ModifiedBy])
			VALUES
				(@RehabPCID,16384,3,0,1,0,@LastUpdated,@ModifiedBy)
			--// COBERTURAS
			INSERT INTO [BasicStepsInProcess]
				([ProcessChartID],[ProcessStep],[Position],[StepRequired],[StepVisibleInProcessList],
				[StepAllowExecution],[LastUpdated],[ModifiedBy])
			VALUES
				(@RehabPCID,524288,4,0,0,1,@LastUpdated,@ModifiedBy)
			--// ALBARAN
			INSERT INTO [BasicStepsInProcess]
				([ProcessChartID],[ProcessStep],[Position],[StepRequired],[StepVisibleInProcessList],
				[StepAllowExecution],[LastUpdated],[ModifiedBy])
			VALUES
				(@RehabPCID,1048576,5,0,0,1,@LastUpdated,@ModifiedBy)
			--// FACTURACION
			INSERT INTO [BasicStepsInProcess]
				([ProcessChartID],[ProcessStep],[Position],[StepRequired],[StepVisibleInProcessList],
				[StepAllowExecution],[LastUpdated],[ModifiedBy])
			VALUES
				(@RehabPCID,2097152,6,0,0,1,@LastUpdated,@ModifiedBy)
			--// CITATION CONFIG
			INSERT INTO [CitationConfig]
				([ProcessChartID],[CitationType],[CitationMandatoryPattern],[DefaultScheduleMode],
				[DefaultCitationConfirmMode],[ShowMultipleResources],[DefaultFindMode],[DefaultCitationMode],
				[AllowMultipleAppointment],[AllowExceedingOverbooking],[AllowMultipleNotice],
				[RequiredNoticeMode],[MedEpisodeProcessChartID],[ShowRequestingPhysician],
				[ShowRequestingLocation],[ShowRequestingInsurer],[ShowRequestingPriority],
				[ShowRequestingReasons],[ShowPrecautions],[ShowContraindications],
				[LastUpdated],[ModifiedBy])
			VALUES
				(@RehabPCID,4,1,2,0,0,1,7,0,1,0,0,0,0,0,0,0,0,0,0,@LastUpdated,@ModifiedBy)
			SET @RehabStepID = (SELECT @@IDENTITY)
			IF 	NOT(@RehabStepID = 0) 
			BEGIN
				INSERT INTO [CitationResourceRel]
					([CitationConfigID],[ResourceElement],[ResourceTypeID],[ResourceID],
					[IsDefault],[Status],[LastUpdated],[ModifiedBy])
				VALUES
					(@RehabStepID,1,@LocationTypeID,@Location1ID,0,1,@LastUpdated,@ModifiedBy)
				INSERT INTO [CitationResourceRel]
					([CitationConfigID],[ResourceElement],[ResourceTypeID],[ResourceID],
					[IsDefault],[Status],[LastUpdated],[ModifiedBy])
				VALUES
					(@RehabStepID,1,@LocationTypeID,@Location2ID,0,1,@LastUpdated,@ModifiedBy)
			END
			--// RECEPTION CONFIG
			INSERT INTO [ReceptionConfig]
				([ProcessChartID],[CitationType],[MedEpisodeProcessChartID],[ShowCustomerChattels],
				[ProformaIsRequired],[SourceType],[Proforma],[ShowDeliveryResults],[CollectionResultsTimeID],
				[OnAdmitCreateMedicalEpisode],[ProcessChargeID],[AbortingReception],
				[AbortingReceptionNotificationID],[AssistanceServiceRequired],[ShowAssistanceServiceType],
				[ShowAttendingPhysician],[AttendingPhysicianID],[AttendingPhysicianRequired],
				[AttendingPhysiciansByAssistanceService],[GuarantorRequired],[PrivateGuarantorName],
				[ShowRequestedPhysician],[RequestedPhysicianRequired],[ShowPreprintsAfterReception],
				[AutoScheduleOnReception],[LastUpdated],[ModifiedBy])
			VALUES
				(@RehabPCID,4,0,0,0,0,'',0,0,0,0,1,0,1,0,1,0,0,1,1,'PRIVADO',1,0,1,1,@LastUpdated,@ModifiedBy)
			SET @RehabStepID = (select @@IDENTITY)
			IF 	NOT(@RehabStepID = 0) 
			BEGIN
				INSERT INTO [ReceptionResourceRel]
					([ReceptionConfigID],[ResourceElement],[ResourceTypeID],[ResourceID],[IsDefault],
					[Status],[LastUpdated],[ModifiedBy])
				VALUES
					(@RehabStepID,1,66,@LocationRehabID,1,1,@LastUpdated,@ModifiedBy)
					
				INSERT INTO [StepPreprint]
					([ProcessChartID],[StepConfig],[StepConfigID],[SourceType],[DirectionType],[ReferenceID],
					[Reference],[Type],[Label],[Copies],[AppointmentElement],[EntityTypeID],[EntityID],
					[LastUpdated],[ModifiedBy])
				VALUES				
					(@RehabPCID,256,@RehabStepID,8,2,0,
					'service://addin/stepAction/client/DoctorsNoteStepAction',16384,'JUSTIFICANTES',
					1,0,0,0,@LastUpdated,@ModifiedBy)
			END
			--// REALIZACION
			INSERT INTO [RealizationConfig]
				([ProcessChartID],[ShowRequestedActs],[ShowOrdersInRequestedActs],[AtOpenShowRequestedActsAs],
				[AtOpenShowRealizedActsAs],[ShowObsInRealizeActs],[ReplicateRoutineIsPossible],
				[ReplicateStepsIsPossible],[ShowResources],[ShowHumanResources],[ShowEquipments],
				[ShowNotifications],[ShowObservations],[ShowMedicalActs],[ShowBillableAmounts],
				[SelectUniqueItemAsDefault],[ShowTabsByAction],[LastUpdated],[ModifiedBy])
			VALUES
				(@RehabPCID,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,@LastUpdated,@ModifiedBy)
			--//  DISCHARGE/LEAVE
			INSERT INTO [LeaveConfig]
				([ProcessChartID],[ShowCustomerChattels],[DischargeReportIsRequired],
				[DischargePhysicianIsRequired],[LeaveOrderRequired],[LeaveOrderID],[AbortingLeave],
				[AbortingLeaveNotificationID],[ExitusReasonID],[LeakageReasonID],[CancelActivity],
				[DefaultReasonCancelActivityID],[DefaultReasonCancelActivityExplanation],
				[CloseScheduledActivity],[DefaultReasonFinalizeSchID],[DefaultReasonFinalizeSchExplanation],
				[CloseOrders],[DefaultReasonFinalizeOrderID],[DefaultReasonFinalizeOrderExplanation],
				[FinalizeMedicalEpisodesIsPossible],[LastUpdated],[ModifiedBy])
			VALUES
				(@RehabPCID,0,0,0,0,0,1,0,0,0,0,0,'',0,0,'',0,0,'',0,@LastUpdated,@ModifiedBy)
			--// COVER STEP
			INSERT INTO [CoverConfig]
				([ProcessChartID],[ChargesNotCovered],[ChargesToCustomerAccount],[PriceRangeReviewID],
				[UsingCurrentPrices],[AdmitPriceModification],[OnlyManualCharges],[CoverAnalysisAtDischarge],
				[DefaultAssistanceAgreeID],[UnconditionalAssistAgreeName],[UnconditionalAgreeName],
				[ErrorCoverNotificationID],[ShowCoverAgreement],[CoverAgreementRequired],[ShowAgreement],
				[AgreementRequired],[ShowAuthorizations],[RequiredAuthorizations],[AuthorizationToNotInvoiceID],
				[LastUpdated],[ModifiedBy])
           VALUES
				(@RehabPCID,1,1,0,1,1,0,1,1,'','',0,0,0,0,0,0,0,4,@LastUpdated,@ModifiedBy)
           --// albaranes cofig
           INSERT INTO [DeliveryNoteConfig]
				([ProcessChartID],[AdmitTaxableModification],[AdmitDeliveryNoteDelete],
				[ErrorDeliveryNoteNotificationID],[LastUpdated],[ModifiedBy])
		    VALUES
				(@RehabPCID,1,1,0,@LastUpdated,@ModifiedBy)
			--// invoice config	
			INSERT INTO [InvoiceConfig]
				([ProcessChartID],[DefaultInvoiceNumberMask],[InvoiceAtDischarge],[DefaultInvoiceAgreementName],
				[ErrorInvoiceNotificationID],[LastUpdated],[ModifiedBy])
			VALUES		
				(@RehabPCID,'Factura',0,'',0,@LastUpdated,@ModifiedBy)
		END
	END
END
--///////////////////////////////////////////////
--// FIN PROCESO REHABILITACION
--///////////////////////////////////////////////

IF NOT(@Error = 0)
BEGIN
	PRINT 'HA OCURRIDO UN ERROR EN EL PROCESAMIENTO ' + @ErrorST
END
ELSE
BEGIN
	--// DESHABILITAR LAS UBICACIONES ANTERIORES
	UPDATE Location SET [Status] = 2 WHERE Name = 'SALA 1' OR Name = 'SALA 2' 
END