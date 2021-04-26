USE [DATABASE]
GO
update extobservationvalue 
set value = cast(cast(cast(cast(eov.value as varbinary(max)) as nvarchar(max)) as varchar(max)) as image)
from extobservationvalue eov
join interpretationvalue iv on eov.id = iv.extobservationvalueid
join interpretationparamconfig ip on ip.id = iv.interpretationparamconfigid
join observation o on o.id = ip.observationid
where o.basictype=256
and substring(cast(cast(eov.value as binary) as varchar(max)),2,1)=CHAR(0)