USE HCDV2RUBER

UPDATE ActMedicalSpecialtyRel 
SET ActMedicalSpecialtyRel.ReferenceCost = Act.ReferenceCost,
ActMedicalSpecialtyRel.ReferencePrice = Act.ReferencePrice,
ActMedicalSpecialtyRel.ReferenceTaxID = Act.ReferenceTaxID
FROM ActMedicalSpecialtyRel JOIN Act ON ActMedicalSpecialtyRel.ActID=Act.[ID]

GO