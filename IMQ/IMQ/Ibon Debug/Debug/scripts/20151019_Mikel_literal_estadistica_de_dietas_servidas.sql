  USE HCDIS
  GO
  UPDATE
[HCDIS].[dbo].[EACAttributeOption]
SET Description='Estad�stica de men�s servidos'
  where Description like 'Estad�stica de dietas servidas' and EACAttributeID=14487
  go
  UPDATE
[HCDIS].[dbo].[EACAttributeOption]
SET Description='Estad�stica de men�s servidos'
  where Description like 'Estad�stica de dietas servidas' and EACAttributeID=13797  
  GO