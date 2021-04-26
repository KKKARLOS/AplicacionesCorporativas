UPDATE POEntry
SET ConfirmStatus=2
FROM POEntry JOIN PurchaseOrder ON PurchaseOrder.[ID]=POEntry.PurchaseOrderID
WHERE POEntry.ConfirmStatus=1 AND PurchaseOrder.PurchaseOrderType=1 AND PurchaseOrder.ConfirmStatus IN (2,4)

UPDATE PurchaseOrder SET ConfirmStatus=3
WHERE ConfirmStatus IN (2,4) AND PurchaseOrder.PurchaseOrderType=1