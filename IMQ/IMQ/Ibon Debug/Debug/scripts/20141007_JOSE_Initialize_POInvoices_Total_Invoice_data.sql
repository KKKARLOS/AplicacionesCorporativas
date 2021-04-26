UPDATE POEntry SET TotalBIInvoice=TotalBIReal WHERE TotalBIInvoice=0
UPDATE POEntry SET TotalTaxInvoice=TotalTaxReal WHERE TotalTaxInvoice=0

UPDATE POInvoice SET TotalBIReal=TotalInvoiceQty, BalanceQty=0 WHERE TotalBIReal=0
UPDATE POInvoice SET TotalTaxReal=TotalInvoiceTax, BalanceTax=0 WHERE TotalTaxReal=0