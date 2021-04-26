USE [DBNAME]
GO

UPDATE CitationConfig SET CitationMandatoryPattern=1
WHERE (CitationMandatoryPattern IN (2,3,4,6))

UPDATE WaitingListConfig SET WLMandatoryPattern=1
WHERE (WLMandatoryPattern IN (2,3,4,6))