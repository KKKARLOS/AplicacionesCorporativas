UPDATE CustomerAccountCharge
SET ServiceChargeCode = 
		(	--Drug
			SELECT ISNULL(DI.NationalCode, DI.EquivalentNationalCode) NationalCode
			FROM DrugInfo DI
			WHERE DI.ItemID=I.ID
		UNION
			--Unidosis
			SELECT ISNULL(DI.NationalCode, DI.EquivalentNationalCode) NationalCode
			FROM DrugUnidosisRelationship DUR
			JOIN DrugInfo DI ON DUR.ParentItemID=DI.ItemID
			WHERE DUR.ChildItemID=I.ID
		)
FROM CustomerAccountCharge CAC
JOIN Item I ON CAC.RealizeEntityID=I.ID
WHERE CAC.RealizeElementID=127 
AND I.ItemType IN (17,25)