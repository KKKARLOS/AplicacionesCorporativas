insert into [Routine] (Code, Name, Description, RoutineTypeID, IsCodified,Encoder,RoutineClassificationID, 
RoutineDocument,PreviousPreparation,ConfirmedBy, CreatedBy,	
RegistrationDateTime, ConfirmedDateTime, AvailPattern, TimingControl, StepsControl, AdditionalPlanningRequires,
EstimatedDurationID, FrequencyOfApplicationID, ReferenceCost,ReferencePrice, TaxTypeID,Status,ModifiedBy,LastUpdated)
values('DEVOLUCIONPERSONAL','DEVOLUCIÓN PERSONAL','DEVOLUCIÓN PERSONAL',6,0,'',11,'','','44681293','44681293',
GETDATE(),GETDATE(),0,0,0,0,66,0,0,0,1,3,'44681293',GETDATE())

declare @id int
select @id=id from [Routine] where Code = 'DEVOLUCIONPERSONAL'
insert into RoutineLocationRel (RoutineID,LocationID,LastUpdated,ModifiedBy)
values(@id,417, GETDATE(),'44681293')
insert into RoutineLocationRel (RoutineID,LocationID,LastUpdated,ModifiedBy)
values(@id,421, GETDATE(),'44681293')
insert into RoutineLocationRel (RoutineID,LocationID,LastUpdated,ModifiedBy)
values(@id,422, GETDATE(),'44681293')
insert into RoutineLocationRel (RoutineID,LocationID,LastUpdated,ModifiedBy)
values(@id,433, GETDATE(),'44681293')
insert into RoutineLocationRel (RoutineID,LocationID,LastUpdated,ModifiedBy)
values(@id,447, GETDATE(),'44681293')
insert into RoutineLocationRel (RoutineID,LocationID,LastUpdated,ModifiedBy)
values(@id,449, GETDATE(),'44681293')
insert into RoutineLocationRel (RoutineID,LocationID,LastUpdated,ModifiedBy)
values(@id,451, GETDATE(),'44681293')
insert into RoutineLocationRel (RoutineID,LocationID,LastUpdated,ModifiedBy)
values(@id,466, GETDATE(),'44681293')
insert into RoutineLocationRel (RoutineID,LocationID,LastUpdated,ModifiedBy)
values(@id,586, GETDATE(),'44681293')