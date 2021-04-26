DECLARE @AssistanceAgreementID int
DECLARE @InsurerCoverAgreementID int
DECLARE @AgreementID int
DECLARE @InsurerAgreementID int
DECLARE @AgreeConditionID int
DECLARE @InsurerConditionID int

SET @AssistanceAgreementID = ISNULL((SELECT TOP 1 E.ID FROM EACElement E WHERE (E.Name = 'AssistanceAgreementEntity')), 0)
SET @InsurerCoverAgreementID = ISNULL((SELECT TOP 1 E.ID FROM EACElement E WHERE (E.Name = 'InsurerCoverAgreementEntity')), 0)
SET @AgreementID = ISNULL((SELECT TOP 1 E.ID FROM EACElement E WHERE (E.Name = 'AgreementEntity')), 0)
SET @InsurerAgreementID = ISNULL((SELECT TOP 1 E.ID FROM EACElement E WHERE (E.Name = 'InsurerAgreementEntity')), 0)
SET @AgreeConditionID = ISNULL((SELECT TOP 1 E.ID FROM EACElement E WHERE (E.Name = 'AgreeConditionEntity')), 0)
SET @InsurerConditionID = ISNULL((SELECT TOP 1 E.ID FROM EACElement E WHERE (E.Name = 'InsurerConditionEntity')), 0)

UPDATE DeliveryNote SET AssignedCode=HAA.AssignedCode, Name=HAA.Name, CalculationMode=HAA.CalculationMode
FROM DeliveryNote DN
	JOIN HistoryAssistanceAgreement HAA ON HAA.ID=DN.CoverEntityID AND DN.CoverElementID=@AssistanceAgreementID

UPDATE DeliveryNote SET AssignedCode=HICA.AssignedCode, Name=HICA.Name, CalculationMode=HICA.CalculationMode
FROM DeliveryNote DN
	JOIN HistoryInsurerCoverAgreement HICA ON HICA.ID=DN.CoverEntityID AND DN.CoverElementID=@InsurerCoverAgreementID

UPDATE DeliveryNoteEntry SET AssignedCode=HA.AssignedCode, Name=HA.Name, CalculationMode=HA.CalculationMode, FactorCode=HA.FactorCode, 
	ModificationFactor=HA.ModificationFactor, CalculationCostMode=HA.CalculationCostMode, ExclusiveConditions=HA.ExclusiveConditions
FROM DeliveryNoteEntry DNE
	JOIN HistoryAgreement HA ON HA.ID=DNE.AgreeEntityID AND DNE.AgreeElementID=@AgreementID
	
UPDATE DeliveryNoteEntry SET AssignedCode=HIA.AssignedCode, Name=HIA.Name, CalculationMode=HIA.CalculationMode, FactorCode=HIA.FactorCode, 
	ModificationFactor=HIA.ModificationFactor, CalculationCostMode=HIA.CalculationCostMode, ExclusiveConditions=HIA.ExclusiveConditions
FROM DeliveryNoteEntry DNE
	JOIN HistoryInsurerAgreement HIA ON HIA.ID=DNE.AgreeEntityID AND DNE.AgreeElementID=@InsurerAgreementID
	
UPDATE DeliveryNoteCondEntry SET AssignedCode=HAC.Code, Name=HAC.Name, FactorCode=HAC.FactorCode, ModificationFactor=HAC.ModificationFactor,
	CalculationCostMode=HAC.CalculationCostMode
FROM DeliveryNoteCondEntry DNCE
	JOIN HistoryAgreeCondition HAC ON HAC.ID=DNCE.CondEntityID AND DNCE.CondElementID=@AgreeConditionID

UPDATE DeliveryNoteCondEntry SET AssignedCode=HIC.AssignedCode, Name=HIC.Name, FactorCode=HIC.FactorCode, ModificationFactor=HIC.ModificationFactor,
	CalculationCostMode=HIC.CalculationCostMode
FROM DeliveryNoteCondEntry DNCE
	JOIN HistoryInsurerCondition HIC ON HIC.ID=DNCE.CondEntityID AND DNCE.CondElementID=@InsurerConditionID 

	