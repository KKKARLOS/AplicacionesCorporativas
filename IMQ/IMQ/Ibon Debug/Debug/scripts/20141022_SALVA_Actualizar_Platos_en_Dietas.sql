update RoutineDietaryServiceRel 
set CareCenterID=9, SeasonType=16, StartDateTime=S.StartAt, EndDateTime=S.EndingTo
from RoutineDietaryServiceRel RDS
join Season S on S.ID=RDS.SeasonID