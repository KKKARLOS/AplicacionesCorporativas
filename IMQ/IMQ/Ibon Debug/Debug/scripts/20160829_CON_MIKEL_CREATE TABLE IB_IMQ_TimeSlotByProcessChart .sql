


	CREATE TABLE [IB_IMQ_TimeSlotByProcessChart]
	(
		ID int IDENTITY(1,1),
		ProcessChartID int ,
		ProcessChartName nchar(50),
		TimeSlot int,
		PRIMARY KEY (ID)
	);
	  
	 INSERT INTO IB_IMQ_TimeSlotByProcessChart (ProcessChartID, [ProcessChartName], [TimeSlot]) 
	VALUES ( 1, 'URGENCIAS', 10) 
	
		 INSERT INTO IB_IMQ_TimeSlotByProcessChart (ProcessChartID, [ProcessChartName], [TimeSlot]) 
	VALUES ( 2, 'INGRESOS', 10) 
	
		INSERT INTO IB_IMQ_TimeSlotByProcessChart (ProcessChartID, [ProcessChartName], [TimeSlot]) 
	VALUES ( 9, 'RADIOLOGIA', 5) 
	
			INSERT INTO IB_IMQ_TimeSlotByProcessChart (ProcessChartID, [ProcessChartName], [TimeSlot]) 
	VALUES ( 10, 'HOSPITAL DE DIA', 10) 
	
			INSERT INTO IB_IMQ_TimeSlotByProcessChart (ProcessChartID, [ProcessChartName], [TimeSlot]) 
	VALUES ( 14, 'TRATAMIENTOS', 10) 
	
		INSERT INTO IB_IMQ_TimeSlotByProcessChart (ProcessChartID, [ProcessChartName], [TimeSlot]) 
	VALUES ( 18, 'AMBULATORIO', 10) 
	
			INSERT INTO IB_IMQ_TimeSlotByProcessChart (ProcessChartID, [ProcessChartName], [TimeSlot]) 
	VALUES ( 20, 'FARMACIA', 10) 
	
			INSERT INTO IB_IMQ_TimeSlotByProcessChart (ProcessChartID, [ProcessChartName], [TimeSlot]) 
	VALUES ( 21, 'CONSULTAS EXTERNAS', 10) 
	
			INSERT INTO IB_IMQ_TimeSlotByProcessChart (ProcessChartID, [ProcessChartName], [TimeSlot]) 
	VALUES ( 22, 'PACIENTES EXTERNOS', 10) 
	
	
		


  