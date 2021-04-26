UPDATE E1
SET E1.DesignRequired=0, E1.[Required]=0 
FROM EACAttribute E1 INNER JOIN EACElement E2 ON E2.ID=E1.EACElementID 
WHERE E1.[Name] LIKE 'PurchaseOrderNumber'
AND (E1.DesignRequired=1 OR E1.[Required]=1)

UPDATE E1
SET E1.DesignRequired=0, E1.[Required]=0 
FROM EACAttribute E1 INNER JOIN EACElement E2 ON E2.ID=E1.EACElementID 
WHERE E1.[Name] LIKE 'utxtPONumber'
AND (E1.DesignRequired=1 OR E1.[Required]=1)
AND (E2.[Name] LIKE 'SII.HCD.BackOfficeModule.PurchaseOrderListView'
	OR E2.[Name] LIKE 'SII.HCD.BackOfficeModule.PurchaseOrderNewEdit')