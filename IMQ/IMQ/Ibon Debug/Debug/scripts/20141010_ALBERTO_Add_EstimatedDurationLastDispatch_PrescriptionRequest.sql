UPDATE PrescriptionRequest SET EstimatedDurationLastDispatch=LastDispatchDateTime
WHERE (Dispatchment IN (4,1))