DECLARE @InvoiceEntityID int 
SET @InvoiceEntityID = 206

UPDATE Invoice SET TotalPaid=TotalQty+TaxQty WHERE PaymentStatus=3
UPDATE Invoice SET TotalPaid=0 WHERE PaymentStatus=1

UPDATE Invoice SET TotalPaid=
(SELECT SUM(BM.AmountQTY)
FROM BatchMovementElementRel BMR
	JOIN BatchMovement BM ON BMR.BatchMovementID = BM.[ID]
	WHERE BMR.EACElementID=@InvoiceEntityID AND BMR.EntityID=Invoice.[ID]
)
WHERE PaymentStatus=4