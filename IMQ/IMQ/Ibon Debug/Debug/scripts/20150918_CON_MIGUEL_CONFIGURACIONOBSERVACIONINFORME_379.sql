USE HCDIS
UPDATE Observation SET [Name] = 'Informe', [Description] = 'Informe'
WHERE AssignedCode = 'INFINT099'

UPDATE ObservationTemplateRel SET ElementTitle = 'Informe'
FROM  ObservationTemplateRel
JOIN Observation O ON ObservationTemplateRel.ObservationID = O.ID
WHERE O.AssignedCode = 'INFINT099'
