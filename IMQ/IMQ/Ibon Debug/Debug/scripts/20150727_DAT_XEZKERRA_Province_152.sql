update Address
set Province = 'BIZKAIA'
where Province in ('BIZKAYA', 'VIZCAYA')
update Address
set Province = 'GIPUZKOA'
where Province in ('GUIP�ZCOA', 'GUIPUZKOA')
update Address
set Province = 'ARABA'
where Province in ('�LAVA', 'ALAVA')


update Province
set Name = 'ARABA' where Code = '01'
update Province
set Name = 'GIPUZKOA' where Code = '20'
update Province
set Name = 'BIZKAIA' where Code = '48'

update EACAttribute 
set DefaultValue = 'BIZKAIA' 
where EACelementID = 31 and Name = 'Province'