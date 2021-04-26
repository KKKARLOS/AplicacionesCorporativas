USE [database]
GO

UPDATE TimePattern
SET TimeScheme = 4
WHERE TimeScheme = 1

UPDATE TimePattern
SET TimeScheme = 5
WHERE TimeScheme = 2

