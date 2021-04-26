DECLARE @MedicalOrderCitationType int
SET @MedicalOrderCitationType=4

UPDATE CitationConfig 
SET ShowRequestingPhysician=0, ShowRequestingInsurer=0
WHERE CitationType=@MedicalOrderCitationType