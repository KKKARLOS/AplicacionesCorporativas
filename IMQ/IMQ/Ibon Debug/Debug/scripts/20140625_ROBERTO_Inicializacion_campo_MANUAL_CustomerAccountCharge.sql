USE [DBName]
GO
UPDATE CustomerAccountCHarge 
SET [Manual]= CASE 
				WHEN RealizeElementID IS NULL OR RealizeElementID<=0 THEN 1 
				ELSE 0 
			  END