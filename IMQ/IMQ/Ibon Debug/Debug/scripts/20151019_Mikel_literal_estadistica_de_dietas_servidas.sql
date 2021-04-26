  USE HCDIS
  GO
  UPDATE
[HCDIS].[dbo].[EACAttributeOption]
SET Description='Estadística de menús servidos'
  where Description like 'Estadística de dietas servidas' and EACAttributeID=14487
  go
  UPDATE
[HCDIS].[dbo].[EACAttributeOption]
SET Description='Estadística de menús servidos'
  where Description like 'Estadística de dietas servidas' and EACAttributeID=13797  
  GO