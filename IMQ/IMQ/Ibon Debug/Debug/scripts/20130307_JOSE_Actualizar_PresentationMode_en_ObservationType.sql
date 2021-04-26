UPDATE ObservationType SET PresentationMode=
(CASE WHEN PresentationMode=1 THEN 0
	ELSE PresentationMode/2
	END), LastUpdated='20130307'
WHERE LastUpdated<'20130307'