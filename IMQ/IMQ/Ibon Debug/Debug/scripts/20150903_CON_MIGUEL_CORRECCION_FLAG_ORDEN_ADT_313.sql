USE HCDIS

UPDATE [Order] SET ToRequestOrderFlags = 1 +2 + 256 + 2048,
				   ToRequestRequiredOrderFlags =  1 + 2 + 256 + 2048
FROM [Order]
WHERE [Order].ID IN
 (SELECT DISTINCT RC.ReservationOrderID FROM ReservationConfig RC
	JOIN ProcessChart PC ON RC.ProcessChartID = PC.ID
	JOIN EpisodeType ET ON PC.EpisodeConfigID = ET.ID
	WHERE ET.EpisodeCase = 3 /*InPatient*/ OR ET.EpisodeCase = 4 /*DayTreatment*/)