update RoutineDietaryServiceRel
set SeasonType=16
where SeasonID>0 and SeasonType!=16

update RoutineDietaryServiceRel
set SeasonType=1
where SeasonID=0 and SeasonType!=1

update RoutineActDietaryServiceRel
set SeasonType=16
where SeasonID>0 and SeasonType!=16

update RoutineActDietaryServiceRel
set SeasonType=1
where SeasonID=0 and SeasonType!=1
