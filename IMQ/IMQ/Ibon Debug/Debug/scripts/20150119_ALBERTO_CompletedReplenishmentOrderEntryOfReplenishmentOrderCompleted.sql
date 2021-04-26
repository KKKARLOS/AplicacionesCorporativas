
UPDATE ReplenishmentOrderEntry SET [Status]=7
FROM ReplenishmentOrderEntry ROE 
	JOIN ReplenishmentOrder RO ON ROE.ReplenishmentOrderID=RO.[ID]
WHERE RO.[Status]=7 AND ROE.[Status]<>7