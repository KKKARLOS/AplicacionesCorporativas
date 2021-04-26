UPDATE DeliveryNoteEntry SET Units=1 WHERE Units=0
UPDATE DeliveryNoteCondEntry SET Units=1 WHERE Units=0

UPDATE DeliveryNoteEntry SET [Date]=RegistrationDateTime, PriceUnit=TotalTI/Units
UPDATE DeliveryNoteCondEntry SET [Date]=RegistrationDateTime, PriceUnit=TotalTI/Units
UPDATE DeliveryNote SET [Date]=RegistrationDateTime

