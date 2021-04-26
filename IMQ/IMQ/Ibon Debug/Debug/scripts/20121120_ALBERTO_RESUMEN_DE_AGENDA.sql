DECLARE @SingleFillPattern int
DECLARE @CellOverbookFillPattern int
DECLARE @OverbookFillPattern int
SET @SingleFillPattern = 1
SET @CellOverbookFillPattern = 2
SET @OverbookFillPattern = 3


--Location
UPDATE LocationSummaryAvailability SET OcuppiedSlots = OcuppiedSlots + OverbookedSlots, OverbookedSlots = 0
FROM LocationSummaryAvailability LSA WITH(NOLOCK) JOIN AvailPattern AP WITH(NOLOCK) ON LSA.AvailPatternID=AP.[ID] 
WHERE (AP.FillPattern=@SingleFillPattern)

UPDATE LocationSummaryAvailability SET TotalOverbookedSlots = (TotalSlots / AP.CalendarCellCapacity) * AP.OverbookingCapacity
FROM LocationSummaryAvailability LSA WITH(NOLOCK) JOIN AvailPattern AP WITH(NOLOCK) ON LSA.AvailPatternID=AP.[ID] 
WHERE (AP.FillPattern=@CellOverbookFillPattern) 

UPDATE LocationSummaryAvailability SET OcuppiedSlots = OcuppiedSlots + OverbookedSlots, OverbookedSlots = 0
FROM LocationSummaryAvailability LSA WITH(NOLOCK) JOIN AvailPattern AP WITH(NOLOCK) ON LSA.AvailPatternID=AP.[ID] 
WHERE (AP.FillPattern=@CellOverbookFillPattern) AND (LSA.OverbookedSlots < 0)

UPDATE LocationSummaryAvailability SET OcuppiedSlots = OcuppiedSlots + OverbookedSlots, OverbookedSlots = 0
FROM LocationSummaryAvailability LSA WITH(NOLOCK) JOIN AvailPattern AP WITH(NOLOCK) ON LSA.AvailPatternID=AP.[ID] 
WHERE (AP.FillPattern=@OverbookFillPattern) AND (LSA.OverbookedSlots < 0)

--Equipment
UPDATE EquipmentSummaryAvailability SET OcuppiedSlots = OcuppiedSlots + OverbookedSlots, OverbookedSlots = 0
FROM EquipmentSummaryAvailability LSA WITH(NOLOCK) JOIN AvailPattern AP WITH(NOLOCK) ON LSA.AvailPatternID=AP.[ID] 
WHERE (AP.FillPattern=@SingleFillPattern)

UPDATE EquipmentSummaryAvailability SET TotalOverbookedSlots = (TotalSlots / AP.CalendarCellCapacity) * AP.OverbookingCapacity
FROM EquipmentSummaryAvailability LSA WITH(NOLOCK) JOIN AvailPattern AP WITH(NOLOCK) ON LSA.AvailPatternID=AP.[ID] 
WHERE (AP.FillPattern=@CellOverbookFillPattern) 

UPDATE EquipmentSummaryAvailability SET OcuppiedSlots = OcuppiedSlots + OverbookedSlots, OverbookedSlots = 0
FROM EquipmentSummaryAvailability LSA WITH(NOLOCK) JOIN AvailPattern AP WITH(NOLOCK) ON LSA.AvailPatternID=AP.[ID] 
WHERE (AP.FillPattern=@CellOverbookFillPattern) AND (LSA.OverbookedSlots < 0)

UPDATE EquipmentSummaryAvailability SET OcuppiedSlots = OcuppiedSlots + OverbookedSlots, OverbookedSlots = 0
FROM EquipmentSummaryAvailability LSA WITH(NOLOCK) JOIN AvailPattern AP WITH(NOLOCK) ON LSA.AvailPatternID=AP.[ID] 
WHERE (AP.FillPattern=@OverbookFillPattern) AND (LSA.OverbookedSlots < 0)

--Person
UPDATE PersonSummaryAvailability SET OcuppiedSlots = OcuppiedSlots + OverbookedSlots, OverbookedSlots = 0
FROM PersonSummaryAvailability LSA WITH(NOLOCK) JOIN AvailPattern AP WITH(NOLOCK) ON LSA.AvailPatternID=AP.[ID] 
WHERE (AP.FillPattern=@SingleFillPattern)

UPDATE PersonSummaryAvailability SET TotalOverbookedSlots = (TotalSlots / AP.CalendarCellCapacity) * AP.OverbookingCapacity
FROM PersonSummaryAvailability LSA WITH(NOLOCK) JOIN AvailPattern AP WITH(NOLOCK) ON LSA.AvailPatternID=AP.[ID] 
WHERE (AP.FillPattern=@CellOverbookFillPattern) 

UPDATE PersonSummaryAvailability SET OcuppiedSlots = OcuppiedSlots + OverbookedSlots, OverbookedSlots = 0
FROM PersonSummaryAvailability LSA WITH(NOLOCK) JOIN AvailPattern AP WITH(NOLOCK) ON LSA.AvailPatternID=AP.[ID] 
WHERE (AP.FillPattern=@CellOverbookFillPattern) AND (LSA.OverbookedSlots < 0)

UPDATE PersonSummaryAvailability SET OcuppiedSlots = OcuppiedSlots + OverbookedSlots, OverbookedSlots = 0
FROM PersonSummaryAvailability LSA WITH(NOLOCK) JOIN AvailPattern AP WITH(NOLOCK) ON LSA.AvailPatternID=AP.[ID] 
WHERE (AP.FillPattern=@OverbookFillPattern) AND (LSA.OverbookedSlots < 0)