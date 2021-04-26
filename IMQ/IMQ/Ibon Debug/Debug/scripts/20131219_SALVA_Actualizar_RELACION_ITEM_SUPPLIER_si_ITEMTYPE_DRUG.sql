UPDATE SupplierItemRelationship SET UnitsPerPackage=0, PricePerPackage=0
WHERE (ItemID IN (SELECT IT.ID 
				FROM Item IT
				JOIN SupplierItemRelationship SIR ON IT.ID=SIR.ItemID 
				WHERE IT.ItemType=25))