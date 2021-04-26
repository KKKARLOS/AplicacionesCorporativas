ALTER TABLE PurchaseOrder ADD AlbaranCerrado bit
go

ALTER TABLE PurchaseOrder
ADD CONSTRAINT AlbaranCerradodef
DEFAULT 0 FOR AlbaranCerrado
GO

update PurchaseOrder
set AlbaranCerrado=0 -- ponemos todos los albaranes a 0
GO
