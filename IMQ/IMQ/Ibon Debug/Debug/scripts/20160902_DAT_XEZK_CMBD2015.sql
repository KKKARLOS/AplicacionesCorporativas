ALTER TABLE Addin_MDS_TransferCareCenter
ADD CS_COD varchar(9) null 
go

update Addin_MDS_TransferCareCenter set CS_COD = '38'   where Code like '%480097%'
update Addin_MDS_TransferCareCenter set CS_COD = '8375' where Code like '%480084%'
update Addin_MDS_TransferCareCenter set CS_COD = '35'   where Code like '%480046%'
update Addin_MDS_TransferCareCenter set CS_COD = '41'   where Code like '%480123%'
update Addin_MDS_TransferCareCenter set CS_COD = '43'   where Code like '%480160%'
update Addin_MDS_TransferCareCenter set CS_COD = '42'   where Code like '%480059%'
update Addin_MDS_TransferCareCenter set CS_COD = '51'   where Code like '%480237%'
update Addin_MDS_TransferCareCenter set CS_COD = '2869' where Code like '%480293%'
go

update Addin_MDS_LeaveReason set Code = '1' where Name = 'DOMICILIO'
update Addin_MDS_LeaveReason set Code = '4' where Name = 'FALLECIDO'
update Addin_MDS_LeaveReason set Code = '2' where Name = 'TRASLADO A OTRO HOSPITAL'
update Addin_MDS_LeaveReason set Code = '3' where Name = 'ALTA VOLUNTARIA'
update Addin_MDS_LeaveReason set Code = '8' where Name = 'OTRAS CAUSAS'