use [dbname]

update UnidosisInfo set Psychoactive = D.Psychoactive, Doping = D.Doping, HospitalFeature = D.HospitalFeature
from UnidosisInfo
join DrugUnidosisRelationship dur on UnidosisInfo.itemid = dur.ChildItemID
join DrugInfo d on dur.ParentItemID = d.itemid