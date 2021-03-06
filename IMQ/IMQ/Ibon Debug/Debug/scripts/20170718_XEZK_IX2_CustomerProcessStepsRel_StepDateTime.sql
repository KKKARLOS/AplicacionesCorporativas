
drop index [dbo].[CustomerProcessStepsRel].[IX2_CustomerProcessStepsRel_CustomerProcessID]

CREATE NONCLUSTERED INDEX [IX2_CustomerProcessStepsRel_StepDT] ON [dbo].[CustomerProcessStepsRel]
(
	[Step] ASC,
	[CurrentStepID] ASC,
	[StepStatus] ASC,
	[StepDateTime] ASC
)
INCLUDE ( 	[CustomerProcessID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO